using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship {
    public struct ShipPainter {
        private const float WIDTH_FACTOR = 0.6f;

        private Rectangle startPieBounds, tubeBounds, endPieBounds;
        private float startAngle;
        private bool horizontal;
        private bool alive;

        public void Draw(Graphics g) {
            Brush fillBrush = alive ? Palette.ShipAlive : Palette.ShipDead;
            Pen borderPen = Palette.ShipBorder;
            if(fillBrush != null) {
                g.FillEllipse(fillBrush, startPieBounds);
                g.FillRectangle(fillBrush, tubeBounds);
                g.FillEllipse(fillBrush, endPieBounds);
            }
            if(borderPen != null) {
                g.DrawArc(borderPen, startPieBounds, startAngle, 180);
                g.DrawLine(borderPen, TopLeft(tubeBounds), horizontal ? TopRight(tubeBounds) : BottomLeft(tubeBounds));
                g.DrawLine(borderPen, horizontal ? BottomLeft(tubeBounds) : TopRight(tubeBounds), BottomRight(tubeBounds));
                g.DrawArc(borderPen, endPieBounds, -startAngle, horizontal ? 180 : -180);
            }
        }

        public ShipPainter(Rectangle startCell, Rectangle endCell, ShipDirection direction, bool status) {
            startPieBounds = ScaleCentered(startCell, WIDTH_FACTOR);
            endPieBounds = ScaleCentered(endCell, WIDTH_FACTOR);
            horizontal = direction == ShipDirection.Horizontal;
            alive = status;
            Point tubeFrom, tubeTo;
            if(horizontal) {
                startAngle = 90;
                tubeFrom = TopCenter(startPieBounds);
                tubeTo = BottomCenter(endPieBounds);
                
            } else {
                startAngle = 180;
                tubeFrom = CenterLeft(startPieBounds);
                tubeTo = CenterRight(endPieBounds);
            }
            tubeBounds = Rectangle.FromLTRB(tubeFrom.X, tubeFrom.Y, tubeTo.X, tubeTo.Y);
        }

        private static Rectangle ScaleCentered(Rectangle source, float factor) {
            Size newPositionOffset = new SizeF(source.Width * (1 - factor) / 2, source.Height * (1 - factor) / 2).ToSize();
            Point newPosition = source.Location + newPositionOffset;
            Size newSize = new SizeF(source.Width * factor, source.Height * factor).ToSize();
            return new Rectangle(newPosition, newSize);
        }

        private static Point TopLeft(Rectangle r) => new Point(r.Left, r.Top);
        private static Point TopCenter(Rectangle r) => new Point(r.Left + r.Width / 2, r.Top);
        private static Point TopRight(Rectangle r) => new Point(r.Right, r.Top);
        private static Point CenterLeft(Rectangle r) => new Point(r.Left, r.Top + r.Height / 2);
        private static Point CenterRight(Rectangle r) => new Point(r.Right, r.Top + r.Height / 2);
        private static Point BottomLeft(Rectangle r) => new Point(r.Left, r.Bottom);
        private static Point BottomCenter(Rectangle r) => new Point(r.Left + r.Width / 2, r.Bottom);
        private static Point BottomRight(Rectangle r) => new Point(r.Right, r.Bottom);
    }
}
