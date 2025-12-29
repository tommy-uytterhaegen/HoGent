using HoGentMaui.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoGentMaui.ViewModels
{
    // Dit viewmodel wordt gebruikt voor 1 enkele item in de CollectionView. Gewoonlijk zal die binnenin een lijst zijn, meestal een 'ObservableCollection'
    public class JokeItemViewModel : ViewModel
    {
        private string _text;
        public string Text
        {
            get => _text;
            set
            {
                _text = value;

                OnPropertyChanged();
            }
        }

        private string _dateAsString;
        public string DateAsString
        {
            get => _dateAsString;
            set
            {
                _dateAsString = value;

                OnPropertyChanged();
            }
        }
    }
}
