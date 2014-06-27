using Observations.Entities;
using Parse;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Observations.Parse
{
    public class PupilManagement
    {
        //Saves a new or existing pupil
        public async void SavePupil(Pupil pupil)
        {
            ParseObject pupilParse = new ParseObject("Pupil");
            pupilParse.ObjectId = (string.IsNullOrEmpty(pupil.Id)) ? null : pupil.Id;
            pupilParse["Forename"] = pupil.Forename;
            pupilParse["Surname"] = pupil.Surname;
            pupilParse["DOB"] = pupil.DateOfBirth;
            pupilParse["Photo"] = pupil.Image;

            await pupilParse.SaveAsync();
        }

        //Deletes a pupil
        public async void DeletePupil(Pupil pupil)
        {
            ParseObject PupilToDelete = await GetPupilParseObject(pupil.Id);
            await PupilToDelete.DeleteAsync();
        }

        //Gets a pupil from Parse
        private async Task<ParseObject> GetPupilParseObject(string Id)
        {
            try
            {
                ParseQuery<ParseObject> query = ParseObject.GetQuery("Pupil");
                ParseObject po = await query.GetAsync(Id);
                return po;
            }
            catch (ParseException ex)
            {
                throw;
            }
        }

        //Get pupil by Id
        public async Task<Pupil> GetPupil(string Id)
        {
            try
            {
                ParseObject pupil = await GetPupilParseObject(Id);

                return GetPupilFromParseObject(pupil);
            }
            catch (ParseException ex)
            {
                throw;
            }

        }

        //Get pupil by Surname
        public async Task<List<Pupil>> GetPupilsBySurname(string surname)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a list of pupils
        /// TODO:  Impletment class ID filter
        /// </summary>
        /// <param name="ClassId"></param>
        /// <returns></returns>
        public async Task<List<Pupil>> GetAllPupilsByClass(string ClassId)
        {
            try
            {
                var query = ParseObject.GetQuery("Pupil");
                //IEnumerable<ParseObject> results = await query.FindAsync().ConfigureAwait(false);
                IEnumerable<ParseObject> results = await query.FindAsync();
                List<Pupil> pupils = new List<Pupil>();
                foreach (var parseObject in results)
                {
                    Pupil p = GetPupilFromParseObject(parseObject);
                    
                    pupils.Add(p);
                }

                return pupils;
            }
            catch (ParseException ex)
            {
                throw;
            }
        }

        public async Task<List<PupilSurname>> GetAllPupilsByClassGroupBySurname(string ClassId)
        {
            try
            {
                var query = ParseObject.GetQuery("Pupil");
                IEnumerable<ParseObject> results = await query.FindAsync().ConfigureAwait(false);

                var pupils = new List<Pupil>();
                foreach (var parseObject in results)
                {
                    Pupil p = GetPupilFromParseObject(parseObject);

                    pupils.Add(p);
                }

                var PupilsBySurname = pupils.GroupBy(x => x.Surname)
                    .Select(x => new PupilSurname { Surname = x.Key.ToString(), Pupils = x.ToList() });
                return PupilsBySurname.ToList();

            }
            catch (ParseException ex)
            {
                throw;
            }
        }

        public async Task<List<GroupInfoList<object>>> GetGroupsByLetter()
        {
            List<GroupInfoList<object>> groups = new List<GroupInfoList<object>>();

            try
            {
                var query = ParseObject.GetQuery("Pupil");
                IEnumerable<ParseObject> results = await query.FindAsync().ConfigureAwait(false);

                var pupils = new List<Pupil>();
                foreach (var parseObject in results)
                {
                    Pupil p = GetPupilFromParseObject(parseObject);

                    pupils.Add(p);
                }

                var query2 = from item in pupils
                            orderby ((Pupil)item).Surname
                            group item by ((Pupil)item).Surname[0] into g
                            select new { GroupName = g.Key, Items = g };

                foreach (var g in query2)
                {
                    GroupInfoList<object> info = new GroupInfoList<object>();
                    info.Key = g.GroupName;
                    foreach (var item in g.Items)
                    {
                        info.Add(item);
                    }
                    groups.Add(info);
                }
                return groups;
            }
            catch (ParseException ex)
            {
                throw;
            }

            //var query
        }


        /// <summary>
        /// Returns a pupil object from a parse object
        /// </summary>
        /// <param name="pupilParse"></param>
        /// <returns></returns>
        private Pupil GetPupilFromParseObject(ParseObject pupilParse)
        {
            Pupil p = new Pupil();
            p.Id = pupilParse.ObjectId;
            p.Forename = pupilParse.Get<string>("Forename");
            p.Surname = pupilParse.Get<string>("Surname");
            p.DateOfBirth = pupilParse.Get<DateTime>("DOB");
            //p.Image = pupilParse.Get<string>("Photo");
            p.Image = GetRandomImage();

            return p;
        }

        private string GetRandomImage()
        {
            Random r = new Random(1);
            switch (r.Next(3))
            {
                case 1:
                    return "http://cf2.imgobject.com/t/p/w500/bueVXkpCDPX0TlsWd3Uk7QKO3kD.jpg";
                case 2:
                    return "http://cf2.imgobject.com/t/p/w500/mzlw8rHGUSDobS1MJgz8jXXPM06.jpg";
                case 3:
                    return "http://cf2.imgobject.com/t/p/w500/5LjbAjkPBUOD9N2QFPSuTyhomx4.jpg";
                default:
                    return "http://cf2.imgobject.com/t/p/w500/bueVXkpCDPX0TlsWd3Uk7QKO3kD.jpg";
            }
        }
    }
}
