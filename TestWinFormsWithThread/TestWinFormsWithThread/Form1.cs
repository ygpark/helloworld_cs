using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestWinFormsWithThread
{
    public partial class Form1 : Form
    {
        BackgroundWorker _worker1;
        BackgroundWorker _worker2;
        Timer _timer;
        int _count;
        public Form1()
        {
            InitializeComponent();
            _worker1 = new BackgroundWorker();
            _worker2 = new BackgroundWorker();
            _worker1.DoWork += _worker1_DoWork;
            _worker2.DoWork += _worker2_DoWork;
            _timer = new Timer();
            _timer.Interval = 10;
            _timer.Tick += _timer_Tick;
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            label1.Text = _count.ToString();
            label1.Enabled = (_count % 2 == 0) ? true : false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!_worker1.IsBusy)
            {
                _worker1.RunWorkerAsync();
            }

            if (!_worker2.IsBusy)
            {
                _worker2.RunWorkerAsync();
            }

            if (!_timer.Enabled)
            {
                _timer.Start();
            }
        }

        
        private void _worker1_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < 1000; i++)
            {
                System.Threading.Thread.Sleep(10);

                label2.InvokeIfRequired(o =>
                {
                   o.Text = i.ToString();
                   if (i % 2 == 0)
                       o.Enabled = true;
                   else
                       o.Enabled = false;
                });
            }
        }

        private void _worker2_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < 1000; i++)
            {
                System.Threading.Thread.Sleep(10);

                _count = i;
            }
        }
    }
}
