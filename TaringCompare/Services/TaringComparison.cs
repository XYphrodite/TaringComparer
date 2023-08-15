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
            var interpolized = LinearInterpolation(points.Select(p => p.X).ToArray(), points.Select(p => p.Y).ToArray(), precision);
            for (int i = 0; i < interpolized.Item1.Count; i++)
                toReturn.Add(new ObservablePoint { X = interpolized.Item1[i], Y = interpolized.Item2[i] });
            return toReturn;
        }




        //https://www.youtube.com/watch?v=-HvR7MhfFFs
        public static (List<double>, List<double>) NewtonianInterpolation(IEnumerable<double> x_arr, IEnumerable<double> y_arr, int precision)
        {
            int length = x_arr.Count();
            List<double> interpolizedXvalues = new List<double>();
            List<double> interpolizedYvalues = new List<double>();
            double minX = x_arr.Min(), maxX = x_arr.Max();
            List<double> a_arr = new List<double>(); GetRatio();
            double h = (maxX - minX) / precision;
            for (double i = minX; i < maxX; i += h)
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
                    return (y_arr.ElementAt(n) - GetASum(x_arr.ElementAt(n))) / GetXPolynom(x_arr.ElementAt(n), n);
                }
            }
            double GetASum(double x)
            {
                double sum = 0;
                for (int i = 0; i < a_arr.Count; i++)
                    sum += a_arr[i] * GetXPolynom(x, i);
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
        static (List<double>, List<double>) LinearInterpolation(double[] xValues, double[] yValues, int precision)
        {
            if (xValues.Length != yValues.Length)
                throw new ArgumentException("Arrays must have the same length.");

            int n = xValues.Length;
            List<double> interpolizedXvalues = new List<double>();
            List<double> interpolizedYvalues = new List<double>();
            double minX = xValues.Min(), maxX = xValues.Max();
            double h = (maxX - minX) / precision;
            for (double i = minX; i < maxX; i += h)
            {
                interpolizedXvalues.Add(i);
                interpolizedYvalues.Add(Interpolate(i));
            }
            return (interpolizedXvalues, interpolizedYvalues);

            double Interpolate(double x)
            {
                int i = Array.BinarySearch(xValues, x);

                if (i < 0)
                {
                    i = ~i;

                    if (i == 0 || i == n)
                        throw new ArgumentException("X is out of range.");

                    double x1 = xValues[i - 1];
                    double x2 = xValues[i];
                    double y1 = yValues[i - 1];
                    double y2 = yValues[i];

                    double y = y1 + (x - x1) * (y2 - y1) / (x2 - x1);
                    return y;
                }
                else
                {
                    return yValues[i];
                }
            }
        }

        public static double Compare(IEnumerable<double> list1, IEnumerable<double> list2)
        {
            if (list1.Count() != list2.Count()) throw new ArgumentException("Different number of elements in the compared arrays");
            double[] arr = new double[list1.Count()];
            for (int i = 0; i < list1.Count(); i++)
                arr[i] = (list1.ElementAt(i) / list2.ElementAt(i)) is not double.NaN ? list1.ElementAt(i) / list2.ElementAt(i) : 1;
            double sum = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != 1)
                    sum -= Math.Abs(arr[i] - 1)*10;
                sum += 1;
            }
            return sum / arr.Length;
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

    public class CubicSplineInterpolation
    {
        private double[] x;
        private double[] y;
        private double[] a;
        private double[] b;
        private double[] c;
        private double[] d;

        // Конструктор для инициализации массивов
        public CubicSplineInterpolation(double[] x, double[] y)
        {
            this.x = x;
            this.y = y;
            this.a = new double[x.Length];
            this.b = new double[x.Length];
            this.c = new double[x.Length];
            this.d = new double[x.Length];

            CalculateCoefficients();
        }

        // Расчет коэффициентов сплайна
        private void CalculateCoefficients()
        {
            int n = x.Length - 1;

            double[] h = new double[n];
            double[] alpha = new double[n];
            double[] l = new double[n + 1];
            double[] u = new double[n + 1];
            double[] z = new double[n + 1];

            for (int i = 0; i < n; i++)
            {
                h[i] = x[i + 1] - x[i];
            }

            for (int i = 1; i < n; i++)
            {
                alpha[i] = 3 / h[i] * (y[i + 1] - y[i]) - 3 / h[i - 1] * (y[i] - y[i - 1]);
            }

            l[0] = 1;
            u[0] = 0;
            z[0] = 0;

            for (int i = 1; i < n; i++)
            {
                l[i] = 2 * (x[i + 1] - x[i - 1]) - h[i - 1] * u[i - 1];
                u[i] = h[i] / l[i];
                z[i] = (alpha[i] - h[i - 1] * z[i - 1]) / l[i];
            }

            l[n] = 1;
            z[n] = 0;
            c[n] = 0;

            for (int j = n - 1; j >= 0; j--)
            {
                c[j] = z[j] - u[j] * c[j + 1];
                b[j] = (y[j + 1] - y[j]) / h[j] - h[j] * (c[j + 1] + 2 * c[j]) / 3;
                d[j] = (c[j + 1] - c[j]) / (3 * h[j]);
                a[j] = y[j];
            }
        }

        // Интерполяция сплайна
        public double Interpolate(double value)
        {
            int i = FindIndex(value);

            if (i == -1)
            {
                throw new ArgumentException("Значение находится за пределами диапазона интерполяции.");
            }

            double dx = value - x[i];
            return a[i] + b[i] * dx + c[i] * Math.Pow(dx, 2) + d[i] * Math.Pow(dx, 3);
        }

        // Поиск индекса для интерполяции
        private int FindIndex(double value)
        {
            int low = 0;
            int high = x.Length - 1;

            if (value < x[low] || value > x[high])
            {
                return -1;
            }

            while (high - low > 1)
            {
                int mid = (low + high) / 2;

                if (x[mid] <= value)
                {
                    low = mid;
                }
                else
                {
                    high = mid;
                }
            }

            return low;
        }

        public (List<double>, List<double>) Count(int precision)
        {
            int length = x.Length;
            List<double> interpolizedXvalues = new List<double>();
            List<double> interpolizedYvalues = new List<double>();
            double minX = x.Min(), maxX = x.Max();
            double h = (maxX - minX) / precision;
            for (double i = minX; i <= maxX; i += h)
            {
                interpolizedXvalues.Add(i);
                interpolizedYvalues.Add(Interpolate(i));
            }
            return (interpolizedXvalues, interpolizedYvalues);
        }
    }


}
