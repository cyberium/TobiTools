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
        FormationDataEntry CurrentSelectedEntry = null;

        FormationData FormationDataMgr = new FormationData();

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
            FormationDataMgr.Save();
        }

        private void Initialize()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            MaximizeBox = false;
            VirtualClientSize.Width = MainDrawPB.ClientSize.Width * ClientRatio;
            VirtualClientSize.Height = MainDrawPB.ClientSize.Height * ClientRatio;

            MainDrawPB.Image = new Bitmap(MainDrawPB.ClientSize.Width, MainDrawPB.ClientSize.Height);
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

            EntriesDGV.ColumnCount = 4;
            EntriesDGV.Columns[0].Name = "Entry";
            EntriesDGV.Columns[1].Name = "X";
            EntriesDGV.Columns[2].Name = "Y";
            EntriesDGV.Columns[3].Name = "Orientation";
            EntriesDGV.Columns[0].Width = 40;
            EntriesDGV.Columns[1].Width = 60;
            EntriesDGV.Columns[2].Width = 60;
            EntriesDGV.Columns[3].Width = 60;


            ToolTipsRectangle.Height = 40;
            ToolTipsRectangle.Width = 140;

            // check if have at least one entry
            if (FormationDataMgr.Count() == 0)
                FormationDataMgr.AddEntry(1);

            // Fill the DataGridView
            foreach (FormationDataEntry dataEntry in FormationDataMgr.GetEntries())
            {
                DataGridViewRow newRow = (DataGridViewRow)EntriesDGV.Rows[0].Clone();
                newRow.Cells[0].Value = dataEntry.Entry.ToString();
                newRow.Cells[1].Value = dataEntry.MasterX.ToString();
                newRow.Cells[2].Value = dataEntry.MasterY.ToString();
                newRow.Cells[3].Value = dataEntry.MasterO.ToString();
                newRow.Tag = dataEntry.Entry;
                EntriesDGV.Rows.Add(newRow);
            }

            ShowEntry(FormationDataMgr.GetEntries()[0].Entry);

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

        private bool AddRowToMainDGV(string angleStr, string distStr, float oriX = 0, float oriY = 0, float oriO = 0, int entry = 0)
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


                DataGridViewRow entryRow = null;
                if (entry != 0)
                {
                    string entryStr = entry.ToString();
                    foreach(DataGridViewRow row in EntriesDGV.Rows)
                    {
                        if (row.Cells[0].Value.ToString() == entryStr)
                            entryRow = row;
                    }
                }

                /*if (entryRow == null)
                    entryRow = */

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

            slave.SetBaseGamePos(CurrentSelectedEntry.MasterX, CurrentSelectedEntry.MasterY, CurrentSelectedEntry.MasterO);
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

        private void ClearObjects()
        {
            ObjectsList.Clear();
            ObjectsList.Add(0, new Master(VirtualClientSize));

            MainDataDGV.Rows.Clear();

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
                CurrentSelectedEntry.AddSlave(angle, dist);
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

        private void button3_Click(object sender, EventArgs e)
        {
            float angle, dist;
            /*if (ValidateInputBox() && ValidateFloat(angleResultTBX.Text, out angle) && ValidateFloat(distResultTBX.Text, out dist))
            {
                float value = 0;
                if (ValidateFloat(xOriginTBX.Text, out value))
                    BaseGameX = value;

                if (ValidateFloat(yOriginTBX.Text, out value))
                    BaseGameY = value;

                if (ValidateFloat(oOriginTBX.Text, out value))
                    BaseGameO = value;

                foreach (var item in ObjectsList)
                {
                    item.Value.SetBaseGamePos(BaseGameX, BaseGameY, BaseGameO);
                }

                AddRowToMainDGV(angleResultTBX.Text, distResultTBX.Text, BaseGameX, BaseGameY, BaseGameO);
            }*/
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormationDataMgr.Save();
            return;
            //INSERT INTO table_name (column1, column2, column3, ...) VALUES(value1, value2, value3, ...);
            const string format = "INSERT `creature_formation_template` (entry, id, dist, angle) VALUES(`{0}`, `{1}`, `{2}`, `{3}`);";
            List<string> sqlList = new List<string>();
            int entry = 0;
            /*if (!int.TryParse(EntryTBX.Text, out entry) || entry <= 0)
            {
                MessageBox.Show("Invalid formation entry!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }*/

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

        private void EntriesDGV_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            float value = 0;
            int intValue = 0;
            int newWantedEntry = 0;

            DataGridViewRow currRow = EntriesDGV.Rows[e.RowIndex];
            DataGridViewCell currCell = currRow.Cells[e.ColumnIndex];

            switch (e.ColumnIndex)
            {
                case 0:
                    if (currCell.Value != null && ValidateInt(currCell.Value.ToString(), out intValue))
                        newWantedEntry = intValue;
                    else
                    {
                        MessageBox.Show("Invalid entry", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        currCell.Value = currRow.Tag.ToString();
                        return;
                    }

                    break;
                case 1:
                    if (currCell.Value != null && ValidateFloat(currCell.Value.ToString(), out value))
                        CurrentSelectedEntry.MasterX = value;

                    currCell.Value = CurrentSelectedEntry.MasterX.ToString();
                    break;
                case 2:
                    if (currCell.Value != null && ValidateFloat(currCell.Value.ToString(), out value))
                        CurrentSelectedEntry.MasterY = value;

                    currCell.Value = CurrentSelectedEntry.MasterY.ToString();
                    break;
                case 3:
                    if (currCell.Value != null && ValidateFloat(currCell.Value.ToString(), out value))
                        CurrentSelectedEntry.MasterO = value;

                    currCell.Value = CurrentSelectedEntry.MasterO.ToString();
                    break;
                default:
                    break;
            }

            if (currRow.Tag == null)
            {
                // New line!
                if (newWantedEntry == 0 || !FormationDataMgr.IsNewEntryIDValid(newWantedEntry))
                    newWantedEntry = FormationDataMgr.GetAvailableEntryID();
                FormationDataMgr.CreateEntryIfNeed(newWantedEntry);
                currRow.Tag = newWantedEntry;
                currRow.Cells[0].Value = newWantedEntry.ToString();

                for (int i = 1; i < currRow.Cells.Count; ++i)
                {
                    float tempVal = 0;
                    if (currRow.Cells[i].Value == null || !ValidateFloat(currRow.Cells[i].Value.ToString(), out tempVal))
                        currRow.Cells[i].Value = 0;
                }
            }
            else
            {
                // Existing row
                int realEntry = (int)currRow.Tag;
                FormationDataEntry dataEntry = FormationDataMgr.GetEntry(realEntry);
                if (newWantedEntry != 0 && newWantedEntry != realEntry)
                {
                    if (!FormationDataMgr.SetEntry(realEntry, newWantedEntry))
                    {
                        MessageBox.Show("Entry already exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        currCell.Value = currRow.Tag.ToString();
                    }
                    return;
                }
            }

            if (newWantedEntry == CurrentSelectedEntry.Entry)
            {
                foreach (var item in ObjectsList)
                {
                    item.Value.SetBaseGamePos(CurrentSelectedEntry.MasterX, CurrentSelectedEntry.MasterY, CurrentSelectedEntry.MasterO);
                }
            }
        }

        public void ShowEntry(int entry)
        {
            ClearObjects();

            FormationDataEntry currEntry = FormationDataMgr.GetEntry(entry);
            if (currEntry == null)
                return;

            CurrentSelectedEntry = currEntry;

            List<SlaveDataEntry> slaves = currEntry.slaveEntries;
            if (slaves != null)
            {
                foreach (SlaveDataEntry slave in slaves)
                {
                    int rowIndex = MainDataDGV.Rows.Add(slave.ID.ToString(), slave.Angle.ToString(), slave.Distance.ToString());
                    DataGridViewRow currRow = MainDataDGV.Rows[rowIndex];
                    currRow.Tag = BaseID++;
                    AddObject(currRow, slave.Angle, slave.Distance);

                    foreach (DataGridViewCell cell in currRow.Cells)
                        cell.Style.BackColor = Color.Green;
                }
            }
            MainDataDGV.Rows[MainDataDGV.Rows.Count - 1].Cells[0].Value = slaves.Count + 1;
            MainDataDGV.Rows[MainDataDGV.Rows.Count - 1].Cells[0].Style.BackColor = Color.LawnGreen;
            MainDataDGV.Rows[MainDataDGV.Rows.Count - 1].Tag = BaseID++;
        }

        private void EntriesDGV_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.StateChanged != DataGridViewElementStates.Selected)
                return;

            if (EntriesDGV.CurrentRow == null || e.Row.Index == EntriesDGV.CurrentRow.Index)
                return;

            if (e.Row.Tag == null)
                return;

            int entry = 0;
            if (!ValidateInt(e.Row.Tag.ToString(), out entry))
                return;

            if (entry == CurrentSelectedEntry.Entry)
                return;

            ShowEntry(entry);
        }
    }
}
