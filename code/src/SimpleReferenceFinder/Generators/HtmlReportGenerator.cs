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

using Plexdata.SimpleReferenceFinder.Interfaces;
using Plexdata.SimpleReferenceFinder.Models;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Plexdata.SimpleReferenceFinder.Generators
{
    public class HtmlReportGenerator : ISearchReportGenerator
    {
        private SearchReport report = null;

        public HtmlReportGenerator() : base() { }

        public String Generate(SearchReport report)
        {
            if (report is null)
            {
                return String.Empty;
            }

            this.report = report;

            StringBuilder builder = new StringBuilder(1024);

            this.AddHeader(builder);
            this.AddSummary(builder);
            this.AddOptions(builder);
            this.AddTimeStamps(builder);
            this.AddReferences(builder);
            this.AddErrors(builder);
            this.AddFooter(builder);

            return builder.ToString();
        }

        private void AddHeader(StringBuilder builder)
        {
            builder.Append("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD XHTML 1.1//EN\" \"http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd\">");
            builder.Append("<html>");
            builder.Append("<head>");
            builder.Append("<title>Search Report</title>");
            builder.Append("<style>");
            builder.Append("body { font: normal 1em sans-serif; margin: 2em; } ");
            builder.Append("h1 { font-size: 1.4em; color: #2E74B5; } ");
            builder.Append("h2 { font-size: 1.2em; color: #2E74B5; font-style: italic; } ");
            builder.Append("td { vertical-align: top; padding: 0em 1em 0.3em 0em; } ");
            builder.Append("td:first-child { white-space: nowrap; font-weight: bold; } ");
            builder.Append(".offsets th { text-align: left; } ");
            builder.Append(".offsets td { vertical-align: top; white-space: nowrap; min-width: 5em; padding: 0em; } ");
            builder.Append(".offsets td:first-child { font-weight: normal; } ");
            builder.Append("br { display: block; content: \"&nbsp;\"; margin-bottom: 0.3em; } ");
            builder.Append("</style>");
            builder.Append("</head>");
            builder.Append("<body>");
        }

        private void AddSummary(StringBuilder builder)
        {
            builder.Append("<h1>Report Summary</h1>");
            builder.Append("<table>");
            this.AddTableRow(builder, "Report Time", $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} (UTC)");
            this.AddTableRow(builder, "User Name", this.GetUserName());
            builder.Append("</table>");
        }

        private void AddOptions(StringBuilder builder)
        {
            builder.Append("<h1>Search Options</h1>");
            builder.Append("<table>");
            this.AddTableRow(builder, "Base Folder", this.report.BaseFolder);
            this.AddTableRow(builder, "Source Patterns", String.Join(", ", this.report.SourcePatterns));
            this.AddTableRow(builder, "Target Patterns", String.Join(", ", this.report.TargetPatterns));
            this.AddTableRow(builder, "Case Sensitive", this.GetBooleanText(this.report.CaseSensitive));
            this.AddTableRow(builder, "Search Recursive", this.GetBooleanText(this.report.SearchRecursive));
            this.AddTableRow(builder, "Include Folder", this.GetBooleanText(this.report.IncludeFolder));
            builder.Append("</table>");
        }

        private void AddTimeStamps(StringBuilder builder)
        {
            builder.Append("<h1>Elapsed Times</h1>");
            builder.Append("<table>");
            this.AddTableRow(builder, "Overall Time", this.report.OverallElapsed.ToString());
            this.AddTableRow(builder, "Scanning Time", this.report.ScanningElapsed.ToString());
            this.AddTableRow(builder, "Processing Time", this.report.ProcessingElapsed.ToString());
            builder.Append("</table>");
        }

        private void AddReferences(StringBuilder builder)
        {
            builder.Append("<h1>Found Results</h1>");
            builder.Append("<h2>Referenced Items</h2>");
            this.AddSearchResults(builder, this.report.ReferencedResults.ToArray());
            builder.Append("<h2>Unreferenced Items</h2>");
            this.AddSearchResults(builder, this.report.UnreferencedResults.ToArray());
        }

        private void AddSearchResults(StringBuilder builder, SearchResult[] results)
        {
            if (results.Length < 1)
            {
                this.AddEmptySection(builder);
                return;
            }

            for (Int32 index = 0; index < results.Length; index++)
            {
                SearchResult result = results[index];

                builder.Append("<table>");

                this.AddTableRow(builder, "Source File Name", result.Name);
                this.AddTableRow(builder, "Source File Path", result.Path);

                if (result.IsReferenced)
                {
                    this.AddTableRow(builder, "Search Pattern", $"<q>{this.report.SearchOptions.GetSearchPattern(result.File)}</q>");
                    this.AddTableRow(builder, "References", result.ReferenceCount);

                    StringBuilder helper = new StringBuilder();

                    this.AddSearchResults(helper, result.References.ToArray());

                    this.AddTableRow(builder, "Referenced In", helper.ToString());
                }
                else if (result.HasOffsets)
                {
                    StringBuilder helper = new StringBuilder();

                    this.AddSearchOffsets(helper, result.ReferenceOffsets.ToArray());

                    this.AddTableRow(builder, "Offsets", helper.ToString());
                }

                builder.Append("</table>");

                if (index + 1 < results.Length)
                {
                    builder.Append("<hr />");
                }
            }
        }

        private void AddSearchOffsets(StringBuilder builder, SearchOffset[] offsets)
        {
            builder.Append("<table class=\"offsets\">");
            builder.Append("<tr><th>Line</th><th>Column</th></tr>");

            for (Int32 index = 0; index < offsets.Length; index++)
            {
                SearchOffset offset = offsets[index];

                builder.AppendFormat("<tr><td>{0}</td><td>{1}</td></tr>", offset.Line.ToString("N0"), offset.Column.ToString("N0"));
            }

            builder.Append("</table>");
        }

        private void AddErrors(StringBuilder builder)
        {
            builder.Append("<h1>Occurred Errors</h1>");
            builder.Append("<h2>Search Errors</h2>");
            this.AddErrors(builder, this.report.SearchErrors.ToArray());
            builder.Append("<h2>Other Errors</h2>");
            this.AddErrors(builder, this.report.OtherErrors.ToArray());
        }

        private void AddErrors(StringBuilder builder, SearchError[] errors)
        {
            if (errors.Length < 1)
            {
                this.AddEmptySection(builder);
                return;
            }

            for (Int32 index = 0; index < errors.Length; index++)
            {
                SearchError error = errors[index];

                builder.Append("<table>");

                if (error.IsFileSystemSearchError)
                {
                    this.AddTableRow(builder, "Source Name", this.GetFileSystemName(error.Source));
                }

                this.AddTableRow(builder, "Error Message", this.GetExceptionMessage(error.Exception));
                this.AddTableRow(builder, "Stack Trace", this.GetExceptionStackTrace(error.Exception));

                builder.Append("</table>");

                if (index + 1 < errors.Length)
                {
                    builder.Append("<hr />");
                }
            }
        }

        private void AddFooter(StringBuilder builder)
        {
            builder.Append("</body>");
            builder.Append("</html>");
        }

        private void AddTableRow(StringBuilder builder, params Object[] columns)
        {
            if (columns.Length < 1)
            {
                return;
            }

            builder.Append("<tr>");

            foreach (Object column in columns)
            {
                builder.AppendFormat("<td>{0}</td>", column ?? String.Empty);
            }

            builder.Append("</tr>");
        }

        private void AddEmptySection(StringBuilder builder)
        {
            builder.Append("<p>Nothing to show in this section.</p>");
        }

        private String GetUserName()
        {
            return Environment.UserName;
        }

        private String GetBooleanText(Boolean value)
        {
            return value ? "Yes" : "No";
        }

        private String GetFileSystemName(FileSystemInfo source)
        {
            if (source is null)
            {
                return String.Empty;
            }

            return source.FullName;
        }

        private String GetExceptionMessage(Exception exception)
        {
            if (exception is null)
            {
                return String.Empty;
            }

            return exception.Message.Replace(Environment.NewLine, "<br/>");
        }

        private String GetExceptionStackTrace(Exception exception)
        {
            if (exception is null)
            {
                return String.Empty;
            }

            return exception.ToString().Replace(Environment.NewLine, "<br/>");
        }
    }
}
