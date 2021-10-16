
namespace BattleshipDemo {
    partial class Form1 {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.battleshipGame1 = new Battleship.BattleshipGame();
            this.SuspendLayout();
            // 
            // battleshipGame1
            // 
            this.battleshipGame1.AutoSize = true;
            this.battleshipGame1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.battleshipGame1.Location = new System.Drawing.Point(12, 12);
            this.battleshipGame1.Name = "battleshipGame1";
            this.battleshipGame1.Size = new System.Drawing.Size(913, 344);
            this.battleshipGame1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(934, 364);
            this.Controls.Add(this.battleshipGame1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        #endregion

        private Battleship.BattleshipGame battleshipGame1;
    }
}

