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

using Plexdata.SimpleReferenceFinder.Controls;
using Plexdata.SimpleReferenceFinder.Defines;
using Plexdata.SimpleReferenceFinder.Dialogs;
using Plexdata.SimpleReferenceFinder.Extensions;
using Plexdata.SimpleReferenceFinder.Generators;
using Plexdata.SimpleReferenceFinder.Interfaces;
using Plexdata.SimpleReferenceFinder.Models;
using Plexdata.SimpleReferenceFinder.Runners;
using Plexdata.SimpleReferenceFinder.Utilities;
using Plexdata.Utilities.Attributes;
using Plexdata.Utilities.Attributes.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Plexdata.SimpleReferenceFinder
{
    public partial class MainForm : Form, IReportCallbacks
    {
        private delegate void ReportMessageDelegate(String message);
        private delegate void ReportStatusDelegate(SearchStatus status);
        private delegate void ReportTotalsDelegate(Int32 totals);
        private delegate void ReportFolderDelegate(String folder);
        private delegate void ReportResultDelegate(SearchResult result);
        private delegate void ReportResultsDelegate(IEnumerable<SearchResult> results);
        private delegate void ReportErrorDelegate(SearchError error);

        private CancellationTokenSource cancellationTokenSource = null;
        private readonly ObservableCollection<SearchError> searchErrors = new ObservableCollection<SearchError>();
        private readonly ObservableCollection<SearchError> otherErrors = new ObservableCollection<SearchError>();

        private readonly Stopwatch overallStopwatch = new Stopwatch();
        private readonly Stopwatch scanningStopwatch = new Stopwatch();
        private readonly Stopwatch processingStopwatch = new Stopwatch();

        private SearchReport searchReport = null;

        public MainForm()
            : base()
        {
            this.InitializeComponent();
        }

        #region Event Handling

        protected override void OnLoad(EventArgs args)
        {
            this.SetupControls();
            base.OnLoad(args);
        }

        protected override void OnClosing(CancelEventArgs args)
        {
            this.CancelExecution();
            base.OnClosing(args);
        }

        private void OnButtonExitClick(Object sender, EventArgs args)
        {
            base.Close();
        }

        private async void OnButtonPlayClick(Object sender, EventArgs args)
        {
            if (!this.TryGetValidSearchOptions(out SearchOptions searchOptions))
            {
                return;
            }

            this.lstReferenced.Reset();
            this.lstUnreferenced.Reset();
            this.searchErrors.Clear();
            this.otherErrors.Clear();

            this.SetErrorCount(this.searchErrors.Count);

            ISearchRunner searchRunner = this.CreateSearchRunner();

            if (searchRunner != null)
            {
                if (this.cancellationTokenSource == null)
                {
                    this.cancellationTokenSource = new CancellationTokenSource();
                }

                this.searchReport = new SearchReport(searchOptions);

                await searchRunner.Run(this.cancellationTokenSource.Token, this, searchOptions);
            }
        }

        private void OnButtonStopClick(Object sender, EventArgs args)
        {
            this.CancelExecution();
        }

        private void OnButtonReportClick(Object sender, EventArgs args)
        {
            if (this.searchReport == null)
            {
                return;
            }

            try
            {
                SaveFileDialog dialog = new SaveFileDialog()
                {
                    InitialDirectory = this.txtBaseFolder.Text,
                    FileName = "search-result-report.html",
                    Filter = "HTML files (*.html)|*.html|All files (*.*)|*.*",
                    DefaultExt = "*.html"
                };

                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    this.Cursor = Cursors.WaitCursor;

                    this.searchReport.OverallElapsed = this.overallStopwatch.Elapsed;
                    this.searchReport.ScanningElapsed = this.scanningStopwatch.Elapsed;
                    this.searchReport.ProcessingElapsed = this.processingStopwatch.Elapsed;
                    this.searchReport.SearchErrors = this.searchErrors;
                    this.searchReport.OtherErrors = this.otherErrors;
                    this.searchReport.ReferencedResults = this.lstReferenced.AvailableResults;
                    this.searchReport.UnreferencedResults = this.lstUnreferenced.AvailableResults;

                    ISearchReportGenerator reportGenerator = new HtmlReportGenerator();

                    String reportContent = reportGenerator.Generate(this.searchReport);

                    using (StreamWriter writer = new StreamWriter(dialog.FileName))
                    {
                        writer.Write(reportContent);
                    }
                }
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void OnButtonInfoClick(Object sender, EventArgs args)
        {
            InfoDialog dialog = new InfoDialog();
            dialog.ShowDialog(this);
        }

        private void OnButtonBaseFolderClick(Object sender, EventArgs args)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog()
            {
                SelectedPath = this.txtBaseFolder.Text,
                ShowNewFolderButton = false
            };

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                this.txtBaseFolder.Text = dialog.SelectedPath;
            }
        }

        private void OnStatusErrorsClick(Object sender, EventArgs args)
        {
            SearchErrorDialog dialog = new SearchErrorDialog(this.searchErrors);
            dialog.ShowDialog(this);
        }

        private void OnReferencedDoubleClick(Object sender, EventArgs args)
        {
            SearchResult searchResult = this.lstReferenced.SelectedResult;

            if (searchResult != null)
            {
                SearchResultDialog dialog = new SearchResultDialog(searchResult);
                dialog.ShowDialog(this);
            }
        }

        private void OnContextMenuListOpening(Object sender, CancelEventArgs args)
        {
            if (sender is ContextMenuStrip source)
            {
                if (source.SourceControl is SearchResultListView control)
                {
                    this.mnuCopyOne.Enabled = control.HasSelectedResult;
                    this.mnuCopyAll.Enabled = control.HasAvailableResults;
                }
            }
        }

        private void OnMenuCopyOneClick(Object sender, EventArgs args)
        {
            if (sender is ToolStripMenuItem source)
            {
                if (source.Owner is ContextMenuStrip owner)
                {
                    if (owner.SourceControl is SearchResultListView control)
                    {
                        try { Clipboard.SetText(control.SelectedResult.ToClipboard()); } catch { }
                    }
                }
            }
        }

        private void OnMenuCopyAllClick(Object sender, EventArgs args)
        {
            if (sender is ToolStripMenuItem source)
            {
                if (source.Owner is ContextMenuStrip owner)
                {
                    if (owner.SourceControl is SearchResultListView control)
                    {
                        try { Clipboard.SetText(control.AvailableResults.ToClipboard()); } catch { }
                    }
                }
            }
        }

        #endregion

        #region Private Helpers

        private void SetupControls()
        {
            this.ReportStatus(SearchStatus.Unknown);

            this.cmbSearchType.Items.Clear();
            this.cmbSearchType.DisplayMember = nameof(AnnotationAttribute.Display);
            this.cmbSearchType.ValueMember = nameof(AnnotationAttribute.Utilize);
            this.cmbSearchType.DataSource = this.GetSearchTypeDataSource();

            CueBanner.SetBanner(this.txtBaseFolder, "Search base folder...");
            CueBanner.SetBanner(this.txtSourcePattern, "Source search patterns (e.g. *.aspx;*.ascx)...");
            CueBanner.SetBanner(this.txtTargetPattern, "Target search patterns (e.g. *.aspx;*.ascx)...");

            this.ReportMessage(null);
            this.SetErrorCount(0);

            this.SetDebugSearchOptions();
        }

        private IEnumerable<Object> GetSearchTypeDataSource()
        {
            List<AnnotationAttribute> annotations = new List<AnnotationAttribute>();

            foreach (SearchType current in Enum.GetValues(typeof(SearchType)))
            {
                AnnotationAttribute annotation = current.GetAnnotation();

                if (annotation != null && annotation.Visible)
                {
                    annotations.Add(annotation);
                }
            }

            return annotations;
        }

        private Boolean TryGetValidSearchOptions(out SearchOptions searchOptions)
        {
            searchOptions = new SearchOptions();

            if (String.IsNullOrWhiteSpace(this.txtBaseFolder.Text))
            {
                MessageBox.Show(this,
                    "Please choose the base folder where to start searching.",
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!Directory.Exists(this.txtBaseFolder.Text))
            {
                MessageBox.Show(this,
                    $"Sorry, but the folder \"{this.txtBaseFolder.Text}\" does not exist.",
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (String.IsNullOrWhiteSpace(this.txtSourcePattern.Text))
            {
                DialogResult result = MessageBox.Show(this,
                    "You should provide a valid list of source search patterns.",
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (String.IsNullOrWhiteSpace(this.txtTargetPattern.Text))
            {
                DialogResult result = MessageBox.Show(this,
                    "You should provide a valid list of target search patterns.",
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            searchOptions.BaseFolder = this.txtBaseFolder.Text.Trim();
            searchOptions.SourcePatterns = searchOptions.GetSearchPatternsFromString(this.txtSourcePattern.Text.Trim());
            searchOptions.TargetPatterns = searchOptions.GetSearchPatternsFromString(this.txtTargetPattern.Text.Trim());
            searchOptions.CaseSensitive = this.chkCaseSensitive.Checked;
            searchOptions.SearchRecursive = this.chkSearchRecursive.Checked;
            searchOptions.IncludeFolder = this.chkIncludeFolder.Checked;

            return true;
        }

        private ISearchRunner CreateSearchRunner()
        {
            try
            {
                switch ((SearchType)this.cmbSearchType.SelectedValue)
                {
                    case SearchType.FileReferences:
                        return new FileReferencesRunner();
                }
            }
            catch (Exception exception)
            {
                this.ReportError(new SearchError(exception));
            }

            return null;
        }

        private void CancelExecution()
        {
            try
            {
                if (this.cancellationTokenSource != null)
                {
                    this.Cursor = Cursors.WaitCursor;

                    this.cancellationTokenSource.Cancel();
                    this.cancellationTokenSource.Dispose();
                    this.cancellationTokenSource = null;
                }
            }
            catch (Exception exception)
            {
                this.ReportError(new SearchError(exception));
            }
        }

        private void SetErrorCount(Int32 count)
        {
            try
            {
                String format = this.stlErrors.Tag as String;
                this.stlErrors.Text = String.Format(format, count);
                this.stlErrors.Visible = count > 0;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
        }

        private void HandleStarting()
        {
            this.Cursor = Cursors.AppStarting;
            this.grpOptions.Enabled = false;
            this.tbbPlay.Enabled = false;
            this.tbbStop.Enabled = true;
            this.btnReport.Enabled = false;
            this.spbProgress.Visible = true;
            this.ReportMessage(null);

            this.lstReferenced.Reset();
            this.lstUnreferenced.Reset();

            this.overallStopwatch.Reset();
            this.scanningStopwatch.Reset();
            this.processingStopwatch.Reset();

            this.overallStopwatch.Start();
        }

        private void HandleScanning()
        {
            this.spbProgress.Style = ProgressBarStyle.Marquee;
            this.spbProgress.MarqueeAnimationSpeed = 40;

            this.scanningStopwatch.Start();
        }

        private void HandleProcessing()
        {
            this.spbProgress.Style = ProgressBarStyle.Continuous;

            this.scanningStopwatch.Stop();
            this.processingStopwatch.Start();
        }

        private void HandleCompletion(SearchStatus status)
        {
            this.Cursor = Cursors.Default;
            this.grpOptions.Enabled = true;
            this.tbbPlay.Enabled = true;
            this.tbbStop.Enabled = false;
            this.btnReport.Enabled = this.searchReport != null;
            this.spbProgress.Visible = false;

            this.overallStopwatch.Stop();
            this.scanningStopwatch.Stop();
            this.processingStopwatch.Stop();

            String message = String.Empty;

            if (status == SearchStatus.Finished)
            {
                message = String.Format("{0} Referenced: {1}, Unreferenced: {2}", status.GetAnnotation().Remarks, this.lstReferenced.ResultCount, this.lstUnreferenced.ResultCount);
            }
            else
            {
                message = status.GetAnnotation().Remarks;
            }

            this.ReportMessage(message);
        }

        #endregion

        #region Search Callbacks

        public void ReportMessage(String message)
        {
            if (this.IsDisposed) { return; }

            if (this.InvokeRequired)
            {
                this.Invoke(new ReportMessageDelegate(this.ReportMessage), new Object[] { message });
            }
            else
            {
                if (String.IsNullOrWhiteSpace(message))
                {
                    this.stlMessage.Text = String.Empty;
                }
                else
                {
                    this.stlMessage.Text = message;
                }
            }
        }

        public void ReportStatus(SearchStatus status)
        {
            if (this.IsDisposed) { return; }

            if (this.InvokeRequired)
            {
                this.Invoke(new ReportStatusDelegate(this.ReportStatus), new Object[] { status });
            }
            else
            {
                switch (status)
                {
                    case SearchStatus.Starting:
                        this.HandleStarting();
                        break;
                    case SearchStatus.Scanning:
                        this.HandleScanning();
                        break;
                    case SearchStatus.Processing:
                        this.HandleProcessing();
                        break;
                    case SearchStatus.Unknown:
                    case SearchStatus.Finished:
                    case SearchStatus.Canceled:
                    case SearchStatus.Failure:
                        this.HandleCompletion(status);
                        break;
                    default:
                        return;
                }

                this.tipMain.SetToolTip(this.spbProgress.ProgressBar, status.GetAnnotation().Remarks);
            }
        }

        public void ReportTotals(Int32 totals)
        {
            if (this.IsDisposed) { return; }

            if (this.InvokeRequired)
            {
                this.Invoke(new ReportTotalsDelegate(this.ReportTotals), new Object[] { totals });
            }
            else
            {
                this.spbProgress.Step = 1;
                this.spbProgress.Value = 0;
                this.spbProgress.Minimum = 0;
                this.spbProgress.Maximum = totals < 0 ? 0 : totals;
            }
        }

        public void ReportFolder(String folder)
        {
            if (this.IsDisposed) { return; }

            if (this.InvokeRequired)
            {
                this.Invoke(new ReportFolderDelegate(this.ReportFolder), new Object[] { folder });
            }
            else
            {
                if (this.spbProgress.Style != ProgressBarStyle.Marquee)
                {
                    this.spbProgress.PerformStep();
                }

                if (String.IsNullOrWhiteSpace(folder))
                {
                    this.stlMessage.Text = String.Empty;
                }
                else
                {
                    this.stlMessage.Text = folder;
                }
            }
        }

        public void ReportResults(IEnumerable<SearchResult> results)
        {
            if (this.IsDisposed) { return; }

            if (results == null) { return; }

            if (this.InvokeRequired)
            {
                this.Invoke(new ReportResultsDelegate(this.ReportResults), new Object[] { results });
            }
            else
            {
                this.lstReferenced.Replace(results.Where(x => x.IsReferenced));
                this.lstUnreferenced.Replace(results.Where(x => !x.IsReferenced));
            }
        }

        public void ReportError(SearchError error)
        {
            if (this.IsDisposed) { return; }

            if (error == null) { return; }

            if (this.InvokeRequired)
            {
                this.Invoke(new ReportErrorDelegate(this.ReportError), new Object[] { error });
            }
            else
            {
                if (error.IsFileSystemSearchError)
                {
                    this.searchErrors.Add(error);
                    this.SetErrorCount(this.searchErrors.Count);
                }
                else
                {
                    this.otherErrors.Add(error);
                }
            }
        }

        #endregion

        #region Debug Helpers

        [Conditional("DEBUG")]
        private void SetDebugSearchOptions()
        {
            this.txtBaseFolder.Text = @"C:\Temp\SearchReference\BaseFolder";
            this.txtSourcePattern.Text = "*.aspx;*.ascx";
            this.txtTargetPattern.Text = "*.aspx;*.ascx";
        }

        #endregion
    }
}
