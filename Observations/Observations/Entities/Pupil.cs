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
}
