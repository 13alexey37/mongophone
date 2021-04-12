using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using bdphones.Annotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace bdphones.model
{
    public class Phone : INotifyPropertyChanged
    {
        private ObjectId _id;
        private string _title;
        private string _company;
        private int _price;

        public ObjectId Id { get; set; }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        public string Company
        {
            get { return _company; }
            set
            {
                _company = value;
                OnPropertyChanged();
            }
        }

        public int Price
        {
            get { return _price; }
            set
            {
                _price = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
