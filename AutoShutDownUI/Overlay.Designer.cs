namespace AutoShutDown.UI
{
    partial class Overlay
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
            StatusLabel = new Label();
            SuspendLayout();
            // 
            // StatusLabel
            // 
            StatusLabel.Dock = DockStyle.Fill;
            StatusLabel.Font = new Font("Lucida Console", 12F, FontStyle.Regular, GraphicsUnit.Point);
            StatusLabel.ForeColor = Color.Lime;
            StatusLabel.Location = new Point(0, 0);
            StatusLabel.Name = "StatusLabel";
            StatusLabel.Size = new Size(940, 244);
            StatusLabel.TabIndex = 0;
            StatusLabel.Text = "Autoshutdown started. (Status will be available in a minute)";
            StatusLabel.MouseDown += StatusLabel_MouseDown;
            StatusLabel.MouseMove += StatusLabel_MouseMove;
            StatusLabel.MouseUp += StatusLabel_MouseUp;
            // 
            // Overlay
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.HotPink;
            ClientSize = new Size(940, 244);
            Controls.Add(StatusLabel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Overlay";
            ShowInTaskbar = false;
            Text = "Overlay";
            Load += Overlay_Load;
            ResumeLayout(false);
        }

        #endregion

        private Label StatusLabel;
    }
}