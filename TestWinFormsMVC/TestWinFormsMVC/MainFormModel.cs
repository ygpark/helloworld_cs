using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWinFormsMVC
{
    class MainFormModel
    {
        public event EventHandler<string> TextChanged;

        private string _text;
        public string Text 
        { 
            get { return _text; }
            set {
                _text = value;
                if (TextChanged != null) { TextChanged(this, value); }
            }
        }
    }
}
