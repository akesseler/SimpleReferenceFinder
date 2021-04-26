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
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Plexdata.SimpleReferenceFinder.Utilities
{
    public static class CueBanner
    {
        public static Boolean SetBanner(Control control, String banner)
        {
            return CueBanner.SetBanner(control, banner, true);
        }

        public static Boolean SetBanner(Control control, String banner, Boolean show)
        {
            if (control == null)
            {
                throw new ArgumentNullException(nameof(control), $"Parameter \"{nameof(control)}\" must not be null.");
            }

            if (control is TextBox textBox)
            {
                return CueBanner.SetTextBoxBanner(textBox, banner, show);
            }

            if (control is ComboBox comboBox)
            {
                return CueBanner.SetComboBoxBanner(comboBox, banner);
            }

            throw new ArgumentException("Only text box and combo box are supported.", nameof(control));
        }

        private static Boolean SetTextBoxBanner(TextBox textBox, String banner, Boolean show)
        {
            if (textBox.Multiline)
            {
                throw new ArgumentException("Cue banner is not supported on multiline edit controls!", nameof(textBox));
            }

            // MSDN: Minimum operating systems "Windows XP".
            if (Environment.OSVersion.Version.Major >= 5)
            {
                IntPtr result = CueBanner.SendMessage(
                    textBox.Handle,
                    CueBanner.EM_SETCUEBANNER,
                    new IntPtr(show ? 1 : 0),
                    Marshal.StringToBSTR(banner)
                );

                return result.ToInt32() == 1;
            }

            return false;
        }

        private static Boolean SetComboBoxBanner(ComboBox comboBox, String banner)
        {
            // MSDN: Minimum operating systems "Windows Vista"!
            if (Environment.OSVersion.Version.Major > 5)
            {
                IntPtr result = CueBanner.SendMessage(
                    comboBox.Handle,
                    CueBanner.CB_SETCUEBANNER,
                    IntPtr.Zero,
                    Marshal.StringToBSTR(banner)
                );

                return result.ToInt32() == 1;
            }
            // HACK: Setting cue banner for combo boxes under Windows XP.
            //       As mentioned above the cue banner for combo boxes is supported 
            //       since Windows Vista. But who tells that setting the cue banner 
            //       for the edit contol of the combo box is impossible?! ;)
            else if (Environment.OSVersion.Version.Major == 5)
            {
                COMBOBOXINFO cbi = new COMBOBOXINFO();
                cbi.cbSize = Marshal.SizeOf(cbi);
                if (CueBanner.GetComboBoxInfo(comboBox.Handle, ref cbi))
                {
                    IntPtr result = CueBanner.SendMessage(
                        cbi.hwndItem,
                        CueBanner.EM_SETCUEBANNER,
                        new IntPtr(!String.IsNullOrEmpty(banner) ? 1 : 0),
                        Marshal.StringToBSTR(banner)
                    );

                    return result.ToInt32() == 1;
                }
            }

            return false;
        }

        #region Win32 API related implementation.

        // TextBox control message definitions.
        private const Int32 ECM_FIRST = 0x1500;
        private const Int32 EM_SETCUEBANNER = (ECM_FIRST + 1);
        //private const Int32 EM_GETCUEBANNER = (ECM_FIRST + 2);

        // ComboBox control message definitions.
        private const Int32 CBM_FIRST = 0x1700;
        private const Int32 CB_SETCUEBANNER = (CBM_FIRST + 3);
        //private const Int32 CB_GETCUEBANNER = (CBM_FIRST + 4);

        [Flags]
        private enum BUTTONSTATES
        {
            STATE_SYSTEM_NONE = 0,
            STATE_SYSTEM_INVISIBLE = 0x00008000,
            STATE_SYSTEM_PRESSED = 0x00000008
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public Int32 left;
            public Int32 top;
            public Int32 right;
            public Int32 bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct COMBOBOXINFO
        {
            public Int32 cbSize;
            public RECT rcItem;
            public RECT rcButton;
            public BUTTONSTATES stateButton;
            public IntPtr hwndCombo;
            public IntPtr hwndItem;
            public IntPtr hwndList;
        }

        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        private static extern Boolean GetComboBoxInfo(IntPtr hwndCombo, ref COMBOBOXINFO pcbi);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(IntPtr hWnd, Int32 message, IntPtr wParam, IntPtr lParam);

        #endregion
    }
}
