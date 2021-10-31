using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestWinFormsMVC
{
    public partial class MainForm : Form
    {
        private MainFormControl _mainControl;
        private InputNameControl _inputNameControl = new InputNameControl();


        public MainForm()
        {
            InitializeComponent();
            _mainControl = new MainFormControl();
        }

        private void btnGetNameFromNewForm_Click(object sender, EventArgs e)
        {
            var m = _inputNameControl.ShowDialog();
            labelName.Text = m.Name;
        }

        private void btnGetEventFromModel_Click(object sender, EventArgs e)
        {
            _mainControl.setText("defualt");
            //textBox1.Text = _control.getText();
        }
    }
}
