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
using System.Runtime.Serialization;

namespace Plexdata.SimpleReferenceFinder.Models
{
    public class SearchError
    {
        private FileSystemInfo source = null;

        public SearchError(Exception exception)
            : this(null, exception)
        {
        }

        public SearchError(FileSystemInfo source, Exception exception)
            : base()
        {
            this.Source = source;
            this.Exception = exception ?? throw new ArgumentNullException(nameof(exception));
        }

        public Boolean IsFileSystemSearchError
        {
            get
            {
                return !(this.Source is EmptySystemInfo);
            }
        }

        public FileSystemInfo Source
        {
            get
            {
                return this.source;
            }
            private set
            {
                if (value == null)
                {
                    value = new EmptySystemInfo();
                }

                this.source = value;
            }
        }

        public Exception Exception { get; }

        public override String ToString()
        {
            return this.Exception.Message;
        }

        private class EmptySystemInfo : FileSystemInfo
        {
            public override Boolean Exists { get { return false; } }

            public override String Name { get { return String.Empty; } }

            public override String FullName { get { return String.Empty; } }

            public override void Delete() { }

            public override void GetObjectData(SerializationInfo info, StreamingContext context) { }

            public override String ToString()
            {
                return nameof(EmptySystemInfo);
            }
        }
    }
}
