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

using Plexdata.SimpleReferenceFinder.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;

namespace Plexdata.SimpleReferenceFinder.Dialogs
{
    public partial class SearchResultDialog : Form
    {
        private delegate void AssignReferencesDelegate();

        private readonly SearchResult result = null;

        public SearchResultDialog()
            : base()
        {
            this.InitializeComponent();

            // Prevent flickering on list view updates.
            this.lstReferences.GetType()
                .GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(this.lstReferences, true);
        }

        public SearchResultDialog(SearchResult result)
            : this()
        {
            this.result = result;

            if (this.result != null)
            {
                this.result.PropertyChanged += this.OnResultPropertyChanged;
            }
        }

        protected override void OnLoad(EventArgs args)
        {
            base.OnLoad(args);

            if (this.result == null) { return; }

            this.txtName.Text = this.result.Name;
            this.txtPath.Text = this.result.Path;

            this.AssignReferences();
        }

        private void OnMenuCopyClick(Object sender, EventArgs args)
        {
            if (sender is ToolStripMenuItem source)
            {
                if (source.Owner is ContextMenuStrip owner)
                {
                    if (owner.SourceControl is TextBox control)
                    {
                        try { Clipboard.SetText(control.Text); } catch { }
                    }
                }
            }
        }

        private void OnResultPropertyChanged(Object sender, PropertyChangedEventArgs args)
        {
            if (this.IsDisposed) { return; }
            if (this.result == null) { return; }

            if (args.PropertyName == nameof(SearchResult.ReferenceCount) || args.PropertyName == nameof(SearchResult.ReferenceOffsets))
            {
                this.AssignReferences();
            }
        }

        private void AssignReferences()
        {
            if (this.IsDisposed) { return; }
            if (this.result == null) { return; }

            if (this.InvokeRequired)
            {
                this.Invoke(new AssignReferencesDelegate(this.AssignReferences), new Object[] { });
            }
            else
            {
                try
                {
                    this.lstReferences.BeginUpdate();

                    this.lstReferences.Items.Clear();

                    if (this.result.IsReferenced)
                    {
                        List<ListViewItem> items = new List<ListViewItem>();

                        foreach (SearchResult reference in this.result.References)
                        {
                            ListViewItem item = new ListViewItem(reference.Name) { Tag = reference };

                            item.SubItems.Add(reference.Path);
                            item.SubItems.Add(reference.GetDisplayReferenceLines(false));

                            items.Add(item);
                        }

                        this.lstReferences.Items.AddRange(items.ToArray());

                        this.hdrName.Width = -1;
                        this.hdrPath.Width = -2;
                    }
                }
                catch (Exception exception)
                {
                    System.Diagnostics.Debug.WriteLine(exception);
                }
                finally
                {
                    this.lstReferences.EndUpdate();
                }
            }
        }
    }
}
