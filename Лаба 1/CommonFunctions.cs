using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSlab1
{
    public class CommonFunctions
    {
        //Середнє значення
        public double Medium(List<double> randoms)
        {
            return randoms.Sum() / randoms.Count;
        }

        //Дисперсія
        public double Dispersion(List<double> randoms)
        {
            double medium = Medium(randoms);
            double sum = 0;
            for (int i = 0; i < randoms.Count; i++)
            {
                double n = Math.Pow((randoms[i] - medium), 2);
                sum += n;
            }
            return sum / (randoms.Count - 1);
        }

        //Обчислення значення критерію хі-квадрат
        public double Chi(List<int> have, List<double> currency)
        {
            double hi = 0;
            for (int i = 0; i < currency.Count; i++)
            {
                hi += Math.Pow((have[i] - currency[i] * 10000), 2) / (currency[i] * 10000);
            }
            return hi;
        }
    }
}
