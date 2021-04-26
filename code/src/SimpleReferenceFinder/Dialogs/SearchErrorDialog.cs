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
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;

namespace Plexdata.SimpleReferenceFinder.Dialogs
{
    public partial class SearchErrorDialog : Form
    {
        private delegate void AddErrorDelegate(SearchError error);

        private readonly ObservableCollection<SearchError> errors = null;

        public SearchErrorDialog(ObservableCollection<SearchError> errors)
            : base()
        {
            this.InitializeComponent();

            // Prevent flickering on list view updates.
            this.lstErrors.GetType()
                .GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(this.lstErrors, true);

            this.errors = errors ?? new ObservableCollection<SearchError>();
        }

        protected override void OnLoad(EventArgs args)
        {
            base.OnLoad(args);

            try
            {
                for (Int32 index = 0; index < this.errors.Count; index++)
                {
                    this.AddError(this.errors[index]);
                }

                this.lstErrors.AutoResizeColumns(this.errors.Count > 1 ? ColumnHeaderAutoResizeStyle.ColumnContent : ColumnHeaderAutoResizeStyle.HeaderSize);

                this.errors.CollectionChanged += this.OnErrorsCollectionChanged;
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine(exception);
            }
        }

        protected override void OnClosing(CancelEventArgs args)
        {
            try
            {
                this.errors.CollectionChanged -= this.OnErrorsCollectionChanged;
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine(exception);
            }

            base.OnClosing(args);
        }

        private void OnErrorsDoubleClick(Object sender, EventArgs args)
        {
            if (this.lstErrors.SelectedItems.Count == 1)
            {
                ErrorDetailsDialog dialog = new ErrorDetailsDialog((this.lstErrors.SelectedItems[0].Tag as SearchError)?.Exception);
                dialog.ShowDialog(this);
            }
        }

        private void OnErrorsCollectionChanged(Object sender, NotifyCollectionChangedEventArgs args)
        {
            try
            {
                if (args.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (var item in args.NewItems)
                    {
                        this.AddError(item as SearchError);
                    }
                }
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine(exception);
            }
        }

        private void AddError(SearchError error)
        {
            if (this.IsDisposed) { return; }

            if (error == null) { return; }

            if (this.InvokeRequired)
            {
                this.Invoke(new AddErrorDelegate(this.AddError), new Object[] { error });
            }
            else
            {
                ListViewItem item = new ListViewItem(error.Exception.Message) { Tag = error };

                item.SubItems.Add(error.Source.FullName);
                item.ToolTipText = "Double click to show details.";

                this.lstErrors.Items.Add(item);
            }
        }
    }
}
