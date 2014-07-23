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

namespace Observations.ViewModel
{
    public class ObservationViewModel
    {
        public Observation Observation { get; set; }

        public async Task Save(Observation observation)
        {
            try
            {
                ParseObject parseObservation = new ParseObject("Observation");
                parseObservation.ObjectId = (string.IsNullOrEmpty(observation.ID)) ? null : observation.ID;
                parseObservation["ObservationDate"] = observation.ObservationDate.Date;
                parseObservation["Notes"] = observation.Notes;
                
                if(observation.Image != null)
                {
                    await observation.Image.SaveAsync();
                    parseObservation["Image"] = observation.Image;
                }

                if(observation.Video != null)
                {
                    await observation.Video.SaveAsync();
                    parseObservation["Video"] = observation.Video;
                }

                if(observation.Audio != null)
                {
                    await observation.Audio.SaveAsync();
                    parseObservation["Audio"] = observation.Audio;
                }

                var relation = parseObservation.GetRelation<ParseObject>("ObservationLearners");
                foreach (Learner item in observation.Learners)
                {
                    ParseObject parseLearner = new ParseObject("Learner");
                    parseLearner.ObjectId = item.Id;
                    relation.Add(parseLearner);
                }

                await parseObservation.SaveAsync();
            }
            catch (ParseException ex)
            {
                throw;
            }

        }

        public async Task<Observation> GetObservation(string ObservationId)
        {
            try
            {
                ParseQuery<ParseObject> query = ParseObject.GetQuery("Observation");
                ParseObject po = await query.GetAsync("tUtt0ZLC7M");

                var relation = po.GetRelation<ParseObject>("ObservationLearners");
                IEnumerable<ParseObject> ParseLearners = await relation.Query.FindAsync();

                Observation observation = GetObservationFromParseObject(po);

                PupilViewModel pvm = new PupilViewModel();
                List<Learner> learners = new List<Learner>();
                foreach (var parseObject in ParseLearners)
                {
                    Learner l = await pvm.GetLearnerFromParseObject(parseObject);
                    learners.Add(l);
                }

                observation.Learners = learners;
                return observation;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public async Task LoadDataAsync()
        {
            var pupils = await GetObservation("ClassId");

            var pupilsBySurname = pupils.Learners.GroupBy(x => x.Surname[0])
                .Select(x => new LearnerSurname { Surname = x.Key.ToString(), Pupils = x.ToList() })
                .OrderBy(x => x.Surname);
            pupils.LearnerSurname = pupilsBySurname.ToList();

            Observation = pupils;
        }

        private Observation GetObservationFromParseObject(ParseObject observationParse)
        {
            Observation observation = new Observation();
            observation.ID = observationParse.ObjectId;
            //observation.Image = observationParse.Get<ParseFile>("Image");
            //observation.Video = observationParse.Get<ParseFile>("Video");
            //observation.Audio = observationParse.Get<ParseFile>("Audio");
            observation.Notes = observationParse.Get<string>("Notes");

            return observation;
        }

    }
}
