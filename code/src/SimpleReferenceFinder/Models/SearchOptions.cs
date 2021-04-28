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
using System.IO;
using System.Linq;

namespace Plexdata.SimpleReferenceFinder.Models
{
    public class SearchOptions
    {
        private String baseFolder;
        private String[] sourcePatterns;
        private String[] targetPatterns;

        public SearchOptions()
            : base()
        {
            this.BaseFolder = null;
            this.SourcePatterns = null;
            this.TargetPatterns = null;
        }

        public String BaseFolder
        {
            get
            {
                return this.baseFolder;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    value = String.Empty;
                }

                this.baseFolder = value.Trim();
            }
        }

        public String[] SourcePatterns
        {
            get
            {
                return this.sourcePatterns;
            }
            set
            {
                if (value == null)
                {
                    value = new String[0];
                }

                this.sourcePatterns = value.Where(x => !String.IsNullOrWhiteSpace(x)).Select(x => x.Trim()).ToArray();
            }
        }

        public String[] TargetPatterns
        {
            get
            {
                return this.targetPatterns;
            }
            set
            {
                if (value == null)
                {
                    value = new String[0];
                }

                this.targetPatterns = value.Where(x => !String.IsNullOrWhiteSpace(x)).Select(x => x.Trim()).ToArray();
            }
        }

        public Boolean CaseSensitive { get; set; }

        public Boolean SearchRecursive { get; set; }

        public Boolean IncludeFolder { get; set; }

        public Boolean ChangeSeparator { get; set; }

        public String GetBaseFolder()
        {
            return this.BaseFolder.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        }

        public String GetSearchPattern(FileInfo source)
        {
            return this.GetSearchPattern(source, this.GetBaseFolder());
        }

        public String GetSearchPattern(FileInfo source, String folder)
        {
            if (source is null)
            {
                return String.Empty;
            }

            if (String.IsNullOrWhiteSpace(folder))
            {
                return source.Name;
            }

            if (this.IncludeFolder)
            {
                String result = source.FullName
                    .Replace(folder, String.Empty)
                    .TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

                if (this.ChangeSeparator)
                {
                    result = result.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                }

                return result;
            }
            else
            {
                return source.Name;
            }
        }
    }
}
