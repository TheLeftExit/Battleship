using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship {
    public static class Palette {
        public static Brush
            CellHidden = Brushes.Beige,
            CellEmptyHit = Brushes.DeepSkyBlue,
            CellShipHit = Brushes.LightPink,
            CellShipDead = Brushes.DeepSkyBlue,
            ShipAlive = Brushes.DarkGray,
            ShipDead = Brushes.LightPink;
        public static Pen
            GridBorder = Pens.Gray,
            ShipBorder = Pens.Black;
    }
}
