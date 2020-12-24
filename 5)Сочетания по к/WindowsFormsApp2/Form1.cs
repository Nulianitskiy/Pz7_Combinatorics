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
        string writePath = @"D:\Out.txt";//Адрес куда записывать результат

        private void button1_Click(object sender, EventArgs e)
        { 
            int k = (TextBoxK.Text != "") ? Convert.ToInt32(TextBoxK.Text) : 0; // Условный оператор, если текстбокс не пуст, то к = текстбокс, иначе к = 0
            int[] Razm = new int[k];

            int n = (TextBoxN.Text != "") ? Convert.ToInt32(TextBoxN.Text) : 0;
            
            char[] A = new char[n];
            for (int i = 0; i < n; i++)
                A[i] = (char)((char)97 + i);//Хочу, чтобы были буквы
            
            if (n >= k)
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();

                StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default);
                recfan2(Razm, A, n, k, sw);
                sw.Close();

                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
                RichTextBox.Text = elapsedTime;
            }
            else RichTextBox.Text = "Ошибка";
        }


        void recfan2(int[] Razm, char[] A, int n, int k, StreamWriter sw)
        {
            for (int i = 0; i < k; i++)
            {
                Razm[i] = i;
                sw.Write(A[i]);//Вывод первого элемента
            }
            sw.Write("\t");

            while (CombObject(Razm, k, n))//Пока есть сочетания, пишем их
            {
                foreach (int item in Razm)
                    sw.Write(A[item]);

                sw.Write("\t");
            }
        }

        bool CombObject(int[] Razm, int k, int n)
        {
            for (int i = k - 1; i >= 0; i--)
                if (Razm[i] < n - k + i)
                {
                    Razm[i]++;
                    for (int j = i + 1; j < k; j++)
                        Razm[j] = Razm[j - 1] + 1;
                    return true;
                }
            return false;
        }

    }
}
