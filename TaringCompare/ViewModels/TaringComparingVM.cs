using LiveCharts.Defaults;
using LiveCharts;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TaringCompare.Commands;
using TaringCompare.Models;
using TaringCompare.Services;
using LiveCharts.Wpf;

namespace TaringCompare.ViewModels
{
    public class TaringComparingVM : ViewModelBase
    {
        private ObservableCollection<Taring> _tarings;
        private ObservableCollection<Taring> _secondTarings;
        private Taring _selectedTaring;
        private Taring _secondSelectedTaring;
        private string _outputStr;
        private string _firstTaringInfo;
        private string _secondTaringInfo;
        private ChartValues<ObservablePoint> _firtsTaringPoints;
        private ChartValues<ObservablePoint> _secondTaringPoints;
        private ChartValues<ObservablePoint> _firstInterpolated;
        private ChartValues<ObservablePoint> _secondInterpolated;



        public TaringComparingVM()
        {
            _tarings = new ObservableCollection<Taring>();
            _secondTarings = new ObservableCollection<Taring>();
            _selectedTaring = new Taring();
            _secondSelectedTaring = new Taring();

            LoadFromJsonCommand = new RelayCommand(execute => AddFromJsonCommand());


        }
        public ObservableCollection<Taring> Tarings
        {
            get { return _tarings; }
            set
            {
                _tarings = value;
                OnPropertyChanged(nameof(Tarings));
            }
        }
        public ObservableCollection<Taring> SecondTarings
        {
            get { return _secondTarings; }
            set
            {
                _secondTarings = value;
                OnPropertyChanged(nameof(SecondTarings));
            }
        }
        public Taring SelectedTaring
        {
            get { return _selectedTaring; }
            set
            {
                _selectedTaring = value;
                OnPropertyChanged(nameof(SelectedTaring));
                SecondTarings = new ObservableCollection<Taring>(TaringComparison.SelectSuitableTarings(Tarings.ToList(), value.LitersMax));
                FirstTaringInfo = TaringComparison.GetTaringInfo(value);

                var points = new ChartValues<ObservablePoint>();
                _selectedTaring.TaringList.ForEach(ti =>
                {
                    points.Add(new ObservablePoint(ti.RawLevel, ti.LitersLevel));

                });
                FirstTaringPoints = points;
                FirstInterpolated = TaringComparison.Interpolize(points,100);


            }
        }
        public Taring SecondSelectedTaring
        {
            get { return _secondSelectedTaring; }
            set
            {
                _secondSelectedTaring = value;
                OnPropertyChanged(nameof(SecondSelectedTaring));
                SecondTaringInfo = TaringComparison.GetTaringInfo(SecondSelectedTaring);
                var points = new ChartValues<ObservablePoint>();
                if (_secondSelectedTaring is not null)
                {
                    _secondSelectedTaring.TaringList.ForEach(ti =>
                    {
                        points.Add(new ObservablePoint(ti.RawLevel, ti.LitersLevel));
                    });
                    SecondTaringPoints = points;
                    SecondInterpolated = TaringComparison.Interpolize(points, 100);
                }
            }
        }

        public string OutputStr
        {
            get
            {
                return _outputStr;
            }
            set
            {
                _outputStr = value;
                OnPropertyChanged(nameof(OutputStr));
            }
        }
        public string FirstTaringInfo
        {
            get { return _firstTaringInfo; }
            set
            {
                _firstTaringInfo = value;
                OnPropertyChanged(nameof(FirstTaringInfo));
            }
        }
        public string SecondTaringInfo
        {
            get { return _secondTaringInfo; }
            set
            {
                _secondTaringInfo = value;
                OnPropertyChanged(nameof(SecondTaringInfo));
            }
        }
        public ChartValues<ObservablePoint> SecondTaringPoints
        {
            get { return _secondTaringPoints; }
            set
            {
                _secondTaringPoints = value;
                OnPropertyChanged(nameof(SecondTaringPoints));
            }
        }
        public ChartValues<ObservablePoint> FirstTaringPoints
        {
            get { return _firtsTaringPoints; }
            set
            {
                _firtsTaringPoints = value;
                OnPropertyChanged(nameof(FirstTaringPoints));
            }
        }
        public ChartValues<ObservablePoint> FirstInterpolated
        {
            get { return _firstInterpolated; }
            set
            {
                _firstInterpolated = value;
                OnPropertyChanged(nameof(FirstInterpolated));
            }
        }
        public ChartValues<ObservablePoint> SecondInterpolated
        {
            get { return _secondInterpolated; }
            set
            {
                _secondInterpolated = value;
                OnPropertyChanged(nameof(SecondInterpolated));
            }
        }

        public ICommand LoadFromJsonCommand { get; }
        public ICommand Compare { get; }

        private void AddFromJsonCommand()
        {
            Tarings = new ObservableCollection<Taring>(TaringLoader.LoadFromJson());
            OutputStr = $"{Tarings.Count} was loaded from json!";
        }

    }
}
