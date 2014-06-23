using Observations.Entities;
using Parse;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Observations.Parse
{
    public class PupilManagement
    {
        //Saves a new or existing pupil
        public async void SavePupil(Pupil pupil)
        {
            ParseObject pupilParse = new ParseObject("Pupil");
            pupilParse.ObjectId = pupil.Id;
            pupilParse["Forename"] = pupil.Forename;
            pupilParse["Surname"] = pupil.Surname;
            pupilParse["DOB"] = pupil.DateOfBirth;
            pupilParse["Photo"] = pupil.Photo;

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
            p.Photo = pupilParse.Get<byte[]>("Photo");

            return p;
        }
    }
}
