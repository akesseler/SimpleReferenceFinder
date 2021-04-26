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

namespace Plexdata.SimpleReferenceFinder.Models
{
    public class SearchReport
    {
        private SearchOptions searchOptions = null;

        public SearchReport(SearchOptions searchOptions)
            : base()
        {
            this.searchOptions = searchOptions ?? new SearchOptions();
        }

        public SearchOptions SearchOptions { get { return this.searchOptions; } }

        public String BaseFolder { get { return this.searchOptions.BaseFolder; } }

        public String[] SourcePatterns { get { return this.searchOptions.SourcePatterns; } }

        public String[] TargetPatterns { get { return this.searchOptions.TargetPatterns; } }

        public Boolean CaseSensitive { get { return this.searchOptions.CaseSensitive; } }

        public Boolean SearchRecursive { get { return this.searchOptions.SearchRecursive; } }

        public Boolean IncludeFolder { get { return this.searchOptions.IncludeFolder; } }

        public TimeSpan OverallElapsed { get; set; }

        public TimeSpan ScanningElapsed { get; set; }

        public TimeSpan ProcessingElapsed { get; set; }

        public IEnumerable<SearchError> SearchErrors { get; set; }

        public IEnumerable<SearchError> OtherErrors { get; set; }

        public IEnumerable<SearchResult> ReferencedResults { get; set; }

        public IEnumerable<SearchResult> UnreferencedResults { get; set; }
    }
}
