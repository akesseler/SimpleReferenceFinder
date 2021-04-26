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
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Plexdata.SimpleReferenceFinder.Runners
{
    internal class FileReferencesRunner : SearchRunner, ISearchRunner
    {
        public FileReferencesRunner() : base() { }

        protected override void DoWork(SearchOptions options)
        {
            try
            {
                this.ValidateSearchOptions(options);

                base.ReportStatus(SearchStatus.Starting, "Starting...");

                // Tactical sleep to allow showing of start-up feedback.
                Thread.Sleep(800);

                ICollection<SearchResult> searchResults = this.GetAssumedSearchResults(options);

                base.ReportStatus(SearchStatus.Processing, searchResults.Count);

                foreach (FileInfo targetFile in this.GetAffectedTargetFiles(options))
                {
                    base.ThrowIfCancellation();

                    base.ReportFolder($"Processing folder \"{targetFile.DirectoryName}\".");

                    this.DebugDelay(50);

                    this.AnalyzeTargetFile(options, targetFile, searchResults);
                }

                base.ReportResults(searchResults);

                base.ReportStatus(SearchStatus.Finished);
            }
            catch (OperationCanceledException)
            {
                base.ReportStatus(SearchStatus.Canceled);
            }
            catch (Exception error)
            {
                base.ReportStatus(SearchStatus.Failure, error);
            }
        }

        private void ValidateSearchOptions(SearchOptions options)
        {
            DirectoryInfo sourceFolder = new DirectoryInfo(options.BaseFolder);

            if (!sourceFolder.Exists) { throw new DirectoryNotFoundException(); }
        }

        private ICollection<SearchResult> GetAssumedSearchResults(SearchOptions options)
        {
            base.ReportStatus(SearchStatus.Scanning);

            List<SearchResult> result = new List<SearchResult>();

            DirectoryInfo sourceFolder = new DirectoryInfo(options.BaseFolder);

            SearchOption searchOption = this.GetSearchRecursive(options);

            foreach (String sourcePattern in options.SourcePatterns)
            {
                foreach (FileInfo currentFile in sourceFolder.GetFiles(sourcePattern, searchOption))
                {
                    base.ThrowIfCancellation();

                    base.ReportMessage($"Scanning for \"{sourcePattern}\" in \"{currentFile.DirectoryName}\".");

                    result.Add(new SearchResult(currentFile));

                    this.DebugDelay(50);
                }
            }

            return result;
        }

        private IEnumerable<FileInfo> GetAffectedTargetFiles(SearchOptions options)
        {
            DirectoryInfo targetFolder = new DirectoryInfo(options.BaseFolder);

            SearchOption searchOption = this.GetSearchRecursive(options);

            foreach (String targetPattern in options.TargetPatterns)
            {
                foreach (FileInfo currentFile in targetFolder.GetFiles(targetPattern, searchOption))
                {
                    base.ThrowIfCancellation();

                    yield return currentFile;
                }
            }
        }

        private SearchOption GetSearchRecursive(SearchOptions options)
        {
            return options.SearchRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
        }

        private StringComparison GetStringComparison(SearchOptions options)
        {
            return options.CaseSensitive ? StringComparison.InvariantCulture : StringComparison.InvariantCultureIgnoreCase;
        }

        public Boolean IsSearchSourceContained(String candidate, String pattern, StringComparison comparison, out Int32 column)
        {
            column = -1;

            if (candidate == null || candidate.Length < pattern.Length)
            {
                return false;
            }

            column = candidate.IndexOf(pattern, comparison);

            return column >= 0;
        }

        private void AnalyzeTargetFile(SearchOptions options, FileInfo targetFile, IEnumerable<SearchResult> searchResults)
        {
            try
            {
                Int32 lineNumber = 0;

                StringComparison comparison = this.GetStringComparison(options);

                String folder = options.GetBaseFolder();

                using (StreamReader streamReader = new StreamReader(targetFile.FullName))
                {
                    while (!streamReader.EndOfStream)
                    {
                        base.ThrowIfCancellation();

                        // BUG: Remove exception hack...
                        this.DebugException(targetFile.FullName);

                        String currentLine = streamReader.ReadLine() ?? String.Empty;

                        lineNumber++;

                        foreach (SearchResult searchResult in searchResults)
                        {
                            base.ThrowIfCancellation();

                            String pattern = options.GetSearchPattern(searchResult.File, folder);

                            if (this.IsSearchSourceContained(currentLine, pattern, comparison, out Int32 column))
                            {
                                searchResult.AddReference(targetFile);
                                searchResult.AddOffset(targetFile, new SearchOffset(lineNumber, column));
                            }
                        }
                    }
                }
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                base.ReportError(targetFile, exception);
            }
        }

        [Conditional("DEBUG")]
        private void DebugException(String fullName)
        {
            if (fullName == @"C:\Temp\SearchReference\BaseFolder\Neuer Ordner - Kopie (3)\NeuesTextdokument - Kopie (6).aspx") 
            { 
                throw new ArgumentNullException("some parameter", "argument null exception"); 
            }

            if (fullName == @"C:\Temp\SearchReference\BaseFolder\Neuer Ordner - Kopie (5)\NeuesTextdokument - Kopie (10).aspx") 
            {
                throw new NotSupportedException("Wow, exception",
                    new ArgumentException("Wow, inner exception",
                        new ApplicationException("Wow, inner inner exception")));
            }
        }

        [Conditional("DEBUG")]
        private void DebugDelay(Int32 delay)
        {
            if (delay < 10) { delay = 10; }

            Thread.Sleep(delay);
        }
    }
}
