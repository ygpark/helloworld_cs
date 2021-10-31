using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace CSarpSimpleHash
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listView1.AllowDrop = true;
            listView1.DragEnter += ListView1_DragEnter;
            listView1.DragDrop += ListView1_DragDrop;
            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.Columns.Add("파일명", 200, HorizontalAlignment.Left);
            listView1.Columns.Add("경로", 200, HorizontalAlignment.Left);
            listView1.Columns.Add("사이즈", 100, HorizontalAlignment.Right);
            listView1.Columns.Add("파일생성일", 200, HorizontalAlignment.Left);
            listView1.Columns.Add("마지막수정일", 200, HorizontalAlignment.Left);
            listView1.Columns.Add("마지막접근일", 200, HorizontalAlignment.Left);
            listView1.Columns.Add("Hash(MD5)", 250, HorizontalAlignment.Left);
            listView1.HeaderStyle = ColumnHeaderStyle.Clickable;
        }

        private void ListView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy; // Okay
            else
                e.Effect = DragDropEffects.None; // Unknown data, ignore it
        }

        private void ListView1_DragDrop(object sender, DragEventArgs e)
        {
            string[] dropedItems = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            
            
            foreach (string dropedItem in dropedItems)
            {
                FileAttributes attr = File.GetAttributes(dropedItem);

                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    DirectoryInfo di = new DirectoryInfo(dropedItem);
                    addFilesInDirectoryInfoToListView(di);
                }
                else
                {
                    FileInfo fileInfo = new FileInfo(dropedItem);
                    addFileToListView(fileInfo);
                }
                
                }
            }//ListView1_DragDrop

        private void addFilesInDirectoryInfoToListView(DirectoryInfo di)
        {
            FileInfo[] fis = di.GetFiles();
            foreach (FileInfo fi in fis)
            {
                addFileToListView(fi);
            }

            DirectoryInfo[] dis2 = di.GetDirectories();
            foreach (DirectoryInfo di2 in dis2)
            {
                addFilesInDirectoryInfoToListView(di2);
            }
        }

        private void addFileToListView(FileInfo fileInfo)
        {
            ListViewItem lvi = new ListViewItem(fileInfo.Name);
            //파일경로
            lvi.SubItems.Add(fileInfo.DirectoryName);
            //파일크기
            StringBuilder size = new StringBuilder();
            size.Append(fileInfo.Length.ToString());
            size.Append(" Byte");
            lvi.SubItems.Add(size.ToString());
            //파일생성일
            lvi.SubItems.Add(fileInfo.CreationTime.ToString());
            //마지막수정일
            lvi.SubItems.Add(fileInfo.LastWriteTime.ToString());
            //마지막접근일
            lvi.SubItems.Add(fileInfo.LastAccessTime.ToString());
            //해시값(MD5)
            lvi.SubItems.Add(checkMD5(fileInfo.FullName).ToString());
            //add item
            this.listView1.Items.Add(lvi);
        }

        private string checkMD5(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    return ToHex(md5.ComputeHash(stream), true);
                }
            }
        }
        
        private string ToHex(byte[] bytes, bool upperCase)
        {
            StringBuilder result = new StringBuilder(bytes.Length * 2);

            for (int i = 0; i < bytes.Length; i++)
                result.Append(bytes[i].ToString(upperCase ? "X2" : "x2"));

            return result.ToString();
        }

        private void 종료XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveToFile();
        }

        private void toolStripBtnClean_Click(object sender, EventArgs e)
        {
            this.listView1.Items.Clear();
        }

        private void toolStripBtnSave_Click(object sender, EventArgs e)
        {
            saveToFile();
        }

        private void saveToFile()
        {
            int lastIndex = 0;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "저장하기";
            saveFileDialog.DefaultExt = "csv";
            saveFileDialog.Filter = "CSV (*.csv)|*.csv";


            //CSV스타일 헤더 제작
            lastIndex = listView1.Columns.Count - 1;
            StringBuilder sbHeader = new StringBuilder();
            for (int i =0; i < listView1.Columns.Count; i++)
            {
                sbHeader.Append(listView1.Columns[i].Text);
                if(i != lastIndex)
                    sbHeader.Append(",");
            }


            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter file = new System.IO.StreamWriter(saveFileDialog.FileName, false, System.Text.Encoding.UTF8))
                    {
                        //CSV스타일 헤더 저장
                        file.WriteLine(sbHeader.ToString());

                        //CSV스타일 내용 저장
                        foreach (ListViewItem itemRow in this.listView1.Items)
                        {
                            StringBuilder sbContent = new StringBuilder();
                            lastIndex = itemRow.SubItems.Count - 1;
                            for (int i = 0; i < itemRow.SubItems.Count; i++)
                            {
                                sbContent.Append(itemRow.SubItems[i].Text);
                                if (i != lastIndex)
                                    sbContent.Append(",");
                            }
                            file.WriteLine(sbContent.ToString());
                        }
                    }
                }
                catch (System.IO.IOException e)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(saveFileDialog.FileName);
                    sb.Append(" 파일은 다른 프로세스에서 사용 중이므로 프로세스에서 액세스할 수 없습니다.");
                    MessageBox.Show(sb.ToString(), "에러", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
            }
        }

        private void cSharpSimpleHash정보AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm ab = new AboutForm();
            ab.ShowDialog();
        }
    }
}
