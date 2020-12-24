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
        List<string> items = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8" };
        const int n = 8, k = 5;
        string writePath = @"D:\FastSpace\Out.txt";
        private void button1_Click(object sender, EventArgs e)
        {
            int[] Razm = new int[k];
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default);
            recfan2(Razm, items, n, k, sw);
            sw.Close();

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
            RichTextBox.Text = elapsedTime;
        }


        void recfan2(int[] Razm, List<string> items, int n, int k, StreamWriter sw)
        {
            int count = 0;
            for (int i = 0; i < k; i++)
                Razm[i] = 0;

            while (nextCombObject(Razm, n, k))
            {
                foreach (int item in Razm)
                {
                    sw.Write(items[item]);
                }
                sw.Write("\t");
                count++;
            }
            sw.WriteLine(count);
        }

        bool nextCombObject(int[] Razm, int n, int k)
        {
            int j = k - 1;
            while (j >= 0 && Razm[j] == n - 1) j--;
            if (j == -1) return false;
            Razm[j]++;
            for (int i = j + 1; i < k; i++)
                Razm[i] = 0;

            for (int p = 0; p < k - 1; p++)
                for (int l = p + 1; l < k; l++)
                    if (Razm[p] == Razm[l])
                        nextCombObject(Razm, n, k);
            return true;
        }
    }
}
