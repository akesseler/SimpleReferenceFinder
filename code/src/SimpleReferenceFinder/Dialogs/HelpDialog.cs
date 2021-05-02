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

using Plexdata.SimpleReferenceFinder.Properties;
using System;
using System.Text;
using System.Windows.Forms;

namespace Plexdata.SimpleReferenceFinder.Dialogs
{
    public partial class HelpDialog : Form
    {
        private static HelpDialog dialog = null;

        public HelpDialog()
            : base()
        {
            this.InitializeComponent();
        }

        public static void Display()
        {
            if (HelpDialog.dialog == null)
            {
                HelpDialog.dialog = new HelpDialog();
                HelpDialog.dialog.Show();
            }
            else
            {
                HelpDialog.dialog.BringToFront();
            }
        }

        protected override void OnLoad(EventArgs args)
        {
            base.OnLoad(args);
            this.htbContent.Content = Encoding.UTF8.GetString(Resources.HelpContent);
        }

        protected override void OnActivated(EventArgs args)
        {
            base.OnActivated(args);
            this.SetOpacity(1);
        }

        protected override void OnDeactivate(EventArgs args)
        {
            this.SetOpacity(0.8);
            base.OnDeactivate(args);
        }

        protected override void OnClosed(EventArgs args)
        {
            base.OnClosed(args);
            HelpDialog.dialog = null;
        }

        private void OnButtonCloseClick(Object sender, EventArgs args)
        {
            base.Close();
        }

        private void SetOpacity(Double value)
        {
            try
            {
                // BUG: Just catch "Win32Exception" with message "Wrong parameter."
                // Changing the Opacity raises a Win32Exception with message "Wrong parameter." when closing
                // the window or when closing the application at all, but without the possibility to catch it
                // accordingly. Therefore, do not spend more time to find out how to fix it. Just catch that
                // exception and log it.
                base.Opacity = value;
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine(exception);
            }
        }
    }
}
