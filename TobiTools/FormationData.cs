using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.IO;

namespace TobiTools
{
    public class SlaveDataEntry
    {
        public int ID;
        public float Distance;
        public float Angle;

        public SlaveDataEntry(int id, float angle, float distance)
        {
            ID = id;
            Distance = distance;
            Angle = angle;
        }
        public SlaveDataEntry()
        {
            ID = -1;
            Distance = 0;
            Angle = 0;
        }

        public SlaveDataEntry(SlaveDataEntry sEntry)
        {
            ID = sEntry.ID;
            Angle = sEntry.Angle;
            Distance = sEntry.Distance;
        }
    }

    public class FormationDataEntry
    {
        public int Entry;
        public float MasterX;
        public float MasterY;
        public float MasterO;

        public List<SlaveDataEntry> slaveEntries;

        private int GeneratedIDs;

        public FormationDataEntry(int entry, float masterX, float masterY, float masterO)
        {
            Entry = entry;
            MasterX = masterX;
            MasterY = masterY;
            MasterO = masterO;
            slaveEntries = new List<SlaveDataEntry>();

            GeneratedIDs = 1;
        }
        public FormationDataEntry(FormationDataEntry dataEntry)
        {
            Entry = dataEntry.Entry;
            MasterX = dataEntry.MasterX;
            MasterY = dataEntry.MasterY;
            MasterO = dataEntry.MasterO;
            slaveEntries = new List<SlaveDataEntry>();

            foreach(SlaveDataEntry sEntry in dataEntry.slaveEntries)
            {
                slaveEntries.Add(new SlaveDataEntry(sEntry));
            }

            GeneratedIDs = dataEntry.GeneratedIDs;
        }

        public FormationDataEntry()
        {
            Entry = -1;
            MasterX = 0;
            MasterY = 0;
            MasterO = 0;
            slaveEntries = new List<SlaveDataEntry>();

            GeneratedIDs = 1;
        }

        public SlaveDataEntry AddSlave(float angle, float distance, int id = -1)
        {
            if (id >= 0)
            {
                for (int i = 0; i < slaveEntries.Count; ++i)
                {
                    SlaveDataEntry row = slaveEntries[i];
                    if (row.ID == id)
                    {
                        row.Angle = angle;
                        row.Distance = distance;
                        return row;
                    }
                }

                if (GeneratedIDs <= id)
                    GeneratedIDs = id + 1;
            }
            else
                id = GeneratedIDs++;

            SlaveDataEntry newSlave = new SlaveDataEntry(id, angle, distance);
            slaveEntries.Add(newSlave);
            return newSlave;
        }

        public void RemoveSlave(int id, bool fixIDs = true)
        {
            foreach (SlaveDataEntry row in slaveEntries)
            {
                if (row.ID == id)
                {
                    slaveEntries.Remove(row);
                    break;
                }
            }

            if (!fixIDs)
                return;

            // fix ids orders
            GeneratedIDs = 1;
            for(int i = 0; i < slaveEntries.Count; ++i)
            {
                slaveEntries[i].ID = GeneratedIDs++;
            }
        }

        public void EditSlave(int id, float angle, float distance)
        {
            for (int i = 0; i < slaveEntries.Count; ++i)
            {
                SlaveDataEntry row = slaveEntries[i];
                if (row.ID == id)
                {
                    row.Angle = angle;
                    row.Distance = distance;
                    return;
                }
            }
        }

        public SlaveDataEntry GetSlave(int id)
        {
            foreach (SlaveDataEntry row in slaveEntries)
            {
                if (row.ID == id)
                    return row;
            }
            return null;
        }
    }

    class FormationData
    {
        private int AvailableEntryID;
        private JavaScriptSerializer JSONSerializer = new JavaScriptSerializer();
        public FormationData()
        {
            dataList = new List<FormationDataEntry>();
            AvailableEntryID = 0;
            Load();
        }
        private List<FormationDataEntry> dataList;

        public FormationDataEntry AddEntry(int entry, float masterX = 0, float masterY = 0, float masterO = 0)
        {
            foreach (FormationDataEntry row in dataList)
            {
                if (row.Entry == entry)
                {
                    EditEntry(entry, masterX, masterY, masterO);
                    return row;
                }
            }

            dataList.Add(new FormationDataEntry(entry, masterX, masterY, masterO));

            if (entry >= AvailableEntryID)
                AvailableEntryID = entry + 1;

            return dataList[dataList.Count - 1];
        }

        public void RemoveEntry(int entry)
        {
            foreach(FormationDataEntry row in dataList)
            {
                if (row.Entry == entry)
                {
                    dataList.Remove(row);
                    break;
                }
            }
        }

        public void EditEntry(int entry, float masterX = 0, float masterY = 0, float masterO = 0)
        {
            foreach (FormationDataEntry row in dataList)
            {
                if (row.Entry == entry)
                {
                    row.MasterX = masterX;
                    row.MasterY = masterY;
                    row.MasterO = masterO;
                    return;
                }
            }
        }

        public void EditEntry(FormationDataEntry dataEntry)
        {
            for (int i = 0; i < dataList.Count; ++i)
            {
                if (dataList[i].Entry == dataEntry.Entry)
                {
                    dataList[i] = dataEntry;
                    return;
                }
            }
        }

        public FormationDataEntry GetEntry(int entry)
        {
            foreach (FormationDataEntry row in dataList)
            {
                if (row.Entry == entry)
                    return row;
            }
            return null;
        }

        public List<FormationDataEntry> GetEntries()
        {
            return dataList;
        }

        public bool SetEntry(int oldEntry, int newEntry)
        {
            if (newEntry <= 0)
                return false;
            FormationDataEntry currEntry = null;
            foreach (FormationDataEntry dataEntry in dataList)
            {
                if (dataEntry.Entry == newEntry)
                    return false;

                if (dataEntry.Entry == oldEntry)
                    currEntry = dataEntry;
            }

            if (currEntry == null)
                return false;

            currEntry.Entry = newEntry;
            if (AvailableEntryID >= newEntry)
                AvailableEntryID = newEntry + 1;

            return true;
        }

        public int Count() { return dataList.Count; }

        public void AddSlave(int entry, float angle, float distance, int id = 0)
        {
            foreach (FormationDataEntry row in dataList)
            {
                if (row.Entry == entry)
                {
                    row.AddSlave(angle, distance, id);
                    return;
                }
            }

            // no entry found create new one
            FormationDataEntry slaveEntry = AddEntry(entry);
            slaveEntry.AddSlave(angle, distance);
        }

        public void RemoveSlave(int entry, int slaveId)
        {
            foreach (FormationDataEntry row in dataList)
            {
                if (row.Entry == entry)
                {
                    row.RemoveSlave(slaveId);
                    break;
                }
            }
        }

        public void EditSlave(int entry, int slaveId, float angle, float distance)
        {
            foreach (FormationDataEntry row in dataList)
            {
                if (row.Entry == entry)
                {
                    row.EditSlave(slaveId, angle, distance);
                    break;
                }
            }
        }

        public List<SlaveDataEntry> GetSlaves(int entry)
        {
            foreach (FormationDataEntry row in dataList)
            {
                if (row.Entry == entry)
                {
                    return row.slaveEntries;
                }
            }
            return null;
        }

        public SlaveDataEntry GetSlave(int entry, int slaveId)
        {
            foreach (FormationDataEntry row in dataList)
            {
                if (row.Entry == entry)
                    return row.GetSlave(slaveId);
            }
            return null;
        }

        public int GetAvailableEntryID()
        {
            return AvailableEntryID;
        }

        public bool IsNewEntryIDValid(int entry)
        {
            foreach (FormationDataEntry dataEntry in dataList)
            {
                if (dataEntry.Entry == entry)
                    return false;
            }
            return true;
        }

        public void Save(string fileName = "Datas.json")
        {
            var json = JSONSerializer.Serialize(dataList);
            File.WriteAllText(fileName, json);
        }

        public void Load(string fileName = "Datas.json")
        {
            if (!File.Exists(fileName))
                return;

            string json = File.ReadAllText(fileName);
            List<FormationDataEntry> newList = JSONSerializer.Deserialize<List<FormationDataEntry>>(json);

            // Check the values and add them to data list
            List<int> usedEntries = new List<int>();
            for (int i = 0; i < newList.Count; ++i)
            {
                FormationDataEntry formationEntry = newList[i];

                if (usedEntries.BinarySearch(formationEntry.Entry) > 0)
                    continue;

                usedEntries.Add(formationEntry.Entry);
                FormationDataEntry newEntry = new FormationDataEntry(formationEntry.Entry, formationEntry.MasterX, formationEntry.MasterY, formationEntry.MasterO);
                dataList.Add(newEntry);

                if (formationEntry.Entry >= AvailableEntryID)
                    AvailableEntryID = formationEntry.Entry + 1;

                List<int> usedIds = new List<int>();
                for (int j = 0; j < formationEntry.slaveEntries.Count; ++j)
                {
                    SlaveDataEntry slaveEntry = formationEntry.slaveEntries[j];

                    if (usedIds.BinarySearch(slaveEntry.ID) > 0)
                        continue;

                    usedIds.Add(slaveEntry.ID);

                    newEntry.AddSlave(slaveEntry.Angle, slaveEntry.Distance, slaveEntry.ID);
                }
            }
        }

        public void CreateEntryIfNeed(int entry)
        {
            foreach (FormationDataEntry row in dataList)
            {
                if (row.Entry == entry)
                    return;
            }

            dataList.Add(new FormationDataEntry(entry, 0, 0, 0));
            if (entry >= AvailableEntryID)
                AvailableEntryID = entry + 1;
        }

    }
}
