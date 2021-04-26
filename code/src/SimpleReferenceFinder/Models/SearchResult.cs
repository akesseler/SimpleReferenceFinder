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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;

namespace Plexdata.SimpleReferenceFinder.Models
{
    public class SearchResult : IEquatable<SearchResult>, IEquatable<FileInfo>, INotifyPropertyChanged
    {
        private readonly FileInfo file = null;
        private readonly List<SearchOffset> offsets = new List<SearchOffset>();
        private readonly List<SearchResult> references = new List<SearchResult>();

        public event PropertyChangedEventHandler PropertyChanged;

        public SearchResult(FileInfo file)
            : base()
        {
            this.file = file ?? throw new ArgumentNullException(nameof(file));
        }

        public FileInfo File
        {
            get
            {
                return this.file;
            }
        }

        public IEnumerable<SearchResult> References
        {
            get
            {
                return this.references;
            }
        }

        public Boolean IsReferenced
        {
            get
            {
                return this.references.Count > 0;
            }
        }

        public Boolean HasOffsets
        {
            get
            {
                return this.offsets.Count > 0;
            }
        }

        public String Name
        {
            get
            {
                return this.file.Name;
            }
        }

        public String Path
        {
            get
            {
                return this.file.DirectoryName;
            }
        }

        public Int32 ReferenceCount
        {
            get
            {
                return this.references.Count;
            }
        }

        public IEnumerable<SearchOffset> ReferenceOffsets
        {
            get
            {
                return this.offsets;
            }
        }

        public String GetDisplayReferenceCount()
        {
            return this.ReferenceCount.ToString("N0");
        }

        public String GetDisplayReferenceLines(Boolean briefly)
        {
            StringBuilder builder = new StringBuilder(128);

            if (this.offsets.Count < 1)
            {
                if (this.references.Count > 0)
                {
                    for (Int32 index = 0; index < this.references.Count; index++)
                    {
                        builder.Append(this.references[index].GetDisplayReferenceLines(briefly));

                        if (index + 1 < this.references.Count)
                        {
                            builder.Append(";");
                        }
                    }
                }
            }
            else
            {
                if (this.offsets.Count > 0)
                {
                    for (Int32 index = 0; index < this.offsets.Count; index++)
                    {
                        builder.Append(this.offsets[index].GetDisplaySearchOffset(briefly));

                        if (index + 1 < this.offsets.Count)
                        {
                            builder.Append(",");
                        }
                    }
                }
            }

            return builder.ToString();
        }

        public void AddReference(FileInfo reference)
        {
            if (reference != null && this.Find(reference) == null)
            {
                this.references.Add(new SearchResult(reference));

                this.OnRaisePropertyChanged(nameof(this.ReferenceCount));
            }
        }

        public void AddOffset(FileInfo reference, Int32 line)
        {
            this.AddOffset(reference, new SearchOffset(line));
        }

        public void AddOffset(FileInfo reference, SearchOffset offset)
        {
            if (reference == null || offset == null)
            {
                return;
            }

            SearchResult found = this.Find(reference);

            if (found != null && !found.offsets.Contains(offset))
            {
                found.offsets.Add(offset);

                this.OnRaisePropertyChanged(nameof(this.ReferenceOffsets));
            }
        }

        public override Int32 GetHashCode()
        {
            return this.File.GetHashCode();
        }

        public override Boolean Equals(Object other)
        {
            if (other is SearchResult sr)
            {
                return this.Equals(sr);
            }

            if (other is FileInfo fi)
            {
                return this.Equals(fi);
            }

            return base.Equals(other);
        }

        public Boolean Equals([AllowNull] SearchResult other)
        {
            if (other == null)
            {
                return false;
            }

            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            return this.Equals(other.File);
        }

        public Boolean Equals([AllowNull] FileInfo other)
        {
            if (other == null)
            {
                return false;
            }

            if (Object.ReferenceEquals(this.File, other))
            {
                return true;
            }

            return String.Equals(this.File.FullName, other.FullName, StringComparison.Ordinal);
        }

        private void OnRaisePropertyChanged(String property)
        {
            if (!String.IsNullOrWhiteSpace(property))
            {
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
            }
        }

        private SearchResult Find(FileInfo reference)
        {
            if (reference != null)
            {
                foreach (SearchResult current in this.references)
                {
                    if (current.Equals(reference))
                    {
                        return current;
                    }
                }
            }

            return null;
        }
    }
}
