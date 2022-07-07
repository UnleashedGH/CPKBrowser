/*
 * Created by SharpDevelop.
 * User: Unleashed
 * Date: 2/3/2019
 * Time: 5:01 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using System.Windows.Forms;
using System.Collections.Generic;
using CriPakTools;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Security.Principal;
using System.IO.Compression;





namespace CG
{
    /// <summary>
    /// Description of MainForm.
    /// </summary>
    /// 

    public partial class MainForm : Form
    {
        public CPKFile c;
        string CPKPath;
        string CPKName;
        ListViewItem file;
        string[] files;
        string globalPath;
        int folderCount;
        int fileCount;
        string fileStr;
        string folderstr;
        bool isLargeView = true;
        ListViewItem[] items;
        char[] delimiters = new char[] {(char)0x5C};
        bool isBackButtonPressed = false;
        List<Tools.FileReplace> fr = new List<Tools.FileReplace>();
        bool canAcceptCPK = true;
        Stack<TreeNode> nodesStack = new Stack<TreeNode>();
        Stack<int> lastIndexes = new Stack<int>();
        bool OperationGoing = false;
        bool isM2CInstall = false;

        TreeNode currentNodeForSearch = null;
       Tools.CacheFile cacheFile = null;
        string cacheFilePath = null;



        private class MyRenderer : ToolStripProfessionalRenderer
        {
            public MyRenderer() : base(new MyColors()) { }
        }

        private class MyColors : ProfessionalColorTable
        {
            public override Color MenuItemPressedGradientBegin
            {
                get { return Color.Gray; }
            }

            public override Color MenuItemPressedGradientEnd
            {
                get { return Color.LightBlue; }
            }
            public override Color ToolStripDropDownBackground
            {
                get { return Color.LightBlue; }
            }
            public override Color MenuItemSelectedGradientBegin
            {
                get { return Color.Gray; }
            }
            public override Color MenuItemSelectedGradientEnd
            {
                get { return Color.LightBlue; }
            }



  
        }


        public MainForm()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();

            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
        }
        public void inputError()
        {
            MessageBox.Show("Cannot open CPK, an operation is currently ongoing");

        }
        public void workError()
        {
            MessageBox.Show("Cannot Select a new item, an operation is currently ongoing");
        }
        void disableInput()
        {

        }
        void enableInput()
        {

        }
        public void makeDarkTheme()
        {
            //cacheToolStripMenuItem.DropDown
            this.BackColor = Color.FromArgb(28, 28, 28);
            this.listView1.BackColor = Color.FromArgb(37, 37, 38);
            this.treeView1.BackColor = Color.FromArgb(37, 37, 38);
            this.listView1.ForeColor = Color.White;
            this.treeView1.ForeColor = Color.White;

            this.toolStrip1.BackColor = Color.FromArgb(62, 62, 66);
            this.btnSearch.BackColor = Color.FromArgb(62, 62, 66);
            this.btnRevert.BackColor = Color.FromArgb(62, 62, 66);
            this.textBox1.BackColor = Color.FromArgb(62, 62, 66);
            this.menuStrip1.BackColor = Color.FromArgb(66, 66, 66);
            this.contextMenuStrip1.BackColor = Color.FromArgb(66, 66, 66);
            this.contextMenuStrip2.BackColor = Color.FromArgb(66, 66, 66);

            ((ToolStripDropDownMenu)fileToolStripMenuItem.DropDown).BackColor = Color.FromArgb(66, 66, 66);
            ((ToolStripDropDownMenu)toolsToolStripMenuItem.DropDown).BackColor = Color.FromArgb(66, 66, 66);
            ((ToolStripDropDownMenu)cacheToolStripMenuItem.DropDown).BackColor = Color.FromArgb(66, 66, 66);

            this.toolStrip1.ForeColor = Color.White;
            this.btnSearch.ForeColor = Color.White;
            this.btnRevert.ForeColor = Color.White;
            this.textBox1.ForeColor = Color.White;
            this.menuStrip1.ForeColor = Color.White;
            label1.ForeColor = Color.White;
            this.contextMenuStrip1.ForeColor = Color.White;
            this.contextMenuStrip2.ForeColor = Color.White;
            ((ToolStripDropDownMenu)fileToolStripMenuItem.DropDown).ForeColor = Color.White;
            ((ToolStripDropDownMenu)toolsToolStripMenuItem.DropDown).ForeColor = Color.White;
            ((ToolStripDropDownMenu)cacheToolStripMenuItem.DropDown).ForeColor = Color.White;

        }
        public void makeNormalTheme()
        {
            //cacheToolStripMenuItem.DropDown
            this.BackColor = SystemColors.Control;
            this.listView1.BackColor = Color.White;
            this.treeView1.BackColor = Color.White;
            this.listView1.ForeColor = Color.Black;
            this.treeView1.ForeColor = Color.Black;

            this.toolStrip1.BackColor =SystemColors.Control;
            this.btnSearch.BackColor = SystemColors.Control;
            this.btnRevert.BackColor = SystemColors.Control;
            this.textBox1.BackColor = SystemColors.Control;
            this.menuStrip1.BackColor = SystemColors.Control;
            this.contextMenuStrip1.BackColor = SystemColors.Control;
            this.contextMenuStrip2.BackColor = SystemColors.Control;
            ((ToolStripDropDownMenu)fileToolStripMenuItem.DropDown).BackColor = Color.FromArgb(253, 253, 253);
            ((ToolStripDropDownMenu)toolsToolStripMenuItem.DropDown).BackColor = Color.FromArgb(253, 253, 253);
            ((ToolStripDropDownMenu)cacheToolStripMenuItem.DropDown).BackColor = Color.FromArgb(253, 253, 253);

            this.toolStrip1.ForeColor = Color.Black;
            this.btnSearch.ForeColor = Color.Black;
            this.btnRevert.ForeColor = Color.Black;
            this.textBox1.ForeColor = Color.Black;
            this.menuStrip1.ForeColor = Color.Black;
            label1.ForeColor = Color.Black;
            this.contextMenuStrip1.ForeColor = Color.Black;
            this.contextMenuStrip2.ForeColor = Color.Black;
            ((ToolStripDropDownMenu)fileToolStripMenuItem.DropDown).ForeColor = Color.Black;
            ((ToolStripDropDownMenu)toolsToolStripMenuItem.DropDown).ForeColor = Color.Black;
            ((ToolStripDropDownMenu)cacheToolStripMenuItem.DropDown).ForeColor = Color.Black;
        }
         bool IsRunAsAdmin()
        {
            var Principle = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            return Principle.IsInRole(WindowsBuiltInRole.Administrator);
        }

        private void MenuClicked(object sender, EventArgs e)
        {
        
            string p = cacheFile.getPath(((ToolStripMenuItem)sender).Text);
            if (p == "NULL")
            {
                MessageBox.Show("invalid file path, possibily corrupted cache file.");
                return;
            }
            loadCPK(p);
        }
        public void populateCacheMenu()
        {
            cacheToolStripMenuItem.DropDownItems.Clear();
            for (int i = 0; i < cacheFile.content.Count; i++)
            {
                ToolStripItem subItem = new ToolStripMenuItem();
                subItem.Image = imageList1.Images[2];
                subItem.ImageTransparentColor = Color.Transparent;
             
                subItem.Click += MenuClicked;
                subItem.Text = (cacheFile.content[i].fileName);
                cacheToolStripMenuItem.DropDownItems.Add(subItem);
            }
           



        }
        private void SetValuesOnSubItems(List<ToolStripMenuItem> items)
        {
            items.ForEach(item =>
            {
                var dropdown = (ToolStripDropDownMenu)item.DropDown;
                if (dropdown != null)
                {
                    dropdown.ShowImageMargin = false;
                    SetValuesOnSubItems(item.DropDownItems.OfType<ToolStripMenuItem>().ToList());
                }
            });
        }

        public void HideRevert()
        {
            btnRevert.Visible = false;
            this.Refresh();
        }
        void MainFormLoad(object sender, EventArgs e)
        {



            menuStrip1.Renderer = new MyRenderer();


            SetValuesOnSubItems(this.menuStrip1.Items.OfType<ToolStripMenuItem>().ToList());





            //label1.Text = "";

            string[] args = Environment.GetCommandLineArgs();
            ((ToolStripDropDownMenu)fileToolStripMenuItem.DropDown).ShowImageMargin = false;
            ((ToolStripDropDownMenu)toolsToolStripMenuItem.DropDown).ShowImageMargin = false;
            ((ToolStripDropDownMenu)cacheToolStripMenuItem.DropDown).ShowCheckMargin = false;


             cacheFilePath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @"/" + "Recent";

            if (File.Exists(cacheFilePath))
            {
                cacheFile = new CriPakTools.Tools.CacheFile(cacheFilePath);
                populateCacheMenu();

            }

            else
            {
                File.WriteAllLines(cacheFilePath, new string[] { "" });
                cacheFile = new CriPakTools.Tools.CacheFile(cacheFilePath);
            }



            string settingFilePath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @"/" + "Settings";

            if (File.Exists(settingFilePath))
            {
                string[] settings = File.ReadAllLines(settingFilePath);
                if (settings.Length == 7)
                {
                    SharedSettings.extractionPath = settings[0];
                    SharedSettings.CPKDirPath = settings[1];
                    SharedSettings.matchCase = settings[2] == "True";
                    SharedSettings.fullSearch = settings[3] == "True";
                    SharedSettings.viewType = Convert.ToInt16(settings[4]);
                    SharedSettings.encType = Convert.ToInt16(settings[5]);
                    SharedSettings.darkTheme = settings[6] == "True";


                }

            }

            if (SharedSettings.viewType == 0)
                LargeIconViewToolStripMenuItemClick();
            else if (SharedSettings.viewType == 1)
                SmallIconViewToolStripMenuItemClick();



            if (SharedSettings.darkTheme)
                makeDarkTheme();
            else
                makeNormalTheme();
            if (args.Length > 1) // check for M2C file here
            {
               // MessageBox.Show(Path.GetExtension(args[1]));
                if (Path.GetExtension(args[1]) == ".cpk")
                {
                    string p = args[1];
                    string f = Path.GetFileName(args[1]);
               
                    cacheFile.addPath(f, p);
                    populateCacheMenu();
                    loadCPK(args[1]);
                }
                else if (Path.GetExtension(args[1]) == ".m2c")
                {
                    this.Size = new Size(this.Size.Width, this.Size.Height - listView1.Size.Height);
                    installM2C(args[1]);
                }
             
            }
            //listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);


        }

        void loadCPK(string localCpkName)
        {

            if (!canAcceptCPK)
            {
                inputError();
                return;
            }

                if (c != null)
                {
                    c.br.Close();
                    if (c.bw != null)
                     c.bw.Close();
                    c.cpk.FileTable.Clear();
                    c = null;
                }

                c = new CPKFile();
                treeView1.Nodes.Clear();
                listView1.Items.Clear();
                lastIndexes.Clear();
                nodesStack.Clear();
                currentNodeForSearch = null;
                btnRevert.Visible = false;
            
            if (!isM2CInstall)
                fr.Clear();

                CPKPath = localCpkName;
                CPKName = Path.GetFileNameWithoutExtension(localCpkName);
                label1.Text = "opening CPK...";

                fileToolStripMenuItem.Enabled = false;
                toolsToolStripMenuItem.Enabled = false;
                extractAllToolStripMenuItem.Enabled = false;
                btnSearch.Enabled = false;
                textBox1.Enabled = false;


                canAcceptCPK = false;
                OperationGoing = true;

                isBackButtonPressed = false;
                backgroundWorker1.RunWorkerAsync();


            
        }
        void OpenToolStripMenuItemClick(object sender, EventArgs e)
        {
          
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                cacheFile.addPath(openFileDialog1.SafeFileName, openFileDialog1.FileName);
                populateCacheMenu();
                loadCPK(openFileDialog1.FileName);
            }
           

        }
        void BackgroundWorker1DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            c.readCPK(CPKPath, CPKName);

        }
        void BackgroundWorker1RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (isM2CInstall)
            {
           
                isM2CInstall = false;
                label1.Text = "Saving CPK..";
                progressBar1.Value = 0;
                progressBar1.Maximum = c.cpk.FileTable.Count;
                progressBar1.Visible = true;
               
               


                backgroundWorker3.RunWorkerAsync();
            }
            else
            {

           
            treeView1.Nodes.Add(c.cpk.rootNode);

            fileToolStripMenuItem.Enabled = true;
            toolsToolStripMenuItem.Enabled = true;
            extractAllToolStripMenuItem.Enabled = true;
            label1.Text = treeView1.Nodes[0].Text;
            textBox1.Enabled = true;
            btnSearch.Enabled = true;
            canAcceptCPK = true;
            OperationGoing = false;

            }

            //TEST DELETE ME
            //MessageBox.Show(c.cpk.FileTable[c.cpk.FileTable.Count - 5].DirName.ToString());
            //MessageBox.Show(c.cpk.FileTable[c.cpk.FileTable.Count - 1].FileName.ToString());
            //MessageBox.Show(c.cpk.FileTable[c.cpk.FileTable.Count - 2].ID.ToString());
           // MessageBox.Show(c.cpk.FileTable[c.cpk.FileTable.Count - 2].DirName.ToString());
            //for (int i = 0; i < c.cpk.FileTable.Count; i++)
            //{
            //    //FileOffset
            //    MessageBox.Show(c.cpk.FileTable[i].FileName.ToString());
            //    if (c.cpk.FileTable[i].ID != null)
            //    MessageBox.Show("ID " + c.cpk.FileTable[i].ID.ToString());
            //    MessageBox.Show("FileSizePos " + c.cpk.FileTable[i].FileSizePos.ToString());
            //    MessageBox.Show("ExtractSizePos " + c.cpk.FileTable[i].ExtractSizePos.ToString());
            //    MessageBox.Show("FileOffsetPos " + c.cpk.FileTable[i].FileOffsetPos.ToString());
            //    MessageBox.Show("FileOffset  " + c.cpk.FileTable[i].FileOffset.ToString());
            //}

        }
        public string RoundBytes(ulong num)
        {
            if (num < 1024)
                return num + " bytes";
            else if (num < Math.Pow(1024, 2))
                return Math.Round(num / (double)1024, 2) + " KB";
            else if (num < Math.Pow(1024, 3))
                return Math.Round(num / Math.Pow(1024, 2), 2) + " MB";
            else if (num < Math.Pow(1024, 4))
                return Math.Round(num / Math.Pow(1024, 3), 2) + " GB";
            else if (num < Math.Pow(1024, 5))
                return Math.Round(num / Math.Pow(1024, 4), 2) + " TB";

            return "NULL";
        }
        void TreeView1AfterSelect(object sender, TreeViewEventArgs e)
        {

            btnRevert.Visible = false;
             
            //if (listView1.SelectedItems.Count > 0 && isBackButtonPressed == false)
            //{
            //    lastListViewIndex = listView1.SelectedItems[0].Index;

            //}

        
            if (OperationGoing && canAcceptCPK)
            {
                
                 workError();
                return;
            }

            listView1.Items.Clear();


            listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8);
            listView1.View = View.Details;

            //add folders
            for (int i = 0; i < e.Node.Nodes.Count; i++)
            {

                file = new ListViewItem(e.Node.Nodes[i].Text, 0);
                file.Tag = e.Node.Nodes[i].Tag;




                folderCount = e.Node.Nodes[i].Nodes.Count;
                fileCount = (e.Node.Nodes[i].Tag == null ? 0 : e.Node.Nodes[i].Tag.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Length);
                fileStr = (fileCount == 1 ? "File" : "Files");
                folderstr = (folderCount == 1 ? "Folder" : "Folders");
                file.SubItems.Add(string.Format("{0} {3} and {1} {2}", folderCount, fileCount, fileStr, folderstr));
                listView1.Items.Add(file);

            }
            // add files
            if (e.Node.Tag != null)
            {

                files = e.Node.Tag.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < files.Length; i++)
                {
                    file = new ListViewItem(c.cpk.FileTable[Convert.ToInt32(files[i])].FileName.ToString(), 1);
                    file.Tag = Convert.ToInt32(files[i]);
                    file.SubItems.Add(RoundBytes(Convert.ToUInt64(c.cpk.FileTable[Convert.ToInt32(files[i])].ExtractSize)));
                    listView1.Items.Add(file);



                }
            }

            label1.Text = "In: " + e.Node.FullPath;
            if (isLargeView)
            {
                listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16);
                listView1.View = View.Tile;

            }
            if (lastIndexes.Count > 0 && isBackButtonPressed)
            {
                int index = lastIndexes.Pop();
                if (index <= listView1.Items.Count - 1 && index > -1)
                {
                    listView1.Items[index].Selected = true;
                    listView1.Items[index].Focused = true;
                    listView1.EnsureVisible(index);
                    isBackButtonPressed = false;
                }
            }

     
        }


        void AboutToolStripMenuItemClick(object sender, EventArgs e)
        {
            //MessageBox.Show("CPK Browser v5.0\nCreated By Unleashed\nCode is based of CriPakTools by esperknight\nFurther modifed with code from YACE by eternity ","About",MessageBoxButtons.OK,MessageBoxIcon.Information);

            System.Diagnostics.Process.Start("https://discord.gg/UK8AJH6");
        }

        void extractBulkMiniWorker(string path)
        {
         
            label1.Text = "extracting " + path + "...";
            globalPath = path;
           
            //backgroundWorker3.RunWorkerAsync();

            //label1.Text = globalPath + " extracted successfully";
            // backgroundWorker3.RunWorkerAsync();
            Application.DoEvents();

            c.ExtractWithPath(globalPath);
            Application.DoEvents();


          
            label1.Text = globalPath + " extracted successfully";
        }

        void ToolStripMenuItem1Click(object sender, EventArgs e)
        {
        
            bool isExtraction = isExtraction = (listView1.SelectedItems[0].ImageIndex == 0
                || listView1.SelectedItems[0].ImageIndex == 1);

            if (!isExtraction)
            {
                MessageBox.Show("unknown extraction operation.", "Extraction",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            OperationGoing = true;
            canAcceptCPK = false;
            progressBar1.Visible = true;
            progressBar1.Maximum = listView1.SelectedItems.Count;
            for (int i = 0; i < listView1.SelectedItems.Count; i++)
            {
                if (listView1.SelectedItems[i].ImageIndex == 1)
                { // file
                    // MessageBox.Show("TAG OF SINGLE ITEM" +listView1.SelectedItems[i].Tag.ToString());
                    label1.Text = "extracting " + listView1.SelectedItems[i].Text;
                    
                    c.ExtractByIndex((int)listView1.SelectedItems[i].Tag);
                    label1.Text = listView1.SelectedItems[i].Text + " extracted successfully";
                    progressBar1.Increment(1);

                }
                else if (listView1.SelectedItems[i].ImageIndex == 0)
                { // folder
                    string t = (treeView1.SelectedNode.FullPath + @"\" + listView1.SelectedItems[i].Text).Substring(treeView1.Nodes[0].Text.Length + 1).Replace((char)0x5C, (char)0x2F);
              
            
                    extractBulkMiniWorker(t);
                  
                    progressBar1.Increment(1);
                }


            }
            progressBar1.Value = 0;
            progressBar1.Visible = false;

            canAcceptCPK = true;
            OperationGoing = false;

        }


        void ExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            Application.Exit();
        }
        void ListView1MouseClick(object sender, MouseEventArgs e)
        {

            if (OperationGoing)
            {
                workError();
                return;
            }
            bool shouldShowContext = true;
            bool shouldShowReplaceBuildCPK = true;
            bool shouldShowReplaceNoBuildCPK = true;
            bool shouldShowCopyFileName = true;
            bool shouldShowCopyFilePath = true;
            bool shouldShowCopyAbsPath= true;
            if (e.Button == MouseButtons.Right)
            {

                if (listView1.SelectedItems.Count > 1)
                {
                    shouldShowReplaceBuildCPK = false;
                    shouldShowReplaceNoBuildCPK = false;
                    shouldShowCopyFileName = false;
                    shouldShowCopyFilePath = false;
                    shouldShowCopyAbsPath = false;

                    if (listView1.SelectedItems[0].ImageIndex < 0)
                        shouldShowContext = false;

                    contextMenuStrip1.Items[0].Text = "Extract";
                }
                else if (listView1.SelectedItems.Count == 1)
                {
                    string menuExtractText = "Extract File";
                    if (listView1.SelectedItems[0].ImageIndex < 0) 
                        shouldShowContext = false;
                    if (listView1.SelectedItems[0].ImageIndex == 0)
                    {
                        shouldShowReplaceBuildCPK = false;
                        shouldShowReplaceNoBuildCPK = false;
                        menuExtractText = "Extract Folder";
                    }


                    contextMenuStrip1.Items[0].Text = menuExtractText;
                }

                markForReplaceToolStripMenuItem.Enabled = shouldShowReplaceBuildCPK;
                replaceFileNoRebuildToolStripMenuItem.Enabled = shouldShowReplaceNoBuildCPK;
                copyFileNameToolStripMenuItem.Enabled = shouldShowCopyFileName;
                copyFilePathToolStripMenuItem.Enabled = shouldShowCopyFilePath;
                copyAbsloutePathToolStripMenuItem.Enabled = shouldShowCopyAbsPath;
                if (shouldShowContext)
                    contextMenuStrip1.Show(listView1, e.X, e.Y);


            }


        }

        void ListView1DoubleClick(object sender, EventArgs e)
        {
            if (OperationGoing)
            {
                workError();
                return;
            }
            // pkg viewer code, wroks well for now.
            if (listView1.SelectedItems.Count == 1)
            {

            
                if (listView1.SelectedItems[0].ImageIndex == 0)
                {
             
                    for (int i = 0; i < treeView1.SelectedNode.Nodes.Count; i++)
                    {
                        if (treeView1.SelectedNode.Nodes[i].Text == listView1.SelectedItems[0].Text)
                        {
                            treeView1.SelectedNode = treeView1.SelectedNode.Nodes[i];
                            break;
                        }
                    }
                }

                else // a search result
                {
            
                    string[] pathParts;

                    TreeNode currentnode = treeView1.Nodes[0];
                    //MessageBox.Show(currentnode.Text);
                    char[] delimiters = new char[] { '/' };
                    //FullPath = string.Format("{0}/{1}", temp.DirName, temp.FileName);
                    //MessageBox.Show(listView1.SelectedItems[0].Text);
                    pathParts = listView1.SelectedItems[0].Text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                    for (int z = 0; z < pathParts.Length - 1; z++)
                    {
                        // MessageBox.Show(pathParts[z]);
                        treeView1.SelectedNode = currentnode.Nodes[pathParts[z]];
                        currentnode = currentnode.Nodes[pathParts[z]];
                    }
                    Application.DoEvents();
                    int selectedInd = -1;
            
                    for (int z = 0; z < listView1.Items.Count;z++){
                  
                        if (listView1.Items[z].Text == pathParts[pathParts.Length - 1]){
                            selectedInd = z;
                            break;
                        }
                    }

                    if (selectedInd > -1)
                    {
                        listView1.Items[selectedInd].Selected = true;
                        listView1.EnsureVisible(selectedInd);
                    }
                       

                }

            }
        }
        void ExtractAllToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("This will extract ~" + RoundBytes(c.cpk.CPK_Files_Size) + " of data\nAre you sure?", "Extract All", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.OK)
            {


                label1.Text = "Extracting all files... ";
                fileToolStripMenuItem.Enabled = false;
                extractAllToolStripMenuItem.Enabled = false;
                progressBar1.Maximum = c.cpk.FileTable.Count;
                progressBar1.Visible = true;
                canAcceptCPK = false;
                OperationGoing = true;
                backgroundWorker2.RunWorkerAsync();
            }

        }
        void BackgroundWorker2DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {

            for (int i = 0; i < c.cpk.FileTable.Count; i++)
            {

                if (c.cpk.FileTable[i].DirName != null)
                { // actual file and not a header
                    backgroundWorker2.ReportProgress(0, c.cpk.FileTable[i].FileName.ToString());
                    c.ExtractByIndex(i);

                }
            }
        }

        void ChangeOutputPathToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {

                c.extractPath = folderBrowserDialog1.SelectedPath += @"\";
                File.WriteAllLines("Settings", new string[] { folderBrowserDialog1.SelectedPath });

            }
        }

        // void CANEL EXTRACT ALL

        // end sub


        void BackgroundWorker2RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            label1.Text = "All files extracted successfully";
            fileToolStripMenuItem.Enabled = true;
            extractAllToolStripMenuItem.Enabled = true;
       
            progressBar1.Value = 0;
            progressBar1.Visible = false;
            canAcceptCPK = true;
            OperationGoing = false;
        }


        void ListView1DragLeave(object sender, EventArgs e)
        {

        }
      
        void BtnSearchClick(object sender, EventArgs e)
        {
            if (SharedSettings.fullSearch == false)
            {


                currentNodeForSearch = treeView1.SelectedNode; 
                btnRevert.Visible = true;

                if (listView1.Items.Count > 0 && textBox1.Text.Length > 0)
                {
                    StringComparison sc = (SharedSettings.matchCase == true) ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
                    items = new ListViewItem[listView1.Items.Count];
                    listView1.Items.CopyTo(items, 0);
                    listView1.Items.Clear();
                    for (int i = 0; i < items.Length; i++)
                    {
                        //file = new ListViewItem(c.cpk.FileTable[Convert.ToInt32(files[i])].FileName.ToString(),1);
                        //file.Tag = Convert.ToInt32(files[i]);
                        //file.SubItems.Add(RoundBytes(Convert.ToUInt64(c.cpk.FileTable[Convert.ToInt32(files[i])].ExtractSize)));
                        //listView1.Items.Add(file);
                        if (items[i].Text.IndexOf(textBox1.Text, sc) >= 0)
                        {

                            listView1.Items.Add(items[i]);
                        }



                    }

                }

            }
            else
            {

                if (textBox1.Text.Length > 0)
                {
                    listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8);
                    listView1.View = View.Details;
                    StringComparison sc = (SharedSettings.matchCase == true) ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
                    //.Show(c.getFilePath(textBox1.Text));
                    listView1.Items.Clear();
                    Tools.FileOfIntereset[] strArray = c.getFilePath(textBox1.Text, sc);
                    //MessageBox.Show(strArray.Length.ToString());

                    if (strArray.Length > 0)
                    {
                        for (int i = 0; i < strArray.Length; i++)
                        {
                            file = new ListViewItem(strArray[i].filePath, 1);
                            file.Tag = strArray[i].fileIndex;
                            // file.Tag = Convert.ToInt32(files[i]);
                            //file.SubItems.Add(RoundBytes(Convert.ToUInt64(c.cpk.FileTable[Convert.ToInt32(files[i])].ExtractSize)));
                            listView1.Items.Add(file);
                        }
                    }

                    if (isLargeView)
                    {
                        listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16);
                        listView1.View = View.Tile;

                    }
                }
                else
                {
                    MessageBox.Show("Search box is empty..");
                }

            }


        }
        public void LargeIconViewToolStripMenuItemClick()
        {
            listView1.View = View.Tile;
            treeView1.ImageList = imageList2;
            treeView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12);
            listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14);
            treeView1.ItemHeight = 64;
            treeView1.Indent = 47;
            isLargeView = true;
        }
        public void SmallIconViewToolStripMenuItemClick()
        {
            listView1.View = View.Details;
            treeView1.ImageList = imageList1;
            treeView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8);
            listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8);
            treeView1.ItemHeight = 16;
            treeView1.Indent = 12;
            isLargeView = false;

        }

        void ToolStripButton2Click(object sender, EventArgs e)
        {



            if (nodesStack.Count > 0)
            {
            
                isBackButtonPressed = true;
                //MessageBox.Show(treeView1.SelectedNode.Text);
                treeView1.SelectedNode = nodesStack.Pop();
               // MessageBox.Show(treeView1.SelectedNode.Text);
                 if (nodesStack.Count == 0)
                     toolStripButton2.Enabled = false;
            }
            else
            {
                //MessageBox.Show("nodesStack <= 0");
                toolStripButton2.Enabled = false;
            }



        }



        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new Settings(this)).Show();
        }





        private void backgroundWorker2_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            label1.Text = e.UserState.ToString();
            progressBar1.Increment(1);
        }

        private void listView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void listView1_DragDrop(object sender, DragEventArgs e)
        {
        
            string[] DroppedFiles = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (DroppedFiles.Length < 0)
                return;

            if (Path.GetExtension(DroppedFiles[0]).ToLower() == ".cpk")
            {
                string path = DroppedFiles[0];
                string fileName = Path.GetFileName(DroppedFiles[0]);
                cacheFile.addPath(fileName, path);
                populateCacheMenu();
                loadCPK(path);

            }
            else
            {
                MessageBox.Show("this doesn't appear to be a CPK file");
            }
        }

        private void treeView1_DragDrop(object sender, DragEventArgs e)
        {
         
            string[] DroppedFiles = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (DroppedFiles.Length < 0)
                return;

            if (Path.GetExtension(DroppedFiles[0]).ToLower() == ".cpk")
            {
                loadCPK(DroppedFiles[0]);

            }
            else
            {
                MessageBox.Show("this doesn't appear to be a CPK file");
            }
        }

        private void treeView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }


        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (OperationGoing)
            {
                workError();
                return;
            }
            if (e.KeyCode == Keys.A && e.Control)
            {


                foreach (ListViewItem item in listView1.Items)
                {
                    item.Selected = true;
                }
            }
        }

        private void saveCPKToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (c == null)
            {
                MessageBox.Show("open CPK first");
                return;
            }
            label1.Text = "Saving CPK..";
           
            canAcceptCPK = false;
            progressBar1.Maximum = c.cpk.FileTable.Count;
            progressBar1.Visible = true;
         
            fileToolStripMenuItem.Enabled = false;
            extractAllToolStripMenuItem.Enabled = false;
            OperationGoing = true;
   
            
                
            backgroundWorker3.RunWorkerAsync();

        }

        private void markForReplaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(listView1.SelectedItems.Count > 0))
                return;
            int fileIndex = Int32.Parse(listView1.SelectedItems[0].Tag.ToString());

            //string fileName = string.Format("{0}/{1}", c.cpk.FileTable[fileIndex].DirName, c.cpk.FileTable[fileIndex].FileName);
            string fileName = c.cpk.FileTable[fileIndex].FileName.ToString();
            for (int i = 0; i < fr.Count; i++)
            {
                if (fileName == fr[i].ins_name)
                {
                    MessageBox.Show("file is already marked for replace");
                    return;
                }
            }
            string ext = Path.GetExtension(fileName);
            string extNoDot = (ext.Length > 1) ? ext.Substring(1) : ext;
            openFileDialog2.Filter = extNoDot.ToUpper() + " files|*" + ext;
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
              
                Tools.FileReplace frs = new Tools.FileReplace(fileName, File.ReadAllBytes(openFileDialog2.FileName));
                fr.Add(frs);
                MessageBox.Show("file marked for replace successfully");
            }
        }

        private void backgroundWorker3_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {

            FileInfo fi = new FileInfo(CPKPath);

            string outputName = fi.FullName + ".tmp";
       
            BinaryWriter newCPK = new BinaryWriter(File.OpenWrite(outputName));

            List<FileEntry> entries = c.cpk.FileTable.OrderBy(x => x.FileOffset).ToList();
 

            for (int i = 0; i < entries.Count; i++)
            {
             
             
                if (entries[i].FileType != "CONTENT")
                {

                    if (entries[i].FileType == "FILE")
                    {
                        //MessageBox.Show((newCPK.BaseStream.Position).ToString());
                       // MessageBox.Show((c.cpk.ContentOffset).ToString());
                        // I'm too lazy to figure out how to update the ContextOffset position so this works :)
                        if ((ulong)newCPK.BaseStream.Position < c.cpk.ContentOffset)
                        {
                      
                            ulong padLength = c.cpk.ContentOffset - (ulong)newCPK.BaseStream.Position;
                            for (ulong z = 0; z < padLength; z++)
                            {
                                newCPK.Write((byte)0);
                            }
                        }
                    }

                    //for loop here for a new struct that has InsertionName and ReplaceWith with i index
                    string fileName = entries[i].FileName.ToString();
                    bool shouldDoSecondIfStatement = false;
                    int frIndex = -1;
                    //MessageBox.Show(fileName);
                    for (int jj = 0; jj < fr.Count; jj++)
                    {
                        
                        if (fileName == fr[jj].ins_name)
                        {
                          
                           
                            shouldDoSecondIfStatement = true;
                            frIndex = jj;
                          
                          
                            break;
                        }

                    }
                    if (shouldDoSecondIfStatement == false)
                    {
                        c.br.BaseStream.Seek((long)entries[i].FileOffset, SeekOrigin.Begin);

                        entries[i].FileOffset = (ulong)newCPK.BaseStream.Position;
                        c.cpk.UpdateFileEntry(entries[i]);
                     
                    
                            byte[] chunk = c.br.ReadBytes(Int32.Parse(entries[i].FileSize.ToString()));
                            newCPK.Write(chunk);
                        
                   
                    }
                    else
                    {
                        byte[] newbie = fr[frIndex].replaceWith;
                        entries[i].FileOffset = (ulong)newCPK.BaseStream.Position;
                        entries[i].FileSize = Convert.ChangeType(newbie.Length, entries[i].FileSizeType);
                        entries[i].ExtractSize = Convert.ChangeType(newbie.Length, entries[i].FileSizeType);
                        c.cpk.UpdateFileEntry(entries[i]);
                        newCPK.Write(newbie);
                        fr.RemoveAt(frIndex);
                    }

                    if ((newCPK.BaseStream.Position % 0x800) > 0)
                    {
                        long cur_pos = newCPK.BaseStream.Position;
                        for (int j = 0; j < (0x800 - (cur_pos % 0x800)); j++)
                        {
                            newCPK.Write((byte)0);
                        }
                    }
                }

                else
                {
                    // Content is special.... just update the position
                    //MessageBox.Show(entries[i].FileName.ToString());
                    c.cpk.UpdateFileEntry(entries[i]);
                }
                backgroundWorker3.ReportProgress(0);
            }

            //=========================================================================
            //=========================================================================
            //=========================================================================
            //FILES THAT WHERE CORRUPTED WITH THIS bgm2.cpk
            //MAKE BACK UPSS
            //this is the place where we are experimenting with adding new files

            //FileEntry cpy = entries[entries.Count - 1];
            //MessageBox.Show(cpy.FileName.ToString());

            //FileEntry tmp;
            //byte[] newbieExtra;
            //for (int jj = 0; jj < fr.Count; jj++)
            //{

            //    if (fr[jj].path != "")
            //    {


            //        //if ((ulong)newCPK.BaseStream.Position < c.cpk.ContentOffset)
            //        //{
            //        //    ulong padLength = c.cpk.ContentOffset - (ulong)newCPK.BaseStream.Position;
            //        //    for (ulong z = 0; z < padLength; z++)
            //        //    {
            //        //        newCPK.Write((byte)0);
            //        //    }
            //        //}

            //        tmp = cpy;
            //        //entries.Add(cpy);
            //        tmp.FileName = fr[jj].ins_name;
            //        tmp.DirName = fr[jj].path;

            //        newbieExtra = fr[jj].replaceWith;
            //        tmp.FileOffset = (ulong)newCPK.BaseStream.Position;
            //        tmp.FileSize = Convert.ChangeType(newbieExtra.Length, tmp.FileSizeType);
            //        tmp.ExtractSize = Convert.ChangeType(newbieExtra.Length, tmp.FileSizeType);
            //        tmp.FileOffsetPos = entries[entries.Count - 1].FileOffsetPos + 4;
            //        tmp.FileSizePos = entries[entries.Count - 1].FileSizePos + 4;
            //        tmp.ExtractSizePos = entries[entries.Count - 1].ExtractSizePos + 4;

            //        tmp.ID = Convert.ChangeType(Convert.ToInt32(entries[entries.Count - 1].ID) + 1, tmp.FileSizeType); //this will prob not work.. need to emulate IDType..





            //        entries.Add(tmp);
            //        c.cpk.UpdateFileEntry(entries[entries.Count - 1]);
            //        newCPK.Write(newbieExtra);




            //        //if ((newCPK.BaseStream.Position % 0x800) > 0)
            //        //{
            //        //    long cur_pos = newCPK.BaseStream.Position;
            //        //    for (int j = 0; j < (0x800 - (cur_pos % 0x800)); j++)
            //        //    {
            //        //        newCPK.Write((byte)0);
            //        //    }
            //        //}

            //    }


            //}
      

         

            //end of place where we are experimanting with adding new files
            //=========================================================================
            //=========================================================================
            //=========================================================================
            c.cpk.WriteCPK(newCPK); // the CPK Header
            c.cpk.WriteITOC(newCPK);
            c.cpk.WriteTOC(newCPK);
            c.cpk.WriteETOC(newCPK);
            c.cpk.WriteGTOC(newCPK);

            newCPK.Close();
            c.br.Close();

            if (c.bw != null)
                c.bw.Close();
        
            File.Delete(CPKPath);
            File.Move(outputName, CPKPath);
            File.Delete(outputName);


        }

        private void backgroundWorker3_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show ("CPK Saved Successfully");
            progressBar1.Value = 0;
            progressBar1.Visible = false;
            listView1.Enabled = true;
            treeView1.Enabled = true;
            canAcceptCPK = true;

            if (c != null)
            {
                c.br.Close();

                if (c.bw != null)
                    c.bw.Close();
                c.cpk.FileTable.Clear();
                c = null;
            }


            treeView1.Nodes.Clear();
            listView1.Items.Clear();
            fr.Clear();
           fileToolStripMenuItem.Enabled = true;
            extractAllToolStripMenuItem.Enabled = false;
            btnSearch.Enabled = false;
            textBox1.Enabled = false;
            lastIndexes.Clear();
            nodesStack.Clear();
            currentNodeForSearch = null;
            btnRevert.Visible = false;
            isBackButtonPressed = false;
            OperationGoing = false;
            Application.Exit();
          
         



        }

  
      

        private void backgroundWorker3_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            progressBar1.Increment(1);
        }

        private void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {

            if (OperationGoing)
            {
                workError();
                return;
            }
            // beware that this event gets trigged when the CPK first opens
            // and it did push a null tree node before this check
            if (isBackButtonPressed == false && treeView1.SelectedNode != null)
            {
                nodesStack.Push(treeView1.SelectedNode);
                toolStripButton2.Enabled = true;
                if (listView1.SelectedItems.Count > 0)
                    lastIndexes.Push(listView1.SelectedItems[0].Index);


            }
            else
            {
   
                isBackButtonPressed = false;

               // MessageBox.Show(treeView1.SelectedNode.Text);
            }


        }

    

        private void setAsDefaultProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsRunAsAdmin())
            {
                FileAssociations.SetAssoc(".cpk", "CPK_BROWSER_FILE", "CPK File");
                MessageBox.Show("Extension Set Successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No permission to set extension!\nRun CPK Browser as Administrator","No Permission",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
         
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void compareCPKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (c == null)
            {
                MessageBox.Show("open CPK first");
                return;
            }
            (new Compare(this)).Show();
        }

        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }

        void installM2C(string path)
        {

            string TargetCPK;
            string[] paths;



            using (ZipArchive zipArchive =
             ZipFile.Open(path, ZipArchiveMode.Read))
            {
                if (zipArchive.Entries.Count > 0)
                {
                    //hack
                    paths = (zipArchive.Entries[0].FullName).Split(delimiters);
                    TargetCPK = paths[0] + ".cpk";
                    TargetCPK = TargetCPK.Replace("/", string.Empty);
                    TargetCPK = TargetCPK.Replace(@"\", string.Empty);
                    if (!File.Exists(SharedSettings.CPKDirPath + @"\" + TargetCPK))
                    {
                       
                        MessageBox.Show("this M2C file targets " + TargetCPK + " as CPK to install\n but " + TargetCPK +
                            " was not found in the CPK Directory.\nSelect the proper CPK Directory in the 'Settings' tab.");
                        Application.Exit();
                        return;
                    }

                }
                else
                {
                    MessageBox.Show("M2C is empty.");
                    return;
                }

                //DialogResult dialogResult = MessageBox.Show("Before permanently replacing files, do you want to create a backup M2C file? (M2C with files before replacing)","Backup",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                //if (dialogResult == DialogResult.Yes)
                //{

                //}
                label1.Text = "replacing files..";
                string filePathInCPK ="";
                Application.DoEvents();
                for (int i = 0; i < zipArchive.Entries.Count; i++)
                {

                    filePathInCPK = "";
             
                    for (int j = 1; j < paths.Length - 1; j++)
                    {
                        filePathInCPK += paths[j] + ((j + 1 == paths.Length - 1) ? "" : "/");
                        
                    }
                   // MessageBox.Show(filePathInCPK);

                    Tools.FileReplace frs = new Tools.FileReplace(zipArchive.Entries[i].Name, ReadFully(zipArchive.Entries[i].Open()), filePathInCPK);
                    fr.Add(frs);
                }
            }

            isM2CInstall = true;
            loadCPK(SharedSettings.CPKDirPath + @"\" + TargetCPK);
        }
        private void installM2CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog3.ShowDialog() == DialogResult.OK)
            {
                installM2C(openFileDialog3.FileName);
            }
        }

        private void aDMINSetAsDefaultProgramm2cToolStripMenuItem_Click(object sender, EventArgs e)
        {
          


            if (IsRunAsAdmin())
            {
                FileAssociations.SetAssoc(".m2c", "M2C_BROWSER_FILE", "M2C File");
                MessageBox.Show("Extension Set Successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No permission to set extension!\nRun CPK Browser as Administrator", "No Permission", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void createM2CFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new CreateM2C()).Show();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openFileDialog3_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void replaceFileNoRebuildToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int fileIndex = Int32.Parse(listView1.SelectedItems[0].Tag.ToString());
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
         
                byte[] rawBytes = File.ReadAllBytes(openFileDialog2.FileName);
           
                c.ModifyFileInCPK(fileIndex, rawBytes);
              
            }

        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void copyFileNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
                 

                    Clipboard.SetText(listView1.SelectedItems[0].Text);
        }

        private void copyFilePathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
            string s = treeView1.SelectedNode.FullPath;
            Clipboard.SetText(s + @"\" + listView1.SelectedItems[0].Text);
         
            }

              catch ( Exception ex){

                  Clipboard.SetText(listView1.SelectedItems[0].Text);
            }
        }

        private void copyAbsloutePathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

           
          
            string s2 = treeView1.SelectedNode.FullPath.Substring(c.CPKName.Length + 1);
            string s = c.extractPath  + s2;
            Clipboard.SetText(s);
            }

           catch ( Exception ex){
               string s = c.extractPath + listView1.SelectedItems[0].Text;
                   Clipboard.SetText(s);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            btnRevert.Visible = false;

 

            //treeView1.node
            if (currentNodeForSearch != null)
            {
                if (OperationGoing && canAcceptCPK)
                {

                    workError();
                    return;
                }

                listView1.Items.Clear();


                listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8);
                listView1.View = View.Details;

                //add folders
                for (int i = 0; i < currentNodeForSearch.Nodes.Count; i++)
                {

                    file = new ListViewItem(currentNodeForSearch.Nodes[i].Text, 0);
                    file.Tag = currentNodeForSearch.Nodes[i].Tag;




                    folderCount = currentNodeForSearch.Nodes[i].Nodes.Count;
                    fileCount = (currentNodeForSearch.Nodes[i].Tag == null ? 0 : currentNodeForSearch.Nodes[i].Tag.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Length);
                    fileStr = (fileCount == 1 ? "File" : "Files");
                    folderstr = (folderCount == 1 ? "Folder" : "Folders");
                    file.SubItems.Add(string.Format("{0} {3} and {1} {2}", folderCount, fileCount, fileStr, folderstr));
                    listView1.Items.Add(file);

                }

                // add files
                if (currentNodeForSearch.Tag != null)
                {

                    files = currentNodeForSearch.Tag.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    for (int i = 0; i < files.Length; i++)
                    {
                        file = new ListViewItem(c.cpk.FileTable[Convert.ToInt32(files[i])].FileName.ToString(), 1);
                        file.Tag = Convert.ToInt32(files[i]);
                        file.SubItems.Add(RoundBytes(Convert.ToUInt64(c.cpk.FileTable[Convert.ToInt32(files[i])].ExtractSize)));
                        listView1.Items.Add(file);



                    }
                }


                label1.Text = "In: " + currentNodeForSearch.FullPath;
                if (isLargeView)
                {
                    listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16);
                    listView1.View = View.Tile;



                }
                
                treeView1.SelectedNode = currentNodeForSearch;
            }


        }

        private void treeView1_MouseClick(object sender, MouseEventArgs e)
        {
            // Make sure this is the right button.
            if (e.Button != MouseButtons.Right) return;

            // Select this node.
            TreeNode node_here = treeView1.GetNodeAt(e.X, e.Y);
            treeView1.SelectedNode = node_here;

            bool shouldShowContext = true;
            // See if we got a node.
            if (node_here == null) return;

            if (shouldShowContext)
                contextMenuStrip2.Show(treeView1, e.X, e.Y);

        }

        private void extractFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
         
            bool isExtraction = isExtraction = (treeView1.SelectedNode.ImageIndex == 0
            || treeView1.SelectedNode.ImageIndex == 1);

            if (!isExtraction)
            {
                MessageBox.Show("unknown extraction operation.", "Extraction",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            OperationGoing = true;
            canAcceptCPK = false;
            progressBar1.Visible = true;
            progressBar1.Maximum = treeView1.SelectedNode.Nodes.Count;

     

          
                if (treeView1.SelectedNode.ImageIndex == 1)

                {
                  
                    // file
                    // MessageBox.Show("TAG OF SINGLE ITEM" +listView1.SelectedItems[i].Tag.ToString());
                    label1.Text = "extracting " + treeView1.SelectedNode.Text;
                    Application.DoEvents();
                    c.ExtractByIndex((int)treeView1.SelectedNode.Tag);
                    label1.Text = treeView1.SelectedNode.Text + " extracted successfully";
                    progressBar1.Increment(1);

                }
                else if (treeView1.SelectedNode.ImageIndex == 0)
                {
                  
                    // folder
                    string t = (treeView1.SelectedNode.FullPath ).Substring(treeView1.Nodes[0].Text.Length + 1).Replace((char)0x5C, (char)0x2F);

                 
                    extractBulkMiniWorker(t);

                    progressBar1.Increment(1);
                }
        

            
            progressBar1.Value = 0;
            progressBar1.Visible = false;

            canAcceptCPK = true;
            OperationGoing = false;
        }

        private void copyFolderPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string s = treeView1.SelectedNode.FullPath;

            Clipboard.SetText(s);
        }

        private void copyAbsloutePathToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string s2 = treeView1.SelectedNode.FullPath.Substring(c.CPKName.Length + 1);
            string s = c.extractPath + s2;
            Clipboard.SetText(s);
        }

        private void copyFolderNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string s = treeView1.SelectedNode.Text;

            Clipboard.SetText(s);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (cacheFile != null)
                 cacheFile.saveCacheFile(cacheFilePath);
        }

        private void contextMenuStrip2_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }

}

