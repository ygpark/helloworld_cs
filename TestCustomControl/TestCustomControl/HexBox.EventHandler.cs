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
        #region 이벤트 핸들러

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>이벤트핸들러</remarks>
        private void HexBox_Resize(object sender, EventArgs e)
        {
            UpdateRectanglePositioning();
        }





        /// <summary>
        /// 
        /// </summary>
        /// <remarks>이벤트핸들러</remarks>
        private void vScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            //throw new NotImplementedException();
            _scrollVvalue = e.NewValue;
            //Invalidate();
        }





        /// <summary>
        /// 
        /// </summary>
        /// <remarks>이벤트핸들러</remarks>
        private void CustomControl_ControlAdded(object sender, ControlEventArgs e)
        {
            UpdateRectanglePositioning();
        }

        #endregion

    }
}
