using System;
using System.Collections.Generic;
using System.Text;

namespace Extensibility {
    public class ToolStripMenuItemEx : System.Windows.Forms.ToolStripMenuItem {
        private IExtender _Extension = null;
        public IExtender Extension {
            get { return _Extension; }
            set { _Extension = value; }
        }
    }
}
