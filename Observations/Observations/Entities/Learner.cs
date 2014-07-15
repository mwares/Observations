using Parse;
using System;
using System.Collections.Generic;
using System.Text;


namespace Observations.Entities
{
    public class Learner
    {
        public string Id { get; set; }

        public string Forename { get; set; }

        public string Surname { get; set; }

        public DateTime DateOfBirth { get; set; }

        public ParseFile Image { get; set; }
    }

    public class LearnerSurname
    {
        public string Surname { get; set; }

        public List<Learner> Pupils { get; set; }
    }

    public class GroupInfoList<T> : List<object>
    {
        public object Key { get; set; }

        public new IEnumerator<object> GetEnumerator()
        {
            return (System.Collections.Generic.IEnumerator<object>)base.GetEnumerator();
        }
    }
}
