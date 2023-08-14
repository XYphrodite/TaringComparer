using LiveCharts;
using LiveCharts.Defaults;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using TaringCompare.Models;

namespace TaringCompare.Services
{
    public class TaringComparison
    {
        //https://www.youtube.com/watch?v=-HvR7MhfFFs
        public static ChartValues<ObservablePoint> Interpolize(ChartValues<ObservablePoint> points, int precision = 100)
        {
            points = new ChartValues<ObservablePoint>(points.OrderBy(p => p.X));
            ChartValues<ObservablePoint> toReturn = new();
            var interpolized = NewtonianInterpolation(points.Select(p => p.X), points.Select(p => p.Y));
            for(int i=0;i<interpolized.Item1.Count;i++)
                toReturn.Add(new ObservablePoint { X = interpolized.Item1[i], Y = interpolized.Item2[i] });
            return toReturn;
        }


        public static (List<double>,List<double>) NewtonianInterpolation(IEnumerable<double> x_arr, IEnumerable<double> y_arr, int precision = 100)
        {
            int length = x_arr.Count();
            List<double> interpolizedXvalues = new List<double>();
            double minX = x_arr.Min(), maxX = x_arr.Max();
            List<double> a_arr = new List<double>();
            GetRatio();
            List<double> interpolizedYvalues = new List<double>();
            double h = (maxX - minX) / precision;
            for(double i = minX; i < maxX; i += h)
            {
                interpolizedXvalues.Add(i);
                interpolizedYvalues.Add(GetASum(i));
            }


            return (interpolizedXvalues, interpolizedYvalues);

            void GetRatio()
            {
                for (int i = 0; i < length; i++)
                    a_arr.Add(CountRatio());


                double CountRatio()
                {
                    int n = a_arr.Count;
                    return (y_arr.ElementAt(n) - (GetASum(x_arr.ElementAt(n)))) / (GetXPolynom(x_arr.ElementAt(n), n));
                }
            }


            double GetASum(double x)
            {
                double sum = 0;
                for (int i = 0; i < a_arr.Count; i++)
                {
                    sum += a_arr[i] * GetXPolynom(x, i);
                }
                return sum;
            }

            double GetXPolynom(double x, int num)
            {
                double x_p = 1;
                for (int i = 0; i < num; i++)
                    x_p *= (x - x_arr.ElementAt(i));
                return x_p;
            }
        }


        public static double Compare(List<double> list1, List<double> list2)
        {
            if (list1.Count != list2.Count) throw new ArgumentException("Different number of elements in the compared arrays");
            double[] arr = new double[list1.Count];
            for (int i = 0; i < list1.Count; i++)
                arr[i] = list1[i] / list2[i];
            return arr.Sum() / arr.Length;
        }

        public static List<Taring> SelectSuitableTarings(List<Taring> list, ushort litersMax)
        {
            List<Taring> tars = new();
            list.ForEach(t =>
            {
                if (t.LitersMax == litersMax) tars.Add(t);
            });
            return tars;
        }

        public static string GetTaringInfo(Taring taring) => taring != null ? $"Title: {taring.Title}, Description: {taring.Description}, LitersMax: {taring.LitersMax}, Amount of taring items: {taring.TaringList.Count}" : string.Empty;
    }
}
