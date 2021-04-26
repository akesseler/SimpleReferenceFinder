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
    partial class SearchErrorDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchErrorDialog));
            this.btnClose = new System.Windows.Forms.Button();
            this.lstErrors = new System.Windows.Forms.ListView();
            this.hdrError = new System.Windows.Forms.ColumnHeader();
            this.hdrSource = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(597, 226);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // lstErrors
            // 
            this.lstErrors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstErrors.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.hdrError,
            this.hdrSource});
            this.lstErrors.FullRowSelect = true;
            this.lstErrors.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstErrors.HideSelection = false;
            this.lstErrors.LabelWrap = false;
            this.lstErrors.Location = new System.Drawing.Point(12, 12);
            this.lstErrors.MultiSelect = false;
            this.lstErrors.Name = "lstErrors";
            this.lstErrors.ShowItemToolTips = true;
            this.lstErrors.Size = new System.Drawing.Size(660, 208);
            this.lstErrors.TabIndex = 1;
            this.lstErrors.UseCompatibleStateImageBehavior = false;
            this.lstErrors.View = System.Windows.Forms.View.Details;
            this.lstErrors.DoubleClick += new System.EventHandler(this.OnErrorsDoubleClick);
            // 
            // hdrError
            // 
            this.hdrError.Text = "Error";
            // 
            // hdrSource
            // 
            this.hdrSource.Text = "Source";
            // 
            // SearchErrorDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(684, 261);
            this.Controls.Add(this.lstErrors);
            this.Controls.Add(this.btnClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(500, 300);
            this.Name = "SearchErrorDialog";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Search Errors";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ListView lstErrors;
        private System.Windows.Forms.ColumnHeader hdrSource;
        private System.Windows.Forms.ColumnHeader hdrError;
    }
}