using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace CataclysmModder
{
    /// <summary>
    /// Wraps data loaded from one JSON item for display in a listbox.
    /// </summary>
    class ItemDataWrapper : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Reference to the openfiles array.
        /// </summary>
        public int memberOf;

        private CataFile MemberOf
        {
            get { return Storage.GetFileDefForOpenFile(memberOf); }
        }

        public void NotifyKeyChanged(string key)
        {
            Modified = true;

            if (key.Equals(MemberOf.displayMember) || key.Equals(MemberOf.displaySuffix))
                PropertyChanged(this, new PropertyChangedEventArgs("Display"));
        }

        /// <summary>
        /// Get the list of keys from this item.
        /// </summary>
        public Dictionary<string, object> data
        {
            get;
            private set;
        }

        /// <summary>
        /// Get the listbox display name of this item
        /// </summary>
        public string Display
        {
            get
            {
                string suffix = "";
                if (data.ContainsKey(MemberOf.displaySuffix))
                    suffix = (string)data[MemberOf.displaySuffix];

                if (data.ContainsKey(MemberOf.displayMember))
                    return (string)data[MemberOf.displayMember] + suffix;
                else
                    return "[item]";
            }
        }

        /// <summary>
        /// Tracks whether this item has unsaved changes.
        /// </summary>
        public bool Modified = false;


        /// <summary>
        /// Create a new item with data copied from the specified item.
        /// </summary>
        /// <param name="copy"></param>
        public ItemDataWrapper(ItemDataWrapper copy)
        {
            this.memberOf = copy.memberOf;

            data = new Dictionary<string, object>(copy.data);
            int lastitem = 0;
            foreach (ItemDataWrapper i in Storage.OpenItems)
            {
                if (i.Display.StartsWith(copy.Display))
                {
                    try
                    {
                        lastitem = Math.Max(lastitem, Int32.Parse(i.Display.Substring(copy.Display.Length)));
                    }
                    catch (FormatException)
                    {

                    }
                }
            }

            data[MemberOf.displayMember] = copy.Display + (lastitem + 1);
        }

        /// <summary>
        /// Create a new item from loaded data.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="memberOf"></param>
        public ItemDataWrapper(Dictionary<string, object> data, int memberOf)
        {
            this.memberOf = memberOf;
            this.data = data;
        }

        /// <summary>
        /// Create a new, empty item.
        /// </summary>
        /// <param name="memberOf"></param>
        public ItemDataWrapper(int memberOf)
        {
            this.memberOf = memberOf;

            data = new Dictionary<string, object>();
            int lastitem = 0;
            foreach (ItemDataWrapper i in Storage.OpenItems)
            {
                if (i.Display.StartsWith("newitem"))
                {
                    try
                    {
                        lastitem = Math.Max(lastitem, Int32.Parse(i.Display.Substring(7)));
                    }
                    catch (FormatException)
                    {

                    }
                }
            }
            data[MemberOf.displayMember] = "newitem" + (lastitem + 1);
        }
    }


    /// <summary>
    /// This class holds the backing data for all loaded items and handles reading and writing JSON.
    /// </summary>
    static class Storage
    {
        private static bool unsavedChanges = false;
        public static bool UnsavedChanges { get { return unsavedChanges; } }
        
        private static string workspacePath = "";
        public static string WorkingDirectory { get { return workspacePath; } }

        private static int currentFileIndex = -1;
        public static string CurrentFileName
        {
            get
            {
                if (currentFileIndex >= 0)
                    return openFiles[currentFileIndex];
                else
                    return string.Empty;
            }
        }
        public static int CurrentFileIndex
        {
            get
            {
                return currentFileIndex;
            }
        }

        private static int currentItemIndex = -1;
        public static int CurrentItemIndex { get { return currentItemIndex; } }
        public static Dictionary<string, object> CurrentItemData
        {
            get
            {
                if (currentItemIndex >= 0)
                    return openItems[currentFileIndex][currentItemIndex].data;
                else
                    return null;
            }
        }

        public static CataFile GetFileDefForCurrentFile()
        {
            return fileDef[(int)GetFileTypeForCurrentFile()];
        }

        public static CataFile GetFileDefForOpenFile(int index)
        {
            return fileDef[(int)GetFileTypeForOpenFile(index)];
        }

        public static FileType GetFileTypeForCurrentFile()
        {
            return GetFileTypeForOpenFile(currentFileIndex);
        }

        public static FileType GetFileTypeForOpenFile(int index)
        {
            if (index < 0 || index >= openFiles.Length)
                return FileType.NONE;
            else
                return GetFileType(openFiles[index]);
        }

        public static FileType GetFileType(string name)
        {
            string filename = Path.GetFileName(name);
            string[] filedirs = name.Split(Path.DirectorySeparatorChar);
            if (filename.Equals("ship.json"))
                return FileType.SHIPS;
            else if (filename.Equals("weapon.json"))
                return FileType.WEAPONS;
            else if (filename.Equals("element.json"))
                return FileType.MAPELEMENTS;
            else if (filedirs[0].Equals("map"))
                return FileType.MAP;
            else if (filename.Equals("projectile.json"))
                return FileType.PROJECTILES;
            else if (filename.Equals("definition.json"))
                return FileType.DEFINITION;
            else
                return FileType.NONE;
        }

        /// <summary>
        /// List of files found in the directory.
        /// </summary>
        public static string[] OpenFiles { get { return openFiles; } }
        private static string[] openFiles;

        /// <summary>
        /// Items in currently loaded files, slightly parsed
        /// </summary>
        public static BindingList<ItemDataWrapper> OpenItems
        {
            get
            {
                if (currentFileIndex >= 0)
                    return openItems[currentFileIndex];
                else
                    return null;
            }
        }
        public static List<BindingList<ItemDataWrapper>> openItems = new List<BindingList<ItemDataWrapper>>();

        public static bool FilesLoaded { get { return !string.IsNullOrEmpty(workspacePath); } }
        public static bool ItemsLoaded { get { return currentFileIndex >= 0; } }

        public static object[] CraftCategories = new object[0];
        private static List<ItemDataWrapper> RecipeUnknown = new List<ItemDataWrapper>();

        //public static AutoCompleteStringCollection AutocompleteItemSource = new AutoCompleteStringCollection();


        private static List<string>[] PresetDataSources = new List<string>[(int)JsonFormTag.DataSourceType.PRESET_COUNT];


        public enum FileType
        {
            SHIPS,
            WEAPONS,
            PROJECTILES,
            MAPELEMENTS,
            MAP,
            DEFINITION,
            NONE,

            COUNT
        }

        /// <summary>
        /// Contains information about the structure of different files.
        /// </summary>
        private static CataFile[] fileDef = new CataFile[(int)FileType.COUNT];

        //HACK: placement
        public static void HideAllControls()
        {
            foreach (CataFile f in fileDef)
            {
                if (f != null && f.control != null)
                    f.control.Visible = false;
            }
        }


        /// <summary>
        /// Initialize structures that describe how each different type of file should be read.
        /// </summary>
        public static void InitializeFileDefs()
        {
            InitializeDataSources();

            //Editing control needs to be set in Form1 ctor using FileDefSetControl

            fileDef[(int)FileType.SHIPS] = new CataFile("id", new JsonSchema("CataclysmModder.schemas.ship.txt"));
            fileDef[(int)FileType.WEAPONS] = new CataFile("id", new JsonSchema("CataclysmModder.schemas.weapon.txt"));
            fileDef[(int)FileType.PROJECTILES] = new CataFile("id", new JsonSchema("CataclysmModder.schemas.projectile.txt"));
            fileDef[(int)FileType.MAPELEMENTS] = new CataFile("id", new JsonSchema("CataclysmModder.schemas.element.txt"));
            fileDef[(int)FileType.MAP] = new CataFile();
            fileDef[(int)FileType.DEFINITION] = new CataFile();
        }

        public static void FileDefSetControl(FileType type, Control control)
        {
            fileDef[(int)type].control = control;
        }

        public static void FileChanged()
        {
            unsavedChanges = true;
        }

        private static void AutocompleteNeedsModified(object sender, ListChangedEventArgs e)
        {
            switch (e.ListChangedType)
            {
                case ListChangedType.PropertyDescriptorAdded:
                case ListChangedType.PropertyDescriptorChanged:
                case ListChangedType.PropertyDescriptorDeleted:
                case ListChangedType.Reset:

                //These might possibly be handled more efficiently
                //case ListChangedType.ItemChanged:
                case ListChangedType.ItemDeleted:
                    //Rebuild autocomplete list
                    /*ItemDataWrapper added;
                    AutocompleteItemSource.Clear();
                    for (int c = 0; c < openItems.Count; c++)
                    {
                        if (GetFileTypeForOpenFile(c) == FileType.ITEMS
                            || GetFileTypeForOpenFile(c) == FileType.BIONICS)
                        {
                            for (int d = 0; d < openItems[c].Count; d++)
                            {
                                added = openItems[c][d];
                                AutocompleteItemSource.Add(added.Display);
                            }
                        }
                    }*/
                    break;

                case ListChangedType.ItemAdded:
                    //Update autocomplete list
                    /*added = ((BindingList<ItemDataWrapper>)sender)[e.NewIndex];
                    AutocompleteItemSource.Add(added.Display);*/
                    break;
            }
        }

        /// <summary>
        /// Load game files from the specified path.
        /// </summary>
        public static void LoadFiles(string path)
        {
            workspacePath = path;

            //Clear
            foreach (BindingList<ItemDataWrapper> list in openItems)
                list.ListChanged -= AutocompleteNeedsModified;
            openItems.Clear();

            //Load all JSON files in directory and subs
            try
            {
                openFiles = Directory.GetFiles(path, "*.json", SearchOption.AllDirectories);
                for (int c = 0; c < openFiles.Length; c++)
                {
                    openFiles[c] = openFiles[c].Substring(workspacePath.Length + 1);
                    LoadFile(c);
                }
            }
            catch (DirectoryNotFoundException)
            {
                openFiles = new string[0];
            }
        }

        public static void ReloadFiles()
        {
            LoadFiles(workspacePath);
        }

        /// <summary>
        /// Load up items from a specified game file.
        /// </summary>
        public static void LoadFile(int index)
        {
            BindingList<ItemDataWrapper> newItems = new BindingList<ItemDataWrapper>();
            openItems.Add(newItems);

            //Load up contents of the file
            string path = openFiles[index];
            object json = LoadJson(path);
            if (json == null)
                return;

            FileType ftype = GetFileType(path);
            if (fileDef[(int)ftype] == null)
            {
                //Not supported

            }
            else if (ftype == FileType.MAP)
            {

            }
            else if (ftype == FileType.DEFINITION)
            {

            }
            else if (ftype != FileType.NONE)
            {
                //Default parsing
                foreach (Dictionary<string, object> item in (object[])json)
                    newItems.Add(new ItemDataWrapper(item, index));
            }

            //Subscribe to events
            /*if (GetFileTypeForOpenFile(index) == FileType.ITEMS
                || GetFileTypeForOpenFile(index) == FileType.BIONICS)
                newItems.ListChanged += AutocompleteNeedsModified;*/

            //Rebuild autocomplete
            AutocompleteNeedsModified(newItems, new ListChangedEventArgs(ListChangedType.Reset, 0));
        }

        public static void SelectFile(int file)
        {
            currentFileIndex = file;
        }

        public static object LoadJson(string file)
        {
            StreamReader reader = new StreamReader(new FileStream(Path.Combine(workspacePath, file), FileMode.Open));
            string json = reader.ReadToEnd();
            try
            {
                return new JavaScriptSerializer().DeserializeObject(json);
            }
            catch (ArgumentException e)
            {
                MessageBox.Show("Failed to parse JSON from file '" + file + "': " + e.Message,
                    "Argument Exception", MessageBoxButtons.OK);
                return null;
            }
            catch (InvalidOperationException e)
            {
                MessageBox.Show("Failed to parse JSON from file '" + file + "': " + e.Message,
                    "Invalid Operation Exception", MessageBoxButtons.OK);
                return null;
            }
            finally
            {
                reader.Close();
            }
        }

        public static void SaveJsonItem(StreamWriter write, object obj, JsonSchema schema, string pivotKey, int bracketBlockLevel = 1)
        {
            try
            {
                StringBuilder sb = new StringBuilder("[");
                foreach (Dictionary<string, object> item in (object[])obj)
                {
                    string type = (item.ContainsKey(pivotKey) ? (string)item[pivotKey] : "");
                    sb.Append(schema.Serialize(item, type));
                    sb.Append(",");
                }
                //Remove last comma
                sb.Remove(sb.Length - 1, 1);

                sb.Append("]");

                if (Options.DontFormatJson)
                    write.Write(sb.ToString());
                else
                    write.Write(SpaceJson(sb.ToString(), bracketBlockLevel));
            }
            catch (ArgumentException)
            {
                //TODO: error message
                return;
            }
        }

        /*public static void SaveJson(string file, object obj)
        {
            StreamWriter write = new StreamWriter(new FileStream(Path.Combine(workspacePath, file), FileMode.Create));
            try
            {
                string json = new JavaScriptSerializer().Serialize(obj);
                if (Options.DontFormatJson)
                    write.Write(json);
                else
                    write.Write(SpaceJson(json));
            }
            catch (ArgumentException)
            {
                //TODO: error message
            }
            finally
            {
                write.Close();
            }
        }*/
        
        public static string SpaceJson(string json, int bracketBlockLevel = 1)
        {
            StringBuilder newjson = new StringBuilder();
            string nlindent = "\n";

            string indent = "";
            if (Options.IndentWithTabs)
                indent = "\t";
            else
            {
                for (int c = 0; c < Options.IndentSpaces; c++)
                    indent += " ";
            }

            bool quoteOpen = false;
            bool escape = false;
            int squareBrackets = 0; //HACK: slightly
            for (int c = 0; c < json.Length; c++)
            {
                if (!escape && json[c] == '"') quoteOpen = !quoteOpen;
                if (!escape && json[c] == '\\') escape = true;
                else escape = false;

                if (!quoteOpen && (json[c] == ']' || json[c] == '}'))
                {
                    nlindent = nlindent.Substring(0, nlindent.Length-indent.Length);
                    if (squareBrackets <= bracketBlockLevel) newjson.Append(nlindent);
                    newjson.Append(json[c]);
                    if (json[c] == ']') squareBrackets--;
                }
                else if (!quoteOpen && (json[c] == '[' || json[c] == '{'))
                {
                    if (json[c] == '[') squareBrackets++;
                    nlindent += indent;
                    newjson.Append(json[c]);
                    if (squareBrackets <= bracketBlockLevel) newjson.Append(nlindent);
                }
                else if (!quoteOpen && json[c] == ',')
                {
                    newjson.Append(json[c]);
                    if (squareBrackets <= bracketBlockLevel) newjson.Append(nlindent);
                }
                else
                {
                    newjson.Append(json[c]);
                }
            }
            return newjson.ToString();
        }

        public static void TestAllItems()
        {
            for (int c = 0; c < openFiles.Length; c++)
            {
                CataFile fileDef = GetFileDefForOpenFile(c);
                if (fileDef != null && fileDef.control != null)
                {
                    for (int d = 0; d < openItems[c].Count; d++)
                        WinformsUtil.ControlsLoadItem(fileDef.control, openItems[c][d].data);
                }
            }
            MessageBox.Show("Done");
        }

        /// <summary>
        /// Load up one item from current file's JSON.
        /// </summary>
        /// <param name="id"></param>
        public static void LoadItem(int index)
        {
            if (index < 0) return;

            currentItemIndex = index;

            CataFile fileDef = GetFileDefForOpenFile(currentFileIndex);
            if (fileDef != null && fileDef.control != null)
                WinformsUtil.ControlsLoadItem(fileDef.control, CurrentItemData);
        }

        public static void SaveOpenFiles()
        {
            foreach (string file in openFiles)
            {
                if (fileDef[(int)GetFileType(file)].schema != null)
                    SaveFile(file, false);
            }
            unsavedChanges = false;
            MessageBox.Show("All files saved.", "Saving", MessageBoxButtons.OK);
        }

        public static void ExportFile(string file, int[] indices)
        {
            FileType ftype = GetFileTypeForCurrentFile();

            object[] serialData = new object[indices.Length];
            int c = 0;
            foreach (int i in indices)
            {
                serialData[c] = OpenItems[i].data;
                c++;
            }

            using (StreamWriter write = new StreamWriter(GetFilestreamForFile(file)))
            {
                if (!Serialize(serialData, write, ftype))
                {
                    MessageBox.Show("Serializing this file is not supported.", "Error", MessageBoxButtons.OK);
                    return;
                }
            }
        }

        private static FileStream GetFilestreamForFile(string file)
        {
            return new FileStream(Path.Combine(workspacePath, file), FileMode.Create);
        }

        public static string SerializeFile(string file)
        {
            int fileIndex = -1;
            for (int c = 0; c < OpenFiles.Length; c++)
                if (file.Equals(OpenFiles[c]))
                {
                    fileIndex = c;
                    break;
                }
            if (fileIndex == -1)
            {
                //TODO: error
                return "";
            }
            FileType ftype = GetFileTypeForOpenFile(fileIndex);

            //Put data into serializable format
            object[] serialData = new object[openItems[fileIndex].Count];
            int d = 0;
            foreach (ItemDataWrapper v in openItems[fileIndex])
            {
                serialData[d] = v.data;
                d++;
            }

            using (MemoryStream ms = new MemoryStream())
            {
                using (StreamWriter write = new StreamWriter(ms))
                {
                    if (!Serialize(serialData, write, ftype))
                    {
                        MessageBox.Show("Serializing this file is not supported.", "Error", MessageBoxButtons.OK);
                        write.Close();
                        return "";
                    }
                    write.Flush();
                    return Encoding.UTF8.GetString(ms.GetBuffer(), 0, (int)ms.Length);
                }
            }
        }

        public static void SaveFile(string file, bool standalone = true)
        {
            int fileIndex = -1;
            for (int c = 0; c < OpenFiles.Length; c++)
                if (file.Equals(OpenFiles[c]))
                {
                    fileIndex = c;
                    break;
                }
            if (fileIndex == -1)
            {
                //TODO: error
                return;
            }
            FileType ftype = GetFileTypeForOpenFile(fileIndex);

            //Put data into serializable format
            object[] serialData = new object[openItems[fileIndex].Count];
            int d = 0;
            foreach (ItemDataWrapper v in openItems[fileIndex])
            {
                serialData[d] = v.data;
                d++;
            }

            using (StreamWriter write = new StreamWriter(GetFilestreamForFile(file)))
            {
                if (!Serialize(serialData, write, ftype))
                {
                    if (standalone)
                        MessageBox.Show("Serializing this file is not supported.", "Error", MessageBoxButtons.OK);
                    return;
                }
            }

            //Mark items as saved
            foreach (ItemDataWrapper data in openItems[fileIndex])
                data.Modified = false;

            if (standalone)
                MessageBox.Show("File '" + file + "' saved.", "Saving", MessageBoxButtons.OK);

            //Check if other files are still outstanding
            if (standalone)
            {
                foreach (BindingList<ItemDataWrapper> list in openItems)
                    foreach (ItemDataWrapper data in list)
                        if (data.Modified)
                            return;
                unsavedChanges = false;
            }
        }

        private static bool Serialize(object[] serialData, StreamWriter write, FileType ftype)
        {
            if (ftype != FileType.NONE
                && fileDef[(int)ftype] != null && fileDef[(int)ftype].schema != null)
            {
                SaveJsonItem(write, serialData, fileDef[(int)ftype].schema, "");
            }
            else
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// For items, apply a new value to the current item.
        /// </summary>
        public static void ItemApplyValue(string key, object value, bool mandatory)
        {
            Dictionary<string, object> final = WinformsUtil.NavigateKeyPath(CurrentItemData, ref key);
            if (!mandatory &&
                (value.Equals("") ||
                value == null ||
                (value is object[] && ((object[])value).Length == 0)))
            {
                if (final.ContainsKey(key))
                    final.Remove(key);
            }
            else if (!final.ContainsKey(key) || value != final[key])
            {
                final[key] = value;
                unsavedChanges = true;
            }

            openItems[currentFileIndex][currentItemIndex].NotifyKeyChanged(key);
        }

        #region Data Source Getters

        private static void InitializeDataSources()
        {
            
        }

        public static string[] GetDataSource(JsonFormTag.DataSourceType source)
        {
            if (source < JsonFormTag.DataSourceType.PRESET_COUNT)
            {
                return PresetDataSources[(int)source].ToArray();
            }
            else
            {
                switch (source)
                {
                    
                }
            }

            return null;
        }

        public static void UpdateDataSource(JsonFormTag.DataSourceType source, string newEntry)
        {
            if (source < JsonFormTag.DataSourceType.PRESET_MOD_COUNT)
            {
                if (!PresetDataSources[(int)source].Contains(newEntry))
                {
                    PresetDataSources[(int)source].Add(newEntry);
                    WinformsUtil.RefreshDataSources();
                }
            }
        }

        #endregion
    }
}
