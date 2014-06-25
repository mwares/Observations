using System;
using System.Collections.Generic;
using System.Text;
using Observations.Entities;
using Observations.Parse;
using System.Linq;
using System.Threading.Tasks;
using Observations.Helper;

namespace Observations.ViewModel
{
    public class PupilViewModel
    {
        //public List<PupilSurname> Pupils { get; set; }
        public NotifyTaskCompletion<List<PupilSurname>> Pupils { get; private set; }
        public NotifyTaskCompletion<List<GroupInfoList<object>>> PupilsBySurnameLetter { get; private set; }

        public PupilViewModel()
        {
            PupilManagement pm = new PupilManagement();
            Pupils = new NotifyTaskCompletion<List<PupilSurname>>(pm.GetAllPupilsByClassGroupBySurname("ClassId"));
            PupilsBySurnameLetter = new NotifyTaskCompletion<List<GroupInfoList<object>>>(pm.GetGroupsByLetter());

            
            //Pupils = PupilsBySurname.ToList();
        }

        private async Task<List<Pupil>> GetAllPupilsByClassAsync(string ClassId)
        {
            PupilManagement pm = new PupilManagement();
            return await pm.GetAllPupilsByClass("ClassId");




        }

    }
}
