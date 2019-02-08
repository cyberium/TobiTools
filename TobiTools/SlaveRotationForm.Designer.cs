namespace TobiTools
{
    partial class SlaveRotationForm
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
            this.ValidBUT = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.CancelBUT = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // ValidBUT
            // 
            this.ValidBUT.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ValidBUT.Location = new System.Drawing.Point(243, 82);
            this.ValidBUT.Name = "ValidBUT";
            this.ValidBUT.Size = new System.Drawing.Size(75, 23);
            this.ValidBUT.TabIndex = 0;
            this.ValidBUT.Text = "Valid";
            this.ValidBUT.UseVisualStyleBackColor = true;
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(12, 31);
            this.trackBar1.Maximum = 359;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(388, 45);
            this.trackBar1.TabIndex = 1;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // CancelBUT
            // 
            this.CancelBUT.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBUT.Location = new System.Drawing.Point(325, 82);
            this.CancelBUT.Name = "CancelBUT";
            this.CancelBUT.Size = new System.Drawing.Size(75, 23);
            this.CancelBUT.TabIndex = 2;
            this.CancelBUT.Text = "Cancel";
            this.CancelBUT.UseVisualStyleBackColor = true;
            // 
            // SlaveRotationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MistyRose;
            this.ClientSize = new System.Drawing.Size(412, 117);
            this.Controls.Add(this.CancelBUT);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.ValidBUT);
            this.Name = "SlaveRotationForm";
            this.Text = "SlaveRotationForm";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ValidBUT;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Button CancelBUT;
    }
}