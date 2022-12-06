namespace Tram.Simulation
{
    partial class SimulationForm
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
            this.sidebarPanel = new System.Windows.Forms.Panel();
            this.timerLabel = new System.Windows.Forms.Label();
            this.stopButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.titleLabel = new System.Windows.Forms.Label();
            this.renderPanel = new System.Windows.Forms.Panel();
            this.sidebarPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // sidebarPanel
            // 
            this.sidebarPanel.BackColor = System.Drawing.SystemColors.Menu;
            this.sidebarPanel.Controls.Add(this.timerLabel);
            this.sidebarPanel.Controls.Add(this.stopButton);
            this.sidebarPanel.Controls.Add(this.startButton);
            this.sidebarPanel.Controls.Add(this.titleLabel);
            this.sidebarPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebarPanel.Location = new System.Drawing.Point(0, 0);
            this.sidebarPanel.Name = "sidebarPanel";
            this.sidebarPanel.Size = new System.Drawing.Size(172, 814);
            this.sidebarPanel.TabIndex = 0;
            // 
            // timerLabel
            // 
            this.timerLabel.AutoSize = true;
            this.timerLabel.Location = new System.Drawing.Point(60, 62);
            this.timerLabel.Name = "timerLabel";
            this.timerLabel.Size = new System.Drawing.Size(49, 13);
            this.timerLabel.TabIndex = 3;
            this.timerLabel.Text = "07:00:00";
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(3, 80);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(166, 23);
            this.stopButton.TabIndex = 2;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(3, 36);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(166, 23);
            this.startButton.TabIndex = 1;
            this.startButton.Text = "Start Simulation";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Location = new System.Drawing.Point(33, 9);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(109, 13);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Tram Simulation v 0.1";
            // 
            // renderPanel
            // 
            this.renderPanel.Location = new System.Drawing.Point(178, 8);
            this.renderPanel.Name = "renderPanel";
            this.renderPanel.Size = new System.Drawing.Size(800, 800);
            this.renderPanel.TabIndex = 1;
            this.renderPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.renderPanel_MouseMove);
            // 
            // SimulationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(985, 814);
            this.Controls.Add(this.renderPanel);
            this.Controls.Add(this.sidebarPanel);
            this.Name = "SimulationForm";
            this.Text = "SimulationForm";
            this.sidebarPanel.ResumeLayout(false);
            this.sidebarPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel sidebarPanel;
        private System.Windows.Forms.Label timerLabel;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Panel renderPanel;
    }
}