using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MobileApp.Models.DataModels
{
    public class BaseDataModel : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual bool SetProperty<T>(ref T property, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(property, value)) return false;
            property = value;
            OnPropertyChanged(propertyName);
            return true;

        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}