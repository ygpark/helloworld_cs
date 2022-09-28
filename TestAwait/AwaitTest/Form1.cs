using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AwaitTest
{
    public partial class Form1 : Form
    {
        private int _sum1 = 1;
        private int _sum2 = 1;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnAsync_Click(object sender, EventArgs e)
        {
            AsyncRun();
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            SyncRun();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.ShowDialog();
        }

        private void SyncRun()
        {
            FirstAsync();
            SecondAsync();
        }

        private async void AsyncRun()
        {
            int rst;

            label3.Text = "시작";

            //var taskPlusPlus2 = Task.Run(() => Sleep1Plus2());

            var aa = FirstAsync();
            var bb = SecondAsync();

            await aa;
            await bb;

            label3.Text = "종료";

        }

        async private Task FirstAsync()
        {
            await Task.Run( () =>
            {
                _sum1 = 0;
                for (int i = 0; i < 100; i++)
                {
                    Thread.Sleep(30);
                    _sum1++;
                    label1.Invoke(new Action(() =>
                    {
                        label1.Text = _sum1.ToString();
                    }));
                }
            });

            return;
        }

        async private Task SecondAsync()
        {
            await Task.Run( () =>
            {
                _sum2 = 0;
                for (int i = 0; i < 100; i++)
                {
                    Thread.Sleep(30);
                    _sum2++;
                    label2.Invoke(new Action(() =>
                    {
                        label2.Text = _sum2.ToString();
                    }));
                }
            });

            return;
        }
    }
}
