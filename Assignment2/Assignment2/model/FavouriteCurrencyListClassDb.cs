using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using SQLite;

namespace Assignment2.model
{
    public class FavouriteCurrencyListClassDb : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }

        private string _code;// backing field 
        [Unique, MaxLength(10)]
        public string code
        {
            get { return _code; }
            set
            {
                if (value == _code)
                    return;
                _code = value;

                if (PropertyChanged != null)
                {

                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(code)));
                }
            }
        }
        private bool _isHomeCurrency; // backing field 
        public bool isHomeCurrency
        {
            get { return _isHomeCurrency; }
            set
            {
                if (value == _isHomeCurrency)
                    return;
                _isHomeCurrency = value;

                if (PropertyChanged != null)
                {

                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(isHomeCurrency)));
                }
            }
        }

        public FavouriteCurrencyListClassDb()
        {
        }

        public FavouriteCurrencyListClassDb(string code)
        {
            this.code = code;
            this.isHomeCurrency = false;
        }

        public static implicit operator FavouriteCurrencyListClassDb(List<FavouriteCurrencyListClassDb> v)
        {
            throw new NotImplementedException();
        }
    }
}
