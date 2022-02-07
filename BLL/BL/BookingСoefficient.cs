using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BL
{
    public class BookingСoefficient
    {
        public double CalculateСoefficient(int countOfDays)
        {
            double coefficient;
            if (countOfDays <= 0)
            {
                countOfDays = 1;
            }
            switch (countOfDays)
            {
                case 1:
                    coefficient = 2;
                    break;
                case 2:
                    coefficient = 1.95;
                    break;
                case 3:
                    coefficient = 1.9;
                    break;
                case 4:
                    coefficient = 1.85;
                    break;
                case 5:
                    coefficient = 1.8;
                    break;
                case 6:
                    coefficient = 1.75;
                    break;
                case 7:
                    coefficient = 1.7;
                    break;
                case 8:
                    coefficient = 1.65;
                    break;
                case 9:
                    coefficient = 1.6;
                    break;
                default:
                    coefficient = 1.5;
                    break;
            }
            return coefficient;
        }
    }
}
