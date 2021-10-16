using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship {
    public abstract class Board {
        protected CellState[] field;

        public Size Size { get; }
        public CellState this[Point p] {
            get => InBounds(p) ? field[p.Y * Size.Width + p.X] : CellState.OutOfBounds;
            set {
                if (!InBounds(p)) throw new ArgumentOutOfRangeException();
                field[p.Y * Size.Width + p.X] = value;
            }
        }
        public List<Ship> Ships { get; }
        public Point[] Points { get; } // Used to FOREACH the board without any math

        protected Board(Size size, CellState defaultState) {
            Size = size;
            Ships = new List<Ship>();
            field = new CellState[Size.Width * Size.Height];
            for (int i = 0; i < Size.Width * Size.Height; i++)
                field[i] = defaultState;

            Points = new Point[Size.Width * Size.Height];
            for (int i = 0; i < Size.Width; i++)
                for (int j = 0; j < Size.Height; j++)
                    Points[i + j * Size.Width] = new Point(i, j);
        }

        // This event only indicates that an update happened.
        // Any subscribed classes should already have a reference to this Board object.
        public event EventHandler Update;

        protected void InvokeUpdate() =>
            Update?.Invoke(this, new EventArgs());

        protected void SinkShip(Ship ship) {
            ship.Kill();
            foreach (Point p in ship.Cells())
                this[p] = CellState.ShipDead;
            foreach (Point p in GetNeighbors(ship))
                this[p] = CellState.EmptyHit;
        }

        protected IEnumerable<Point> GetNeighbors(Ship ship) {
            List<Point> result = new List<Point>(Size.Width * 2 + Size.Height * 2 + 4);
            Point paddingFrom = new Point(ship.Position.X - 1, ship.Position.Y - 1);
            Point paddingTo = new Point(ship.Tail.X + 1, ship.Tail.Y + 1);
            for (int i = paddingFrom.X; i <= paddingTo.X; i++) {
                for (int j = paddingFrom.Y; j <= paddingTo.Y; j++) {
                    Point p = new Point(i, j);
                    if (InBounds(p) && !ship.Contains(p))
                        yield return p;
                }
            }
        }

        public bool InBounds(Point p) =>
            p.X >= 0 && p.X < Size.Width &&
            p.Y >= 0 && p.Y < Size.Height;

        protected bool InBounds(Ship ship) =>
            ship.Cells().All(InBounds);

        /* Battleship rules hardcoded into game logic:
         * - Ships are one-cell-wide and can be positioned either horizontally or vertically
         * - Ships cannot be positioned within a cell of each other
         * - If a ship is sunk, all cells surrounding it are revealed as empty
         */
    }
}
