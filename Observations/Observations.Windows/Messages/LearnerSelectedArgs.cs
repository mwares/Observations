using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Observations.Entities;

namespace Observations.WindowsRT.Messages
{
    public class LearnerSelectedArgs
    {
        public Learner Learner { get; set; }

        public LearnerSelectedArgs(Learner learner)
        {
            Learner = learner;
        }
    }
}
