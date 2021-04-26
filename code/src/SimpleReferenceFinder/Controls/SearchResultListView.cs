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
using System.Windows.Forms;

namespace Plexdata.SimpleReferenceFinder.Controls
{
    public class SearchResultListView : ListView
    {
        private readonly List<SearchResult> searchResults = new List<SearchResult>();

        public SearchResultListView()
            : base()
        {
            base.DoubleBuffered = true;
            base.VirtualMode = this.VirtualMode;
            base.VirtualListSize = this.searchResults.Count;
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SearchResult SelectedResult
        {
            get
            {
                if (base.SelectedIndices.Count == 1)
                {
                    Int32 index = base.SelectedIndices[0];

                    if (index >= 0 && index < this.searchResults.Count)
                    {
                        return this.searchResults[index];
                    }
                }

                return null;
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Boolean HasSelectedResult
        {
            get
            {
                return base.SelectedIndices.Count == 1 && this.HasAvailableResults;
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IEnumerable<SearchResult> AvailableResults
        {
            get
            {
                return this.searchResults;
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Boolean HasAvailableResults
        {
            get
            {
                return this.searchResults.Count > 0;
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Int32 ResultCount
        {
            get
            {
                return this.searchResults.Count;
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Boolean VirtualMode
        {
            get
            {
                return true;
            }
            set
            {
                // Intentionally not implemented
            }
        }

        public void Reset()
        {
            try
            {
                base.BeginUpdate();

                this.searchResults.Clear();

                base.VirtualListSize = this.searchResults.Count;

                if (this.searchResults.Count > 0 && base.Columns.Count > 1)
                {
                    base.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                    base.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                }
            }
            finally
            {
                base.EndUpdate();
            }
        }

        public void Replace(IEnumerable<SearchResult> searchResults)
        {
            try
            {
                base.BeginUpdate();

                this.searchResults.Clear();
                this.searchResults.AddRange(searchResults);

                base.VirtualListSize = this.searchResults.Count;

                if (this.searchResults.Count > 0 && base.Columns.Count > 1)
                {
                    base.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                    base.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                }
            }
            finally
            {
                base.EndUpdate();
            }
        }

        protected override void OnRetrieveVirtualItem(RetrieveVirtualItemEventArgs args)
        {
            if (args.ItemIndex >= 0 && args.ItemIndex < this.searchResults.Count)
            {
                SearchResult searchResult = this.searchResults[args.ItemIndex];

                args.Item = new ListViewItem(searchResult.Name);
                args.Item.SubItems.Add(searchResult.Path);
                args.Item.SubItems.Add(searchResult.GetDisplayReferenceCount());
                args.Item.SubItems.Add(searchResult.GetDisplayReferenceLines(true));
                args.Item.ToolTipText = "Double click to show details.";
            }
        }
    }
}
