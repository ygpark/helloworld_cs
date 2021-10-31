using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWinFormsMVC
{
    class InputNameControl
    {
        InputNameModel model = new InputNameModel();

        public InputNameModel ShowDialog()
        {
            var form = new InputNameForm();
            form.ShowDialog();
            model.Name = form.textBox1.Text;
            return model;
        }
    }
}
