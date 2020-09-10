using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MSlab1
{
    public partial class Formula3 : Form
    {
        public Formula3()
        {
            InitializeComponent();
        }

        //розрахунок теоритичної кількості елементів на відрізку
        public double Currency(double x1, double x2, double a, double b)
        {
            return (x2 - x1) / (b - a);
        }
       

        public List<double> GenerateZ()
        {
            Random random = new Random();
            List<double> z = new List<double>();
            long a = Convert.ToInt64(Math.Pow(5, 13));
            long c = Convert.ToInt64(Math.Pow(2, 31));
            z.Add(a * random.NextDouble() % c);
            for (int i = 1; i <= 10000; i++)
            {
                z.Add(a * z[i - 1] % c);
            }
            return z;
        }

        private void MakePlot()
        {
            List<double> formulaRandoms = new List<double>();
            Random random = new Random();
            long c = Convert.ToInt64(Math.Pow(2, 31));
           
            for (int i = 0; i < 10000; i++)
            {
                formulaRandoms.Add(GenerateZ()[i]/c);
            }

            formulaRandoms.Sort();
            CommonFunctions common = new CommonFunctions();
            List<double> currency = new List<double>();
            List<int> have = new List<int>();

            double min = formulaRandoms.Min();
            double max = formulaRandoms.Max();
            double h = (max - min) / 20;

            int count = 0;
            double limit = formulaRandoms.Min() + h;
            int b = 0;
            int intervals = 0;
            int intervalCounter = 0;

            for (int i = 0; i < 10000; i++)
            {
                intervalCounter++;
                if (formulaRandoms[i] <= limit)
                    count++;
                else if ((count < 5) && (intervalCounter >= 5))
                {
                    limit += h;
                }
                else
                {
                    chart1.Series["Formula3"].Points.AddXY(limit - h / 2, count);
                    limit += h;
                    have.Add(count);
                    intervals++;
                   
                    currency.Add(Currency(formulaRandoms[b], formulaRandoms[i], min, max));
                    if (i < 9999) b = i + 1;
                    intervalCounter = 0;
                    count = 0;
                }
            }
            double chi = common.Chi(have, currency);

            //значення хі-квадрат для різної к-сті степенів свободи 
            double[] tableChi = {3.8, 6.0, 7.8, 9.5, 11.1, 12.6, 14.1, 15.5, 16.9,
                                  18.3, 19.7, 21.0, 22.4, 23.7, 25.0, 26.3, 27.6, 28.9, 27.1};

            if (chi.CompareTo(tableChi[intervals - 2]) <= 0)
                textBox3.Text = "True";
            else textBox3.Text = "False";
            textBox1.Text = common.Medium(formulaRandoms).ToString();
            textBox2.Text = common.Dispersion(formulaRandoms).ToString();
            textBox5.Text = tableChi[intervals-2].ToString();
            textBox4.Text = chi.ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series["Formula3"].Points.Clear();
            MakePlot();
        }               
    }
}
