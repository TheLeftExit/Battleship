
namespace Battleship {
    partial class BattleshipPainter {
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
            this.SuspendLayout();
            // 
            // BattleshipPainter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DoubleBuffered = true;
            this.Name = "BattleshipPainter";
            this.Size = new System.Drawing.Size(146, 146);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OnClick);
            this.MouseLeave += new System.EventHandler(this.BattleshipPainter_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BattleshipPainter_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
