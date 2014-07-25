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
    public class PupilViewModel : SharedViewModelBase
    {
        public List<LearnerSurname> Items { get; set; }

        //Saves a new or existing pupil
        public async Task SavePupil(Learner pupil)
        {
            try
            {
                ParseObject pupilParse = new ParseObject("Learner");
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

        public async Task<List<LearnerSurname>> LoadDataAsync()
        {
            var pupils = await GetAllPupilsByClass("ClassId");

            var pupilsBySurname = pupils.GroupBy(x => x.Surname[0])
                .Select(x => new LearnerSurname { Surname = x.Key.ToString(), Pupils = x.ToList() })
                .OrderBy(x => x.Surname);
            return pupilsBySurname.ToList();
        }

        //Deletes a pupil
        public async Task DeletePupil(Learner pupil)
        {
            ParseObject PupilToDelete = await GetPupilParseObject(pupil.Id);
            await PupilToDelete.DeleteAsync();
        }

        //Gets a pupil from Parse
        private async Task<ParseObject> GetPupilParseObject(string Id)
        {
            try
            {
                ParseQuery<ParseObject> query = ParseObject.GetQuery("Learner");
                ParseObject po = await query.GetAsync(Id);
                return po;
            }
            catch (ParseException ex)
            {
                throw;
            }
        }

        //Get pupil by Id
        public async Task<Learner> GetPupil(string Id)
        {
            try
            {
                ParseObject pupil = await GetPupilParseObject(Id);

                return await GetLearnerFromParseObject(pupil);
            }
            catch (ParseException ex)
            {
                throw;
            }

        }

        //Get pupil by Surname
        public async Task<List<Learner>> GetPupilsBySurname(string surname)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a list of pupils
        /// TODO:  Impletment class ID filter
        /// </summary>
        /// <param name="ClassId"></param>
        /// <returns></returns>
        public async Task<List<Learner>> GetAllPupilsByClass(string ClassId)
        {
            try
            {
                var query = ParseObject.GetQuery("Learner");
                //IEnumerable<ParseObject> results = await query.FindAsync().ConfigureAwait(false);
                IEnumerable<ParseObject> results = await query.FindAsync();
                List<Learner> pupils = new List<Learner>();
                foreach (var parseObject in results)
                {
                    Learner p = await GetLearnerFromParseObject(parseObject);

                    pupils.Add(p);
                }

                return pupils;
            }
            catch (ParseException ex)
            {
                throw;
            }
        }

        public async Task<List<LearnerSurname>> GetAllPupilsByClassGroupBySurname(string ClassId)
        {
            try
            {
                var query = ParseObject.GetQuery("Learner");
                IEnumerable<ParseObject> results = await query.FindAsync().ConfigureAwait(false);

                var pupils = new List<Learner>();
                foreach (var parseObject in results)
                {
                    Learner p = await GetLearnerFromParseObject(parseObject);

                    pupils.Add(p);
                }

                var PupilsBySurname = pupils.GroupBy(x => x.Surname)
                    .Select(x => new LearnerSurname { Surname = x.Key.ToString(), Pupils = x.ToList() });
                return PupilsBySurname.ToList();

            }
            catch (ParseException ex)
            {
                throw;
            }
        }

        //public async Task<List<GroupInfoList<object>>> GetGroupsByLetter()
        //{
        //    List<GroupInfoList<object>> groups = new List<GroupInfoList<object>>();

        //    try
        //    {
        //        var query = ParseObject.GetQuery("Learner");
        //        IEnumerable<ParseObject> results = await query.FindAsync().ConfigureAwait(false);

        //        var pupils = new List<Learner>();
        //        foreach (var parseObject in results)
        //        {
        //            Learner p = GetLearnerFromParseObject(parseObject);

        //            pupils.Add(p);
        //        }

        //        var query2 = from item in pupils
        //                    orderby ((Learner)item).Surname
        //                    group item by ((Learner)item).Surname[0] into g
        //                    select new { GroupName = g.Key, Items = g };

        //        foreach (var g in query2)
        //        {
        //            GroupInfoList<object> info = new GroupInfoList<object>();
        //            info.Key = g.GroupName;
        //            foreach (var item in g.Items)
        //            {
        //                info.Add(item);
        //            }
        //            groups.Add(info);
        //        }
        //        return groups;
        //    }
        //    catch (ParseException ex)
        //    {
        //        throw;
        //    }

        //    //var query
        //}


        /// <summary>
        /// Returns a pupil object from a parse object
        /// </summary>
        /// <param name="pupilParse"></param>
        /// <returns></returns>
        public async Task<Learner> GetLearnerFromParseObject(ParseObject pupilParse)
        {
            Learner p = new Learner();
            try
            {
                p.Id = pupilParse.ObjectId;
                p.Forename = (await GetParseObject(pupilParse, "String", "Forename")).ToString();
                p.Surname = (await GetParseObject(pupilParse, "String", "Surname")).ToString();
                p.DateOfBirth = (DateTime)(await GetParseObject(pupilParse, "DateTime", "DOB"));
                p.Image = (await GetParseObject(pupilParse, "ParseFile", "Photo") != null) ? pupilParse.Get<ParseFile>("Photo") : await GetDefaultImage();
                if (p.Image != null)
                    p.ImageLocation = p.Image.Url;
            }
            catch (Exception ex)
            {

            }

            return p;
        }



        public async Task<ParseFile> GetDefaultImage()
        {
            Image image = new Image();
            image.Source = ImageSource.FromResource("Observations.Images.765-default-person.png");
            return new ParseFile("DefaultImage", ConvertFileToByteArray.GetEmbeddedResourceBytes(typeof(PupilViewModel).GetTypeInfo().Assembly, "765-default-person.png"));
            //System.Reflection.Assembly asm = Assembly.Load(typeof(PupilManagement).GetTypeInfo().Assembly, ")
        }
    }
}
