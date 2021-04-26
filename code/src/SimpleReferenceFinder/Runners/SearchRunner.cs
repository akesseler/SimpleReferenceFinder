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

using Plexdata.SimpleReferenceFinder.Defines;
using Plexdata.SimpleReferenceFinder.Interfaces;
using Plexdata.SimpleReferenceFinder.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Plexdata.SimpleReferenceFinder.Runners
{
    internal abstract class SearchRunner : ISearchRunner
    {
        #region Fields

        private CancellationToken token;
        private IReportCallbacks callbacks;

        #endregion

        #region Construction

        protected SearchRunner()
            : base()
        {
            this.token = default;
            this.callbacks = null;
        }

        #endregion

        #region Properties

        protected Boolean IsCancellation
        {
            get
            {
                return this.token.IsCancellationRequested;
            }
        }

        #endregion

        #region Methods

        public Task Run(CancellationToken token, IReportCallbacks callbacks, SearchOptions options)
        {
            this.token = token;
            this.callbacks = callbacks ?? throw new ArgumentNullException(nameof(callbacks));
            if (options is null) { throw new ArgumentNullException(nameof(options)); }

            return this.RunInternal(options);
        }

        protected void ThrowIfCancellation()
        {
            this.token.ThrowIfCancellationRequested();
        }

        protected void ReportMessage(String message)
        {
            this.callbacks.ReportMessage(message);
        }

        protected void ReportStatus(SearchStatus status)
        {
            this.callbacks.ReportStatus(status);
        }

        protected void ReportStatus(SearchStatus status, String message)
        {
            this.callbacks.ReportStatus(status);

            if (!String.IsNullOrWhiteSpace(message))
            {
                this.callbacks.ReportMessage(message);
            }
        }

        protected void ReportStatus(SearchStatus status, Int32 totals)
        {
            this.callbacks.ReportStatus(status);
            this.callbacks.ReportTotals(totals);
        }

        protected void ReportStatus(SearchStatus status, Exception error)
        {
            this.callbacks.ReportStatus(status);

            if (error != null)
            {
                this.callbacks.ReportError(new SearchError(error));
            }
        }

        protected void ReportFolder(String folder)
        {
            this.callbacks.ReportFolder(folder);
        }

        protected void ReportResults(IEnumerable< SearchResult> results)
        {
            this.callbacks.ReportResults(results);
        }

        protected void ReportError(FileSystemInfo source, Exception error)
        {
            if (error == null)
            {
                System.Diagnostics.Debug.Assert(false, "Reporting an error should include an exception instance.");
                return;
            }

            if (source == null)
            {
                this.callbacks.ReportError(new SearchError(error));
            }
            else
            {
                this.callbacks.ReportError(new SearchError(source, error));
            }
        }

        protected abstract void DoWork(SearchOptions options);

        private async Task RunInternal(SearchOptions options)
        {
            await Task.Run(() => { this.DoWork(options); }).ConfigureAwait(false);
        }

        #endregion
    }
}
