using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship {
    public class BoardOwnerView : Board {
        private BoardOpponentView opponentView;

        public BoardOwnerView(Size size) : base(size, CellState.Empty) { }

        public BoardOpponentView CreateOpponentView() {
            if (opponentView != null)
                throw new InvalidOperationException();
            BoardOpponentView result = new BoardOpponentView(this);
            opponentView = result;
            return result;
        }

        public bool CanAddShip(Size shape, Point position) {
            Ship assumedShip = new Ship(shape, position);
            if (!InBounds(assumedShip))
                return false;
            foreach (Point p in assumedShip.Cells())
                if (this[p] != CellState.Empty)
                    return false;
            foreach (Point p in GetNeighbors(assumedShip))
                if (this[p] != CellState.Empty)
                    return false;
            return true;
        }

        public void AddShip(Size shape, Point position) {
            Ship ship = new Ship(shape, position);
            Ships.Add(ship);
            foreach (Point p in ship.Cells())
                this[p] = CellState.Ship;
            InvokeUpdate();
        }

        internal HitResult ProcessHit(Point p) {
            HitResult result;
            if(this[p] == CellState.Empty) {
                this[p] = CellState.EmptyHit;
                result = new HitResult(CellState.EmptyHit);
            } else {
                this[p] = CellState.ShipHit;
                Ship ship = GetShip(p);
                if (ShipDown(ship)) {
                    SinkShip(ship);
                    result = new HitResult(ship);
                } else {
                    result = new HitResult(CellState.ShipHit);
                }
            }
            InvokeUpdate();
            return result;
        }

        private Ship GetShip(Point p) =>
            Ships.First(x => x.Contains(p));

        private bool ShipDown(Ship ship) =>
            ship.Cells().All(x => this[x] == CellState.ShipHit);

        public void RandomizeShips(ShipCollection ships, Random random) {
            // This can be improved, in terms of an algorithm, and I'd love to do that as a separate task, if that'd been within the scope of this task.
            // This assumes that there is no way to run out of space with the given field and ships.
            while (!ships.IsEmpty()) {
                List<(Point Position, Size Shape)> possibleShips = new List<(Point, Size)>();
                foreach (Size shape in ships.ShipsAvailable()) {
                    foreach (Point p in Points)
                        if (CanAddShip(shape, p))
                            possibleShips.Add((p, shape));
                }
                if (possibleShips.Count == 0)
                    throw new InvalidOperationException();
                (Point Position, Size Shape) newShip = possibleShips[random.Next(possibleShips.Count)];
                AddShip(newShip.Shape, newShip.Position);
                ships.Take(newShip.Shape);
            }
        }
    }

    public class BoardOpponentView : Board {
        private BoardOwnerView ownerView;

        internal BoardOpponentView(BoardOwnerView boardOwnerView) : base(boardOwnerView.Size, CellState.Hidden) {
            ownerView = boardOwnerView;
        }

        public bool CanHit(Point p) =>
            this[p] == CellState.Hidden;

        public bool Hit(Point p) {
            HitResult outcome = ownerView.ProcessHit(p);
            this[p] = outcome.State;
            if(outcome.Ship != null) {
                SinkShip(outcome.Ship);
                Ships.Add(outcome.Ship);
            }
            InvokeUpdate();
            return outcome.State == CellState.ShipHit;
        }
    }

    internal struct HitResult {
        public CellState State;
        public Ship Ship;
        public HitResult(CellState state) {
            State = state;
            Ship = null;
        }
        public HitResult(Ship ship) {
            State = CellState.ShipHit;
            Ship = ship;
        }
    }
}
