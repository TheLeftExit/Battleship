
namespace Battleship {
    partial class BattleshipGame {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.eventsListBox = new System.Windows.Forms.ListBox();
            this.shipPlacementGroupBox = new System.Windows.Forms.GroupBox();
            this.sizeSelectorComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.statusLabel = new System.Windows.Forms.Label();
            this.restartButton = new System.Windows.Forms.Button();
            this.playerFieldPainter = new Battleship.BattleshipPainter();
            this.opponentFieldPainter = new Battleship.BattleshipPainter();
            this.shipPlacementGroupBox.SuspendLayout();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // eventsListBox
            // 
            this.eventsListBox.FormattingEnabled = true;
            this.eventsListBox.Location = new System.Drawing.Point(618, 107);
            this.eventsListBox.Name = "eventsListBox";
            this.eventsListBox.Size = new System.Drawing.Size(283, 212);
            this.eventsListBox.TabIndex = 2;
            // 
            // shipPlacementGroupBox
            // 
            this.shipPlacementGroupBox.Controls.Add(this.sizeSelectorComboBox);
            this.shipPlacementGroupBox.Location = new System.Drawing.Point(618, 19);
            this.shipPlacementGroupBox.Name = "shipPlacementGroupBox";
            this.shipPlacementGroupBox.Size = new System.Drawing.Size(283, 48);
            this.shipPlacementGroupBox.TabIndex = 3;
            this.shipPlacementGroupBox.TabStop = false;
            this.shipPlacementGroupBox.Text = "Ship placement";
            // 
            // sizeSelectorComboBox
            // 
            this.sizeSelectorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sizeSelectorComboBox.FormattingEnabled = true;
            this.sizeSelectorComboBox.Location = new System.Drawing.Point(6, 19);
            this.sizeSelectorComboBox.Name = "sizeSelectorComboBox";
            this.sizeSelectorComboBox.Size = new System.Drawing.Size(271, 21);
            this.sizeSelectorComboBox.TabIndex = 0;
            this.sizeSelectorComboBox.SelectedIndexChanged += new System.EventHandler(this.sizeSelectorComboBox_SelectedIndexChanged);
            // 
            // groupBox
            // 
            this.groupBox.AutoSize = true;
            this.groupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox.Controls.Add(this.statusLabel);
            this.groupBox.Controls.Add(this.restartButton);
            this.groupBox.Controls.Add(this.playerFieldPainter);
            this.groupBox.Controls.Add(this.shipPlacementGroupBox);
            this.groupBox.Controls.Add(this.opponentFieldPainter);
            this.groupBox.Controls.Add(this.eventsListBox);
            this.groupBox.Location = new System.Drawing.Point(3, 3);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(907, 338);
            this.groupBox.TabIndex = 4;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Battleship";
            // 
            // statusLabel
            // 
            this.statusLabel.Location = new System.Drawing.Point(618, 73);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(196, 28);
            this.statusLabel.TabIndex = 5;
            this.statusLabel.Text = "Place your ships\r\n(right-click to rotate)";
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.statusLabel.Click += new System.EventHandler(this.statusLabel_Click);
            // 
            // restartButton
            // 
            this.restartButton.Location = new System.Drawing.Point(820, 73);
            this.restartButton.Name = "restartButton";
            this.restartButton.Size = new System.Drawing.Size(75, 28);
            this.restartButton.TabIndex = 4;
            this.restartButton.Text = "Restart";
            this.restartButton.UseVisualStyleBackColor = true;
            this.restartButton.Click += new System.EventHandler(this.restartButton_Click);
            // 
            // playerFieldPainter
            // 
            this.playerFieldPainter.BackColor = System.Drawing.Color.SkyBlue;
            this.playerFieldPainter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.playerFieldPainter.Location = new System.Drawing.Point(312, 19);
            this.playerFieldPainter.Name = "playerFieldPainter";
            this.playerFieldPainter.Size = new System.Drawing.Size(300, 300);
            this.playerFieldPainter.TabIndex = 0;
            this.playerFieldPainter.OnFieldClick += new System.EventHandler<Battleship.BattleshipClickEventArgs>(this.playerFieldPainter_OnFieldClick);
            // 
            // opponentFieldPainter
            // 
            this.opponentFieldPainter.BackColor = System.Drawing.Color.SkyBlue;
            this.opponentFieldPainter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.opponentFieldPainter.Location = new System.Drawing.Point(6, 19);
            this.opponentFieldPainter.Name = "opponentFieldPainter";
            this.opponentFieldPainter.Size = new System.Drawing.Size(300, 300);
            this.opponentFieldPainter.TabIndex = 1;
            this.opponentFieldPainter.OnFieldClick += new System.EventHandler<Battleship.BattleshipClickEventArgs>(this.opponentFieldPainter_OnFieldClick);
            // 
            // BattleshipGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.groupBox);
            this.Name = "BattleshipGame";
            this.Size = new System.Drawing.Size(913, 344);
            this.shipPlacementGroupBox.ResumeLayout(false);
            this.groupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BattleshipPainter playerFieldPainter;
        private BattleshipPainter opponentFieldPainter;
        private System.Windows.Forms.ListBox eventsListBox;
        private System.Windows.Forms.GroupBox shipPlacementGroupBox;
        private System.Windows.Forms.ComboBox sizeSelectorComboBox;
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Button restartButton;
    }
}
