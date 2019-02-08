using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TobiTools
{
    public partial class SlaveRotationForm : Form
    {
        private Form1 MainForm = null;
        private FormationDataEntry CurrentFormationEntry = null;
        public SlaveRotationForm(Form1 mainForm, FormationDataEntry dataEntry)
        {
            MainForm = mainForm;
            CurrentFormationEntry = dataEntry;
            InitializeComponent();
            trackBar1.Value = 180;
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            MainForm.RotateEntry(CurrentFormationEntry, trackBar1.Value - 180);
            MainForm.ShowEntry(CurrentFormationEntry);
        }
    }
}
