using System;
using System.Diagnostics; // для таймера
using System.IO;
using System.Windows.Forms;

namespace PZ_7_7_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public int n = 8;
        public int k = 5;
        string writePath = @"Out.txt";

        private void buttonAnswer_Click(object sender, EventArgs e)
        {
            int[] a = new int[n];

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default);
            Print(a, n, sw, k);
            sw.Close();

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
            RichTextBox.Text = elapsedTime;
        }
        
        void swap(int[] a, int i, int j)
        {
            int s = a[i];
            a[i] = a[j];
            a[j] = s;
        }

        void Print(int[] a, int n, StreamWriter sw, int k)
        {
            int q = 2;
            RichTextBox1.Text = "";

            for (int i = 0; i < n; i++)
            {
                a[i] = i + 1;
                RichTextBox1.Text += a[i];
                if (i < 7)
                {
                    RichTextBox1.Text += ",";
                }
            }

            while (NextSet(a, n, k))
            {
                for (int i = 0; i < k; i++)
                {
                    sw.Write(a[i]);
                }
                sw.Write("\t");
                q++;
            }
            sw.Write("Кол-во элементов ="+(q*5));
        }//запись в файл ответа

        bool NextSet(int[] a, int n, int k)
        {
            int j;
            do  // повторяем пока не будет найдено следующее размещение
            {
                j = n - 2;
                while (j != -1 && a[j] >= a[j + 1]) j--;
                if (j == -1)
                    return false; // больше размещений нет
                int m = n - 1;
                while (a[j] >= a[m]) m--;
                swap(a, j, m);
                int l = j + 1, r = n - 1; // сортируем оставшуюся часть последовательности
                while (l < r)
                    swap(a, l++, r--);
            }
            while (j > k - 1);
            return true;
        }
    }
}
