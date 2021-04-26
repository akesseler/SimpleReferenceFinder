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
using System.Linq;
using System.Text;

namespace Plexdata.SimpleReferenceFinder.Extensions
{
    public static class SearchResultExtension
    {
        public static String ToClipboard(this SearchResult searchResult)
        {
            if (searchResult is null)
            {
                return String.Empty;
            }

            StringBuilder stringBuilder = new StringBuilder();

            SearchResultExtension.AddSearchResult(stringBuilder, searchResult, 0);

            return stringBuilder.ToString();
        }

        public static String ToClipboard(this IEnumerable<SearchResult> searchResults)
        {
            if (searchResults is null || !searchResults.Any())
            {
                return String.Empty;
            }

            StringBuilder stringBuilder = new StringBuilder();

            foreach (SearchResult searchResult in searchResults)
            {
                SearchResultExtension.AddSearchResult(stringBuilder, searchResult, 0);
            }

            return stringBuilder.ToString();
        }

        private static void AddSearchResult(StringBuilder stringBuilder, SearchResult searchResult, Int32 depth)
        {
            if (searchResult is null) { return; }

            stringBuilder.AppendFormat("{0}\"{1}\"", String.Empty.PadLeft(depth, '\t'), searchResult.File.FullName);

            if (searchResult.HasOffsets)
            {
                stringBuilder.Append('\t');

                SearchOffset[] offsets = searchResult.ReferenceOffsets.ToArray();

                for (Int32 index = 0; index < offsets.Length; index++)
                {
                    SearchOffset offset = offsets[index];

                    stringBuilder.AppendFormat("(L:{0},C:{1})", offset.Line, offset.Column);

                    if (index + 1 < offsets.Length)
                    {
                        stringBuilder.Append('\t');
                    }
                }
            }

            stringBuilder.AppendLine();

            if (!searchResult.IsReferenced)
            {
                return;
            }

            depth++;

            foreach (SearchResult reference in searchResult.References)
            {
                SearchResultExtension.AddSearchResult(stringBuilder, reference, depth);
            }
        }
    }
}
