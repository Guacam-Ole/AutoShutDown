namespace AutoShutDown.UI
{
    partial class ProcessList
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
            ProcessesList = new ListView();
            SuspendLayout();
            // 
            // ProcessesList
            // 
            ProcessesList.Dock = DockStyle.Fill;
            ProcessesList.Location = new Point(0, 0);
            ProcessesList.Name = "ProcessesList";
            ProcessesList.Size = new Size(800, 450);
            ProcessesList.TabIndex = 0;
            ProcessesList.UseCompatibleStateImageBehavior = false;
            ProcessesList.View = View.List;
            ProcessesList.DoubleClick += ProcessesList_DoubleClick;
            // 
            // ProcessList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size(800, 450);
            Controls.Add(ProcessesList);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            MinimizeBox = false;
            Name = "ProcessList";
            Text = "List of currently running processes. Doubleclick to add";
            Load += ProcessList_Load;
            ResumeLayout(false);
        }

        #endregion

        private ListView ProcessesList;
    }
}