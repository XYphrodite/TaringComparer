using System;
using System.Collections.Generic;
using System.Linq;
using TaringCompare.Models;

namespace TaringCompare.Services
{
    public class TaringComparison
    {
        public List<double> Interpolize(List<TaringItem> list, int precision)
        {
            throw new NotImplementedException();
            //return new List<double>();
        }

        public static double Compare(List<double> list1, List<double> list2)
        {
            if (list1.Count != list2.Count) throw new ArgumentException("Different number of elements in the compared arrays");
            double[] arr = new double[list1.Count];
            for (int i = 0; i < list1.Count; i++)
            {
                arr[i] = list1[i] / list2[i];
            }
            return arr.Sum() / arr.Length;
        }
    }
}
