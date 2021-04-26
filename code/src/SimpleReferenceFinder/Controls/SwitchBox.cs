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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace Plexdata.SimpleReferenceFinder.Controls
{
    public class SwitchBox : CheckBox
    {
        private Int32 spacing = 5;
        private Size textSize;
        private Size checkSize;
        private readonly StringFormat stringFormat;
        private readonly SwitchBoxSettings switchSettings;

        public SwitchBox()
            : base()
        {
            base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw |
                ControlStyles.Selectable | ControlStyles.SupportsTransparentBackColor |
                ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer |
                ControlStyles.OptimizedDoubleBuffer, true);

            this.stringFormat = new StringFormat()
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Center,
                FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.MeasureTrailingSpaces,
                Trimming = StringTrimming.None,
                HotkeyPrefix = HotkeyPrefix.Show
            };

            this.switchSettings = new SwitchBoxSettings(this);
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Appearance Appearance
        {
            get
            {
                return base.Appearance;
            }
            set
            {
                base.Appearance = value;
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new ContentAlignment CheckAlign
        {
            get
            {
                return base.CheckAlign;
            }
            set
            {
                base.CheckAlign = value;
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new CheckState CheckState
        {
            get
            {
                return base.CheckState;
            }
            set
            {
                base.CheckState = value;
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new FlatStyle FlatStyle
        {
            get
            {
                return base.FlatStyle;
            }
            set
            {
                base.FlatStyle = value;
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new FlatButtonAppearance FlatAppearance
        {
            get
            {
                return base.FlatAppearance;
            }
        }

        [DefaultValue(5)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("The gap between switch box and switch text, but only if a text is set.")]
        public Int32 Spacing
        {
            get
            {
                return this.spacing;
            }
            set
            {
                if (value < 0)
                {
                    value = 0;
                }

                this.spacing = value;
                base.Invalidate();
            }
        }

        [Browsable(true)]
        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Description("Configure switch colors.")]
        public SwitchBoxSettings SwitchSettings
        {
            get
            {
                return this.switchSettings;
            }
        }

        public override Boolean AutoSize
        {
            get
            {
                return base.AutoSize;
            }
            set
            {
                // Changing value of AutoSize automatically changes value of
                // AutoEllipsis. Therefore, prevent this behavior if necessary.

                Boolean ellipsis = base.AutoEllipsis;

                base.AutoSize = value;

                if (base.AutoEllipsis != ellipsis)
                {
                    base.AutoEllipsis = ellipsis;
                }
            }
        }

        public override Size GetPreferredSize(Size proposedSize)
        {
            if (this.AutoSize)
            {
                return this.GetCalculatedPreferredSize();
            }

            return base.GetPreferredSize(proposedSize);
        }

        protected override void Dispose(Boolean disposing)
        {
            if (base.IsDisposed) { return; }

            if (disposing)
            {
                this.stringFormat.Dispose();
            }

            base.Dispose(disposing);
        }

        protected override void OnTextChanged(EventArgs args)
        {
            base.OnTextChanged(args);

            this.textSize = this.GetTextSize();

            base.Invalidate();
        }

        protected override void OnResize(EventArgs args)
        {
            Int32 height = base.ClientSize.Height - base.Padding.Vertical;

            this.checkSize = new Size(2 * height, height);

            base.OnResize(args);
        }

        protected override void SetBoundsCore(Int32 x, Int32 y, Int32 width, Int32 height, BoundsSpecified specified)
        {
            if (specified == BoundsSpecified.Size || specified == BoundsSpecified.All)
            {
                Size preferred = this.GetPreferredSize(new Size(width, height));

                if (width < preferred.Width)
                {
                    width = preferred.Width;
                }

                if (height < preferred.Height)
                {
                    height = preferred.Height;
                }
            }

            base.SetBoundsCore(x, y, width, height, specified);
        }

        protected override void OnPaint(PaintEventArgs args)
        {
            if (base.IsDisposed) { return; }

            Padding padding = base.Padding;
            Rectangle bounds = base.ClientRectangle;

            this.DrawBackground(args.Graphics, bounds);

            bounds.X += padding.Left;
            bounds.Y += padding.Top;
            bounds.Width -= padding.Horizontal;
            bounds.Height -= padding.Vertical;

            this.DrawBox(args.Graphics, bounds);
            this.DrawText(args.Graphics, bounds);
        }

        private void DrawBackground(Graphics graphics, Rectangle bounds)
        {
            using (Brush background = this.GetBackgroundBrush())
            {
                graphics.FillRectangle(background, bounds);
            }
        }

        private void DrawBox(Graphics graphics, Rectangle outer)
        {
            GraphicsState state = graphics.Save();

            try
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;

                Rectangle inner = new Rectangle(outer.X, outer.Y, this.checkSize.Width - 1, this.checkSize.Height - 1);

                this.DrawSlider(graphics, inner);

                this.DrawHandle(graphics, inner);
            }
            finally
            {
                graphics.Restore(state);
            }
        }

        private void DrawSlider(Graphics graphics, Rectangle bounds)
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                Int32 x = bounds.X;
                Int32 y = bounds.Y;
                Int32 h = bounds.Height;

                Int32 dx = bounds.Height - (bounds.Height * this.SwitchSettings.SliderPercent) / 100;

                // Make it even (divisible by two).
                dx += (dx % 2 == 0) ? 0 : 1;

                x += dx;
                y += dx;
                h -= 2 * dx;

                if (h < 1) { return; }

                path.AddArc(x, y, h, h, 90, 180);

                x = bounds.Right - bounds.Height;
                x += dx;

                path.AddArc(x, y, h, h, -90, 180);
                path.CloseFigure();

                using (Brush brush = this.GetSwitchSliderFillingBrush())
                {
                    graphics.FillPath(brush, path);
                }

                if (this.SwitchSettings.IsSliderOutlineUsed)
                {
                    using (Pen pen = this.GetSwitchSliderOutlinePen())
                    {
                        graphics.DrawPath(pen, path);
                    }
                }
            }
        }

        private void DrawHandle(Graphics graphics, Rectangle bounds)
        {
            Int32 x = base.Checked ? bounds.Right - bounds.Height : bounds.X;
            Int32 y = bounds.Y;
            Int32 h = bounds.Height;

            Int32 dx = bounds.Height - (bounds.Height * this.SwitchSettings.HandlePercent) / 100;

            // Make it even (divisible by two).
            dx += (dx % 2 == 0) ? 0 : 1;

            x += dx;
            y += dx;
            h -= 2 * dx;

            if (h < 1) { return; }

            using (Brush brush = this.GetSwitchHandleFillingBrush())
            {
                graphics.FillEllipse(brush, x, y, h, h);
            }

            if (this.SwitchSettings.IsHandleOutlineUsed)
            {
                using (Pen pen = this.GetSwitchHandleOutlinePen())
                {
                    graphics.DrawEllipse(pen, x, y, h, h);
                }
            }
        }

        private void DrawText(Graphics graphics, Rectangle outer)
        {
            if (!this.IsText()) { return; }

            Int32 x = outer.X + this.Spacing + this.checkSize.Width;
            Int32 y = outer.Y;
            Int32 w = outer.Right - x;
            Int32 h = outer.Bottom - y;

            Rectangle inner = new Rectangle(x, y, w, h);

            using (Brush foreground = this.GetForegroundBrush())
            using (StringFormat format = this.GetStringFormat())
            {
                graphics.DrawString(base.Text, base.Font, foreground, inner, format);
            }

            if (!this.AutoSize)
            {
                // BUG: The focus rectangle does not surround the text only.
                //      It surrounds instead the whole text area (inner bounds,
                //      calculated above). But it's ok for the moment...
            }

            this.DrawFocus(graphics, inner);
        }

        private void DrawFocus(Graphics graphics, Rectangle bounds)
        {
            if (base.Focused && base.ShowFocusCues)
            {
                ControlPaint.DrawFocusRectangle(graphics, bounds, base.ForeColor, base.BackColor);
            }
        }

        private StringFormat GetStringFormat()
        {
            StringFormat result = this.stringFormat.Clone() as StringFormat;

            if (base.AutoEllipsis)
            {
                result.Trimming = StringTrimming.EllipsisCharacter;
            }
            else
            {
                result.Trimming = StringTrimming.None;
            }

            if (!this.AutoSize)
            {
                switch (base.TextAlign)
                {
                    case ContentAlignment.TopLeft:
                        result.Alignment = StringAlignment.Near;
                        result.LineAlignment = StringAlignment.Near;
                        break;
                    case ContentAlignment.TopCenter:
                        result.Alignment = StringAlignment.Center;
                        result.LineAlignment = StringAlignment.Near;
                        break;
                    case ContentAlignment.TopRight:
                        result.Alignment = StringAlignment.Far;
                        result.LineAlignment = StringAlignment.Near;
                        break;
                    case ContentAlignment.MiddleLeft:
                        result.Alignment = StringAlignment.Near;
                        result.LineAlignment = StringAlignment.Center;
                        break;
                    case ContentAlignment.MiddleCenter:
                        result.Alignment = StringAlignment.Center;
                        result.LineAlignment = StringAlignment.Center;
                        break;
                    case ContentAlignment.MiddleRight:
                        result.Alignment = StringAlignment.Far;
                        result.LineAlignment = StringAlignment.Center;
                        break;
                    case ContentAlignment.BottomLeft:
                        result.Alignment = StringAlignment.Near;
                        result.LineAlignment = StringAlignment.Far;
                        break;
                    case ContentAlignment.BottomCenter:
                        result.Alignment = StringAlignment.Center;
                        result.LineAlignment = StringAlignment.Far;
                        break;
                    case ContentAlignment.BottomRight:
                        result.Alignment = StringAlignment.Far;
                        result.LineAlignment = StringAlignment.Far;
                        break;
                }
            }
            else
            {
                result.Alignment = StringAlignment.Near;
                result.LineAlignment = StringAlignment.Center;
            }

            return result;
        }

        private Brush GetBackgroundBrush()
        {
            return new SolidBrush(base.BackColor);
        }

        private Brush GetForegroundBrush()
        {
            return new SolidBrush(base.Enabled ? base.ForeColor : SystemColors.ControlDark);
        }

        private Pen GetSwitchSliderOutlinePen()
        {
            if (base.Enabled)
            {
                if (base.Checked)
                {
                    return new Pen(this.SwitchSettings.SliderOutlineOn, this.SwitchSettings.SliderOutlineWidth);
                }
                else
                {
                    return new Pen(this.SwitchSettings.SliderOutlineOff, this.SwitchSettings.SliderOutlineWidth);
                }
            }
            else
            {
                return new Pen(Color.LightGray, this.SwitchSettings.SliderOutlineWidth);
            }
        }

        private Color GetSwitchSliderFillingColor()
        {
            if (base.Enabled)
            {
                if (base.Checked)
                {
                    return this.SwitchSettings.SliderFillingOn;
                }
                else
                {
                    return this.SwitchSettings.SliderFillingOff;
                }
            }
            else
            {
                return Color.LightGray;
            }
        }

        private Brush GetSwitchSliderFillingBrush()
        {
            return new SolidBrush(this.GetSwitchSliderFillingColor());
        }

        private Pen GetSwitchHandleOutlinePen()
        {
            if (base.Enabled)
            {
                if (base.Checked)
                {
                    return new Pen(this.SwitchSettings.HandleOutlineOn, this.SwitchSettings.HandleOutlineWidth);
                }
                else
                {
                    return new Pen(this.SwitchSettings.HandleOutlineOff, this.SwitchSettings.HandleOutlineWidth);
                }
            }
            else
            {
                return new Pen(Color.WhiteSmoke, this.SwitchSettings.HandleOutlineWidth);
            }
        }

        private Color GetSwitchHandleFillingColor()
        {
            if (base.Enabled)
            {
                if (base.Checked)
                {
                    return this.SwitchSettings.HandleFillingOn;
                }
                else
                {
                    return this.SwitchSettings.HandleFillingOff;
                }
            }
            else
            {
                return Color.WhiteSmoke;
            }
        }

        private Brush GetSwitchHandleFillingBrush()
        {
            return new SolidBrush(this.GetSwitchHandleFillingColor());
        }

        private Boolean IsText()
        {
            return !String.IsNullOrWhiteSpace(base.Text);
        }

        private Size GetTextSize()
        {
            if (!this.IsText())
            {
                return new Size(0, base.Font.Height);
            }

            using (Graphics graphics = this.CreateGraphics())
            {
                Size result = graphics.MeasureString(base.Text, base.Font, Point.Empty, this.stringFormat).ToSize();

                result.Width += 1;

                return result;
            }
        }

        private Size GetCalculatedPreferredSize()
        {
            // Text width might be zero (indicating empty text)
            // but text height is at least font height.

            Size result = this.GetTextSize();

            result.Width += 2 * result.Height + (result.Width > 0 ? this.Spacing : 0);

            result.Width += base.Padding.Horizontal;
            result.Height += base.Padding.Vertical;

            return result;
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class SwitchBoxSettings
        {
            private readonly SwitchBox parent;

            private Color handleFillingOn;
            private Color handleFillingOff;
            private Color handleOutlineOn;
            private Color handleOutlineOff;
            private Single handleOutlineWidth;
            private Int32 handlePercent;

            private Color sliderFillingOn;
            private Color sliderFillingOff;
            private Color sliderOutlineOn;
            private Color sliderOutlineOff;
            private Single sliderOutlineWidth;
            private Int32 sliderPercent;

            public SwitchBoxSettings(SwitchBox parent)
            {
                this.parent = parent ?? throw new ArgumentNullException(nameof(parent));

                this.handleFillingOn = SwitchBoxSettings.DefaultHandleFillingOn;
                this.handleFillingOff = SwitchBoxSettings.DefaultHandleFillingOff;
                this.handleOutlineOn = SwitchBoxSettings.DefaultHandleOutlineOn;
                this.handleOutlineOff = SwitchBoxSettings.DefaultHandleOutlineOff;
                this.handleOutlineWidth = SwitchBoxSettings.DefaultHandleOutlineWidth;
                this.handlePercent = SwitchBoxSettings.DefaultHandlePercent;

                this.sliderFillingOn = SwitchBoxSettings.DefaultSliderFillingOn;
                this.sliderFillingOff = SwitchBoxSettings.DefaultSliderFillingOff;
                this.sliderOutlineOn = SwitchBoxSettings.DefaultSliderOutlineOn;
                this.sliderOutlineOff = SwitchBoxSettings.DefaultSliderOutlineOff;
                this.sliderOutlineWidth = SwitchBoxSettings.DefaultSliderOutlineWidth;
                this.sliderPercent = SwitchBoxSettings.DefaultSliderPercent;
            }

            [Browsable(false)]
            public static Color DefaultHandleFillingOn { get { return Color.SteelBlue; } }

            [Browsable(false)]
            public static Color DefaultHandleFillingOff { get { return Color.White; } }

            [Browsable(false)]
            public static Color DefaultHandleOutlineOn { get { return Color.LightSteelBlue; } }

            [Browsable(false)]
            public static Color DefaultHandleOutlineOff { get { return Color.LightGray; } }

            [Browsable(false)]
            public const Single DefaultHandleOutlineWidth = 1F;

            [Browsable(false)]
            public const Int32 DefaultHandlePercent = 100;

            [Browsable(false)]
            public static Color DefaultSliderFillingOn { get { return Color.LightSteelBlue; } }

            [Browsable(false)]
            public static Color DefaultSliderFillingOff { get { return Color.LightGray; } }

            [Browsable(false)]
            public static Color DefaultSliderOutlineOn { get { return Color.LightSteelBlue; } }

            [Browsable(false)]
            public static Color DefaultSliderOutlineOff { get { return Color.LightGray; } }

            [Browsable(false)]
            public const Single DefaultSliderOutlineWidth = 1F;

            [Browsable(false)]
            public const Int32 DefaultSliderPercent = 80;

            [Browsable(true)]
            [DefaultValue(typeof(Color), "SteelBlue")]
            [EditorBrowsable(EditorBrowsableState.Advanced)]
            [Description("The filling color of the handle when the switch is checked.")]
            public Color HandleFillingOn
            {
                get
                {
                    return this.handleFillingOn;
                }
                set
                {
                    if (value == null || value == Color.Empty)
                    {
                        value = SwitchBoxSettings.DefaultHandleFillingOn;
                    }

                    if (value == Color.Transparent)
                    {
                        throw new ArgumentException("Transparent color is not supported.", nameof(this.HandleFillingOn));
                    }

                    if (this.handleFillingOn != value)
                    {
                        this.handleFillingOn = value;
                        this.parent.Invalidate();
                    }
                }
            }

            [Browsable(true)]
            [DefaultValue(typeof(Color), "White")]
            [EditorBrowsable(EditorBrowsableState.Advanced)]
            [Description("The filling color of the handle when the switch is unchecked.")]
            public Color HandleFillingOff
            {
                get
                {
                    return this.handleFillingOff;
                }
                set
                {
                    if (value == null || value == Color.Empty)
                    {
                        value = SwitchBoxSettings.DefaultHandleFillingOff;
                    }

                    if (value == Color.Transparent)
                    {
                        throw new ArgumentException("Transparent color is not supported.", nameof(this.HandleFillingOff));
                    }

                    if (this.handleFillingOff != value)
                    {
                        this.handleFillingOff = value;
                        this.parent.Invalidate();
                    }
                }
            }

            [Browsable(true)]
            [DefaultValue(typeof(Color), "LightSteelBlue")]
            [EditorBrowsable(EditorBrowsableState.Advanced)]
            [Description("The outline color of the handle when the switch is checked.")]
            public Color HandleOutlineOn
            {
                get
                {
                    return this.handleOutlineOn;
                }
                set
                {
                    if (value == null || value == Color.Empty)
                    {
                        value = SwitchBoxSettings.DefaultHandleOutlineOn;
                    }

                    if (value == Color.Transparent)
                    {
                        throw new ArgumentException("Transparent color is not supported.", nameof(this.HandleOutlineOn));
                    }

                    if (this.handleOutlineOn != value)
                    {
                        this.handleOutlineOn = value;
                        this.parent.Invalidate();
                    }
                }
            }

            [Browsable(true)]
            [DefaultValue(typeof(Color), "LightGray")]
            [EditorBrowsable(EditorBrowsableState.Advanced)]
            [Description("The outline color of the handle when the switch is unchecked.")]
            public Color HandleOutlineOff
            {
                get
                {
                    return this.handleOutlineOff;
                }
                set
                {
                    if (value == null || value == Color.Empty)
                    {
                        value = SwitchBoxSettings.DefaultHandleOutlineOff;
                    }

                    if (value == Color.Transparent)
                    {
                        throw new ArgumentException("Transparent color is not supported.", nameof(this.HandleOutlineOff));
                    }

                    if (this.handleOutlineOff != value)
                    {
                        this.handleOutlineOff = value;
                        this.parent.Invalidate();
                    }
                }
            }

            [Browsable(true)]
            [DefaultValue(SwitchBoxSettings.DefaultHandleOutlineWidth)]
            [EditorBrowsable(EditorBrowsableState.Always)]
            [Description("The width of the handle outline.")]
            public Single HandleOutlineWidth
            {
                get
                {
                    return this.handleOutlineWidth;
                }
                set
                {
                    if (value < 0F)
                    {
                        value = SwitchBoxSettings.DefaultHandleOutlineWidth;
                    }

                    this.handleOutlineWidth = value;
                    this.parent.Invalidate();
                }
            }

            [Browsable(true)]
            [DefaultValue(SwitchBoxSettings.DefaultHandlePercent)]
            [EditorBrowsable(EditorBrowsableState.Always)]
            [Description("The percentage of the height of the handle. Be careful when changing this value. It may cause unwanted effects.")]
            public Int32 HandlePercent
            {
                get
                {
                    return this.handlePercent;
                }
                set
                {
                    if (value < 0)
                    {
                        value = SwitchBoxSettings.DefaultHandlePercent;
                    }

                    this.handlePercent = value;
                    this.parent.Invalidate();
                }
            }

            [Browsable(false)]
            public Boolean IsHandleOutlineUsed
            {
                get
                {
                    return this.handleOutlineWidth > 0F;
                }
            }

            [Browsable(true)]
            [DefaultValue(typeof(Color), "LightSteelBlue")]
            [EditorBrowsable(EditorBrowsableState.Advanced)]
            [Description("The filling color of the slider when the switch is checked.")]
            public Color SliderFillingOn
            {
                get
                {
                    return this.sliderFillingOn;
                }
                set
                {
                    if (value == null || value == Color.Empty)
                    {
                        value = SwitchBoxSettings.DefaultSliderFillingOn;
                    }

                    if (value == Color.Transparent)
                    {
                        throw new ArgumentException("Transparent color is not supported.", nameof(this.SliderFillingOn));
                    }

                    if (this.sliderFillingOn != value)
                    {
                        this.sliderFillingOn = value;
                        this.parent.Invalidate();
                    }
                }
            }

            [Browsable(true)]
            [DefaultValue(typeof(Color), "LightGray")]
            [EditorBrowsable(EditorBrowsableState.Advanced)]
            [Description("The filling color of the slider when the switch is unchecked.")]
            public Color SliderFillingOff
            {
                get
                {
                    return this.sliderFillingOff;
                }
                set
                {
                    if (value == null || value == Color.Empty)
                    {
                        value = SwitchBoxSettings.DefaultSliderFillingOff;
                    }

                    if (value == Color.Transparent)
                    {
                        throw new ArgumentException("Transparent color is not supported.", nameof(this.SliderFillingOff));
                    }

                    if (this.sliderFillingOff != value)
                    {
                        this.sliderFillingOff = value;
                        this.parent.Invalidate();
                    }
                }
            }

            [Browsable(true)]
            [DefaultValue(typeof(Color), "LightSteelBlue")]
            [EditorBrowsable(EditorBrowsableState.Advanced)]
            [Description("The outline color of the slider when the switch is checked.")]
            public Color SliderOutlineOn
            {
                get
                {
                    return this.sliderOutlineOn;
                }
                set
                {
                    if (value == null || value == Color.Empty)
                    {
                        value = SwitchBoxSettings.DefaultSliderOutlineOn;
                    }

                    if (value == Color.Transparent)
                    {
                        throw new ArgumentException("Transparent color is not supported.", nameof(this.SliderOutlineOn));
                    }

                    if (this.sliderOutlineOn != value)
                    {
                        this.sliderOutlineOn = value;
                        this.parent.Invalidate();
                    }
                }
            }

            [Browsable(true)]
            [DefaultValue(typeof(Color), "LightGray")]
            [EditorBrowsable(EditorBrowsableState.Advanced)]
            [Description("The outline color of the slider when the switch is unchecked.")]
            public Color SliderOutlineOff
            {
                get
                {
                    return this.sliderOutlineOff;
                }
                set
                {
                    if (value == null || value == Color.Empty)
                    {
                        value = SwitchBoxSettings.DefaultSliderOutlineOff;
                    }

                    if (value == Color.Transparent)
                    {
                        throw new ArgumentException("Transparent color is not supported.", nameof(this.SliderOutlineOff));
                    }

                    if (this.sliderOutlineOff != value)
                    {
                        this.sliderOutlineOff = value;
                        this.parent.Invalidate();
                    }
                }
            }

            [Browsable(true)]
            [DefaultValue(SwitchBoxSettings.DefaultSliderOutlineWidth)]
            [EditorBrowsable(EditorBrowsableState.Always)]
            [Description("The width of the slider outline.")]
            public Single SliderOutlineWidth
            {
                get
                {
                    return this.sliderOutlineWidth;
                }
                set
                {
                    if (value < 0F)
                    {
                        value = SwitchBoxSettings.DefaultSliderOutlineWidth;
                    }

                    this.sliderOutlineWidth = value;
                    this.parent.Invalidate();
                }
            }

            [Browsable(true)]
            [DefaultValue(SwitchBoxSettings.DefaultSliderPercent)]
            [EditorBrowsable(EditorBrowsableState.Always)]
            [Description("The percentage of the height of the slider. Be careful when changing this value. It may cause unwanted effects.")]
            public Int32 SliderPercent
            {
                get
                {
                    return this.sliderPercent;
                }
                set
                {
                    if (value < 0)
                    {
                        value = SwitchBoxSettings.DefaultSliderPercent;
                    }

                    this.sliderPercent = value;
                    this.parent.Invalidate();
                }
            }

            [Browsable(false)]
            public Boolean IsSliderOutlineUsed
            {
                get
                {
                    return this.sliderOutlineWidth > 0F;
                }
            }

            public override String ToString()
            {
                return String.Empty;
            }
        }
    }
}
