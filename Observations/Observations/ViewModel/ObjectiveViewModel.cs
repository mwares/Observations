using Observations.Entities;
using Parse;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Observations.ViewModel
{
    public class ObjectiveViewModel
    {
        public List<ObjectivesGrouped> Items { get; set; }

        //Get all objectivies
        public async Task<List<Objective>> GetAllObjectivies()
        {
            try
            {
                ParseQuery<ParseObject> queryObjectives = ParseObject.GetQuery("Objectives");
                IEnumerable<ParseObject> resultsObjectivies = await queryObjectives.FindAsync();

                //ParseQuery<ParseObject>
                

                List<Objective> objectives = new List<Objective>();
                foreach (var parseObject in resultsObjectivies)
                {
                    Objective p = GetObjectiveFromParseObject(parseObject);

                    objectives.Add(p);
                }

                return objectives;
                //return await GetParentAndChildObjectivies("0", objectives);

            }
            catch (ParseException ex)
            {
                throw;
            }
        }

        public async Task LoadDataAsync()
        {
            var objectives = await GetAllObjectivies();

            //var objectivesGrouped = objectives.GroupBy(x => x.ParentId)
            //    .Select(x => new ObjectivesGrouped { ParentId = x.Key.ToString(), ChildObjectives = x.ToList() })
            //    .OrderBy(x => x.Title);
            Items = await GetParentAndChildObjectivies("0", objectives);

        }

        private async Task<List<ObjectivesGrouped>> GetParentAndChildObjectivies(string parentId, List<Objective> allObjectivies)
        {
            List<ObjectivesGrouped> objectiviesGrouped = new List<ObjectivesGrouped>();

            foreach (var item in allObjectivies.Where(x => x.ParentId.Equals(parentId)).OrderBy(x => x.Order))
            {
                objectiviesGrouped.Add(new ObjectivesGrouped
                {
                    Id = item.Id,
                    ParentId = item.ParentId,
                    Header = item.Header,
                    Order = item.Order,
                    HasChildCategory = item.HasChildCategory,
                    ChildObjectives = await GetParentAndChildObjectivies(item.Id, allObjectivies)
                });
            }

            return objectiviesGrouped;
        }

        /// <summary>
        /// Returns a Objective object from a parse object
        /// </summary>
        /// <param name="objectiveParse"></param>
        /// <returns></returns>
        private Objective GetObjectiveFromParseObject(ParseObject objectiveParse)
        {
            Objective o = new Objective();
            o.Id = objectiveParse.ObjectId;
            o.Header = objectiveParse.Get<string>("Header");
            o.ParentId = (objectiveParse.Get<string>("ParentObjectId") != null) ? objectiveParse.Get<string>("ParentObjectId") : null;
            o.Order = objectiveParse.Get<int>("Order");
            o.HasChildCategory = objectiveParse.Get<bool>("HasChildCategory");
            return o;
        }
    }
}
