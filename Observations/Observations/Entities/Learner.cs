using GalaSoft.MvvmLight;
using Parse;
using System;
using System.Collections.Generic;
using System.Text;


namespace Observations.Entities
{
    public class Learner : ObservableObject
    {
        public string Id { get; set; }

        public string Forename { get; set; }

        public string Surname { get; set; }

        public DateTime DateOfBirth { get; set; }

        public ParseFile Image { get; set; }
    }


    public class LearnerSurname : ObservableObject
    {
        private string surname;
        public string Surname
        {
            get { return surname; }
            set
            {
                if (value == surname)
                    return;
                surname = value;
                RaisePropertyChanged("Surname");
            }
        }

        private List<Learner> pupils;

        public List<Learner> Pupils 
        { 
            get{return pupils;}
            set
            {
                if (value == pupils)
                    return;
                pupils = value;
                RaisePropertyChanged("Pupils");
            } 
        }
    }
}
