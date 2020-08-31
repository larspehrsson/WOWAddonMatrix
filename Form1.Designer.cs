namespace WOW_Addon_manager
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.OpdaterBtn = new System.Windows.Forms.Button();
            this.clearbtn = new System.Windows.Forms.Button();
            this.dependbtn = new System.Windows.Forms.Button();
            this.AccountComboBox = new System.Windows.Forms.ComboBox();
            this.RealmComboBox = new System.Windows.Forms.ComboBox();
            this.versiondb = new System.Windows.Forms.ComboBox();
            this.TemplateResetBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 33);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1229, 589);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridView1_CellMouseClick);
            this.dataGridView1.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridView1_CellMouseDoubleClick);
            this.dataGridView1.CellToolTipTextNeeded += new System.Windows.Forms.DataGridViewCellToolTipTextNeededEventHandler(this.dataGridView1_CellToolTipTextNeeded);
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            // 
            // OpdaterBtn
            // 
            this.OpdaterBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OpdaterBtn.Location = new System.Drawing.Point(1166, 4);
            this.OpdaterBtn.Name = "OpdaterBtn";
            this.OpdaterBtn.Size = new System.Drawing.Size(75, 23);
            this.OpdaterBtn.TabIndex = 3;
            this.OpdaterBtn.Text = "Update";
            this.OpdaterBtn.UseVisualStyleBackColor = true;
            this.OpdaterBtn.Click += new System.EventHandler(this.OpdaterBtn_Click);
            // 
            // clearbtn
            // 
            this.clearbtn.Location = new System.Drawing.Point(12, 4);
            this.clearbtn.Name = "clearbtn";
            this.clearbtn.Size = new System.Drawing.Size(75, 23);
            this.clearbtn.TabIndex = 4;
            this.clearbtn.Text = "Clear all";
            this.clearbtn.UseVisualStyleBackColor = true;
            this.clearbtn.Click += new System.EventHandler(this.clearbtn_Click);
            // 
            // dependbtn
            // 
            this.dependbtn.Location = new System.Drawing.Point(94, 4);
            this.dependbtn.Name = "dependbtn";
            this.dependbtn.Size = new System.Drawing.Size(129, 23);
            this.dependbtn.TabIndex = 5;
            this.dependbtn.Text = "Check dependencies";
            this.dependbtn.UseVisualStyleBackColor = true;
            this.dependbtn.Click += new System.EventHandler(this.dependbtn_Click);
            // 
            // AccountComboBox
            // 
            this.AccountComboBox.FormattingEnabled = true;
            this.AccountComboBox.Location = new System.Drawing.Point(360, 5);
            this.AccountComboBox.Name = "AccountComboBox";
            this.AccountComboBox.Size = new System.Drawing.Size(126, 21);
            this.AccountComboBox.TabIndex = 6;
            this.AccountComboBox.SelectedIndexChanged += new System.EventHandler(this.AccountComboBox_SelectedIndexChanged);
            // 
            // RealmComboBox
            // 
            this.RealmComboBox.FormattingEnabled = true;
            this.RealmComboBox.Location = new System.Drawing.Point(493, 5);
            this.RealmComboBox.Name = "RealmComboBox";
            this.RealmComboBox.Size = new System.Drawing.Size(121, 21);
            this.RealmComboBox.TabIndex = 7;
            this.RealmComboBox.SelectedIndexChanged += new System.EventHandler(this.RealmComboBox_SelectedIndexChanged);
            // 
            // versiondb
            // 
            this.versiondb.FormattingEnabled = true;
            this.versiondb.Location = new System.Drawing.Point(620, 5);
            this.versiondb.Name = "versiondb";
            this.versiondb.Size = new System.Drawing.Size(158, 21);
            this.versiondb.TabIndex = 10;
            this.versiondb.SelectedIndexChanged += new System.EventHandler(this.versiondb_SelectedIndexChanged);
            // 
            // TemplateResetBtn
            // 
            this.TemplateResetBtn.Location = new System.Drawing.Point(229, 4);
            this.TemplateResetBtn.Name = "TemplateResetBtn";
            this.TemplateResetBtn.Size = new System.Drawing.Size(107, 23);
            this.TemplateResetBtn.TabIndex = 11;
            this.TemplateResetBtn.Text = "Reset to template";
            this.TemplateResetBtn.UseVisualStyleBackColor = true;
            this.TemplateResetBtn.Click += new System.EventHandler(this.TemplateResetBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1253, 634);
            this.Controls.Add(this.TemplateResetBtn);
            this.Controls.Add(this.versiondb);
            this.Controls.Add(this.RealmComboBox);
            this.Controls.Add(this.AccountComboBox);
            this.Controls.Add(this.dependbtn);
            this.Controls.Add(this.clearbtn);
            this.Controls.Add(this.OpdaterBtn);
            this.Controls.Add(this.dataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "World of Warcraft - Addon overview";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button OpdaterBtn;
        private System.Windows.Forms.Button clearbtn;
        private System.Windows.Forms.Button dependbtn;
        private System.Windows.Forms.ComboBox AccountComboBox;
        private System.Windows.Forms.ComboBox RealmComboBox;
        private System.Windows.Forms.ComboBox versiondb;
        private System.Windows.Forms.Button TemplateResetBtn;

    }
}

