using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestWinFormsMVC
{
    class MainFormControl
    {
        private MainFormModel _model;

        public MainFormControl()
        {
            _model = new MainFormModel();
            _model.TextChanged += _model_TextChanged1; ;
        }

        private void _model_TextChanged1(object sender, string e)
        {
            MessageBox.Show(e);
        }

        public void setText(string text)
        {
            _model.Text = text;
        }

        public string getText()
        {
            return _model.Text;
        }
    }
}
