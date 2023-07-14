namespace AutoShutDownUI
{
    partial class Settings
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
            components = new System.ComponentModel.Container();
            TaskLabel = new Label();
            MouseLabel = new Label();
            DownloadLabel = new Label();
            SelectTasks = new Button();
            TasksList = new TextBox();
            MouseMinuteSelect = new NumericUpDown();
            DownloadRate = new NumericUpDown();
            DownloadMultiplyer = new ComboBox();
            MinutesLabel = new Label();
            SaveButton = new Button();
            Notifyer = new NotifyIcon(components);
            Hint = new ToolTip(components);
            WarningMinutes = new NumericUpDown();
            ShutdownCommand = new TextBox();
            WarningMinutesLabel = new Label();
            label2 = new Label();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)MouseMinuteSelect).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DownloadRate).BeginInit();
            ((System.ComponentModel.ISupportInitialize)WarningMinutes).BeginInit();
            SuspendLayout();
            // 
            // TaskLabel
            // 
            TaskLabel.AutoSize = true;
            TaskLabel.Location = new Point(12, 15);
            TaskLabel.Name = "TaskLabel";
            TaskLabel.Size = new Size(34, 15);
            TaskLabel.TabIndex = 1;
            TaskLabel.Text = "Tasks";
            // 
            // MouseLabel
            // 
            MouseLabel.AutoSize = true;
            MouseLabel.Location = new Point(12, 43);
            MouseLabel.Name = "MouseLabel";
            MouseLabel.Size = new Size(46, 15);
            MouseLabel.TabIndex = 2;
            MouseLabel.Text = "Mouse:";
            // 
            // DownloadLabel
            // 
            DownloadLabel.AutoSize = true;
            DownloadLabel.Location = new Point(12, 72);
            DownloadLabel.Name = "DownloadLabel";
            DownloadLabel.Size = new Size(61, 15);
            DownloadLabel.TabIndex = 3;
            DownloadLabel.Text = "Download";
            // 
            // SelectTasks
            // 
            SelectTasks.Location = new Point(730, 12);
            SelectTasks.Name = "SelectTasks";
            SelectTasks.Size = new Size(126, 23);
            SelectTasks.TabIndex = 4;
            SelectTasks.Text = "Add";
            SelectTasks.UseVisualStyleBackColor = true;
            // 
            // TasksList
            // 
            TasksList.Location = new Point(147, 12);
            TasksList.Name = "TasksList";
            TasksList.Size = new Size(577, 23);
            TasksList.TabIndex = 5;
            Hint.SetToolTip(TasksList, "If one of these Tasks are present do NOT shutdown the Computer");
            // 
            // MouseMinuteSelect
            // 
            MouseMinuteSelect.Location = new Point(147, 41);
            MouseMinuteSelect.Maximum = new decimal(new int[] { 1440, 0, 0, 0 });
            MouseMinuteSelect.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            MouseMinuteSelect.Name = "MouseMinuteSelect";
            MouseMinuteSelect.Size = new Size(87, 23);
            MouseMinuteSelect.TabIndex = 6;
            Hint.SetToolTip(MouseMinuteSelect, "Minutes without Mousemovement before shutting down");
            MouseMinuteSelect.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // DownloadRate
            // 
            DownloadRate.Location = new Point(147, 70);
            DownloadRate.Name = "DownloadRate";
            DownloadRate.Size = new Size(87, 23);
            DownloadRate.TabIndex = 7;
            Hint.SetToolTip(DownloadRate, "Max Download-Amount when shutting down");
            // 
            // DownloadMultiplyer
            // 
            DownloadMultiplyer.DropDownStyle = ComboBoxStyle.DropDownList;
            DownloadMultiplyer.FormattingEnabled = true;
            DownloadMultiplyer.Items.AddRange(new object[] { "Bytes/s", "KBytes/s", "MBytes/s" });
            DownloadMultiplyer.Location = new Point(240, 69);
            DownloadMultiplyer.Name = "DownloadMultiplyer";
            DownloadMultiplyer.Size = new Size(121, 23);
            DownloadMultiplyer.TabIndex = 8;
            // 
            // MinutesLabel
            // 
            MinutesLabel.AutoSize = true;
            MinutesLabel.Location = new Point(240, 43);
            MinutesLabel.Name = "MinutesLabel";
            MinutesLabel.Size = new Size(50, 15);
            MinutesLabel.TabIndex = 12;
            MinutesLabel.Text = "Minutes";
            // 
            // SaveButton
            // 
            SaveButton.Location = new Point(777, 232);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(75, 23);
            SaveButton.TabIndex = 13;
            SaveButton.Text = "Save";
            SaveButton.UseVisualStyleBackColor = true;
            // 
            // Notifyer
            // 
            Notifyer.Text = "Notifyer";
            Notifyer.Visible = true;
            // 
            // WarningMinutes
            // 
            WarningMinutes.Location = new Point(147, 137);
            WarningMinutes.Maximum = new decimal(new int[] { 1440, 0, 0, 0 });
            WarningMinutes.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            WarningMinutes.Name = "WarningMinutes";
            WarningMinutes.Size = new Size(87, 23);
            WarningMinutes.TabIndex = 14;
            Hint.SetToolTip(WarningMinutes, "Show a warning that the system is going to shut down. Set to 0 to disable this");
            WarningMinutes.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // ShutdownCommand
            // 
            ShutdownCommand.Location = new Point(147, 166);
            ShutdownCommand.Name = "ShutdownCommand";
            ShutdownCommand.Size = new Size(577, 23);
            ShutdownCommand.TabIndex = 17;
            Hint.SetToolTip(ShutdownCommand, "Command to run. Empty to switch back to default");
            // 
            // WarningMinutesLabel
            // 
            WarningMinutesLabel.AutoSize = true;
            WarningMinutesLabel.Location = new Point(240, 139);
            WarningMinutesLabel.Name = "WarningMinutesLabel";
            WarningMinutesLabel.Size = new Size(51, 15);
            WarningMinutesLabel.TabIndex = 15;
            WarningMinutesLabel.Text = "Seconds";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 139);
            label2.Name = "label2";
            label2.Size = new Size(76, 15);
            label2.TabIndex = 16;
            label2.Text = "Warningtime";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 169);
            label1.Name = "label1";
            label1.Size = new Size(123, 15);
            label1.TabIndex = 18;
            label1.Text = "Shutdown-Command";
            // 
            // Settings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(864, 267);
            ControlBox = false;
            Controls.Add(label1);
            Controls.Add(ShutdownCommand);
            Controls.Add(label2);
            Controls.Add(WarningMinutesLabel);
            Controls.Add(WarningMinutes);
            Controls.Add(SaveButton);
            Controls.Add(MinutesLabel);
            Controls.Add(DownloadMultiplyer);
            Controls.Add(DownloadRate);
            Controls.Add(MouseMinuteSelect);
            Controls.Add(TasksList);
            Controls.Add(SelectTasks);
            Controls.Add(DownloadLabel);
            Controls.Add(MouseLabel);
            Controls.Add(TaskLabel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "Settings";
            Text = "AutoShutdown-Settings";
            ((System.ComponentModel.ISupportInitialize)MouseMinuteSelect).EndInit();
            ((System.ComponentModel.ISupportInitialize)DownloadRate).EndInit();
            ((System.ComponentModel.ISupportInitialize)WarningMinutes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label TaskLabel;
        private Label MouseLabel;
        private Label DownloadLabel;
        private Button SelectTasks;
        private TextBox TasksList;
        private ToolTip Hint;
        private NumericUpDown MouseMinuteSelect;
        private NumericUpDown DownloadRate;
        private ComboBox DownloadMultiplyer;
        private Label MinutesLabel;
        private Button SaveButton;
        private NotifyIcon Notifyer;
        private Label WarningMinutesLabel;
        private NumericUpDown WarningMinutes;
        private Label label2;
        private TextBox ShutdownCommand;
        private Label label1;
    }
}