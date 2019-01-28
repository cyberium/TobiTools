using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using TobiTools.Properties;

namespace TobiTools
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void LoadSettings()
        {
            xOriginTBX.Text = Settings.Default.xOri;
            yOriginTBX.Text = Settings.Default.yOri;
            zOriginTBX.Text = Settings.Default.zOri;
            oOriginTBX.Text = Settings.Default.oOri;
            xDestTBX.Text   = Settings.Default.xDest;
            yDestTBX.Text   = Settings.Default.yDest;
            zDestTBX.Text   = Settings.Default.zDest;
            oDestTBX.Text   = Settings.Default.oDest;

            x1OrientTBX.Text = Settings.Default.x1Orient;
            y1OrientTBX.Text = Settings.Default.y1Orient;
            z1OrientTBX.Text = Settings.Default.z1Orient;
            x2OrientTBX.Text = Settings.Default.x2Orient;
            y2OrientTBX.Text = Settings.Default.y2Orient;
            z2OrientTBX.Text = Settings.Default.z2Orient;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.xOri  = xOriginTBX.Text;
            Settings.Default.yOri  = yOriginTBX.Text;
            Settings.Default.zOri  = zOriginTBX.Text;
            Settings.Default.oOri  = oOriginTBX.Text;
            Settings.Default.xDest = xDestTBX.Text;
            Settings.Default.yDest = yDestTBX.Text;
            Settings.Default.zDest = zDestTBX.Text;
            Settings.Default.oDest = oDestTBX.Text;

            Settings.Default.x1Orient = x1OrientTBX.Text;
            Settings.Default.y1Orient = y1OrientTBX.Text;
            Settings.Default.z1Orient = z1OrientTBX.Text;
            Settings.Default.x2Orient = x2OrientTBX.Text;
            Settings.Default.y2Orient = y2OrientTBX.Text;
            Settings.Default.z2Orient = z2OrientTBX.Text;
            Settings.Default.Save();
        }

        private void TextBox_Enter(object sender, EventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            float xOri, yOri, zOri, oOri;
            float xDest, yDest, zDest, oDest;
            if (!float.TryParse(xOriginTBX.Text, out xOri))
            {
                MessageBox.Show("xOrigin is not valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!float.TryParse(yOriginTBX.Text, out yOri))
            {
                MessageBox.Show("yOrigin is not valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!float.TryParse(zOriginTBX.Text, out zOri))
            {
                MessageBox.Show("zOrigin is not valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!float.TryParse(oOriginTBX.Text, out oOri))
            {
                MessageBox.Show("oOrigin is not valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!float.TryParse(xDestTBX.Text, out xDest))
            {
                MessageBox.Show("xDestination is not valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!float.TryParse(yDestTBX.Text, out yDest))
            {
                MessageBox.Show("yDestination is not valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!float.TryParse(zDestTBX.Text, out zDest))
            {
                MessageBox.Show("zDestination is not valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!float.TryParse(oDestTBX.Text, out oDest))
            {
                MessageBox.Show("oDestination is not valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Vector3 origin = new Vector3(xOri, yOri, zOri);
            Vector3 dest = new Vector3(xDest, yDest, zDest);

            float distance = Vector3.Distance(origin, dest);
            distResultTBX.Text = distance.ToString();

            double angle = Math.PI - Math.Atan2(origin.Y - dest.Y, origin.X - dest.X) + (Math.PI / 2);
            double degrees = ((180 / Math.PI) * angle);
            degrees = (degrees + 360) % 360;
            angleResultTBX.Text = degrees.ToString();
        }

        private void ComputeOriBut_Click(object sender, EventArgs e)
        {
            float xOri, yOri, zOri;
            float xDest, yDest, zDest;
            if (!float.TryParse(x1OrientTBX.Text, out xOri))
            {
                MessageBox.Show("x1 is not valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!float.TryParse(y1OrientTBX.Text, out yOri))
            {
                MessageBox.Show("y1 is not valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!float.TryParse(z1OrientTBX.Text, out zOri))
            {
                MessageBox.Show("z1 is not valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!float.TryParse(x2OrientTBX.Text, out xDest))
            {
                MessageBox.Show("x2 is not valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!float.TryParse(y2OrientTBX.Text, out yDest))
            {
                MessageBox.Show("y2 is not valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!float.TryParse(z2OrientTBX.Text, out zDest))
            {
                MessageBox.Show("z2 is not valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Vector3 p1 = new Vector3(xOri, yOri, zOri);
            Vector3 p2 = new Vector3(xDest, yDest, zDest);
            double angle = Math.PI - Math.Atan2(p1.Y - p2.Y, p1.X - p2.X) + (Math.PI/2);
            double degrees = ((180 / Math.PI) * angle);
            degrees = (degrees + 360) % 360;
            orientationResDegTBX.Text = degrees.ToString();
            orientationResRadTBX.Text = angle.ToString();
        }
    }
}
