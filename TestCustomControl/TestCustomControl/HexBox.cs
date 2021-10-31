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
    public partial class HexBox : Control
    {
        /// <summary>
        /// 더미 데이터
        /// </summary>
        byte[] _data;
        VScrollBar _vScrollBar;

        Rectangle _recContent;
        Rectangle _recHex;
        int _scrollVvalue;
        string _hexStringFormat = "X";
        /// <summary>
		/// Contains string format information for text drawing
		/// </summary>
		StringFormat _stringFormat;
        /// <summary>
		/// 눈에 보이는 가로 최대 바이트 수
		/// </summary>
		int _iHexMaxHBytes;
        /// <summary>
        /// Contains the maximum of visible vertical bytes
        /// </summary>
        int _iHexMaxVBytes;
        /// <summary>
        /// Contains the maximum of visible bytes.
        /// </summary>
        int _iHexMaxBytes;


        public HexBox()
        {
            InitializeComponent();
            SetImprovementDrawingSpeed();

            _vScrollBar = new VScrollBar();
            _vScrollBar.Scroll += vScrollBar_Scroll;
            
            //TODO : 스크롤바
            _vScrollBar.Maximum = 100;

            BackColor = Color.White;
            Font = SystemFonts.MessageBoxFont;
            _stringFormat = new StringFormat(StringFormat.GenericTypographic);
            _stringFormat.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;

            _data = new byte[1600];
            for (int i = 0; i < _data.Length; i++)
            {
                _data[i] = (byte)(i % 255);
            }

            //this.ControlAdded += CustomControl_ControlAdded;

            this.Resize += HexBox_Resize;

        }


    }
}
