/*
 * MIT License
 * 
 * Copyright (c) 2021 plexdata.de
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

namespace Plexdata.SimpleReferenceFinder
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.cmbSearchType = new System.Windows.Forms.ComboBox();
            this.lblSearchType = new System.Windows.Forms.Label();
            this.tbsMain = new System.Windows.Forms.ToolStrip();
            this.tbbExit = new System.Windows.Forms.ToolStripButton();
            this.tbsSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tbbPlay = new System.Windows.Forms.ToolStripButton();
            this.tbbStop = new System.Windows.Forms.ToolStripButton();
            this.tbsSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnReport = new System.Windows.Forms.ToolStripButton();
            this.tbsSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tbbInfo = new System.Windows.Forms.ToolStripDropDownButton();
            this.mnuInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.grpOptions = new System.Windows.Forms.GroupBox();
            this.chkChangeSeparator = new Plexdata.SimpleReferenceFinder.Controls.SwitchBox();
            this.chkIncludeFolder = new Plexdata.SimpleReferenceFinder.Controls.SwitchBox();
            this.lblTargetPattern = new System.Windows.Forms.Label();
            this.txtTargetPattern = new System.Windows.Forms.TextBox();
            this.chkSearchRecursive = new Plexdata.SimpleReferenceFinder.Controls.SwitchBox();
            this.chkCaseSensitive = new Plexdata.SimpleReferenceFinder.Controls.SwitchBox();
            this.txtSourcePattern = new System.Windows.Forms.TextBox();
            this.lblSourcePattern = new System.Windows.Forms.Label();
            this.btnBaseFolder = new System.Windows.Forms.Button();
            this.txtBaseFolder = new System.Windows.Forms.TextBox();
            this.lblBaseFolder = new System.Windows.Forms.Label();
            this.tipMain = new System.Windows.Forms.ToolTip(this.components);
            this.stpMain = new System.Windows.Forms.StatusStrip();
            this.stlMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.stlErrors = new System.Windows.Forms.ToolStripStatusLabel();
            this.spbProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.lstReferenced = new Plexdata.SimpleReferenceFinder.Controls.SearchResultListView();
            this.hdrReferencedFile = new System.Windows.Forms.ColumnHeader();
            this.hdrReferencedPath = new System.Windows.Forms.ColumnHeader();
            this.hdrReferencedCount = new System.Windows.Forms.ColumnHeader();
            this.hdrReferencedLines = new System.Windows.Forms.ColumnHeader();
            this.cmsList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuShowResult = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuCopyOne = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCopyAll = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuOpenFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.lstUnreferenced = new Plexdata.SimpleReferenceFinder.Controls.SearchResultListView();
            this.hdrUnreferencedFile = new System.Windows.Forms.ColumnHeader();
            this.hdrUnreferencedPath = new System.Windows.Forms.ColumnHeader();
            this.spcResults = new Plexdata.SimpleReferenceFinder.Controls.SplitContainerEx();
            this.grpReferenced = new System.Windows.Forms.GroupBox();
            this.grpUnreferenced = new System.Windows.Forms.GroupBox();
            this.tbsMain.SuspendLayout();
            this.grpOptions.SuspendLayout();
            this.stpMain.SuspendLayout();
            this.cmsList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spcResults)).BeginInit();
            this.spcResults.Panel1.SuspendLayout();
            this.spcResults.Panel2.SuspendLayout();
            this.spcResults.SuspendLayout();
            this.grpReferenced.SuspendLayout();
            this.grpUnreferenced.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbSearchType
            // 
            this.cmbSearchType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSearchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearchType.Location = new System.Drawing.Point(120, 25);
            this.cmbSearchType.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.cmbSearchType.Name = "cmbSearchType";
            this.cmbSearchType.Size = new System.Drawing.Size(727, 23);
            this.cmbSearchType.TabIndex = 1;
            this.tipMain.SetToolTip(this.cmbSearchType, "Choose type of references to be resolved.");
            // 
            // lblSearchType
            // 
            this.lblSearchType.AutoSize = true;
            this.lblSearchType.Location = new System.Drawing.Point(13, 28);
            this.lblSearchType.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
            this.lblSearchType.Name = "lblSearchType";
            this.lblSearchType.Size = new System.Drawing.Size(72, 15);
            this.lblSearchType.TabIndex = 0;
            this.lblSearchType.Text = "Search &Type:";
            // 
            // tbsMain
            // 
            this.tbsMain.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tbsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbbExit,
            this.tbsSeparator1,
            this.tbbPlay,
            this.tbbStop,
            this.tbsSeparator2,
            this.btnReport,
            this.tbsSeparator3,
            this.tbbInfo});
            this.tbsMain.Location = new System.Drawing.Point(0, 0);
            this.tbsMain.Name = "tbsMain";
            this.tbsMain.Size = new System.Drawing.Size(884, 37);
            this.tbsMain.TabIndex = 0;
            // 
            // tbbExit
            // 
            this.tbbExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbbExit.Image = global::Plexdata.SimpleReferenceFinder.Properties.Resources.exit_24x24;
            this.tbbExit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbbExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbExit.Name = "tbbExit";
            this.tbbExit.Padding = new System.Windows.Forms.Padding(3);
            this.tbbExit.Size = new System.Drawing.Size(34, 34);
            this.tbbExit.Text = "Exit";
            this.tbbExit.ToolTipText = "Close main window and exit application.";
            this.tbbExit.Click += new System.EventHandler(this.OnButtonExitClick);
            // 
            // tbsSeparator1
            // 
            this.tbsSeparator1.Name = "tbsSeparator1";
            this.tbsSeparator1.Size = new System.Drawing.Size(6, 37);
            // 
            // tbbPlay
            // 
            this.tbbPlay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbbPlay.Image = global::Plexdata.SimpleReferenceFinder.Properties.Resources.play_24x24;
            this.tbbPlay.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbbPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbPlay.Name = "tbbPlay";
            this.tbbPlay.Padding = new System.Windows.Forms.Padding(3);
            this.tbbPlay.Size = new System.Drawing.Size(34, 34);
            this.tbbPlay.Text = "Play";
            this.tbbPlay.ToolTipText = "Run program for current settings.";
            this.tbbPlay.Click += new System.EventHandler(this.OnButtonPlayClick);
            // 
            // tbbStop
            // 
            this.tbbStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbbStop.Image = global::Plexdata.SimpleReferenceFinder.Properties.Resources.stop_24x24;
            this.tbbStop.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbbStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbStop.Name = "tbbStop";
            this.tbbStop.Padding = new System.Windows.Forms.Padding(3);
            this.tbbStop.Size = new System.Drawing.Size(34, 34);
            this.tbbStop.Text = "Stop";
            this.tbbStop.ToolTipText = "Stop program execution.";
            this.tbbStop.Click += new System.EventHandler(this.OnButtonStopClick);
            // 
            // tbsSeparator2
            // 
            this.tbsSeparator2.Name = "tbsSeparator2";
            this.tbsSeparator2.Size = new System.Drawing.Size(6, 37);
            // 
            // btnReport
            // 
            this.btnReport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnReport.Image = global::Plexdata.SimpleReferenceFinder.Properties.Resources.report_24x24;
            this.btnReport.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnReport.Name = "btnReport";
            this.btnReport.Padding = new System.Windows.Forms.Padding(3);
            this.btnReport.Size = new System.Drawing.Size(34, 34);
            this.btnReport.Text = "Report";
            this.btnReport.ToolTipText = "Show search result report.";
            this.btnReport.Click += new System.EventHandler(this.OnButtonReportClick);
            // 
            // tbsSeparator3
            // 
            this.tbsSeparator3.Name = "tbsSeparator3";
            this.tbsSeparator3.Size = new System.Drawing.Size(6, 37);
            // 
            // tbbInfo
            // 
            this.tbbInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbbInfo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuInfo,
            this.mnuHelp});
            this.tbbInfo.Image = global::Plexdata.SimpleReferenceFinder.Properties.Resources.info_24x24;
            this.tbbInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbInfo.Name = "tbbInfo";
            this.tbbInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tbbInfo.Size = new System.Drawing.Size(43, 34);
            this.tbbInfo.Text = "Info";
            this.tbbInfo.ToolTipText = "Show info or help.";
            // 
            // mnuInfo
            // 
            this.mnuInfo.Name = "mnuInfo";
            this.mnuInfo.Size = new System.Drawing.Size(108, 22);
            this.mnuInfo.Text = "&Info...";
            this.mnuInfo.ToolTipText = "Show program info box.";
            this.mnuInfo.Click += new System.EventHandler(this.OnMenuAboutClick);
            // 
            // mnuHelp
            // 
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(108, 22);
            this.mnuHelp.Text = "&Help...";
            this.mnuHelp.ToolTipText = "Show program help.";
            this.mnuHelp.Click += new System.EventHandler(this.OnMenuHelpClick);
            // 
            // grpOptions
            // 
            this.grpOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpOptions.Controls.Add(this.chkChangeSeparator);
            this.grpOptions.Controls.Add(this.chkIncludeFolder);
            this.grpOptions.Controls.Add(this.lblTargetPattern);
            this.grpOptions.Controls.Add(this.txtTargetPattern);
            this.grpOptions.Controls.Add(this.chkSearchRecursive);
            this.grpOptions.Controls.Add(this.chkCaseSensitive);
            this.grpOptions.Controls.Add(this.txtSourcePattern);
            this.grpOptions.Controls.Add(this.lblSourcePattern);
            this.grpOptions.Controls.Add(this.btnBaseFolder);
            this.grpOptions.Controls.Add(this.txtBaseFolder);
            this.grpOptions.Controls.Add(this.lblBaseFolder);
            this.grpOptions.Controls.Add(this.cmbSearchType);
            this.grpOptions.Controls.Add(this.lblSearchType);
            this.grpOptions.Location = new System.Drawing.Point(12, 47);
            this.grpOptions.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.grpOptions.Name = "grpOptions";
            this.grpOptions.Size = new System.Drawing.Size(860, 170);
            this.grpOptions.TabIndex = 1;
            this.grpOptions.TabStop = false;
            this.grpOptions.Text = "Options";
            // 
            // chkChangeSeparator
            // 
            this.chkChangeSeparator.AutoSize = true;
            this.chkChangeSeparator.Checked = true;
            this.chkChangeSeparator.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkChangeSeparator.Location = new System.Drawing.Point(514, 141);
            this.chkChangeSeparator.Name = "chkChangeSeparator";
            this.chkChangeSeparator.Size = new System.Drawing.Size(141, 17);
            this.chkChangeSeparator.SwitchSettings.HandleFillingOn = System.Drawing.Color.White;
            this.chkChangeSeparator.SwitchSettings.HandleOutlineOn = System.Drawing.Color.White;
            this.chkChangeSeparator.SwitchSettings.HandleOutlineWidth = 0F;
            this.chkChangeSeparator.SwitchSettings.HandlePercent = 90;
            this.chkChangeSeparator.SwitchSettings.SliderFillingOn = System.Drawing.Color.DimGray;
            this.chkChangeSeparator.SwitchSettings.SliderOutlineOn = System.Drawing.Color.DimGray;
            this.chkChangeSeparator.SwitchSettings.SliderOutlineWidth = 0F;
            this.chkChangeSeparator.SwitchSettings.SliderPercent = 100;
            this.chkChangeSeparator.TabIndex = 12;
            this.chkChangeSeparator.Text = "Change &Separator";
            this.tipMain.SetToolTip(this.chkChangeSeparator, "Switch on to change path separator \'\\\' into \'/\'.");
            this.chkChangeSeparator.UseVisualStyleBackColor = true;
            // 
            // chkIncludeFolder
            // 
            this.chkIncludeFolder.AutoSize = true;
            this.chkIncludeFolder.Checked = true;
            this.chkIncludeFolder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIncludeFolder.Location = new System.Drawing.Point(387, 141);
            this.chkIncludeFolder.Name = "chkIncludeFolder";
            this.chkIncludeFolder.Size = new System.Drawing.Size(121, 17);
            this.chkIncludeFolder.SwitchSettings.HandleFillingOn = System.Drawing.Color.White;
            this.chkIncludeFolder.SwitchSettings.HandleOutlineOn = System.Drawing.Color.White;
            this.chkIncludeFolder.SwitchSettings.HandleOutlineWidth = 0F;
            this.chkIncludeFolder.SwitchSettings.HandlePercent = 90;
            this.chkIncludeFolder.SwitchSettings.SliderFillingOn = System.Drawing.Color.DimGray;
            this.chkIncludeFolder.SwitchSettings.SliderOutlineOn = System.Drawing.Color.DimGray;
            this.chkIncludeFolder.SwitchSettings.SliderOutlineWidth = 0F;
            this.chkIncludeFolder.SwitchSettings.SliderPercent = 100;
            this.chkIncludeFolder.TabIndex = 11;
            this.chkIncludeFolder.Text = "&Include Folder";
            this.tipMain.SetToolTip(this.chkIncludeFolder, "Switch on to include partial folder paths in reference comparison.");
            this.chkIncludeFolder.UseVisualStyleBackColor = true;
            // 
            // lblTargetPattern
            // 
            this.lblTargetPattern.AutoSize = true;
            this.lblTargetPattern.Location = new System.Drawing.Point(13, 115);
            this.lblTargetPattern.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
            this.lblTargetPattern.Name = "lblTargetPattern";
            this.lblTargetPattern.Size = new System.Drawing.Size(88, 15);
            this.lblTargetPattern.TabIndex = 7;
            this.lblTargetPattern.Text = "&Target Patterns:";
            // 
            // txtTargetPattern
            // 
            this.txtTargetPattern.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTargetPattern.Location = new System.Drawing.Point(120, 112);
            this.txtTargetPattern.Name = "txtTargetPattern";
            this.txtTargetPattern.Size = new System.Drawing.Size(727, 23);
            this.txtTargetPattern.TabIndex = 8;
            this.tipMain.SetToolTip(this.txtTargetPattern, "Provide a semicolon separated list of search patterns, wildcards are supported.");
            // 
            // chkSearchRecursive
            // 
            this.chkSearchRecursive.AutoSize = true;
            this.chkSearchRecursive.Checked = true;
            this.chkSearchRecursive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSearchRecursive.Location = new System.Drawing.Point(246, 141);
            this.chkSearchRecursive.Name = "chkSearchRecursive";
            this.chkSearchRecursive.Size = new System.Drawing.Size(135, 17);
            this.chkSearchRecursive.SwitchSettings.HandleFillingOn = System.Drawing.Color.White;
            this.chkSearchRecursive.SwitchSettings.HandleOutlineOn = System.Drawing.Color.White;
            this.chkSearchRecursive.SwitchSettings.HandleOutlineWidth = 0F;
            this.chkSearchRecursive.SwitchSettings.HandlePercent = 90;
            this.chkSearchRecursive.SwitchSettings.SliderFillingOn = System.Drawing.Color.DimGray;
            this.chkSearchRecursive.SwitchSettings.SliderOutlineOn = System.Drawing.Color.DimGray;
            this.chkSearchRecursive.SwitchSettings.SliderOutlineWidth = 0F;
            this.chkSearchRecursive.SwitchSettings.SliderPercent = 100;
            this.chkSearchRecursive.TabIndex = 10;
            this.chkSearchRecursive.Text = "Search &Recursive";
            this.tipMain.SetToolTip(this.chkSearchRecursive, "Switch on to search in sub-folders too.");
            this.chkSearchRecursive.UseVisualStyleBackColor = true;
            // 
            // chkCaseSensitive
            // 
            this.chkCaseSensitive.AutoSize = true;
            this.chkCaseSensitive.Location = new System.Drawing.Point(120, 141);
            this.chkCaseSensitive.Name = "chkCaseSensitive";
            this.chkCaseSensitive.Size = new System.Drawing.Size(120, 17);
            this.chkCaseSensitive.SwitchSettings.HandleFillingOn = System.Drawing.Color.White;
            this.chkCaseSensitive.SwitchSettings.HandleOutlineOn = System.Drawing.Color.White;
            this.chkCaseSensitive.SwitchSettings.HandleOutlineWidth = 0F;
            this.chkCaseSensitive.SwitchSettings.HandlePercent = 90;
            this.chkCaseSensitive.SwitchSettings.SliderFillingOn = System.Drawing.Color.DimGray;
            this.chkCaseSensitive.SwitchSettings.SliderOutlineOn = System.Drawing.Color.DimGray;
            this.chkCaseSensitive.SwitchSettings.SliderOutlineWidth = 0F;
            this.chkCaseSensitive.SwitchSettings.SliderPercent = 100;
            this.chkCaseSensitive.TabIndex = 9;
            this.chkCaseSensitive.Text = "&Case Sensitive";
            this.tipMain.SetToolTip(this.chkCaseSensitive, "Switch on to search case sensitive.");
            this.chkCaseSensitive.UseVisualStyleBackColor = true;
            // 
            // txtSourcePattern
            // 
            this.txtSourcePattern.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSourcePattern.Location = new System.Drawing.Point(120, 83);
            this.txtSourcePattern.Name = "txtSourcePattern";
            this.txtSourcePattern.Size = new System.Drawing.Size(727, 23);
            this.txtSourcePattern.TabIndex = 6;
            this.tipMain.SetToolTip(this.txtSourcePattern, "Provide a semicolon separated list of search patterns, wildcards are supported.");
            // 
            // lblSourcePattern
            // 
            this.lblSourcePattern.AutoSize = true;
            this.lblSourcePattern.Location = new System.Drawing.Point(13, 86);
            this.lblSourcePattern.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
            this.lblSourcePattern.Name = "lblSourcePattern";
            this.lblSourcePattern.Size = new System.Drawing.Size(92, 15);
            this.lblSourcePattern.TabIndex = 5;
            this.lblSourcePattern.Text = "&Source Patterns:";
            // 
            // btnBaseFolder
            // 
            this.btnBaseFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBaseFolder.Location = new System.Drawing.Point(822, 54);
            this.btnBaseFolder.Name = "btnBaseFolder";
            this.btnBaseFolder.Size = new System.Drawing.Size(25, 23);
            this.btnBaseFolder.TabIndex = 4;
            this.btnBaseFolder.Text = "...";
            this.tipMain.SetToolTip(this.btnBaseFolder, "Browse for current base folder.");
            this.btnBaseFolder.UseVisualStyleBackColor = true;
            this.btnBaseFolder.Click += new System.EventHandler(this.OnButtonBaseFolderClick);
            // 
            // txtBaseFolder
            // 
            this.txtBaseFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBaseFolder.Location = new System.Drawing.Point(120, 54);
            this.txtBaseFolder.Name = "txtBaseFolder";
            this.txtBaseFolder.Size = new System.Drawing.Size(696, 23);
            this.txtBaseFolder.TabIndex = 3;
            this.tipMain.SetToolTip(this.txtBaseFolder, "Base folder to start searching in.");
            // 
            // lblBaseFolder
            // 
            this.lblBaseFolder.AutoSize = true;
            this.lblBaseFolder.Location = new System.Drawing.Point(13, 57);
            this.lblBaseFolder.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
            this.lblBaseFolder.Name = "lblBaseFolder";
            this.lblBaseFolder.Size = new System.Drawing.Size(70, 15);
            this.lblBaseFolder.TabIndex = 2;
            this.lblBaseFolder.Text = "Base &Folder:";
            // 
            // stpMain
            // 
            this.stpMain.BackColor = System.Drawing.Color.Transparent;
            this.stpMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stlMessage,
            this.stlErrors,
            this.spbProgress});
            this.stpMain.Location = new System.Drawing.Point(0, 733);
            this.stpMain.Name = "stpMain";
            this.stpMain.ShowItemToolTips = true;
            this.stpMain.Size = new System.Drawing.Size(884, 28);
            this.stpMain.TabIndex = 4;
            // 
            // stlMessage
            // 
            this.stlMessage.Margin = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.stlMessage.Name = "stlMessage";
            this.stlMessage.Size = new System.Drawing.Size(705, 18);
            this.stlMessage.Spring = true;
            this.stlMessage.Text = "???";
            this.stlMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // stlErrors
            // 
            this.stlErrors.BackColor = System.Drawing.Color.Salmon;
            this.stlErrors.Margin = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.stlErrors.Name = "stlErrors";
            this.stlErrors.Size = new System.Drawing.Size(49, 18);
            this.stlErrors.Tag = "Errors: {0}";
            this.stlErrors.Text = "Errors: 0";
            this.stlErrors.ToolTipText = "Click to show errors.";
            this.stlErrors.Click += new System.EventHandler(this.OnStatusErrorsClick);
            // 
            // spbProgress
            // 
            this.spbProgress.Margin = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.spbProgress.Name = "spbProgress";
            this.spbProgress.Size = new System.Drawing.Size(100, 18);
            // 
            // lstReferenced
            // 
            this.lstReferenced.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.hdrReferencedFile,
            this.hdrReferencedPath,
            this.hdrReferencedCount,
            this.hdrReferencedLines});
            this.lstReferenced.ContextMenuStrip = this.cmsList;
            this.lstReferenced.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstReferenced.FullRowSelect = true;
            this.lstReferenced.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstReferenced.HideSelection = false;
            this.lstReferenced.Location = new System.Drawing.Point(8, 21);
            this.lstReferenced.MultiSelect = false;
            this.lstReferenced.Name = "lstReferenced";
            this.lstReferenced.ShowItemToolTips = true;
            this.lstReferenced.Size = new System.Drawing.Size(844, 211);
            this.lstReferenced.TabIndex = 0;
            this.lstReferenced.UseCompatibleStateImageBehavior = false;
            this.lstReferenced.View = System.Windows.Forms.View.Details;
            this.lstReferenced.DoubleClick += new System.EventHandler(this.OnReferencedDoubleClick);
            // 
            // hdrReferencedFile
            // 
            this.hdrReferencedFile.Text = "File";
            // 
            // hdrReferencedPath
            // 
            this.hdrReferencedPath.Text = "Path";
            // 
            // hdrReferencedCount
            // 
            this.hdrReferencedCount.Text = "Count";
            // 
            // hdrReferencedLines
            // 
            this.hdrReferencedLines.Text = "Lines";
            // 
            // cmsList
            // 
            this.cmsList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuShowResult,
            this.mnuSeparator1,
            this.mnuCopyOne,
            this.mnuCopyAll,
            this.mnuSeparator2,
            this.mnuOpenFolder});
            this.cmsList.Name = "cmsList";
            this.cmsList.Size = new System.Drawing.Size(202, 104);
            this.cmsList.Opening += new System.ComponentModel.CancelEventHandler(this.OnContextMenuListOpening);
            // 
            // mnuShowResult
            // 
            this.mnuShowResult.Name = "mnuShowResult";
            this.mnuShowResult.Size = new System.Drawing.Size(201, 22);
            this.mnuShowResult.Text = "Show &Result Details";
            this.mnuShowResult.Click += new System.EventHandler(this.OnMenuShowResultClick);
            // 
            // mnuSeparator1
            // 
            this.mnuSeparator1.Name = "mnuSeparator1";
            this.mnuSeparator1.Size = new System.Drawing.Size(198, 6);
            // 
            // mnuCopyOne
            // 
            this.mnuCopyOne.Name = "mnuCopyOne";
            this.mnuCopyOne.Size = new System.Drawing.Size(201, 22);
            this.mnuCopyOne.Text = "&Copy to Clipboard";
            this.mnuCopyOne.Click += new System.EventHandler(this.OnMenuCopyOneClick);
            // 
            // mnuCopyAll
            // 
            this.mnuCopyAll.Name = "mnuCopyAll";
            this.mnuCopyAll.Size = new System.Drawing.Size(201, 22);
            this.mnuCopyAll.Text = "Copy &All to Clipboard";
            this.mnuCopyAll.Click += new System.EventHandler(this.OnMenuCopyAllClick);
            // 
            // mnuSeparator2
            // 
            this.mnuSeparator2.Name = "mnuSeparator2";
            this.mnuSeparator2.Size = new System.Drawing.Size(198, 6);
            // 
            // mnuOpenFolder
            // 
            this.mnuOpenFolder.Name = "mnuOpenFolder";
            this.mnuOpenFolder.Size = new System.Drawing.Size(201, 22);
            this.mnuOpenFolder.Text = "&Open Containing Folder";
            this.mnuOpenFolder.Click += new System.EventHandler(this.OnMenuOpenFolderClick);
            // 
            // lstUnreferenced
            // 
            this.lstUnreferenced.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.hdrUnreferencedFile,
            this.hdrUnreferencedPath});
            this.lstUnreferenced.ContextMenuStrip = this.cmsList;
            this.lstUnreferenced.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstUnreferenced.FullRowSelect = true;
            this.lstUnreferenced.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstUnreferenced.HideSelection = false;
            this.lstUnreferenced.Location = new System.Drawing.Point(8, 21);
            this.lstUnreferenced.MultiSelect = false;
            this.lstUnreferenced.Name = "lstUnreferenced";
            this.lstUnreferenced.Size = new System.Drawing.Size(844, 217);
            this.lstUnreferenced.TabIndex = 0;
            this.lstUnreferenced.UseCompatibleStateImageBehavior = false;
            this.lstUnreferenced.View = System.Windows.Forms.View.Details;
            // 
            // hdrUnreferencedFile
            // 
            this.hdrUnreferencedFile.Text = "File";
            // 
            // hdrUnreferencedPath
            // 
            this.hdrUnreferencedPath.Text = "Path";
            // 
            // spcResults
            // 
            this.spcResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spcResults.Location = new System.Drawing.Point(12, 230);
            this.spcResults.Name = "spcResults";
            this.spcResults.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spcResults.Panel1
            // 
            this.spcResults.Panel1.Controls.Add(this.grpReferenced);
            this.spcResults.Panel1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 6);
            // 
            // spcResults.Panel2
            // 
            this.spcResults.Panel2.Controls.Add(this.grpUnreferenced);
            this.spcResults.Size = new System.Drawing.Size(860, 500);
            this.spcResults.SplitterArrangeEnabled = true;
            this.spcResults.SplitterDistance = 248;
            this.spcResults.TabIndex = 1;
            this.spcResults.TabStop = false;
            // 
            // grpReferenced
            // 
            this.grpReferenced.Controls.Add(this.lstReferenced);
            this.grpReferenced.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpReferenced.Location = new System.Drawing.Point(0, 0);
            this.grpReferenced.Name = "grpReferenced";
            this.grpReferenced.Padding = new System.Windows.Forms.Padding(8, 5, 8, 10);
            this.grpReferenced.Size = new System.Drawing.Size(860, 242);
            this.grpReferenced.TabIndex = 2;
            this.grpReferenced.TabStop = false;
            this.grpReferenced.Text = "Referenced";
            // 
            // grpUnreferenced
            // 
            this.grpUnreferenced.Controls.Add(this.lstUnreferenced);
            this.grpUnreferenced.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpUnreferenced.Location = new System.Drawing.Point(0, 0);
            this.grpUnreferenced.Name = "grpUnreferenced";
            this.grpUnreferenced.Padding = new System.Windows.Forms.Padding(8, 5, 8, 10);
            this.grpUnreferenced.Size = new System.Drawing.Size(860, 248);
            this.grpUnreferenced.TabIndex = 3;
            this.grpUnreferenced.TabStop = false;
            this.grpUnreferenced.Text = "Unreferenced";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(884, 761);
            this.Controls.Add(this.spcResults);
            this.Controls.Add(this.stpMain);
            this.Controls.Add(this.grpOptions);
            this.Controls.Add(this.tbsMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(750, 600);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Simple Reference Finder";
            this.tbsMain.ResumeLayout(false);
            this.tbsMain.PerformLayout();
            this.grpOptions.ResumeLayout(false);
            this.grpOptions.PerformLayout();
            this.stpMain.ResumeLayout(false);
            this.stpMain.PerformLayout();
            this.cmsList.ResumeLayout(false);
            this.spcResults.Panel1.ResumeLayout(false);
            this.spcResults.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spcResults)).EndInit();
            this.spcResults.ResumeLayout(false);
            this.grpReferenced.ResumeLayout(false);
            this.grpUnreferenced.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbSearchType;
        private System.Windows.Forms.Label lblSearchType;
        private System.Windows.Forms.ToolStrip tbsMain;
        private System.Windows.Forms.ToolStripButton tbbExit;
        private System.Windows.Forms.GroupBox grpOptions;
        private System.Windows.Forms.ToolTip tipMain;
        private System.Windows.Forms.ToolStripSeparator tbsSeparator1;
        private System.Windows.Forms.ToolStripButton tbbPlay;
        private System.Windows.Forms.ToolStripButton tbbStop;
        private System.Windows.Forms.Label lblBaseFolder;
        private Plexdata.SimpleReferenceFinder.Controls.SwitchBox chkCaseSensitive;
        private System.Windows.Forms.TextBox txtSourcePattern;
        private System.Windows.Forms.Label lblSourcePattern;
        private System.Windows.Forms.Button btnBaseFolder;
        private System.Windows.Forms.TextBox txtBaseFolder;
        private Plexdata.SimpleReferenceFinder.Controls.SwitchBox chkSearchRecursive;
        private System.Windows.Forms.StatusStrip stpMain;
        private System.Windows.Forms.ToolStripStatusLabel stlMessage;
        private Plexdata.SimpleReferenceFinder.Controls.SearchResultListView lstReferenced;
        private System.Windows.Forms.ColumnHeader hdrReferencedFile;
        private System.Windows.Forms.ColumnHeader hdrReferencedPath;
        private System.Windows.Forms.ColumnHeader hdrReferencedCount;
        private System.Windows.Forms.ToolStripProgressBar spbProgress;
        private Plexdata.SimpleReferenceFinder.Controls.SearchResultListView lstUnreferenced;
        private System.Windows.Forms.ColumnHeader hdrUnreferencedFile;
        private System.Windows.Forms.ColumnHeader hdrUnreferencedPath;
        private System.Windows.Forms.Label lblTargetPattern;
        private System.Windows.Forms.TextBox txtTargetPattern;
        private System.Windows.Forms.ToolStripStatusLabel stlErrors;
        private Plexdata.SimpleReferenceFinder.Controls.SplitContainerEx spcResults;
        private System.Windows.Forms.GroupBox grpReferenced;
        private System.Windows.Forms.GroupBox grpUnreferenced;
        private Plexdata.SimpleReferenceFinder.Controls.SwitchBox chkIncludeFolder;
        private System.Windows.Forms.ColumnHeader hdrReferencedLines;
        private System.Windows.Forms.ToolStripSeparator tbsSeparator2;
        private System.Windows.Forms.ToolStripButton btnReport;
        private System.Windows.Forms.ToolStripSeparator tbsSeparator3;
        private System.Windows.Forms.ContextMenuStrip cmsList;
        private System.Windows.Forms.ToolStripMenuItem mnuCopyOne;
        private System.Windows.Forms.ToolStripMenuItem mnuCopyAll;
        private Controls.SwitchBox chkChangeSeparator;
        private System.Windows.Forms.ToolStripSeparator mnuSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuOpenFolder;
        private System.Windows.Forms.ToolStripMenuItem mnuShowResult;
        private System.Windows.Forms.ToolStripSeparator mnuSeparator2;
        private System.Windows.Forms.ToolStripDropDownButton tbbInfo;
        private System.Windows.Forms.ToolStripMenuItem mnuInfo;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
    }
}

