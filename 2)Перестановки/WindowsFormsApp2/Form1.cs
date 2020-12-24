using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics; // для таймера
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<string> Alf = new List<string>();
        int n = 0;
        string writePath = @"D:\FastSpace\Out.txt"; //Требует корректировки

        private void button2_Click(object sender, EventArgs e)
        {
            Alf.Add(TextBoxAlf.Text);
            n++;
            TextBoxWord.Text += TextBoxAlf.Text + " ";
            TextBoxAlf.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int[] Razm = new int[n];
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default);
            recfan2(Razm, Alf, n, sw);
            sw.Close();

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
            RichTextBox.Text = elapsedTime;
        }


        void recfan2(int[] Razm, List<string> Alf, int n, StreamWriter sw)
        {
            for (int i = 0; i < n; i++)
            {
                Razm[i] = i;
                sw.Write(Alf[i]);//Вывод первого КО
            }
            sw.Write("\t");

            while (CombObject(Razm, n))
            {
                foreach (int item in Razm)
                {
                    sw.Write(Alf[item]);
                }
                sw.Write("\t");
            }

        }

        bool CombObject(int[] Razm, int n)
        {
            int i = n - 2;
            while (i >= 0 && Razm[i] >= Razm[i + 1]) i--;
            if (i == -1)
                return false;
            int j = n - 1;

            while (Razm[i] >= Razm[j])
                j--;
            int t = Razm[i];
            Razm[i] = Razm[j];
            Razm[j] = t;

            for (int x = i + 1, y = n - 1; x < y; x++, y--)
            {
                t = Razm[y];
                Razm[y] = Razm[x];
                Razm[x] = t;
            }
            return true;
        }
    }
}
