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
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Plexdata.SimpleReferenceFinder.Controls
{
    public class HtmlTextBox : Panel
    {
        private readonly WebBrowser browser = null;

        public HtmlTextBox()
            : base()
        {
            this.browser = new WebBrowser()
            {
                Dock = DockStyle.Fill
            };

            this.Controls.Add(this.browser);
        }

        [Browsable(true)]
        [Category("Appearance")]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public String Content
        {
            get
            {
                return this.browser.DocumentText;
            }
            set
            {
                this.browser.Navigate("about:blank");
                this.browser.Document.OpenNew(false);
                this.browser.Document.Write(value ?? String.Empty);
                this.browser.Refresh();
            }
        }

        protected override void Dispose(Boolean disposing)
        {
            if (this.IsDisposed)
            {
                return;
            }

            if (disposing)
            {
                this.browser.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
