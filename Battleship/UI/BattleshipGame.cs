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
    public partial class BattleshipGame : UserControl {
        private static readonly Size FIELD_SIZE = new Size(10, 10);

        private Random random;

        private BoardOwnerView playerOwnerView;
        private BoardOpponentView playerOpponentView;
        private BoardOwnerView aiOwnerView;
        private BoardOpponentView aiOpponentView;
        private ShipCollection playerShips;
        private ShipCollection aiShips;

        private BattleshipAI ai;

        bool canHit;
        private Size? selectedShip;

        public BattleshipGame() {
            InitializeComponent();
            random = new Random();
            InitializeGame();
        }

        private void InitializeGame() {
            playerOwnerView = new BoardOwnerView(FIELD_SIZE);
            playerOpponentView = playerOwnerView.CreateOpponentView();
            aiOwnerView = new BoardOwnerView(FIELD_SIZE);
            aiOpponentView = aiOwnerView.CreateOpponentView();

            playerShips = new ShipCollection(4, 3, 2, 1);
            aiShips = new ShipCollection(4, 3, 2, 1);

            ai = new BattleshipAI(aiOwnerView, playerOpponentView, aiShips, random);
            ai.PlaceShips();

            playerFieldPainter.BindToField(playerOwnerView);
            opponentFieldPainter.BindToField(aiOpponentView);

            UpdateShipSelection();
            UpdateGameState(GameState.ShipPlacement);
            eventsListBox.Items.Clear();
        }

        private void UpdateShipSelection() {
            int selectedIndex = sizeSelectorComboBox.SelectedIndex;
            sizeSelectorComboBox.Items.Clear();
            for (int i = 0; i < playerShips.Counts.Length; i++) {
                if (playerShips.Counts[i] > 0) {
                    sizeSelectorComboBox.Items.Add(new SizeSelectorItem(i, playerShips.Counts[i]));
                }
            }
            if (sizeSelectorComboBox.Items.Count == 0) {
                sizeSelectorComboBox.Items.Add(new SizeSelectorItem());
                sizeSelectorComboBox.Enabled = false;
                UpdateGameState(GameState.HitPlacement);
            } else {
                sizeSelectorComboBox.Enabled = true;
            }
            sizeSelectorComboBox.SelectedIndex = Math.Max(0, Math.Min(sizeSelectorComboBox.Items.Count - 1, selectedIndex));
        }

        private void sizeSelectorComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            SizeSelectorItem item = (SizeSelectorItem)sizeSelectorComboBox.SelectedItem;
            if (item.Available == 0)
                SelectShip(-1);
            else {
                SelectShip(item.Size);
            }
        }
        private void SelectShip(int size) {
            if (size == -1) {
                selectedShip = null;
                playerFieldPainter.SelectShip(null);
                return;
            }
            if (!selectedShip.HasValue || selectedShip.Value.Height == 0)
                selectedShip = new Size(size, 0);
            else
                selectedShip = new Size(0, size);
            playerFieldPainter.SelectShip(selectedShip);
        }

        private void RotateShip() {
            if (selectedShip.HasValue)
                selectedShip = new Size(selectedShip.Value.Height, selectedShip.Value.Width);
            playerFieldPainter.SelectShip(selectedShip);
        }

        private void UpdateGameState(GameState gameState) {
            switch (gameState) {
                case GameState.Inactive: // Transition from gameplay to end screen
                    canHit = false;
                    statusLabel.Text = "Game over!";
                    break;
                case GameState.HitPlacement: // Transition from ship placement to gameplay
                    canHit = true;
                    statusLabel.Text = "Your turn";
                    SelectShip(-1);
                    break;
                case GameState.ShipPlacement:
                    canHit = false;
                    statusLabel.Text = "Place your ships (right click to rotate)" + Environment.NewLine + "Click here to randomize positions";
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

        private enum GameState {
            Inactive,
            ShipPlacement,
            HitPlacement
        }

        private void playerFieldPainter_OnFieldClick(object sender, BattleshipClickEventArgs e) {
            if(e.Button == MouseButtons.Right) {
                if(selectedShip.HasValue)
                    RotateShip();
                return;
            }
            if(e.Button == MouseButtons.Left){
                if (selectedShip.HasValue) {
                    if (playerOwnerView.CanAddShip(selectedShip.Value, e.Target)) {
                        playerOwnerView.AddShip(selectedShip.Value, e.Target);
                        playerShips.Take(selectedShip.Value);
                        UpdateShipSelection();
                    }
                }
            }
        }

        private void restartButton_Click(object sender, EventArgs e) {
            InitializeGame();
        }

        private void opponentFieldPainter_OnFieldClick(object sender, BattleshipClickEventArgs e) {
            if(e.Button == MouseButtons.Left) {
                if (canHit) {
                    if (aiOpponentView.CanHit(e.Target)) {
                        bool hitAgain = aiOpponentView.Hit(e.Target);
                        if (hitAgain) {
                            Log($"You shot at ({e.Target.X}, {e.Target.Y}) and hit a ship!");
                            CheckForGameOver();
                        } else {
                            Log($"You shot at ({e.Target.X}, {e.Target.Y}) and missed.");
                            ProcessAITurn();
                        }
                    }
                }
            }
        }

        private void ProcessAITurn() {
            bool aiHitAgain = true;
            while (aiHitAgain) {
                Point aiTarget = ai.GetNextHit();
                aiHitAgain = playerOpponentView.Hit(aiTarget);
                if (aiHitAgain)
                    Log($"Your opponent shot at ({aiTarget.X}, {aiTarget.Y}) and hit a ship!");
                else {
                    Log($"Your opponent shot at ({aiTarget.X}, {aiTarget.Y}) and missed!");
                }
                if (CheckForGameOver())
                    break;
            }
        }

        private bool PlayerWon() => aiOwnerView.Ships.All(x => !x.Alive);
        private bool AIWon() => playerOwnerView.Ships.All(x => !x.Alive);
        private bool CheckForGameOver() {
            if (PlayerWon()) {
                UpdateGameState(GameState.Inactive);
                Log("You win!");
                return true;
            } else if (AIWon()) {
                UpdateGameState(GameState.Inactive);
                Log("Your opponent wins!");
                return true;
            }
            return false;
        }

        private void Log(string s) {
            eventsListBox.Items.Add(s);
            eventsListBox.SelectedIndex = eventsListBox.Items.Count - 1;
            eventsListBox.SelectedIndex = -1;
        }

        private void statusLabel_Click(object sender, EventArgs e) {
            playerOwnerView.RandomizeShips(playerShips, random);
            UpdateShipSelection();
            UpdateGameState(GameState.HitPlacement);
        }
    }
}
