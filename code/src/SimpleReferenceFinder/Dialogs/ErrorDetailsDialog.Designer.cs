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

namespace Plexdata.SimpleReferenceFinder.Dialogs
{
    partial class ErrorDetailsDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Node1");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Node2");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Node0", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ErrorDetailsDialog));
            this.btnClose = new System.Windows.Forms.Button();
            this.spcDetails = new Plexdata.SimpleReferenceFinder.Controls.SplitContainerEx();
            this.trvDetails = new System.Windows.Forms.TreeView();
            this.txtDetails = new System.Windows.Forms.TextBox();
            this.cmsMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.lblType = new System.Windows.Forms.Label();
            this.lblText = new System.Windows.Forms.Label();
            this.txtType = new System.Windows.Forms.TextBox();
            this.txtText = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.spcDetails)).BeginInit();
            this.spcDetails.Panel1.SuspendLayout();
            this.spcDetails.Panel2.SuspendLayout();
            this.spcDetails.SuspendLayout();
            this.cmsMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(497, 326);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // spcDetails
            // 
            this.spcDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spcDetails.Location = new System.Drawing.Point(12, 70);
            this.spcDetails.Name = "spcDetails";
            // 
            // spcDetails.Panel1
            // 
            this.spcDetails.Panel1.Controls.Add(this.trvDetails);
            // 
            // spcDetails.Panel2
            // 
            this.spcDetails.Panel2.Controls.Add(this.txtDetails);
            this.spcDetails.Size = new System.Drawing.Size(560, 250);
            this.spcDetails.SplitterArrangeEnabled = true;
            this.spcDetails.SplitterArrangePosition = 30;
            this.spcDetails.SplitterDistance = 186;
            this.spcDetails.TabIndex = 5;
            this.spcDetails.TabStop = false;
            // 
            // trvDetails
            // 
            this.trvDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvDetails.HideSelection = false;
            this.trvDetails.Location = new System.Drawing.Point(0, 0);
            this.trvDetails.Name = "trvDetails";
            treeNode1.Name = "Node1";
            treeNode1.Text = "Node1";
            treeNode2.Name = "Node2";
            treeNode2.Text = "Node2";
            treeNode3.Name = "Node0";
            treeNode3.Text = "Node0";
            this.trvDetails.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3});
            this.trvDetails.Size = new System.Drawing.Size(186, 250);
            this.trvDetails.TabIndex = 0;
            // 
            // txtDetails
            // 
            this.txtDetails.BackColor = System.Drawing.SystemColors.Window;
            this.txtDetails.ContextMenuStrip = this.cmsMain;
            this.txtDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDetails.Location = new System.Drawing.Point(0, 0);
            this.txtDetails.Multiline = true;
            this.txtDetails.Name = "txtDetails";
            this.txtDetails.ReadOnly = true;
            this.txtDetails.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtDetails.Size = new System.Drawing.Size(370, 250);
            this.txtDetails.TabIndex = 0;
            this.txtDetails.WordWrap = false;
            // 
            // cmsMain
            // 
            this.cmsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCopy});
            this.cmsMain.Name = "cmsMain";
            this.cmsMain.Size = new System.Drawing.Size(172, 26);
            // 
            // mnuCopy
            // 
            this.mnuCopy.Name = "mnuCopy";
            this.mnuCopy.Size = new System.Drawing.Size(171, 22);
            this.mnuCopy.Text = "&Copy to Clipboard";
            this.mnuCopy.Click += new System.EventHandler(this.OnMenuCopyClick);
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(12, 15);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(34, 15);
            this.lblType.TabIndex = 1;
            this.lblType.Text = "T&ype:";
            // 
            // lblText
            // 
            this.lblText.AutoSize = true;
            this.lblText.Location = new System.Drawing.Point(12, 44);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(31, 15);
            this.lblText.TabIndex = 3;
            this.lblText.Text = "Te&xt:";
            // 
            // txtType
            // 
            this.txtType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtType.BackColor = System.Drawing.SystemColors.Window;
            this.txtType.ContextMenuStrip = this.cmsMain;
            this.txtType.Location = new System.Drawing.Point(52, 12);
            this.txtType.Name = "txtType";
            this.txtType.ReadOnly = true;
            this.txtType.Size = new System.Drawing.Size(520, 23);
            this.txtType.TabIndex = 2;
            // 
            // txtText
            // 
            this.txtText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtText.BackColor = System.Drawing.SystemColors.Window;
            this.txtText.ContextMenuStrip = this.cmsMain;
            this.txtText.Location = new System.Drawing.Point(52, 41);
            this.txtText.Name = "txtText";
            this.txtText.ReadOnly = true;
            this.txtText.Size = new System.Drawing.Size(520, 23);
            this.txtText.TabIndex = 4;
            // 
            // ErrorDetailsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.txtText);
            this.Controls.Add(this.txtType);
            this.Controls.Add(this.lblText);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.spcDetails);
            this.Controls.Add(this.btnClose);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(500, 300);
            this.Name = "ErrorDetailsDialog";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Error Details";
            this.spcDetails.Panel1.ResumeLayout(false);
            this.spcDetails.Panel2.ResumeLayout(false);
            this.spcDetails.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spcDetails)).EndInit();
            this.spcDetails.ResumeLayout(false);
            this.cmsMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private Plexdata.SimpleReferenceFinder.Controls.SplitContainerEx spcDetails;
        private System.Windows.Forms.TreeView trvDetails;
        private System.Windows.Forms.TextBox txtDetails;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label lblText;
        private System.Windows.Forms.TextBox txtType;
        private System.Windows.Forms.TextBox txtText;
        private System.Windows.Forms.ContextMenuStrip cmsMain;
        private System.Windows.Forms.ToolStripMenuItem mnuCopy;
    }
}