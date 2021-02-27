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

        private string _name;// backing field 
        [MaxLength(255)]
        public string name
        {
            get { return _name; }
            set
            {
                if (value == _name)
                    return;
                _name = value;

                if (PropertyChanged != null)
                {

                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(name)));
                }
            }
        }
        private string _code;// backing field 
        [MaxLength(255)]
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
    }
}
