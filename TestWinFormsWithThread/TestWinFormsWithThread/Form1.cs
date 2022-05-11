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
            _worker1.DoWork += _worker1_DoWork;
            _worker1.RunWorkerCompleted += _worker1_RunWorkerCompleted;
            _worker1.ProgressChanged += _worker1_ProgressChanged;
            _worker1.WorkerReportsProgress = true;
            _worker2 = new BackgroundWorker();
            _worker2.DoWork += _worker2_DoWork;
            _timer = new Timer();
            _timer.Interval = 10;
            _timer.Tick += _timer_Tick;
        }

        private void _worker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void _worker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("완료");
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
            for (int i = 0; i < 100; i++)
            {
                System.Threading.Thread.Sleep(10);

                //ReportProgress는 GUI Thread에서 호출됨.
                _worker1.ReportProgress(i+1);

                //Thread(BackgroundWoker)에서 GUI에 접근할 때 InvokeIfRequired 사용해야함.
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
            for (int i = 0; i < 100; i++)
            {
                System.Threading.Thread.Sleep(10);

                _count = i;
            }
        }
    }
}
