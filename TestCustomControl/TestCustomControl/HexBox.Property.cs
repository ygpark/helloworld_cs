using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace TestCustomControl
{
    partial class HexBox
    {

        #region 프로퍼티
        
        /// <summary>
        /// Gets or sets the background color for the control.
        /// </summary>
        [DefaultValue(typeof(Color), "White")]
        public override Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value; }
        }





        /// <summary>
        /// The font used to display text in the hexbox.
        /// </summary>
        /// 
        public override Font Font
        {
            get { return base.Font; }
            set
            {
                if (value == null)
                    return;

                base.Font = value;
                this.UpdateRectanglePositioning();
                this.Invalidate();
            }
        }





        /// <summary>
        /// Gets or sets the maximum count of bytes in one line.
        /// </summary>
        /// <remarks>
        /// UseFixedBytesPerLine property no longer has to be set to true for this to work
        /// </remarks>
        [DefaultValue(16), Category("Hex"), Description("Gets or sets the maximum count of bytes in one line.")]
        public int BytesPerLine
        {
            get { return _bytesPerLine; }
            set
            {
                if (_bytesPerLine == value)
                    return;

                _bytesPerLine = value;
                //OnBytesPerLineChanged(EventArgs.Empty);

                UpdateRectanglePositioning();
                Invalidate();
            }
        } int _bytesPerLine = 16;





        /// <summary>
        /// Contains the size of a single character in pixel
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SizeF CharSize
        {
            get { return _charSize; }
            private set
            {
                if (_charSize == value)
                    return;
                _charSize = value;
                //if (CharSizeChanged != null)
                //    CharSizeChanged(this, EventArgs.Empty);
            }
        } SizeF _charSize;





        /// <summary>
        /// Gets or sets the background color for the selected bytes.
        /// </summary>
        [DefaultValue(typeof(Color), "Blue"), Category("Hex"), Description("Gets or sets the background color for the selected bytes.")]
        public Color SelectionBackColor
        {
            get { return _selectionBackColor; }
            set { _selectionBackColor = value; Invalidate(); }
        }
        Color _selectionBackColor = Color.Blue;





        /// <summary>
        /// Gets or sets the foreground color for the selected bytes.
        /// </summary>
        [DefaultValue(typeof(Color), "White"), Category("Hex"), Description("Gets or sets the foreground color for the selected bytes.")]
        public Color SelectionForeColor
        {
            get { return _selectionForeColor; }
            set { _selectionForeColor = value; Invalidate(); }
        } Color _selectionForeColor = Color.White;





        [DefaultValue(false), Category("Hex"), Description("Gets or sets the visibility of a vertical scroll bar.")]
        public bool VScrollBarVisible
        {
            get
            {
                return this._vScrollBarVisible;
            }
            set
            {
                if (_vScrollBarVisible == value)
                    return;

                _vScrollBarVisible = value;

                if (_vScrollBarVisible)
                    Controls.Add(_vScrollBar);
                else
                    Controls.Remove(_vScrollBar);

                UpdateRectanglePositioning();
                //UpdateScrollSize();

                //OnVScrollBarVisibleChanged(EventArgs.Empty);
            }
        } bool _vScrollBarVisible = false;

        #endregion

    }
}
