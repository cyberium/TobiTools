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
using System.Text.RegularExpressions;
using TobiTools.Properties;
using System.Globalization;

namespace TobiTools
{
    public partial class Form1 : Form
    {
        Dictionary<int, Object> ObjectsList = new Dictionary<int, Object>();
        NumberStyles NumStyle = System.Globalization.NumberStyles.AllowDecimalPoint;
        CultureInfo Culture = CultureInfo.InvariantCulture;

        int BaseID = 1;
        public Form1()
        {
            InitializeComponent();
            LoadSettings();
            Initialize();
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
            Settings.Default.Save();
        }

        private void Initialize()
        {
            MainDrawPB.Image = new Bitmap(380, 380);
            ObjectsList.Add(0, new Master());
            MainDataDGV.ColumnCount = 3;
            MainDataDGV.Columns[0].Name = "Id";
            MainDataDGV.Columns[1].Name = "Angle";
            MainDataDGV.Columns[2].Name = "Dist";

            MainDataDGV.Columns[0].Width = 50;
            MainDataDGV.Columns[1].Width = 100;
            MainDataDGV.Columns[2].Width = 100;
            MainDataDGV.Rows[0].Tag = BaseID++ ;

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

        private void MainTimer_Tick(object sender, EventArgs e)
        {
            /*Image image = MainDrawPB.Image;

            foreach(var item in ObjectsList)
            {
                item.Value.Render(Graphics.FromImage(image));
            }

            MainDrawPB.Refresh();*/
        }

        private bool ValidateFloat(String valueStr, out float value)
        {
            value = -1;
            if (!Regex.IsMatch(valueStr, @"^[0-9]*(?:\.[0-9]*)?$"))
                return false;

            if (!float.TryParse(valueStr, NumStyle, Culture, out value))
                return false;

            return true;
        }

        private bool ValidateInt(String valueStr, out int value)
        {
            value = -1;
            if (!Regex.IsMatch(valueStr, @"^[0-9]*$"))
                return false;

            if (!int.TryParse(valueStr, out value))
                return false;

            return true;
        }

        public static Vector2 Rotate(Vector2 v, float degrees)
        {
            double sin = Math.Sin(degrees * (Math.PI / 180));
            double cos = Math.Cos(degrees * (Math.PI / 180));

            float tx = v.X;
            float ty = v.Y;
            v.X = (float) ((cos * tx) - (sin * ty));
            v.Y = (float) ((sin * tx) + (cos * ty));
            return v;
        }

        private void AddObject(int idx, float angle, float dist)
        {
            Slave slave = null;
            Object obj = null;
            Point pos = new Point();
            if (!ObjectsList.TryGetValue(idx, out obj))
                slave = new Slave(pos);
            else
                slave = obj as Slave;

            Vector2 orig = new Vector2(0, dist * 20);
            Vector2 rotPos = Rotate(orig, angle);
            Vector2 finalPoint = rotPos;

            int x, y;
            x = Convert.ToInt32(finalPoint.X);
            y = Convert.ToInt32(finalPoint.Y);

            slave.SetScreenPosition(new Point(x, y));
            if (obj == null)
                ObjectsList.Add(idx, slave);

            MainDrawPB.Invalidate();
        }

        private void RemoveObject(int idx)
        {
            if (ObjectsList.Remove(idx))
                MainDrawPB.Invalidate();
        }

        private void MainDataDGV_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int id = -1;
            float angle = -1;
            float dist = -1;

            DataGridViewRow currRow = MainDataDGV.CurrentRow;
            foreach (DataGridViewCell cell in currRow.Cells)
            {
                switch (cell.ColumnIndex)
                {
                    case 0:
                        if (cell.Value == null)
                            continue;
                        if (cell.Value == null || !ValidateInt(cell.Value.ToString(), out id))
                            cell.Style.BackColor = Color.IndianRed;
                        else
                            cell.Style.BackColor = Color.LawnGreen;
                        break;

                    case 1:
                        if (cell.Value == null)
                            continue;
                        if (cell.Value == null || !ValidateFloat(cell.Value.ToString(), out angle))
                            cell.Style.BackColor = Color.IndianRed;
                        else
                            cell.Style.BackColor = Color.LawnGreen;
                        break;

                    case 2:
                        if (cell.Value == null)
                            continue;
                        if (cell.Value == null || !ValidateFloat(cell.Value.ToString(), out dist))
                            cell.Style.BackColor = Color.IndianRed;
                        else
                            cell.Style.BackColor = Color.LawnGreen;
                        break;

                    default:
                        break;
                }
            }

            if (id >= 0 && angle >= 0 && dist >= 0)
            {
                foreach (DataGridViewCell cell in currRow.Cells)
                    cell.Style.BackColor = Color.Green;

                AddObject((int)currRow.Tag, angle, dist);
            }
            else
            {
                if (currRow.Tag != null)
                    RemoveObject((int)currRow.Tag);
            }
        }

        private void MainDrawPB_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(MainDrawPB.BackColor);

            foreach (var item in ObjectsList)
            {
                item.Value.Render(e.Graphics);
            }
        }

        private void MainDataDGV_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            RemoveObject((int)e.Row.Tag);
        }

        private void MainDataDGV_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Tag = BaseID++;
        }
    }
}
