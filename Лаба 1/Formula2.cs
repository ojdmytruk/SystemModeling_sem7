using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MSlab1
{
    public partial class Formula2 : Form
    {
        public Formula2()
        {
            InitializeComponent();
        }
        
        public double Integral(Func<double, double> f, double a, double b)
        {
            double h = (b - a) / 500;
            double partSum1 = 0;
            double partSum2 = 0;
            for (int i = 1; i <= 500; i++)
            {
                double xi = a + i * h;
                if (i <= 500 - 1)
                {
                    partSum1 += f(xi);
                }

                double xi_1 = a + (i - 1) * h;
                partSum2 += f((xi + xi_1) / 2);
            }

            var result = h / 3d * (1d / 2d * f(a) + partSum1 + 2 * partSum2 + 1d / 2d * f(b));
            return result;
        }


        private void MakePlot()
        {
            
            List<double> formulaRandoms = new List<double>();
            Random random = new Random();           

            double alpha = random.NextDouble();
            double sigma = random.NextDouble();
            textBox6.Text = alpha.ToString();
            textBox7.Text = sigma.ToString();
            List<double> miu = new List<double>();
            for (int i = 0; i < 10000; i++)
            {
                miu.Add(random.NextDouble());                
            }
            int kForSum = 12;
            for (int i = 0; i < 10000; i++)
            {
                double miuSum = new double();

                if (10000 - i < 12)
                {
                    kForSum = 10000 - i;
                }
                for (int k = i; k < i + kForSum - 1; k++)
                {
                    miuSum += miu[k];
                }
                miuSum -= 6;
                formulaRandoms.Add(sigma * miuSum + alpha);
                
            }

            formulaRandoms.Sort();

            double h = (formulaRandoms.Max() - formulaRandoms.Min()) / 20;

            int count = 0;
            double limit = formulaRandoms.Min() + h;
            
            CommonFunctions common = new CommonFunctions();
            double medium = common.Medium(formulaRandoms);
            double disp = common.Dispersion(formulaRandoms);
            double NormDistribution(double x) => /*1*/ Math.Exp(-(Math.Pow(x - medium, 2) / (2 * disp))) / Math.Sqrt(2 * Math.PI * disp);


            List<int> have = new List<int>();
            int a = 0;
            List<double> currency = new List<double>();
            int intervals = 0;
            double currentH = h;
            int intervalCounter = 0;


            for (int i = 0; i < 10000; i++)
            {
                intervalCounter++;
                if (formulaRandoms[i] <= limit)
                    count++;
                else if ((count < 5) && (intervalCounter >= 5))
                {
                    limit += h;
                    currentH += h;
                }
                else
                {
                    chart1.Series["Formula2"].Points.AddXY(limit - currentH / 2, count);
                    limit += h;
                    intervals++;
                    if (a != 0) have.Add(count);
                    if ((a != 0) && (formulaRandoms[a] == formulaRandoms[i]) && (i != 9999))
                    {
                        //теоритична частота - інтеграл від функції щільності                       
                        currency.Add(Integral(NormDistribution, formulaRandoms[a], formulaRandoms[i + 1]));
                    }
                    else if ((a != 0) && (formulaRandoms[a] == formulaRandoms[i]) && (i == 9999))
                    {
                        currency.Add(Integral(NormDistribution, formulaRandoms[a - 1], formulaRandoms[i]));
                    }
                    else if (a != 0)
                        currency.Add(Integral(NormDistribution, formulaRandoms[a], formulaRandoms[i]));
                    //currency.Add(Integral(NormDistribution, limit - currentH, limit));
                    currentH = h;
                    
                    a = i+1;
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
            textBox1.Text = medium.ToString();
            textBox2.Text = disp.ToString();
            textBox5.Text = tableChi[intervals-3].ToString();
            textBox4.Text = chi.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series["Formula2"].Points.Clear();
            MakePlot();
        }
    }
}

