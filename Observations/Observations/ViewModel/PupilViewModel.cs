using Observations.Entities;
using Parse;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;


using Xamarin.Forms;
using Observations.Helper;
using System.Reflection;
//using Windows.ApplicationModel.Resources.Core;

namespace Observations.ViewModel
{
    public class PupilViewModel
    {
        public List<PupilSurname> Items { get; set; }

        //Saves a new or existing pupil
        public async Task SavePupil(Pupil pupil)
        {
            try
            {
                ParseObject pupilParse = new ParseObject("Pupil");
                pupilParse.ObjectId = (string.IsNullOrEmpty(pupil.Id)) ? null : pupil.Id;
                pupilParse["Forename"] = pupil.Forename;
                pupilParse["Surname"] = pupil.Surname;
                pupilParse["DOB"] = pupil.DateOfBirth;

                if (pupil.Image != null)
                {
                    await pupil.Image.SaveAsync();
                    pupilParse["Photo"] = pupil.Image;
                }
                await pupilParse.SaveAsync();
            }
            catch (ParseException ex)
            {
                throw;
            }
        }

        public async Task LoadDataAsync()
        {
            var pupils = await GetAllPupilsByClass("ClassId");

            var pupilsBySurname = pupils.GroupBy(x => x.Surname[0])
                .Select(x => new PupilSurname { Surname = x.Key.ToString(), Pupils = x.ToList() })
                .OrderBy(x => x.Surname);
            Items = pupilsBySurname.ToList();
        }

        //Deletes a pupil
        public async Task DeletePupil(Pupil pupil)
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
            p.Image = (pupilParse.Get<ParseFile>("Photo") != null) ? pupilParse.Get<ParseFile>("Photo") : GetDefaultImage();

            return p;
        }

        private ParseFile GetDefaultImage()
        {
            Image image = new Image();
            image.Source = ImageSource.FromResource("Observations.Images.765-default-person.png");
            return new ParseFile("DefaultImage", ConvertFileToByteArray.GetEmbeddedResourceBytes(typeof(PupilViewModel).GetTypeInfo().Assembly, "765-default-person.png"));
            //System.Reflection.Assembly asm = Assembly.Load(typeof(PupilManagement).GetTypeInfo().Assembly, ")
        }
    }
}
