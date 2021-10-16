using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battleship {
    public enum CellState {
        Empty,
        EmptyHit,
        Ship,
        ShipHit,
        ShipDead,
        Hidden,
        OutOfBounds
    }

    public enum ShipDirection {
        Horizontal,
        Vertical
    }

    public enum PositionStatus {
        OK,
        OutOfBounds,
        CollisionDirect,
        CollisionNeighbor,
    }

    public class BattleshipClickEventArgs : EventArgs {
        public Point Target { get; }
        public MouseButtons Button { get; }
        public BattleshipClickEventArgs(Point p, MouseButtons b) {
            Target = p;
            Button = b;
        }
    }

    public struct SizeSelectorItem {
        public int Size { get; }
        public int Available { get; }
        public SizeSelectorItem(int size, int count) {
            Size = size;
            Available = count;
        }
        public override string ToString() => Available == 0 ? "All ships used" : $"Size: {Size + 1} (available: {Available})";
    }
}
