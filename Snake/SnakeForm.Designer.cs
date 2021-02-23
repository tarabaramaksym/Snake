namespace Snake
{
    partial class SnakeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.frameTimer = new System.Windows.Forms.Timer(this.components);
            this.objective = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.objective)).BeginInit();
            this.SuspendLayout();
            // 
            // frameTimer
            // 
            this.frameTimer.Interval = 50;
            this.frameTimer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // objective
            // 
            this.objective.BackColor = System.Drawing.Color.Gold;
            this.objective.Location = new System.Drawing.Point(176, 271);
            this.objective.Name = "objective";
            this.objective.Size = new System.Drawing.Size(20, 20);
            this.objective.TabIndex = 0;
            this.objective.TabStop = false;
            // 
            // SnakeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(444, 381);
            this.Controls.Add(this.objective);
            this.MaximumSize = new System.Drawing.Size(460, 420);
            this.MinimumSize = new System.Drawing.Size(460, 420);
            this.Name = "SnakeForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.SnakeForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SnakeForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.objective)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer frameTimer;
        private System.Windows.Forms.PictureBox objective;
    }
}

