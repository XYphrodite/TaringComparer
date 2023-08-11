using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TaringCompare.Commands;
using TaringCompare.Models;
using TaringCompare.Services;

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



        public TaringComparingVM()
        {
            _tarings = new ObservableCollection<Taring>();
            _secondTarings = new ObservableCollection<Taring>();
            _selectedTaring = new Taring();
            _secondSelectedTaring = new Taring();

            LoadFromJsonCommand = new RelayCommand(execute => AddFromJsonCommand());
            Compare = new RelayCommand(compare => CompareTaring());


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
                OnPropertyChanged(nameof(SecondTarings));
                FirstTaringInfo = TaringComparison.GetTaringInfo(value);
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
                OnPropertyChanged(nameof(SecondTaringInfo));
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

        public ICommand LoadFromJsonCommand { get; }
        public ICommand Compare { get; }

        private void AddFromJsonCommand()
        {
            Tarings = new ObservableCollection<Taring>(TaringLoader.LoadFromJson());
            OutputStr = $"{Tarings.Count} was loaded from json!";
        }
        private void CompareTaring()
        {
            var l1 = TaringComparison.Interpolize(SelectedTaring.TaringList, 100);
            var l2 = TaringComparison.Interpolize(SecondSelectedTaring.TaringList, 100);
            var res = TaringComparison.Compare(l1, l2);
            //draw charts
        }

    }
}
