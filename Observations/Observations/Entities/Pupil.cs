using System;
using System.Collections.Generic;
using System.Text;

namespace Observations.Entities
{
    public class Pupil
    {
        public string Id { get; set; }

        public string Forename { get; set; }

        public string Surname { get; set; }

        public DateTime DateOfBirth { get; set; }

        public object Photo { get; set; }
    }

    public class PupilSurname
    {
        public string Forename { get; set; }

        public List<Pupil> Pupils { get; set; }
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
