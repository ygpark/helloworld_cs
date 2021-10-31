using System;
using System.Collections.Generic;
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
        /// <summary>
        /// OnPaint 함수의 그리기 속도를 향상시킵니다. (예: 300ms -> 15ms)
        /// </summary>
        /// <remarks>
        /// 이 함수를 실행하면 Visual Studio에서 빌드-실행-종료 했을 때 디자이너에서 HexBox가 존재하지만 그려지지 않는 부작용이 있다.
        /// 닫았다가 다시 열면 보인다. Visual Studio 버그인듯..
        /// </remarks>
        /// <see cref="https://docs.microsoft.com/ko-kr/dotnet/api/system.windows.forms.controlstyles?view=net-5.0"/>
        private void SetImprovementDrawingSpeed()
        {
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
        }





        /// <summary>
        /// 
        /// </summary>
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            var brush = new SolidBrush(Color.Black);
            var start = new Point(0, 0);
            var end = new Point(this.Width, this.Height);
            //pe.Graphics.DrawLine(new Pen(brush), start, new Point(_recContent.Width, _recContent.Height));
            //pe.Graphics.DrawString(_scrollVvalue.ToString(), Font, brush, new PointF(10, 10));

            //UpdateRectanglePositioning();
            //Region r = new Region(ClientRectangle);
            //r.Exclude(_recContent);
            //pe.Graphics.ExcludeClip(r);


            PaintHex(pe.Graphics, 0, _data.Length);
        }





        /// <summary>
        /// 
        /// </summary>
        private void PaintHex(Graphics g, long startByte, long endByte)
        {
            Brush brush = new SolidBrush(GetDefaultForeColor());
            Brush selBrush = new SolidBrush(_selectionForeColor);
            Brush selBrushBack = new SolidBrush(_selectionBackColor);

            int counter = -1;
            long intern_endByte = Math.Min(_data.Length - 1, endByte + _iHexMaxHBytes);

            for (long i = startByte; i < intern_endByte + 1; i++)
            {
                counter++;
                Point gridPoint = GetGridPointByByteIndex(counter);
                byte b = _data[i];

                //bool isSelectedByte = i >= _bytePos && i <= (_bytePos + _selectionLength - 1) && _selectionLength != 0;

                PaintHexString(g, b, brush, gridPoint);
            }

            //PaintHexString(g, b, brush, gridPoint);
        }





        void PaintHexString(Graphics g, byte b, Brush brush, Point gridPoint)
        {
            PointF pixelPointF = GetPixelPointByGridPoint(gridPoint);

            string sB = ConvertByteToHex(b);

            g.DrawString(sB.Substring(0, 1), Font, brush, pixelPointF, _stringFormat);
            pixelPointF.X += _charSize.Width;
            g.DrawString(sB.Substring(1, 1), Font, brush, pixelPointF, _stringFormat);
        }




        /// <summary>
        /// 2차원 배열 좌표계를 픽셀 좌표계로 변환합니다.
        /// </summary>
        /// <param name="gp">2차원 배열 좌표계</param>
        /// <returns>픽셀 좌표계</returns>
        PointF GetPixelPointByGridPoint(Point gp)
        {
            float x = _recHex.X + gp.X * (_charSize.Width * 3); // 3 = 2 Char + 1 Space
            float y = _recHex.Y + gp.Y * _charSize.Height;

            return new PointF(x, y);
        }











        /// <summary>
        /// 컨트롤 내부의 직사각형 영역에 대한 위치를 업데이트합니다.
        /// </summary>
        /// <remarks>
        /// 호출 시점
        /// 1) 폰트 변경
        /// 1) 스크롤바, 라인 정보, 열 정보, 문자열 영역 등 내부 구성요소의 Visible이 변경될 때
        /// 1) ReSize 이벤트 호출시
        /// 1) 고해상도 스크린(High DPI resolution screen)을 위한 ScaleControl(SizeF, BoundsSpecified) 함수 오버라이드
        /// </remarks>
        void UpdateRectanglePositioning()
        {
            // 문자 사이즈 계산
            SizeF charSize;
            using (var graphics = this.CreateGraphics())
            {
                charSize = graphics.MeasureString("A", Font, 100, _stringFormat);
            }
            CharSize = new SizeF((float)Math.Ceiling(charSize.Width), (float)Math.Ceiling(charSize.Height));


            _vScrollBar.Left = this.Width - _vScrollBar.Width;
            _vScrollBar.Top = 0;
            _vScrollBar.Height = this.Height;

            if (VScrollBarVisible)
            {
                _recContent = new Rectangle(0,
                0,
                this.Width - _vScrollBar.Width,
                this.Height
                );

                _recHex = _recContent;
            }
            else
            {
                _recContent = new Rectangle(0,
                0,
                this.Width,
                this.Height
                );

                _recHex = _recContent;
            }


            //
            //_iHexMaxBytes = _iHexMaxHBytes * _iHexMaxVBytes;
            _iHexMaxBytes = BytesPerLine * _iHexMaxVBytes;
        }





        Color GetDefaultForeColor()
        {
            if (Enabled)
                return ForeColor;
            else
                return Color.Gray;
        }




        /// <summary>
        /// byteIndex를 2차원 배열 좌표계로 변환합니다.
        /// </summary>
        /// <![CDATA[
        ///       (col) 0  1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 |
        /// (row)|------------------------------------------------------|
        ///   0  |      00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 |
        ///   1  |      00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 |
        ///   2  |      00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 |
        /// ]]>
        /// <param name="byteIndex"></param>
        /// <returns></returns>
        Point GetGridPointByByteIndex(long byteIndex)
        {
            int row = (int)Math.Floor((double)byteIndex / (double)BytesPerLine);
            int column = (int)(byteIndex % BytesPerLine);

            Point res = new Point(column, row);
            return res;
        }





        /// <summary>
        /// Converts a byte array to a hex string. For example: {10,11} = "0A 0B"
        /// </summary>
        /// <param name="data">the byte array</param>
        /// <returns>the hex string</returns>
        string ConvertBytesToHex(byte[] data)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in data)
            {
                string hex = ConvertByteToHex(b);
                sb.Append(hex);
                sb.Append(" ");
            }
            if (sb.Length > 0)
                sb.Remove(sb.Length - 1, 1);
            string result = sb.ToString();
            return result;
        }





        /// <summary>
        /// Byte를 Hex 문자열로 변환합니다. 예시: "10" = "0A"
        /// </summary>
        /// <param name="b">the byte to format</param>
        /// <returns>the hex string</returns>
        string ConvertByteToHex(byte b)
        {
            string sB = b.ToString(_hexStringFormat, System.Threading.Thread.CurrentThread.CurrentCulture);
            if (sB.Length == 1)
                sB = "0" + sB;
            return sB;
        }





        /// <summary>
        /// Converts the hex string to an byte array. The hex string must be separated by a space char ' '. If there is any invalid hex information in the string the result will be null.
        /// </summary>
        /// <param name="hex">the hex string separated by ' '. For example: "0A 0B 0C"</param>
        /// <returns>the byte array. null if hex is invalid or empty</returns>
        byte[] ConvertHexToBytes(string hex)
        {
            if (string.IsNullOrEmpty(hex))
                return null;

            hex = hex.Trim();
            var hexArray = hex.Split(' ');
            var byteArray = new byte[hexArray.Length];

            for (int i = 0; i < hexArray.Length; i++)
            {
                var hexValue = hexArray[i];

                byte b;
                var isByte = ConvertHexToByte(hexValue, out b);
                if (!isByte)
                    return null;
                byteArray[i] = b;
            }

            return byteArray;
        }





        bool ConvertHexToByte(string hex, out byte b)
        {
            bool isByte = byte.TryParse(hex, System.Globalization.NumberStyles.HexNumber, System.Threading.Thread.CurrentThread.CurrentCulture, out b);
            return isByte;
        }

    }
}
