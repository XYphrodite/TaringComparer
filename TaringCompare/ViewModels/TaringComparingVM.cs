using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
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


        public TaringComparingVM()
        {
            _tarings = new ObservableCollection<Taring>();
            _secondTarings = new ObservableCollection<Taring>();
            _selectedTaring = new Taring();
            _secondSelectedTaring = new Taring();
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
                SecondTarings = new ObservableCollection<Taring>(TaringComparison.SelectSuitableTarings(SecondTarings.ToList(), value.LitersMax));
            }
        }
        public Taring SecondSelectedTaring
        {
            get { return _secondSelectedTaring; }
            set
            {
                _secondSelectedTaring = value;
                OnPropertyChanged(nameof(SecondSelectedTaring));
            }
        }

        public ICommand LoadFromJsonCommand { get; }

    }
}
