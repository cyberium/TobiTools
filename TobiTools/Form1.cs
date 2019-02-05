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
using System.Threading;
using System.IO;

namespace TobiTools
{
    public partial class Form1 : Form
    {
        struct TooltipsInfos
        {
            public string name;
            public string id;
            public string gameCoord;
        }

        Dictionary<int, Object> ObjectsList = new Dictionary<int, Object>();
        NumberStyles NumStyle = NumberStyles.AllowDecimalPoint | NumberStyles.Float;
        CultureInfo Culture = CultureInfo.InvariantCulture;
        bool tooltipsShow = false;
        Rectangle ToolTipsRectangle = new Rectangle();
        Object ToolTipsSelectedObj = null;
        TooltipsInfos ToolTipsInfo;
        Font TTipsFont = new Font("Arial", 8, FontStyle.Regular);
        float BaseGameX = 0; float BaseGameY = 0; float BaseGameO = 0;

        static string SavedDataFileName = "creature_formation_template.sql";
        static int ClientRatio = 10;
        Size VirtualClientSize;

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
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            MaximizeBox = false;
            VirtualClientSize.Width = MainDrawPB.ClientSize.Width * ClientRatio;
            VirtualClientSize.Height = MainDrawPB.ClientSize.Height * ClientRatio;

            MainDrawPB.Image = new Bitmap(MainDrawPB.ClientSize.Width, MainDrawPB.ClientSize.Height);
            ObjectsList.Add(0, new Master(VirtualClientSize));
            MainDataDGV.ColumnCount = 3;
            MainDataDGV.Columns[0].Name = "Id";
            MainDataDGV.Columns[1].Name = "Angle";
            MainDataDGV.Columns[2].Name = "Dist";

            MainDataDGV.Columns[0].Width = 40;
            MainDataDGV.Columns[1].Width = 60;
            MainDataDGV.Columns[2].Width = 60;

            MainDataDGV.Rows[0].Cells[0].Value = MainDataDGV.Rows.Count;
            MainDataDGV.Rows[0].Tag = BaseID++ ;

            MainDataDGV.Columns[0].ReadOnly = true;

            InGameDGV.ColumnCount = 3;
            InGameDGV.Columns[0].Name = "X";
            InGameDGV.Columns[1].Name = "Y";
            InGameDGV.Columns[2].Name = "Orientation";
            InGameDGV.Columns[0].Width = 60;
            InGameDGV.Columns[1].Width = 60;
            InGameDGV.Columns[2].Width = 60;
            InGameDGV.Rows.Add("0", "0", "0");

            EntriesDGV.ColumnCount = 4;
            EntriesDGV.Columns[0].Name = "Entry";
            EntriesDGV.Columns[1].Name = "X";
            EntriesDGV.Columns[2].Name = "Y";
            EntriesDGV.Columns[3].Name = "Orientation";
            EntriesDGV.Columns[0].Width = 40;
            EntriesDGV.Columns[1].Width = 60;
            EntriesDGV.Columns[2].Width = 60;
            EntriesDGV.Columns[3].Width = 60;
            EntriesDGV.Rows.Add("1", "0", "0", "0");


            ToolTipsRectangle.Height = 40;
            ToolTipsRectangle.Width = 140;
            EntryLoader();
        }

        private void EntryLoader()
        {
            string[] InputData = File.ReadAllLines(SavedDataFileName);

            foreach(string sql in InputData)
            {
                int entry = 0; int id = 0; float angle = 0; float dist = 0;

                if (!ParserInsertSQL(sql, out entry, out id, out angle, out dist))
                {
                    Console.WriteLine("Error occured while trying to parse {0}", sql);
                    continue;
                }

            }
        }

        private bool ParserInsertSQL(string sql, out int entry, out int id, out float angle, out float dist)
        {
            entry = 0; id = 0; angle = 0; dist = 0;

            if (!sql.StartsWith("INSERT", StringComparison.OrdinalIgnoreCase) || !sql.EndsWith(";"))
            {

                return false;
            }

            string[] colName = { "entry", "id", "dist", "angle", "VALUES" };

            int startIdx = 0;
            for (int i = 0; i < colName.Count<string>(); i++)
            {
                int currIndex = sql.IndexOf(colName[i], startIdx, StringComparison.OrdinalIgnoreCase);
                if (currIndex > 0)
                    startIdx = currIndex;
                else
                    return false;
            }

            int parStartIdx = sql.IndexOf('(', startIdx);
            int parEndIndx = sql.IndexOf(')', parStartIdx);

            if (parStartIdx < 0 || parEndIndx < 0)
                return false;

            ++parStartIdx;
            string values = sql.Substring(parStartIdx, parEndIndx - parStartIdx);

            values = values.Replace("`", String.Empty);
            string[] splitedValues = Array.ConvertAll(values.Split(','), p => p.Trim());

            if (splitedValues.Count<string>() != 4)
                return false;

            if (!ValidateInt(splitedValues[0], out entry))
                return false;

            if (!ValidateInt(splitedValues[1], out id))
                return false;

            if (!ValidateFloat(splitedValues[2], out angle))
                return false;

            if (!ValidateFloat(splitedValues[3], out dist))
                return false;

            return true;
        }

        private void TextBox_Enter(object sender, EventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        private bool ValidateInputBox()
        {
            float xOri, yOri, zOri, oOri;
            float xDest, yDest, zDest, oDest;
            if (!ValidateFloat(xOriginTBX.Text, out xOri))
            {
                MessageBox.Show("xOrigin is not valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!ValidateFloat(yOriginTBX.Text, out yOri))
            {
                MessageBox.Show("yOrigin is not valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!ValidateFloat(zOriginTBX.Text, out zOri))
            {
                MessageBox.Show("zOrigin is not valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!ValidateFloat(oOriginTBX.Text, out oOri))
            {
                MessageBox.Show("oOrigin is not valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!ValidateFloat(xDestTBX.Text, out xDest))
            {
                MessageBox.Show("xDestination is not valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!ValidateFloat(yDestTBX.Text, out yDest))
            {
                MessageBox.Show("yDestination is not valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!ValidateFloat(zDestTBX.Text, out zDest))
            {
                MessageBox.Show("zDestination is not valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!ValidateFloat(oDestTBX.Text, out oDest))
            {
                MessageBox.Show("oDestination is not valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            float xOri, yOri, zOri, oOri;
            float xDest, yDest, zDest, oDest;
            if (!ValidateFloat(xOriginTBX.Text, out xOri))
            {
                MessageBox.Show("xOrigin is not valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!ValidateFloat(yOriginTBX.Text, out yOri))
            {
                MessageBox.Show("yOrigin is not valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!ValidateFloat(zOriginTBX.Text, out zOri))
            {
                MessageBox.Show("zOrigin is not valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!ValidateFloat(oOriginTBX.Text, out oOri))
            {
                MessageBox.Show("oOrigin is not valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!ValidateFloat(xDestTBX.Text, out xDest))
            {
                MessageBox.Show("xDestination is not valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!ValidateFloat(yDestTBX.Text, out yDest))
            {
                MessageBox.Show("yDestination is not valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!ValidateFloat(zDestTBX.Text, out zDest))
            {
                MessageBox.Show("zDestination is not valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!ValidateFloat(oDestTBX.Text, out oDest))
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
            double degreesToAdd = ((180 / Math.PI) * oOri);
            degrees += degreesToAdd;
            angleResultTBX.Text = degrees.ToString();
        }

        private void MainTimer_Tick(object sender, EventArgs e)
        {
        }

        private bool ValidateFloat(String valueStr, out float value)
        {
            value = -1;
            if (!Regex.IsMatch(valueStr, @"^[\-\+]?[0-9]*(?:[\.\,][0-9]*)?$"))
                return false;

            valueStr = valueStr.Replace(',', '.');

            if (!float.TryParse(valueStr, NumStyle, Culture, out value))
                return false;

            return true;
        }

        private bool ValidateInt(String valueStr, out int value)
        {
            value = -1;
            if (!Regex.IsMatch(valueStr, @"^[\-\+]?[0-9]*$"))
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

        private bool AddRowToMainDGV(string angleStr, string distStr)
        {
            float angle, dist;
            if (ValidateFloat(angleStr, out angle) && ValidateFloat(distStr, out dist))
            {
                int rowIndex = MainDataDGV.Rows.Add(MainDataDGV.Rows.Count.ToString(), angleResultTBX.Text, distResultTBX.Text);
                DataGridViewRow currRow = MainDataDGV.Rows[rowIndex];
                currRow.Tag = BaseID++;
                AddObject(currRow, angle, dist);
                MainDataDGV.Rows[MainDataDGV.Rows.Count- 1].Cells[0].Value = MainDataDGV.Rows.Count;

                foreach (DataGridViewCell cell in currRow.Cells)
                    cell.Style.BackColor = Color.Green;

                return true;
            }

            return false;
        }

        private void AddObject(DataGridViewRow currRow, float angle, float dist)
        {
            Slave slave = null;
            Object obj = null;

            int idx = (int)currRow.Tag;
            if (!ObjectsList.TryGetValue(idx, out obj))
                slave = new Slave(VirtualClientSize);
            else
                slave = obj as Slave;

            Vector2 orig = new Vector2(0, dist * ClientRatio);
            Vector2 rotPos = Rotate(orig, angle);
            Vector2 finalPoint = rotPos;

            int x, y;
            x = Convert.ToInt32(finalPoint.X);
            y = Convert.ToInt32(finalPoint.Y);

            slave.SetBaseGamePos(BaseGameX, BaseGameY, BaseGameO);
            slave.SetScreenPosition(new Point(x, y));


            slave.SetLinkedRow(currRow);
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
            int validCell = 0;

            DataGridViewRow currRow = MainDataDGV.CurrentRow;
            foreach (DataGridViewCell cell in currRow.Cells)
            {
                switch (cell.ColumnIndex)
                {
                    case 0:
                        if (cell.Value == null)
                            continue;
                        if (!ValidateInt(cell.Value.ToString(), out id))
                            cell.Style.BackColor = Color.IndianRed;
                        else
                        {
                            cell.Style.BackColor = Color.LawnGreen;
                            ++validCell;
                        }
                        break;

                    case 1:
                        if (cell.Value == null)
                            continue;
                        if (!ValidateFloat(cell.Value.ToString(), out angle))
                            cell.Style.BackColor = Color.IndianRed;
                        else
                        {
                            cell.Style.BackColor = Color.LawnGreen;
                            ++validCell;
                        }
                        break;

                    case 2:
                        if (cell.Value == null)
                            continue;
                        if (!ValidateFloat(cell.Value.ToString(), out dist))
                            cell.Style.BackColor = Color.IndianRed;
                        else
                        {
                            cell.Style.BackColor = Color.LawnGreen;
                            ++validCell;
                        }
                        break;

                    default:
                        break;
                }
            }

            if (validCell == 3)
            {
                foreach (DataGridViewCell cell in currRow.Cells)
                    cell.Style.BackColor = Color.Green;

                AddObject(currRow, angle, dist);
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

            if (tooltipsShow)
            {
                Rectangle idRect = ToolTipsRectangle;
                idRect.Y = idRect.Y + 12;
                Rectangle coordRect = ToolTipsRectangle;
                coordRect.Y = idRect.Y + 12;
                e.Graphics.FillRectangle(Brushes.DarkOliveGreen, ToolTipsRectangle);
                e.Graphics.DrawString(ToolTipsInfo.name, TTipsFont, Brushes.White, ToolTipsRectangle, StringFormat.GenericDefault);
                e.Graphics.DrawString(ToolTipsInfo.id, TTipsFont, Brushes.White, idRect, StringFormat.GenericDefault);
                e.Graphics.DrawString(ToolTipsInfo.gameCoord, TTipsFont, Brushes.White, coordRect, StringFormat.GenericDefault);
            }
        }

        private void MainDataDGV_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            RemoveObject((int)e.Row.Tag);

            int rowFound = 1;
            foreach(DataGridViewRow row in MainDataDGV.Rows)
            {
                if (row.Tag == e.Row.Tag)
                {
                    rowFound = 0;
                    continue;
                }

                row.Cells[0].Value = row.Index + rowFound;
            }
        }

        private void MainDataDGV_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells[0].Value = MainDataDGV.Rows.Count;
            e.Row.Tag = BaseID++;
        }

        private void MainDrawPB_MouseMove(object sender, MouseEventArgs e)
        {
            foreach (var item in ObjectsList)
            {
                Point screenLoc = new Point(e.X - Object.ClientXOffset, -(e.Y - Object.ClientYOffset));
                //Console.WriteLine("MousePos= " + screenLoc.ToString() + " also " + e.Location.ToString());
                if (item.Value.Contain(screenLoc))
                {
                    ToolTipsSelectedObj = item.Value;
                    ToolTipsInfo.name = item.Value.GetName();
                    ToolTipsInfo.gameCoord = item.Value.GetGameCoord();
                    if (item.Value.GetLinkedRow() != null)
                    {
                        ToolTipsInfo.id = item.Value.GetLinkedRow().Cells[0].Value.ToString();
                        item.Value.GetLinkedRow().Selected = true;
                    }
                    else
                        ToolTipsInfo.id = "Master_ID";
                    ToolTipsRectangle.X = e.X;
                    ToolTipsRectangle.Y = e.Y - ToolTipsRectangle.Height;
                    tooltipsShow = true;
                    MainDrawPB.Invalidate();
                    return;
                }
            }

            ToolTipsSelectedObj = null;
            tooltipsShow = false;
            MainDrawPB.Invalidate();
        }

        private void InGameDGV_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            float value = 0;

            DataGridViewRow currRow = InGameDGV.CurrentRow;
            DataGridViewCell currCell = currRow.Cells[e.ColumnIndex];

            switch (e.ColumnIndex)
            {
                case 0:
                    if (currCell.Value != null && ValidateFloat(currCell.Value.ToString(), out value))
                        BaseGameX = value;

                    currCell.Value = BaseGameX.ToString();
                    break;
                case 1:
                    if (currCell.Value != null && ValidateFloat(currCell.Value.ToString(), out value))
                        BaseGameY = value;

                    currCell.Value = BaseGameY.ToString();
                    break;
                case 2:
                    if (currCell.Value != null && ValidateFloat(currCell.Value.ToString(), out value))
                        BaseGameO = value;

                    currCell.Value = BaseGameO.ToString();
                    break;
                default:
                    break;
            }

            foreach (var item in ObjectsList)
            {
                item.Value.SetBaseGamePos(BaseGameX, BaseGameY, BaseGameO);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            float angle, dist;
            if (ValidateInputBox() && ValidateFloat(angleResultTBX.Text, out angle) && ValidateFloat(distResultTBX.Text, out dist))
            {
                float value = 0;
                if (ValidateFloat(xOriginTBX.Text, out value))
                    BaseGameX = value;
                InGameDGV.Rows[0].Cells[0].Value = BaseGameX.ToString();

                if (ValidateFloat(yOriginTBX.Text, out value))
                    BaseGameY = value;
                InGameDGV.Rows[0].Cells[1].Value = BaseGameY.ToString();

                if (ValidateFloat(oOriginTBX.Text, out value))
                    BaseGameO = value;
                InGameDGV.Rows[0].Cells[2].Value = BaseGameO.ToString();

                foreach (var item in ObjectsList)
                {
                    item.Value.SetBaseGamePos(BaseGameX, BaseGameY, BaseGameO);
                }

                AddRowToMainDGV(angleResultTBX.Text, distResultTBX.Text);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //INSERT INTO table_name (column1, column2, column3, ...) VALUES(value1, value2, value3, ...);
            const string format = "INSERT `creature_formation_template` (entry, id, dist, angle) VALUES(`{0}`, `{1}`, `{2}`, `{3}`);";
            List<string> sqlList = new List<string>();
            int entry = 0;
            if (!int.TryParse(EntryTBX.Text, out entry) || entry <= 0)
            {
                MessageBox.Show("Invalid formation entry!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (DataGridViewRow currRow in MainDataDGV.Rows)
            {
                int id = -1;
                float angle = -1;
                float dist = -1;
                int validCell = 0;

                foreach (DataGridViewCell cell in currRow.Cells)
                {
                    switch (cell.ColumnIndex)
                    {
                        case 0:
                            if (cell.Value == null)
                                continue;
                            if (!ValidateInt(cell.Value.ToString(), out id))
                                cell.Style.BackColor = Color.IndianRed;
                            else
                                ++validCell;
                            break;

                        case 1:
                            if (cell.Value == null)
                                continue;
                            if (!ValidateFloat(cell.Value.ToString(), out angle))
                                cell.Style.BackColor = Color.IndianRed;
                            else
                                ++validCell;
                            break;

                        case 2:
                            if (cell.Value == null)
                                continue;
                            if (!ValidateFloat(cell.Value.ToString(), out dist))
                                cell.Style.BackColor = Color.IndianRed;
                            else
                                ++validCell;
                            break;

                        default:
                            break;
                    }
                }

                if (validCell == 3)
                {
                    string sql = string.Format(format, entry.ToString(), id.ToString(), angle.ToString(), dist.ToString());
                    sqlList.Add(sql);
                }
            }

            if (sqlList.Count <= 0)
                return;

            File.WriteAllLines(SavedDataFileName, sqlList);

        }

        private void EntryTBX_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void EntryTBX_TextChanged(object sender, EventArgs e)
        {
            int value = 0;
            TextBox tb = sender as TextBox;
            if (!ValidateInt(tb.Text, out value) || value <= 0)
                tb.BackColor = Color.IndianRed;
            else
            {
                tb.BackColor = Color.Green;
                tb.Text = value.ToString();
            }
        }
    }
}
