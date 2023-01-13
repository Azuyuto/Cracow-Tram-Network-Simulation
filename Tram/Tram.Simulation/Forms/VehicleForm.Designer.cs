namespace Tram.Simulation.Forms
{
    partial class VehicleForm
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
            this.summaryTabPage = new System.Windows.Forms.TabPage();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.propertiesLabel = new System.Windows.Forms.RichTextBox();
            this.summaryTabPage.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // summaryTabPage
            // 
            this.summaryTabPage.BackColor = System.Drawing.Color.White;
            this.summaryTabPage.Controls.Add(this.propertiesLabel);
            this.summaryTabPage.Location = new System.Drawing.Point(4, 22);
            this.summaryTabPage.Name = "summaryTabPage";
            this.summaryTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.summaryTabPage.Size = new System.Drawing.Size(482, 640);
            this.summaryTabPage.TabIndex = 0;
            this.summaryTabPage.Text = "Podsumowanie";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.summaryTabPage);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(490, 666);
            this.tabControl.TabIndex = 0;
            // 
            // propertiesLabel
            // 
            this.propertiesLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.propertiesLabel.Location = new System.Drawing.Point(3, 3);
            this.propertiesLabel.Name = "propertiesLabel";
            this.propertiesLabel.Size = new System.Drawing.Size(476, 634);
            this.propertiesLabel.TabIndex = 0;
            this.propertiesLabel.Text = "";
            // 
            // VehicleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(490, 666);
            this.Controls.Add(this.tabControl);
            this.Name = "VehicleForm";
            this.Text = "VehicleForm";
            this.summaryTabPage.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabPage summaryTabPage;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.RichTextBox propertiesLabel;
    }
}