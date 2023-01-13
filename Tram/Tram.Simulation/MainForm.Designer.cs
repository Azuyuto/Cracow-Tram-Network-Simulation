namespace Tram.Simulation
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.renderPanel = new System.Windows.Forms.Panel();
            this.zoomInButton = new System.Windows.Forms.Panel();
            this.zoomOutButton = new System.Windows.Forms.Panel();
            this.zoomOriginalButton = new System.Windows.Forms.Panel();
            this.centerScreenButton = new System.Windows.Forms.Panel();
            this.aboutUsButton = new System.Windows.Forms.Panel();
            this.rightPanel = new System.Windows.Forms.Panel();
            this.vehiclesGridView = new System.Windows.Forms.DataGridView();
            this.completedGridView = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Last = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Info = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rightPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vehiclesGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.completedGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // renderPanel
            // 
            this.renderPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.renderPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.renderPanel.Location = new System.Drawing.Point(12, 12);
            this.renderPanel.Name = "renderPanel";
            this.renderPanel.Size = new System.Drawing.Size(715, 715);
            this.renderPanel.TabIndex = 1;
            this.renderPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.renderPanel_Paint);
            this.renderPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.renderPanel_MouseMove);
            // 
            // zoomInButton
            // 
            this.zoomInButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.zoomInButton.BackColor = System.Drawing.Color.White;
            this.zoomInButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("zoomInButton.BackgroundImage")));
            this.zoomInButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.zoomInButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.zoomInButton.Location = new System.Drawing.Point(733, 13);
            this.zoomInButton.Name = "zoomInButton";
            this.zoomInButton.Size = new System.Drawing.Size(48, 48);
            this.zoomInButton.TabIndex = 4;
            this.zoomInButton.Click += new System.EventHandler(this.ZoomInButton_Click);
            // 
            // zoomOutButton
            // 
            this.zoomOutButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.zoomOutButton.BackColor = System.Drawing.Color.White;
            this.zoomOutButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("zoomOutButton.BackgroundImage")));
            this.zoomOutButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.zoomOutButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.zoomOutButton.Location = new System.Drawing.Point(733, 67);
            this.zoomOutButton.Name = "zoomOutButton";
            this.zoomOutButton.Size = new System.Drawing.Size(48, 48);
            this.zoomOutButton.TabIndex = 5;
            this.zoomOutButton.Click += new System.EventHandler(this.ZoomOutButton_Click);
            // 
            // zoomOriginalButton
            // 
            this.zoomOriginalButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.zoomOriginalButton.BackColor = System.Drawing.Color.White;
            this.zoomOriginalButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("zoomOriginalButton.BackgroundImage")));
            this.zoomOriginalButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.zoomOriginalButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.zoomOriginalButton.Location = new System.Drawing.Point(733, 121);
            this.zoomOriginalButton.Name = "zoomOriginalButton";
            this.zoomOriginalButton.Size = new System.Drawing.Size(48, 48);
            this.zoomOriginalButton.TabIndex = 5;
            this.zoomOriginalButton.Click += new System.EventHandler(this.zoomOriginalButton_Click);
            // 
            // centerScreenButton
            // 
            this.centerScreenButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.centerScreenButton.BackColor = System.Drawing.Color.White;
            this.centerScreenButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("centerScreenButton.BackgroundImage")));
            this.centerScreenButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.centerScreenButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.centerScreenButton.Location = new System.Drawing.Point(733, 175);
            this.centerScreenButton.Name = "centerScreenButton";
            this.centerScreenButton.Size = new System.Drawing.Size(48, 48);
            this.centerScreenButton.TabIndex = 5;
            this.centerScreenButton.Click += new System.EventHandler(this.centerScreenButton_Click);
            // 
            // aboutUsButton
            // 
            this.aboutUsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.aboutUsButton.BackColor = System.Drawing.Color.White;
            this.aboutUsButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("aboutUsButton.BackgroundImage")));
            this.aboutUsButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.aboutUsButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.aboutUsButton.Location = new System.Drawing.Point(733, 229);
            this.aboutUsButton.Name = "aboutUsButton";
            this.aboutUsButton.Size = new System.Drawing.Size(48, 48);
            this.aboutUsButton.TabIndex = 7;
            this.aboutUsButton.Paint += new System.Windows.Forms.PaintEventHandler(this.aboutUsButton_Paint);
            // 
            // rightPanel
            // 
            this.rightPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rightPanel.AutoScroll = true;
            this.rightPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rightPanel.Controls.Add(this.completedGridView);
            this.rightPanel.Controls.Add(this.vehiclesGridView);
            this.rightPanel.Location = new System.Drawing.Point(787, 12);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size(388, 716);
            this.rightPanel.TabIndex = 0;
            // 
            // vehiclesGridView
            // 
            this.vehiclesGridView.AllowUserToAddRows = false;
            this.vehiclesGridView.AllowUserToDeleteRows = false;
            this.vehiclesGridView.AllowUserToResizeColumns = false;
            this.vehiclesGridView.AllowUserToResizeRows = false;
            this.vehiclesGridView.BackgroundColor = System.Drawing.Color.White;
            this.vehiclesGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.vehiclesGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.Last,
            this.Info});
            this.vehiclesGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vehiclesGridView.Location = new System.Drawing.Point(0, 0);
            this.vehiclesGridView.MultiSelect = false;
            this.vehiclesGridView.Name = "vehiclesGridView";
            this.vehiclesGridView.ReadOnly = true;
            this.vehiclesGridView.RowHeadersVisible = false;
            this.vehiclesGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.vehiclesGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.vehiclesGridView.ShowCellErrors = false;
            this.vehiclesGridView.ShowEditingIcon = false;
            this.vehiclesGridView.ShowRowErrors = false;
            this.vehiclesGridView.Size = new System.Drawing.Size(386, 714);
            this.vehiclesGridView.TabIndex = 0;
            this.vehiclesGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.vehiclesGridView_CellContentClick);
            // 
            // completedGridView
            // 
            this.completedGridView.AllowUserToAddRows = false;
            this.completedGridView.AllowUserToDeleteRows = false;
            this.completedGridView.AllowUserToResizeColumns = false;
            this.completedGridView.AllowUserToResizeRows = false;
            this.completedGridView.BackgroundColor = System.Drawing.Color.White;
            this.completedGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.completedGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1});
            this.completedGridView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.completedGridView.Location = new System.Drawing.Point(0, 595);
            this.completedGridView.MultiSelect = false;
            this.completedGridView.Name = "completedGridView";
            this.completedGridView.ReadOnly = true;
            this.completedGridView.RowHeadersVisible = false;
            this.completedGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.completedGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.completedGridView.ShowCellErrors = false;
            this.completedGridView.ShowEditingIcon = false;
            this.completedGridView.ShowRowErrors = false;
            this.completedGridView.Size = new System.Drawing.Size(386, 119);
            this.completedGridView.TabIndex = 1;
            this.completedGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.completedGridView_CellClick);
            // 
            // Id
            // 
            this.Id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Id.HeaderText = "Nr & Kierunek";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Last
            // 
            this.Last.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Last.HeaderText = "Ostatni przystanek";
            this.Last.Name = "Last";
            this.Last.ReadOnly = true;
            this.Last.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Info
            // 
            this.Info.HeaderText = "Info";
            this.Info.Image = global::Tram.Simulation.Properties.Resources.information_small;
            this.Info.Name = "Info";
            this.Info.ReadOnly = true;
            this.Info.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Info.Width = 24;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.HeaderText = "Zakończone trasy";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1187, 740);
            this.Controls.Add(this.aboutUsButton);
            this.Controls.Add(this.zoomOutButton);
            this.Controls.Add(this.zoomOriginalButton);
            this.Controls.Add(this.renderPanel);
            this.Controls.Add(this.centerScreenButton);
            this.Controls.Add(this.rightPanel);
            this.Controls.Add(this.zoomInButton);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.rightPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.vehiclesGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.completedGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel renderPanel;
        private System.Windows.Forms.Panel zoomInButton;
        private System.Windows.Forms.Panel zoomOutButton;
        private System.Windows.Forms.Panel zoomOriginalButton;
        private System.Windows.Forms.Panel centerScreenButton;
        private System.Windows.Forms.Panel aboutUsButton;
        private System.Windows.Forms.Panel rightPanel;
        private System.Windows.Forms.DataGridView vehiclesGridView;
        private System.Windows.Forms.DataGridView completedGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Last;
        private System.Windows.Forms.DataGridViewImageColumn Info;
    }
}