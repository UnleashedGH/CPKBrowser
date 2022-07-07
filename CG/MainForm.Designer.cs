/*
 * Created by SharpDevelop.
 * User: Unleashed
 * Date: 2/3/2019
 * Time: 5:01 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace CG
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.ComponentModel.BackgroundWorker backgroundWorker1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem extractAllToolStripMenuItem;
		private System.ComponentModel.BackgroundWorker backgroundWorker2;
		private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.ImageList imageList2;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnSearch;

		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// 
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.markForReplaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.replaceFileNoRebuildToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyFileNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyFilePathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAbsloutePathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.installM2CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveCPKToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setAsDefaultProgramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aDMINSetAsDefaultProgramm2cToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extractAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compareCPKToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createM2CFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cacheToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.label1 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.backgroundWorker3 = new System.ComponentModel.BackgroundWorker();
            this.openFileDialog3 = new System.Windows.Forms.OpenFileDialog();
            this.btnRevert = new System.Windows.Forms.Button();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.extractFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyFolderNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyFolderPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAbsloutePathToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.AllowColumnReorder = true;
            this.listView1.AllowDrop = true;
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView1.FullRowSelect = true;
            this.listView1.LargeImageList = this.imageList2;
            this.listView1.Location = new System.Drawing.Point(3, 3);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(357, 511);
            this.listView1.SmallImageList = this.imageList1;
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Tile;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.listView1_DragDrop);
            this.listView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.listView1_DragEnter);
            this.listView1.DoubleClick += new System.EventHandler(this.ListView1DoubleClick);
            this.listView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyDown);
            this.listView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ListView1MouseClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 265;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "";
            this.columnHeader2.Width = 95;
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "output-onlinepngtools.png");
            this.imageList2.Images.SetKeyName(1, "shell32_1.ico");
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "cbrj2-9n7jf.ico");
            this.imageList1.Images.SetKeyName(1, "cb950-qv640.ico");
            this.imageList1.Images.SetKeyName(2, "packing.ico");
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(0, 0);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.markForReplaceToolStripMenuItem,
            this.replaceFileNoRebuildToolStripMenuItem,
            this.copyFileNameToolStripMenuItem,
            this.copyFilePathToolStripMenuItem,
            this.copyAbsloutePathToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.ShowImageMargin = false;
            this.contextMenuStrip1.Size = new System.Drawing.Size(274, 158);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripMenuItem1.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(273, 22);
            this.toolStripMenuItem1.Text = "Extract";
            this.toolStripMenuItem1.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.toolStripMenuItem1.Click += new System.EventHandler(this.ToolStripMenuItem1Click);
            // 
            // markForReplaceToolStripMenuItem
            // 
            this.markForReplaceToolStripMenuItem.Name = "markForReplaceToolStripMenuItem";
            this.markForReplaceToolStripMenuItem.Size = new System.Drawing.Size(273, 22);
            this.markForReplaceToolStripMenuItem.Text = "Mark for Replace (requires rebuilding CPK)";
            this.markForReplaceToolStripMenuItem.Click += new System.EventHandler(this.markForReplaceToolStripMenuItem_Click);
            // 
            // replaceFileNoRebuildToolStripMenuItem
            // 
            this.replaceFileNoRebuildToolStripMenuItem.Name = "replaceFileNoRebuildToolStripMenuItem";
            this.replaceFileNoRebuildToolStripMenuItem.Size = new System.Drawing.Size(273, 22);
            this.replaceFileNoRebuildToolStripMenuItem.Text = "Replace File (Injection, No Rebuild)";
            this.replaceFileNoRebuildToolStripMenuItem.Click += new System.EventHandler(this.replaceFileNoRebuildToolStripMenuItem_Click);
            // 
            // copyFileNameToolStripMenuItem
            // 
            this.copyFileNameToolStripMenuItem.Name = "copyFileNameToolStripMenuItem";
            this.copyFileNameToolStripMenuItem.Size = new System.Drawing.Size(273, 22);
            this.copyFileNameToolStripMenuItem.Text = "Copy File Name";
            this.copyFileNameToolStripMenuItem.Click += new System.EventHandler(this.copyFileNameToolStripMenuItem_Click);
            // 
            // copyFilePathToolStripMenuItem
            // 
            this.copyFilePathToolStripMenuItem.Name = "copyFilePathToolStripMenuItem";
            this.copyFilePathToolStripMenuItem.Size = new System.Drawing.Size(273, 22);
            this.copyFilePathToolStripMenuItem.Text = "Copy File Path";
            this.copyFilePathToolStripMenuItem.Click += new System.EventHandler(this.copyFilePathToolStripMenuItem_Click);
            // 
            // copyAbsloutePathToolStripMenuItem
            // 
            this.copyAbsloutePathToolStripMenuItem.Name = "copyAbsloutePathToolStripMenuItem";
            this.copyAbsloutePathToolStripMenuItem.Size = new System.Drawing.Size(273, 22);
            this.copyAbsloutePathToolStripMenuItem.Text = "Copy Absolute Path";
            this.copyAbsloutePathToolStripMenuItem.Click += new System.EventHandler(this.copyAbsloutePathToolStripMenuItem_Click);
            // 
            // treeView1
            // 
            this.treeView1.AllowDrop = true;
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList2;
            this.treeView1.Indent = 50;
            this.treeView1.ItemHeight = 64;
            this.treeView1.Location = new System.Drawing.Point(3, 3);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(273, 511);
            this.treeView1.TabIndex = 1;
            this.treeView1.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeSelect);
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView1AfterSelect);
            this.treeView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeView1_DragDrop);
            this.treeView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeView1_DragEnter);
            this.treeView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseClick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "CPK Files|*.cpk";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.cacheToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(673, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.installM2CToolStripMenuItem,
            this.saveCPKToolStripMenuItem,
            this.setAsDefaultProgramToolStripMenuItem,
            this.aDMINSetAsDefaultProgramm2cToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(281, 22);
            this.openToolStripMenuItem.Text = "Open CPK";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItemClick);
            // 
            // installM2CToolStripMenuItem
            // 
            this.installM2CToolStripMenuItem.Name = "installM2CToolStripMenuItem";
            this.installM2CToolStripMenuItem.Size = new System.Drawing.Size(281, 22);
            this.installM2CToolStripMenuItem.Text = "Install M2C";
            this.installM2CToolStripMenuItem.Click += new System.EventHandler(this.installM2CToolStripMenuItem_Click);
            // 
            // saveCPKToolStripMenuItem
            // 
            this.saveCPKToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.saveCPKToolStripMenuItem.Name = "saveCPKToolStripMenuItem";
            this.saveCPKToolStripMenuItem.Size = new System.Drawing.Size(281, 22);
            this.saveCPKToolStripMenuItem.Text = "Save CPK (Rebuild)";
            this.saveCPKToolStripMenuItem.Click += new System.EventHandler(this.saveCPKToolStripMenuItem_Click);
            // 
            // setAsDefaultProgramToolStripMenuItem
            // 
            this.setAsDefaultProgramToolStripMenuItem.Name = "setAsDefaultProgramToolStripMenuItem";
            this.setAsDefaultProgramToolStripMenuItem.Size = new System.Drawing.Size(281, 22);
            this.setAsDefaultProgramToolStripMenuItem.Text = "[ADMIN] Set as Default Program (.cpk)";
            this.setAsDefaultProgramToolStripMenuItem.Click += new System.EventHandler(this.setAsDefaultProgramToolStripMenuItem_Click);
            // 
            // aDMINSetAsDefaultProgramm2cToolStripMenuItem
            // 
            this.aDMINSetAsDefaultProgramm2cToolStripMenuItem.Name = "aDMINSetAsDefaultProgramm2cToolStripMenuItem";
            this.aDMINSetAsDefaultProgramm2cToolStripMenuItem.Size = new System.Drawing.Size(281, 22);
            this.aDMINSetAsDefaultProgramm2cToolStripMenuItem.Text = "[ADMIN] Set as Default Program (.m2c)";
            this.aDMINSetAsDefaultProgramm2cToolStripMenuItem.Click += new System.EventHandler(this.aDMINSetAsDefaultProgramm2cToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(281, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItemClick);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.extractAllToolStripMenuItem,
            this.compareCPKToolStripMenuItem,
            this.createM2CFileToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            this.toolsToolStripMenuItem.Click += new System.EventHandler(this.toolsToolStripMenuItem_Click);
            // 
            // extractAllToolStripMenuItem
            // 
            this.extractAllToolStripMenuItem.Enabled = false;
            this.extractAllToolStripMenuItem.Name = "extractAllToolStripMenuItem";
            this.extractAllToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.extractAllToolStripMenuItem.Text = "Extract All";
            this.extractAllToolStripMenuItem.Click += new System.EventHandler(this.ExtractAllToolStripMenuItemClick);
            // 
            // compareCPKToolStripMenuItem
            // 
            this.compareCPKToolStripMenuItem.Name = "compareCPKToolStripMenuItem";
            this.compareCPKToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.compareCPKToolStripMenuItem.Text = "Compare CPK";
            this.compareCPKToolStripMenuItem.Click += new System.EventHandler(this.compareCPKToolStripMenuItem_Click);
            // 
            // createM2CFileToolStripMenuItem
            // 
            this.createM2CFileToolStripMenuItem.Name = "createM2CFileToolStripMenuItem";
            this.createM2CFileToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.createM2CFileToolStripMenuItem.Text = "Create M2C File";
            this.createM2CFileToolStripMenuItem.Click += new System.EventHandler(this.createM2CFileToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // cacheToolStripMenuItem
            // 
            this.cacheToolStripMenuItem.Name = "cacheToolStripMenuItem";
            this.cacheToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.cacheToolStripMenuItem.Text = "Recent";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(151, 20);
            this.aboutToolStripMenuItem.Text = "Unleashed @ The Citadel";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItemClick);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker1DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker1RunWorkerCompleted);
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.WorkerReportsProgress = true;
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker2DoWork);
            this.backgroundWorker2.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker2_ProgressChanged);
            this.backgroundWorker2.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker2RunWorkerCompleted);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 594);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(276, 23);
            this.label1.TabIndex = 3;
            this.label1.Text = "Status: idle";
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.Description = "Select Extract Path (the default path is the CPK directory)";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 66);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listView1);
            this.splitContainer1.Size = new System.Drawing.Size(646, 517);
            this.splitContainer1.SplitterDistance = 279;
            this.splitContainer1.TabIndex = 7;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Enabled = false;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(383, 32);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(272, 26);
            this.textBox1.TabIndex = 8;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Enabled = false;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Location = new System.Drawing.Point(298, 27);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(79, 36);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearchClick);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.Enabled = false;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(36, 34);
            this.toolStripButton2.Text = "Back";
            this.toolStripButton2.Click += new System.EventHandler(this.ToolStripButton2Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.CanOverflow = false;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(9, 29);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(118, 37);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(65, 34);
            this.toolStripButton1.Text = "Open CPK";
            this.toolStripButton1.Click += new System.EventHandler(this.OpenToolStripMenuItemClick);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(298, 589);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(360, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 10;
            this.progressBar1.Visible = false;
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "Replace With..";
            // 
            // backgroundWorker3
            // 
            this.backgroundWorker3.WorkerReportsProgress = true;
            this.backgroundWorker3.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker3_DoWork);
            this.backgroundWorker3.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker3_ProgressChanged);
            this.backgroundWorker3.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker3_RunWorkerCompleted);
            // 
            // openFileDialog3
            // 
            this.openFileDialog3.Filter = "M2C Files|*.m2c";
            this.openFileDialog3.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog3_FileOk);
            // 
            // btnRevert
            // 
            this.btnRevert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRevert.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRevert.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRevert.Location = new System.Drawing.Point(209, 27);
            this.btnRevert.Name = "btnRevert";
            this.btnRevert.Size = new System.Drawing.Size(79, 36);
            this.btnRevert.TabIndex = 11;
            this.btnRevert.Text = "Revert";
            this.btnRevert.UseVisualStyleBackColor = true;
            this.btnRevert.Visible = false;
            this.btnRevert.Click += new System.EventHandler(this.button1_Click);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(0, 0);
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.extractFolderToolStripMenuItem,
            this.copyFolderNameToolStripMenuItem,
            this.copyFolderPathToolStripMenuItem,
            this.copyAbsloutePathToolStripMenuItem1});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.ShowImageMargin = false;
            this.contextMenuStrip2.Size = new System.Drawing.Size(155, 92);
            this.contextMenuStrip2.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip2_Opening);
            // 
            // extractFolderToolStripMenuItem
            // 
            this.extractFolderToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.extractFolderToolStripMenuItem.Name = "extractFolderToolStripMenuItem";
            this.extractFolderToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.extractFolderToolStripMenuItem.Text = "Extract Folder";
            this.extractFolderToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.extractFolderToolStripMenuItem.Click += new System.EventHandler(this.extractFolderToolStripMenuItem_Click);
            // 
            // copyFolderNameToolStripMenuItem
            // 
            this.copyFolderNameToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.copyFolderNameToolStripMenuItem.Name = "copyFolderNameToolStripMenuItem";
            this.copyFolderNameToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.copyFolderNameToolStripMenuItem.Text = "Copy Folder Name";
            this.copyFolderNameToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.copyFolderNameToolStripMenuItem.Click += new System.EventHandler(this.copyFolderNameToolStripMenuItem_Click);
            // 
            // copyFolderPathToolStripMenuItem
            // 
            this.copyFolderPathToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.copyFolderPathToolStripMenuItem.Name = "copyFolderPathToolStripMenuItem";
            this.copyFolderPathToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.copyFolderPathToolStripMenuItem.Text = "Copy Folder Path";
            this.copyFolderPathToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.copyFolderPathToolStripMenuItem.Click += new System.EventHandler(this.copyFolderPathToolStripMenuItem_Click);
            // 
            // copyAbsloutePathToolStripMenuItem1
            // 
            this.copyAbsloutePathToolStripMenuItem1.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.copyAbsloutePathToolStripMenuItem1.Name = "copyAbsloutePathToolStripMenuItem1";
            this.copyAbsloutePathToolStripMenuItem1.Size = new System.Drawing.Size(154, 22);
            this.copyAbsloutePathToolStripMenuItem1.Text = "Copy Absolute Path";
            this.copyAbsloutePathToolStripMenuItem1.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.copyAbsloutePathToolStripMenuItem1.Click += new System.EventHandler(this.copyAbsloutePathToolStripMenuItem1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 614);
            this.Controls.Add(this.btnRevert);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "CriPack Browser v6.7";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainFormLoad);
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ToolStripMenuItem saveCPKToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem compareCPKToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem markForReplaceToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.ComponentModel.BackgroundWorker backgroundWorker3;
        private System.Windows.Forms.ToolStripMenuItem setAsDefaultProgramToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem installM2CToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createM2CFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aDMINSetAsDefaultProgramm2cToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog3;
        private System.Windows.Forms.ToolStripMenuItem replaceFileNoRebuildToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyFileNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyFilePathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyAbsloutePathToolStripMenuItem;
        private System.Windows.Forms.Button btnRevert;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem extractFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyFolderPathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyAbsloutePathToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem copyFolderNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cacheToolStripMenuItem;
    }
		}
		

	

