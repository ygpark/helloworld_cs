using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AwaitTest
{
    public partial class Form1 : Form
    {
        private int _sum = 1;

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

        private void SyncRun()
        {
            for (int i = 0; i < 10; i++)
            {
                int rst = Sleep1Plus1();
                label1.Text = rst.ToString();
            }
        }

        private async void AsyncRun()
        {
            int rst;
            
            for (int i = 0; i < 10; i++)
            {
                var taskPlusPlus = Task.Run(() => Sleep1Plus1()); // 여기서 PlusPlus() 메소드가 실행된다.
                rst = await taskPlusPlus;                            // 여기서 PlusPlus() 메소드가 끝날때까지 기다린다.
                label1.Text = rst.ToString();
            }
        }

        /// <summary>
        /// 1초 Sleep 후 1을 더한다.
        /// </summary>
        /// <returns></returns>
        private int Sleep1Plus1()
        {
            
            Thread.Sleep(1000);
            return _sum++;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.ShowDialog();
        }
    }
}
