using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CriPakTools;

using System.IO;

namespace CG
{
 

    public partial class Compare : Form
    {
        private MainForm mainform;
        CPKFile c2;
        string CPKName;
        string CPKPath;
        List<FileEntry> list3;
        List<FileEntry> list4;
        bool btn1PurposeDone = false;
        bool btn2PurposeDone = false;
        public Compare(MainForm mf)
        {
            this.mainform = mf;
            InitializeComponent();
        }
        void makeDarkTheme()
        {
            this.BackColor = Color.FromArgb(37, 37, 38);
            this.groupBox1.BackColor = Color.FromArgb(62, 62, 66);
            this.groupBox1.ForeColor = Color.White;
            label2.BackColor = Color.FromArgb(37, 37, 38);
            label2.ForeColor = Color.White;
            button1.BackColor = Color.FromArgb(62, 62, 66);
            button1.ForeColor = Color.White;
            button2.BackColor = Color.FromArgb(62, 62, 66);
            button2.ForeColor = Color.White;
        
        }
        void makeNormalTheme()
        {
            this.BackColor = SystemColors.Control;
            this.groupBox1.BackColor = SystemColors.Control;
            this.groupBox1.ForeColor = Color.Black;
            label2.BackColor = SystemColors.Control;
            label2.ForeColor = Color.Black;
            button1.BackColor = SystemColors.Control;
            button1.ForeColor = Color.Black;
            button2.BackColor = SystemColors.Control;
            button2.ForeColor = Color.Black;
        }
        private void Compare_Load(object sender, EventArgs e)
        {
            if (SharedSettings.darkTheme)
                makeDarkTheme();
            else
                makeNormalTheme();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (btn1PurposeDone)
            {
                MessageBox.Show("CPK already open");
                return;
            }
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                CPKPath = openFileDialog1.FileName;
                CPKName = Path.GetFileNameWithoutExtension(CPKPath);
          
                if (mainform.c.CPKName != CPKName)
                {
                    MessageBox.Show("CPK names mismatch");
                    return;
                }
                else
                {
                    label1.Text = "Compare againts CPK: " + openFileDialog1.SafeFileName;
                    label2.Text = "opening CPK..";
                    if (c2 != null)
                    {
                        c2.br.Close();
                        c2.cpk.FileTable.Clear();
                        c2 = null;
                    }
                    c2 = new CPKFile();

                    btn1PurposeDone = true;
               
                    backgroundWorker1.RunWorkerAsync();
                }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (c2 == null)
            {
                MessageBox.Show("Open CPK #2 first");
                return;
            }
            if (btn2PurposeDone)
            {
                MessageBox.Show("Compare Done");
                return;
            }
    
            label2.Text = "Comparing...";
      
            btn2PurposeDone = true;
            backgroundWorker2.RunWorkerAsync();
           
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            c2.readCPK(CPKPath, CPKName);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            label2.Text = "Ready to Compare";
            MessageBox.Show(c2.cpk.FileTable.Count.ToString());
            button2.Enabled = true;
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
           list3 = mainform.c.cpk.FileTable.Except(c2.cpk.FileTable, new IdComparer()).ToList();
           //list4 = mainform.c.cpk.FileTable.Except(c2.cpk.FileTable, new IdComparer2(mainform)).ToList();
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            label2.Text = "Compare done";
            string fileName;
            richTextBox1.AppendText("Files that are in CPK #1 and not CPK #2" + Environment.NewLine);
            richTextBox1.AppendText("====================================" + Environment.NewLine);
            foreach (FileEntry s in list3)
            {
                fileName = string.Format("{0}/{1}", s.DirName, s.FileName);
                richTextBox1.AppendText(fileName + Environment.NewLine);
            }

            //richTextBox1.AppendText("Files that are different in CPK #2" + Environment.NewLine);
            //richTextBox1.AppendText("====================================" + Environment.NewLine);
            //foreach (FileEntry s in list4)
            //{
            //    fileName = string.Format("{0}/{1}", s.DirName, s.FileName);
            //    richTextBox1.AppendText(fileName + Environment.NewLine);
            //}
        }
    }
    public class IdComparer : IEqualityComparer<FileEntry>
    {
        public int GetHashCode(FileEntry co)
        {
            if (co == null)
            {
                return 0;
            }
            return string.Format("{0}/{1}", co.DirName, co.FileName).GetHashCode();
        }

        public bool Equals(FileEntry x1, FileEntry x2)
        {
            if (object.ReferenceEquals(x1, x2))
            {
                return true;
            }
            if (object.ReferenceEquals(x1, null) ||
                object.ReferenceEquals(x2, null))
            {
                return false;
            }
            string fileName1 = string.Format("{0}/{1}", x1.DirName, x1.FileName);
            string fileName2 = string.Format("{0}/{1}", x2.DirName, x2.FileName);
            return fileName1 == fileName2;
        }
    }


    public class IdComparer2 : IEqualityComparer<FileEntry>
    {
        MainForm main = null;
        public IdComparer2(MainForm mf)
        {
            main = mf;
        }
        public int GetHashCode(FileEntry co)
        {
            if (co == null)
            {
                return 0;
            }
            return string.Format("{0}/{1}", co.DirName, co.FileName).GetHashCode();
        }

        public bool Equals(FileEntry x1, FileEntry x2)
        {
            //if (object.ReferenceEquals(x1, x2))
            //{
            //    return true;
            //}
            //if (object.ReferenceEquals(x1, null) ||
            //    object.ReferenceEquals(x2, null))
            //{
            //    return false;
            //}

            using (var alg = System.Security.Cryptography.SHA512.Create())
            {
  
                byte[] chunk1 = main.c.getChunk(x1.FileName.ToString());
                byte[] chunk2 = main.c.getChunk(x2.FileName.ToString());
                string hash1 = null;
                string hash2 = null;
                if (chunk1 != null)
                {
                    alg.ComputeHash(chunk1);
                    hash1 = BitConverter.ToString(alg.Hash);
                }
              
                if (chunk2 != null)
                {
                    alg.ComputeHash(chunk2);
                    hash2 = BitConverter.ToString(alg.Hash);
                }
           

                return hash1 == hash2;

            }
         
        }
    }

}
