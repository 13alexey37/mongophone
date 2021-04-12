using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using bdphones.Annotations;
using bdphones.model;
using bdphones.viewmodel.tools;
using MongoDB.Bson;
using MongoDB.Driver;

namespace bdphones.viewmodel
{
    class AppViewModel : INotifyPropertyChanged
    {
        private Phone _selectedPhone;
        private MyCommand _addCommand;
        private MyCommand _removeCommand;
        private MyCommand _updateCommand;
        private IMongoCollection<Phone> collection;

        public ObservableCollection<Phone> Phones { get; set; }

        public MyCommand AddCommand
        {
            get
            {
                return _addCommand ?? (_addCommand = new MyCommand(obj =>
                {
                    Phone phone = new Phone();
                    Phones.Insert(0, phone);
                    collection.InsertOne(phone);
                    SelectedPhone = phone;
                }));
            }
            set
            {
                int a = 0;
            }
        }

        public MyCommand RemoveCommand
        {
            get
            {
                return _removeCommand ?? (_removeCommand = new MyCommand(obj =>
                {
                    Phone phone = obj as Phone;
                    Phones.Remove(phone);
                    collection.DeleteOne(p=>p.Title==phone.Title);
                },obj => Phones.Count > 0 && SelectedPhone!=null));
                
            }
        }

        public MyCommand UpdateCommand
        {
            get
            {
                return _updateCommand ?? (_updateCommand = new MyCommand(obj =>
                {
                    
                }));
            }
            set
            {
                int a = 0;
            }
        }

        public Phone SelectedPhone
        {
            get
            {
                return _selectedPhone;
            }
            set
            {
                _selectedPhone = value;
                OnPropertyChanged();
            }
        }

        public AppViewModel()
        {
            var inst = DBManager.Instance;
            collection = DBManager.Instance.Coll;
            using (var cursor = collection.FindSync(new BsonDocument()))
            {
                Phones = new ObservableCollection<Phone>(cursor.ToList());
            }
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
