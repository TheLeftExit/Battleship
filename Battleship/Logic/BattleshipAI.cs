using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship {
    public class BattleshipAI {
        private BoardOwnerView ownField;
        private BoardOpponentView opposingField;
        private ShipCollection shipsAvailable;

        private Random random;
        
        public BattleshipAI(BoardOwnerView field, BoardOpponentView opponentField, ShipCollection ships, Random r) {
            ownField = field;
            opposingField = opponentField;
            shipsAvailable = ships;
            random = r;
        }

        public void PlaceShips() {
            ownField.RandomizeShips(shipsAvailable, random);
        }

        public Point GetNextHit() {
            Point target;
            // Attempt to find and finish any unfinished ships
            foreach(Point p in opposingField.Points) {
                CellState state = opposingField[p];
                if(state == CellState.ShipHit) {
                    target = GetTargetFromFirstFoundShipHit(p);
                    return target;
                }
            }
            // If nothing found, hit a random cell
            Point[] eligibleTargets = opposingField.Points.Where(opposingField.CanHit).ToArray();
            target = eligibleTargets[random.Next(eligibleTargets.Length)];
            return target;
        }

        private Point GetTargetFromFirstFoundShipHit(Point shipHit) {
            Size visibleShip = new Size(0, 0);

            Point nextHorizontal = new Point(shipHit.X + 1, shipHit.Y);
            Point nextVertical = new Point(shipHit.X, shipHit.Y + 1);

            // Get the visible part of the ship
            if (opposingField[nextHorizontal] == CellState.ShipHit) {
                nextHorizontal.X++;
                while (opposingField[nextHorizontal] == CellState.ShipHit)
                    nextHorizontal.X++;
                nextHorizontal.X--;
                visibleShip.Width = nextHorizontal.X - shipHit.X;
            } else if (opposingField[nextVertical] == CellState.ShipHit) {
                nextVertical.Y++;
                while (opposingField[nextVertical] == CellState.ShipHit)
                    nextVertical.Y++;
                nextVertical.Y--;
                visibleShip.Height = nextVertical.Y - shipHit.Y;
            }

            return GetTargetFromVisibleShip(shipHit, visibleShip);
        }

        private Point GetTargetFromVisibleShip(Point position, Size shape) {
            List<Point> possiblePoints = new List<Point>(4);
            if(shape.Width == 0) {
                Point top = new Point(position.X, position.Y - 1);
                Point bottom = new Point(position.X, position.Y + shape.Height + 1);
                if (opposingField[bottom] == CellState.Hidden)
                    possiblePoints.Add(bottom);
                if (opposingField[top] == CellState.Hidden)
                    possiblePoints.Add(top);
            }
            if(shape.Height == 0) {
                Point left = new Point(position.X - 1, position.Y);
                Point right = new Point(position.X + shape.Width + 1, position.Y);
                if (opposingField[right] == CellState.Hidden)
                    possiblePoints.Add(right);
                if (opposingField[left] == CellState.Hidden)
                    possiblePoints.Add(left);
            }
            return possiblePoints[random.Next(possiblePoints.Count)];
        }
    }
}
