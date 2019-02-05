namespace TobiTools
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.xOriginTBX = new System.Windows.Forms.TextBox();
            this.yOriginTBX = new System.Windows.Forms.TextBox();
            this.zOriginTBX = new System.Windows.Forms.TextBox();
            this.xDestTBX = new System.Windows.Forms.TextBox();
            this.yDestTBX = new System.Windows.Forms.TextBox();
            this.zDestTBX = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.distResultTBX = new System.Windows.Forms.TextBox();
            this.angleResultTBX = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.oDestTBX = new System.Windows.Forms.TextBox();
            this.oOriginTBX = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.MainDataDGV = new System.Windows.Forms.DataGridView();
            this.MainDrawPB = new System.Windows.Forms.PictureBox();
            this.MainTimer = new System.Windows.Forms.Timer(this.components);
            this.label13 = new System.Windows.Forms.Label();
            this.InGameDGV = new System.Windows.Forms.DataGridView();
            this.button3 = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.EntryTBX = new System.Windows.Forms.TextBox();
            this.EntriesDGV = new System.Windows.Forms.DataGridView();
            this.label15 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainDataDGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainDrawPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InGameDGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EntriesDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // xOriginTBX
            // 
            this.xOriginTBX.Location = new System.Drawing.Point(22, 22);
            this.xOriginTBX.Name = "xOriginTBX";
            this.xOriginTBX.Size = new System.Drawing.Size(100, 20);
            this.xOriginTBX.TabIndex = 0;
            this.xOriginTBX.Click += new System.EventHandler(this.TextBox_Enter);
            this.xOriginTBX.Enter += new System.EventHandler(this.TextBox_Enter);
            // 
            // yOriginTBX
            // 
            this.yOriginTBX.Location = new System.Drawing.Point(22, 48);
            this.yOriginTBX.Name = "yOriginTBX";
            this.yOriginTBX.Size = new System.Drawing.Size(100, 20);
            this.yOriginTBX.TabIndex = 1;
            this.yOriginTBX.Click += new System.EventHandler(this.TextBox_Enter);
            this.yOriginTBX.Enter += new System.EventHandler(this.TextBox_Enter);
            // 
            // zOriginTBX
            // 
            this.zOriginTBX.Location = new System.Drawing.Point(22, 74);
            this.zOriginTBX.Name = "zOriginTBX";
            this.zOriginTBX.Size = new System.Drawing.Size(100, 20);
            this.zOriginTBX.TabIndex = 2;
            this.zOriginTBX.Click += new System.EventHandler(this.TextBox_Enter);
            this.zOriginTBX.Enter += new System.EventHandler(this.TextBox_Enter);
            // 
            // xDestTBX
            // 
            this.xDestTBX.Location = new System.Drawing.Point(164, 22);
            this.xDestTBX.Name = "xDestTBX";
            this.xDestTBX.Size = new System.Drawing.Size(100, 20);
            this.xDestTBX.TabIndex = 4;
            this.xDestTBX.Click += new System.EventHandler(this.TextBox_Enter);
            this.xDestTBX.Enter += new System.EventHandler(this.TextBox_Enter);
            // 
            // yDestTBX
            // 
            this.yDestTBX.Location = new System.Drawing.Point(164, 48);
            this.yDestTBX.Name = "yDestTBX";
            this.yDestTBX.Size = new System.Drawing.Size(100, 20);
            this.yDestTBX.TabIndex = 5;
            this.yDestTBX.Click += new System.EventHandler(this.TextBox_Enter);
            this.yDestTBX.Enter += new System.EventHandler(this.TextBox_Enter);
            // 
            // zDestTBX
            // 
            this.zDestTBX.Location = new System.Drawing.Point(164, 74);
            this.zDestTBX.Name = "zDestTBX";
            this.zDestTBX.Size = new System.Drawing.Size(100, 20);
            this.zDestTBX.TabIndex = 6;
            this.zDestTBX.Click += new System.EventHandler(this.TextBox_Enter);
            this.zDestTBX.Enter += new System.EventHandler(this.TextBox_Enter);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(352, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Compute";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // distResultTBX
            // 
            this.distResultTBX.Location = new System.Drawing.Point(352, 51);
            this.distResultTBX.Name = "distResultTBX";
            this.distResultTBX.ReadOnly = true;
            this.distResultTBX.Size = new System.Drawing.Size(100, 20);
            this.distResultTBX.TabIndex = 9;
            // 
            // angleResultTBX
            // 
            this.angleResultTBX.Location = new System.Drawing.Point(352, 77);
            this.angleResultTBX.Name = "angleResultTBX";
            this.angleResultTBX.ReadOnly = true;
            this.angleResultTBX.Size = new System.Drawing.Size(100, 20);
            this.angleResultTBX.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Master";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(161, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Slave";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "X";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(146, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "X";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(146, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Y";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Y";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(146, 77);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Z";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(4, 77);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Z";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(286, 54);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(49, 13);
            this.label11.TabIndex = 21;
            this.label11.Text = "Distance";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(286, 80);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(34, 13);
            this.label12.TabIndex = 22;
            this.label12.Text = "Angle";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(4, 103);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(15, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "O";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(146, 103);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(15, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "O";
            // 
            // oDestTBX
            // 
            this.oDestTBX.Location = new System.Drawing.Point(164, 100);
            this.oDestTBX.Name = "oDestTBX";
            this.oDestTBX.Size = new System.Drawing.Size(100, 20);
            this.oDestTBX.TabIndex = 7;
            this.oDestTBX.Click += new System.EventHandler(this.TextBox_Enter);
            this.oDestTBX.Enter += new System.EventHandler(this.TextBox_Enter);
            // 
            // oOriginTBX
            // 
            this.oOriginTBX.Location = new System.Drawing.Point(22, 100);
            this.oOriginTBX.Name = "oOriginTBX";
            this.oOriginTBX.Size = new System.Drawing.Size(100, 20);
            this.oOriginTBX.TabIndex = 3;
            this.oOriginTBX.Click += new System.EventHandler(this.TextBox_Enter);
            this.oOriginTBX.Enter += new System.EventHandler(this.TextBox_Enter);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(871, 417);
            this.tabControl1.TabIndex = 23;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button3);
            this.tabPage1.Controls.Add(this.zDestTBX);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.xOriginTBX);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.yOriginTBX);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.zOriginTBX);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.oOriginTBX);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.xDestTBX);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.yDestTBX);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.oDestTBX);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.distResultTBX);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.angleResultTBX);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(832, 391);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Dist/Angle";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label15);
            this.tabPage2.Controls.Add(this.EntriesDGV);
            this.tabPage2.Controls.Add(this.EntryTBX);
            this.tabPage2.Controls.Add(this.label14);
            this.tabPage2.Controls.Add(this.InGameDGV);
            this.tabPage2.Controls.Add(this.label13);
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.MainDataDGV);
            this.tabPage2.Controls.Add(this.MainDrawPB);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(863, 391);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Visual Formation";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(391, 360);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(466, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Do not click this!";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // MainDataDGV
            // 
            this.MainDataDGV.Location = new System.Drawing.Point(390, 115);
            this.MainDataDGV.MultiSelect = false;
            this.MainDataDGV.Name = "MainDataDGV";
            this.MainDataDGV.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.IndianRed;
            this.MainDataDGV.Size = new System.Drawing.Size(200, 239);
            this.MainDataDGV.TabIndex = 1;
            this.MainDataDGV.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.MainDataDGV_CellEndEdit);
            this.MainDataDGV.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.MainDataDGV_UserAddedRow);
            this.MainDataDGV.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.MainDataDGV_UserDeletingRow);
            // 
            // MainDrawPB
            // 
            this.MainDrawPB.BackColor = System.Drawing.Color.DarkSlateGray;
            this.MainDrawPB.Location = new System.Drawing.Point(6, 6);
            this.MainDrawPB.Name = "MainDrawPB";
            this.MainDrawPB.Size = new System.Drawing.Size(380, 380);
            this.MainDrawPB.TabIndex = 0;
            this.MainDrawPB.TabStop = false;
            this.MainDrawPB.Paint += new System.Windows.Forms.PaintEventHandler(this.MainDrawPB_Paint);
            this.MainDrawPB.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainDrawPB_MouseMove);
            // 
            // MainTimer
            // 
            this.MainTimer.Enabled = true;
            this.MainTimer.Tick += new System.EventHandler(this.MainTimer_Tick);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(392, 6);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(121, 13);
            this.label13.TabIndex = 13;
            this.label13.Text = "Current InGame Position";
            // 
            // InGameDGV
            // 
            this.InGameDGV.AllowUserToAddRows = false;
            this.InGameDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.InGameDGV.Location = new System.Drawing.Point(392, 22);
            this.InGameDGV.Name = "InGameDGV";
            this.InGameDGV.RowHeadersVisible = false;
            this.InGameDGV.Size = new System.Drawing.Size(198, 57);
            this.InGameDGV.TabIndex = 14;
            this.InGameDGV.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.InGameDGV_CellEndEdit);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(446, 22);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(113, 23);
            this.button3.TabIndex = 23;
            this.button3.Text = "Add to Visualisator";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(392, 92);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(79, 13);
            this.label14.TabIndex = 15;
            this.label14.Text = "Formation entry";
            // 
            // EntryTBX
            // 
            this.EntryTBX.BackColor = System.Drawing.Color.IndianRed;
            this.EntryTBX.Location = new System.Drawing.Point(477, 89);
            this.EntryTBX.Name = "EntryTBX";
            this.EntryTBX.Size = new System.Drawing.Size(53, 20);
            this.EntryTBX.TabIndex = 16;
            this.EntryTBX.Text = "0";
            this.EntryTBX.TextChanged += new System.EventHandler(this.EntryTBX_TextChanged);
            this.EntryTBX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EntryTBX_KeyPress);
            // 
            // EntriesDGV
            // 
            this.EntriesDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.EntriesDGV.Location = new System.Drawing.Point(596, 22);
            this.EntriesDGV.Name = "EntriesDGV";
            this.EntriesDGV.Size = new System.Drawing.Size(261, 332);
            this.EntriesDGV.TabIndex = 17;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(593, 6);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(39, 13);
            this.label15.TabIndex = 18;
            this.label15.Text = "Entries";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Pink;
            this.ClientSize = new System.Drawing.Size(871, 417);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Tobi Tools";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainDataDGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainDrawPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InGameDGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EntriesDGV)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox xOriginTBX;
        private System.Windows.Forms.TextBox yOriginTBX;
        private System.Windows.Forms.TextBox zOriginTBX;
        private System.Windows.Forms.TextBox xDestTBX;
        private System.Windows.Forms.TextBox yDestTBX;
        private System.Windows.Forms.TextBox zDestTBX;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox distResultTBX;
        private System.Windows.Forms.TextBox angleResultTBX;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox oDestTBX;
        private System.Windows.Forms.TextBox oOriginTBX;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.PictureBox MainDrawPB;
        private System.Windows.Forms.Timer MainTimer;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView MainDataDGV;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DataGridView InGameDGV;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox EntryTBX;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.DataGridView EntriesDGV;
        private System.Windows.Forms.Label label15;
    }
}

