using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;

namespace Extensibility {
    public class Extensions : System.Collections.CollectionBase {

        public delegate void extensionClickedEventHandler(object sender, EventArgs e);
        public event extensionClickedEventHandler ExtensionClicked;

        public int add(IExtender extension) {
            return this.List.Add(extension);
        }
        public void remove(int index) {
            this.List.Remove(index);
        }
        public IExtender item(int index) {
            return (IExtender)this.List[index];
        }
        public IExtender item(String name) {
            IExtender ret = null;
            foreach (IExtender ext in this.List) {
                if(ext.Name.Equals(name)){
                    ret = ext;
                    break;
                }
            }
            return ret;
        }

        public int populate(String directory, bool searchSubs, ToolStripMenuItem menuItem) {
            int ret = this.populate(directory, searchSubs);
            if (ret > 0) {
                foreach (IExtender ext in this.List) {
                    ToolStripMenuItemEx item = new ToolStripMenuItemEx();
                    item.Click += new EventHandler(item_Click);
                    if (ext.MenuText != null) item.Text = ext.MenuText;
                    if (ext.Image != null) item.Image = ext.Image;
                    if (ext.Description != null) item.ToolTipText = ext.Description;
                    item.Extension = ext;
                    menuItem.DropDownItems.Add(item);
                }
            }
            return ret;
        }

        void item_Click(object sender, EventArgs e) {
            ExtensionClicked.Invoke(sender, e);
        }

        public int populate(String directory) {
            return this.populate(directory, false);
        }

        public int populate(String directory, bool searchSubs) {
            if(!Directory.Exists(directory)){
                throw new IOException("Directory not found");
            }
            DirectoryInfo dir = new DirectoryInfo(directory);
            this.searchDir(dir, searchSubs);
            return this.Count;
        }

        private void searchDir(DirectoryInfo dir, bool searchSubs) {
            FileInfo[] files = dir.GetFiles("*.dll");
            if (files.Length == 0) {
                throw new IOException("No dll files found in " + dir.FullName);
            }

            foreach (FileInfo f in files) {
                String fileName = f.FullName;
                Assembly assy = Assembly.LoadFile(fileName);
                Type[] types = assy.GetTypes();
                foreach (Type t in types) {
                    if (t.GetInterface("IExtender") != null) {
                        IExtender ext = (IExtender)assy.CreateInstance(t.FullName);
                        this.add(ext);
                    }
                }
            }

            if (searchSubs) {
                foreach(DirectoryInfo subDir in dir.GetDirectories()){
                    this.searchDir(subDir, searchSubs);
                }
            }

        }

    }
}
