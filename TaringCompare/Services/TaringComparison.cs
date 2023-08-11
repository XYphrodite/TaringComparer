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
        public static ChartValues<ObservablePoint> Interpolize(ChartValues<ObservablePoint> points, int precision = 100)
        {
            points = new ChartValues<ObservablePoint>(points.OrderBy(p => p.X));
            ChartValues<ObservablePoint> toReturn = new();
            int n = points.Count - 1;
            double minX = points.Min(p => p.X), maxX = points.Max(p => p.X);
            double h = (maxX - minX) / (precision - 1);


            double[] a_arr = new double[n];
            for (int i = 0; i < a_arr.Length; i++)
            {
                a_arr[i] = FindA(a_arr, i);
            }


            for (double i = minX; i < maxX; i += h)
            {
                var newPoint = new ObservablePoint { X = i };
                double newY = FindY(a_arr, i, points.Select(p => p.X).ToArray());
                newPoint.Y = newY;
                toReturn.Add(newPoint);
            }
            return toReturn;









            double FindY(double[] a_arr, double x, double[] x_arr)
            {
                double y = 0;
                for (int i = 0; i < a_arr.Length; i++)
                {
                    y += a_arr[i] * FindXPolynom(x, x_arr, i);
                }
                return y;
            }

            double FindXPolynom(double x, double[] x_arr, int num)
            {
                double xPolynom = 1;
                for (int i = 0; i < num - 1; i++)
                {
                    xPolynom *= (x - x_arr[i-1]);
                }
                return xPolynom;
            }
            double FindA(double[] a_arr, int i)
            {

                if (i == 0)
                    return points[i].Y;
                else
                {
                    double a = points[i].Y - a_arr[i - 1] * FindXPolynom(points[i].X, points.Select(p => p.X).ToArray(), i);
                    return a;
                }
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
