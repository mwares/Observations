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
        public List<PupilSurname> Items { get; set; }
        private PupilManagement pm;

        public PupilViewModel()
        {
            pm = new PupilManagement();
        }

        public async Task LoadDataAsync()
        {
            PupilManagement pm = new PupilManagement();
            var pupils = await pm.GetAllPupilsByClass("ClassId");

            var pupilsBySurname = pupils.GroupBy(x => x.Surname[0])
                .Select(x => new PupilSurname { Surname = x.Key.ToString(), Pupils = x.ToList() })
                .OrderBy(x => x.Surname);
            Items = pupilsBySurname.ToList();
        }

        public async Task AddPupil()
        {

        }

        public async Task Save(Pupil pupil)
        {
            await pm.SavePupil(pupil);
        }
    }
}
