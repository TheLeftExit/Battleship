using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battleship {
    public partial class BattleshipPainter : UserControl {
        private Board battleshipField;
        private Size? selectedShip;

        public BattleshipPainter() {
            InitializeComponent();
            gridMap = new List<(Point, Point)>();
            cellMap = new Dictionary<Point, Rectangle>();
            shipMap = new List<ShipPainter>();
        }

        public void BindToField(Board field) {
            if (battleshipField != null)
                UnbindFromField();
            battleshipField = field;
            field.Update += OnFieldUpdate;
            InvalidateMapping();
        }

        public void UnbindFromField() {
            if (battleshipField != null) {
                battleshipField.Update -= OnFieldUpdate;
                battleshipField = null;
                InvalidateMapping();
            }
        }

        private void OnFieldUpdate(object sender, EventArgs e) {
            InvalidateMapping();
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            if (battleshipField == null)
                return;
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            foreach (Point p in battleshipField.Points)
                DrawCell(g, p, battleshipField[p]);

            shipPreview?.Draw(g);
            foreach (ShipPainter ship in shipMap) {
                ship.Draw(g);
            }

            foreach ((Point From, Point To) line in gridMap)
                g.DrawLine(Palette.GridBorder, line.From, line.To);
        }

        private void DrawCell(Graphics g, Point p, CellState state) {
            Rectangle target = cellMap[p];
            switch (battleshipField[p]) {
                case CellState.Hidden:
                    g.FillRectangle(Palette.CellHidden, target);
                    break;
                case CellState.EmptyHit:
                    g.FillRectangle(Palette.CellEmptyHit, target);
                    break;
                case CellState.ShipHit:
                    g.FillRectangle(Palette.CellShipHit, target);
                    break;
                case CellState.ShipDead:
                    g.FillRectangle(Palette.CellShipDead, target);
                    break;
            }
        }

        #region Cached drawing resources
        private List<(Point From, Point To)> gridMap;
        private Dictionary<Point, Rectangle> cellMap;
        private List<ShipPainter> shipMap;
        private ShipPainter? shipPreview;

        private void InvalidateMapping() {
            gridMap.Clear();
            cellMap.Clear();
            shipMap.Clear();
            if (battleshipField == null)
                return;

            Rectangle drawingArea = ClientRectangle;
            Size fieldSize = battleshipField.Size;

            int[] xOffsets = new int[fieldSize.Width + 1];
            int[] yOffsets = new int[fieldSize.Height + 1];
            for (int i = 0; i < xOffsets.Length; i++)
                xOffsets[i] = drawingArea.Width * i / fieldSize.Width;
            for (int i = 0; i < yOffsets.Length; i++)
                yOffsets[i] = drawingArea.Height * i / fieldSize.Height;

            // gridMap
            for(int i = 0; i < xOffsets.Length - 1; i++)
                gridMap.Add((new Point(xOffsets[i], 0), new Point(xOffsets[i], drawingArea.Height)));
            for (int i = 0; i < yOffsets.Length - 1; i++)
                gridMap.Add((new Point(0, yOffsets[i]), new Point(drawingArea.Width, yOffsets[i])));

            // cellMap
            for(int i = 0; i < xOffsets.Length - 1; i++)
                for(int j = 0; j < yOffsets.Length - 1; j++) {
                    Point cell = new Point(i, j);
                    Size cellSize = new Size(xOffsets[i + 1] - xOffsets[i], yOffsets[j + 1] - yOffsets[j]);
                    Point cellPosition = new Point(xOffsets[i], yOffsets[j]);
                    Rectangle cellRectangle = new Rectangle(cellPosition, cellSize) ;
                    cellMap.Add(cell, cellRectangle);
                }

            // shipMap
            foreach(Ship ship in battleshipField.Ships) {
                shipMap.Add(new ShipPainter(cellMap[ship.Position], cellMap[ship.Tail], ship.Direction, ship.Alive));
            }

            Invalidate();
        }
        #endregion
        public void SelectShip(Size? shape) {
            selectedShip = shape;
            InvalidateShipPreview(MousePosition);
        }

        public event EventHandler<BattleshipClickEventArgs> OnFieldClick;

        private Point ScreenToCell(Point p) => ClientRectangle.Contains(p) ? cellMap.First(x => x.Value.Contains(p)).Key : new Point(-1, -1);

        private void OnClick(object sender, MouseEventArgs e) {
            Point affectedCell = ScreenToCell(e.Location);
            OnFieldClick?.Invoke(this, new BattleshipClickEventArgs(affectedCell, e.Button));
        }

        private void InvalidateShipPreview(Point position) {
            if (!selectedShip.HasValue || !battleshipField.InBounds(position) || !battleshipField.InBounds(position + selectedShip.Value)) {
                shipPreview = null;
                Invalidate();
                return;
            }
            ShipDirection direction = selectedShip.Value.Width == 0 ? ShipDirection.Vertical : ShipDirection.Horizontal;
            bool drawAlive = (battleshipField as BoardOwnerView).CanAddShip(selectedShip.Value, position);
            shipPreview = new ShipPainter(cellMap[position], cellMap[position + selectedShip.Value], direction, drawAlive);
            Invalidate();
        }

        private void BattleshipPainter_MouseMove(object sender, MouseEventArgs e) {
            InvalidateShipPreview(ScreenToCell(e.Location));
        }

        private void BattleshipPainter_MouseLeave(object sender, EventArgs e) {
            InvalidateShipPreview(new Point(-1, -1));
        }
    }
}
