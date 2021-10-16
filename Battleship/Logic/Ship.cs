using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship {
    public class Ship {
        public Size Shape { get; } // A vector defining the ship's size and direction.
        public Point Position { get; }
        public bool Alive { get; private set; }

        public Ship(Size shape, Point position) {
            Shape = shape;
            Position = position;
            Alive = true;
        }

        public Point Tail => Position + Shape;
        public ShipDirection Direction => Shape.Height == 0 ? ShipDirection.Horizontal : ShipDirection.Vertical;

        public void Kill() {
            Alive = false;
        }

        public IEnumerable<Point> Cells() {
            for (int i = Position.X; i <= Tail.X; i++)
                for (int j = Position.Y; j <= Tail.Y; j++)
                    yield return new Point(i, j);
        }

        public bool Contains(Point p) {
            return
                p.X >= Position.X && p.X <= Tail.X &&
                p.Y >= Position.Y && p.Y <= Tail.Y;
        }

        public static Size CreateShape(int size, ShipDirection direction) {
            return direction == ShipDirection.Horizontal ? new Size(size, 0) : new Size(0, size);
        }
    }
}
