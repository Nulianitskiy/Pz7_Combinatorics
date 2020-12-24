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

        const int n = 8;
        string[] A = new string[n] { "1", "2", "3", "4", "5", "6", "7", "8" };
        int c = 3;
        string writePath = @"D:\FastSpace\Out.txt";

        private void button1_Click(object sender, EventArgs e)
        { 
            int k = (TextBox.Text != "") ? Convert.ToInt32(TextBox.Text) : 0; // Условный оператор, если текстбокс не пуст, то к = текстбокс, иначе к = 0
            int[] Razm = new int[k - c];

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default);
            recfan2(Razm, A, n, k, c, sw);
            sw.Close();

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
            RichTextBox.Text += elapsedTime;

        }


        void recfan2(int[] Razm, string[] A, int n, int k, int c, StreamWriter sw)
        {
            int[] Pvt = new int[c];
            int[] Word = new int[k];
            for (int i = 0; i < k; i++)
                Word[i] = 0;

            for (int i = 0; i < c; i++)
            {
                Pvt[i] = i;
                Word[i] = 1;
            }
            for (int i = 0; i < k - c; i++)
                Razm[i] = 0;

            foreach (int item in Word)
                sw.Write(A[item]);
            sw.Write("\t");

            while (Sochetaniya_po_k(Pvt, n, c))
                while (Razmeshenie_s_povtoreniem(Razm, k - c, n))
                {
                    unite(Word, Razm, Pvt, k, c);
                    foreach (int item in Word)
                        sw.Write(A[item]);
                    sw.Write("\t");
                }
        }
        bool Sochetaniya_po_k(int[] Razm, int n, int c)
        {
            for (int i = c - 1; i >= 0; i--)
                if (Razm[i] < n - c + i)
                {
                    Razm[i]++;
                    for (int j = i + 1; j < c; j++)
                        Razm[j] = Razm[j - 1] + 1;

                    return true;
                }
            return false;
        }

        bool Razmeshenie_s_povtoreniem(int[] Razm, int k, int n)
        {
            int j = k - 1;
            while (j >= 0 && Razm[j] == n - 1) j--;
            if (j == -1) return false;
            Razm[j]++;

            for (int i = j + 1; i < k; i++)
                Razm[i] = 0;
            return true;
        }

        void unite(int[] Word, int[] Razm, int[] Pvt, int k, int c)
        {
            for (int i = 0; i < k; i++)
                Word[i] = 0;
            for (int i = 0; i < c; i++)
                Word[Pvt[i]] = 1;
           
        }
    }
}
