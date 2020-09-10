using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MSlab1
{
    public partial class Formula1 : Form
    {
        public Formula1()
        {
            InitializeComponent();
        }       
               

        public double ExpDistribution (double x, double lambda)
        {            
            return 1 - Math.Exp(-x * lambda);
        }

        //розрахунок теоритичної кількості елементів на відрізку
        public double Currency(double a, double b, double lambda)
        {
            return ExpDistribution(b, lambda) - ExpDistribution(a, lambda);
        }        

        public List<double> GenerateRandoms()
        {
            Random random = new Random();
            List<double> randoms = new List<double>();
            for (int i = 0; i < 10000; i++)
            {
                randoms.Add(random.NextDouble());
            }
            return randoms;
        }

        private void MakePlot ()
        {
            List<double> formulaRandoms = new List<double>();
            Random random = new Random();
            List<double> currency = new List<double>();


            int lambda = random.Next(1, 25);
            for (int i=0; i<10000; i++)
            {
                formulaRandoms.Add(-(Math.Log(GenerateRandoms()[i]) / lambda));
            }

            formulaRandoms.Sort();

            double h = (formulaRandoms.Max() - formulaRandoms.Min()) / 20;

            CommonFunctions common = new CommonFunctions();
            int a = 0;
            int count = 0;
            double limit = formulaRandoms.Min() + h;

            List<int> have = new List<int>();
            int intervals = 0;
            int intervalCounter = 0;
            double medium = common.Medium(formulaRandoms);
            double currentH = h;

            for (int i = 0; i<10000; i++)
            {
                intervalCounter++;
                if (formulaRandoms[i] <= limit)
                    count++;                  
                else if ((count < 5) && (intervalCounter>=5))
                {
                    limit += h;
                    currentH += h;
                }
                else
                {
                    chart2.Series["Formula1"].Points.AddXY(limit - currentH / 2, count);
                    limit += h;
                    intervals ++;
                    have.Add(count);
                    if ((formulaRandoms[a] == formulaRandoms[i]) && (i != 9999))
                    {
                        //теоритична частота - інтеграл від функції щільності                       
                        currency.Add(Currency(formulaRandoms[a],formulaRandoms[i+1], 1/medium));
                    }
                    else if ((formulaRandoms[a] == formulaRandoms[i]) && (i == 9999))
                    {
                        currency.Add(Currency(formulaRandoms[a-1], formulaRandoms[i], 1 / medium));
                    }
                    else 
                        currency.Add(Currency(formulaRandoms[a], formulaRandoms[i], 1 / medium));
                    currentH = h;
                    a = i + 1;
                    count = 0;
                    intervalCounter = 0;
                }                
            }
            double chi = common.Chi(have, currency);

            //значення хі-квадрат для різної к-сті степенів свободи 
            double[] tableChi = {3.8, 6.0, 7.8, 9.5, 11.1, 12.6, 14.1, 15.5, 16.9,
                                  18.3, 19.7, 21.0, 22.4, 23.7, 25.0, 26.3, 27.6, 28.9, 27.1};

            if (chi.CompareTo(tableChi[intervals-2]) <= 0) 
                textBox3.Text = "True";
            else textBox3.Text = "False";
            textBox6.Text = tableChi[intervals-2].ToString();
            textBox1.Text = medium.ToString();
            textBox2.Text = common.Dispersion(formulaRandoms).ToString();
            textBox5.Text = lambda.ToString();
            textBox4.Text = chi.ToString();            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            chart2.Series["Formula1"].Points.Clear();
            MakePlot();
        }
    }
}
