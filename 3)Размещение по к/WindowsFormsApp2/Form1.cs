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
        int n = 0, k = 0;
        string writePath = @"D:\FastSpace\Out.txt";

        private void button2_Click(object sender, EventArgs e)
        {
            Alf.Add(TextBoxAlfIn.Text);
            n++;
            TextBoxAlfOut.Text += TextBoxAlfIn.Text + " ";
            TextBoxAlfIn.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            k = (TextBoxWord.Text != "") ? Convert.ToInt32(TextBoxWord.Text) : 0;
            int[] Razm = new int[k];
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default);
            recfan3(Razm, Alf, n, k, sw);
            sw.Close();

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
            RichTextBox.Text = elapsedTime;
        }


        //void recfan2(int[] Razm, List<string> Alf, int n, int k, StreamWriter sw)
        //{
        //    for (int i = 0; i < k; i++)
        //    {
        //        Razm[i] = i + 1;
        //        sw.Write(Alf[i]);//Вывод первого КО
        //    }
        //    sw.Write("\t");

        //    while (CombObject(Razm, n, k))
        //    {
        //        foreach (int item in Razm)
        //        {
        //            sw.Write(Alf[item]);
        //        }
        //        sw.Write("\t");
        //    }

        //}

        bool CombObject(int[] Razm, int n)
        {
                int i = n - 2;
                while (i >= 0 && Razm[i] >= Razm[i+1]) i--;
                if (i != -1)
                {
                    int j = i + 1;
                    while (Razm[i] < Razm[j + 1])
                        j++;
                    int t = Razm[i];
                    Razm[i] = Razm[j];
                    Razm[j] = t;
                }
                else return false;

                for (int x = i + 1, y = n - 1; x < y; x++, y--)
                {
                    int t = Razm[x];
                    Razm[x] = Razm[y];
                    Razm[y] = t;
                }
            return true;
        }

        void printCO (int[] Razm, List<string> Alf, int n, StreamWriter sw)
        {
            foreach (int item in Razm)
            {
                sw.Write(Alf[item - 1]);
            }
            sw.Write("\t");
        }

        void recfan3(int[] Razm, List<string> Alf, int n, int k, StreamWriter sw)
        {
            int i;
            for (i = 0; i < n; i++) Razm[i] = i + 1;
            do
            {
                printCO(Razm, Alf, n, sw);

                i = n - 2;
                while (i >= 0 && Razm[i] >= Razm[i + 1]) i = i - 1;

                if (i >= 0)
                {
                    int j = i + 1;
                    while (Razm[i] < Razm[j + 1]) j++;
                    int temp = Razm[i];
                    Razm[i] = Razm[j];
                    Razm[j] = temp;
                }

                for (int x = i + 1, y = n - 1; x < y; x++, y--)
                {
                    int temp = Razm[x];
                    Razm[x] = Razm[y];
                    Razm[y] = temp;
                }

            } while (i >= 0);
        }
        //bool generate()
        //{
        //    int m = k;
        //    for (int i = n - 1; i >= 0; --i)
        //    {
        //        if (!Razm[i])
        //        {
        //            a[m] = i + 1;
        //            m++;
        //        }
        //    }
        //    if (next_permutation(a.begin(), a.end()))
        //    {
        //        for (int i = k; i < n; ++i) Razm[a[i] - 1] = 0;
        //        for (int i = 0; i < k; ++i) Razm[a[i] - 1] = 1;
        //        return 1;
        //    }
        //    else
        //        return 0;
        //}
    }
}
