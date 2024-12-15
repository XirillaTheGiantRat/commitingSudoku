using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public static class Analysis // Methods that will help us find the optimal parameter
    {

        public static (double, double) MeanAndStd(List<double> list)
        {
            double mean = list.Average();
            double variance = (list.Average(x => Math.Pow(x - mean, 2))) / list.Count; // \frac{\Sigma (x - \overline{x})^2}{N}
            double std = Math.Sqrt(variance);
            return (mean, std);
        }

        public static int GetBestS(List<(int, double)> list) // Get the S corresponding to the minimal mean value of list(S, mean)
        {
            double minTime = list.Min(x => x.Item2);
            int index = list.FindIndex(x => x.Item2 == minTime);
            int BestS = list[index].Item1;
            return BestS;
        }
    }
}