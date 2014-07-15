using Parse;
using System;
using System.Collections.Generic;
using System.Text;

namespace Observations.Entities
{
    public class Observation
    {
        public string ID { get; set; }

        public DateTime ObservationDate { get; set; }

        private List<Learner> learners;
        public List<Learner> Learners
        {
            get
            {
                if (learners == null)
                    return new List<Learner>();
                else
                    return learners;
            }
            set{learners = value;}
        }

        public string Notes { get; set; }

        public ParseFile Image { get; set; }

        public ParseFile Video { get; set; }

        public ParseFile Audio { get; set; }

        public List<LearnerSurname> LearnerSurname { get; set; }
    }
}
