using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace WebUwp.Core.Models
{
    [DataContract]
    public class User //: INotifyPropertyChanged
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public byte[] PasswordHash { get; set; }
        [DataMember]
        public byte[] PasswordSalt { get; set; }
        /*
                public string FullName => $"{FirstName} {LastName}";

                public event PropertyChangedEventHandler PropertyChanged;
                private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));



                public string firstName
                {
                    get => FirstName;
                    set
                    {
                        if (value != FirstName) return;
                        FirstName = value;
                        OnPropertyChanged(nameof(firstName));
                        OnPropertyChanged(nameof(FullName));
                    }
                }

                public string lastName
                {
                    get => LastName;
                    set
                    {
                        if (value != LastName) return;
                        FirstName = value;
                        OnPropertyChanged(nameof(lastName));
                        OnPropertyChanged(nameof(FullName));
                    }
                }

                public string username
                {
                    get => Username;
                    set
                    {
                        if (value != Username) return;
                        FirstName = value;
                        OnPropertyChanged(nameof(username));
                    }
                }




                public override string ToString()
                {
                    return FirstName + LastName;
                }
                */
    }

}
