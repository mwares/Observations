using System;
using System.Collections.Generic;
using System.Text;

namespace Observations.Entities
{
    public class Objective
    {
        public string Id { get; set; }

        public string Header { get; set; }

        public string ParentId { get; set; }

        public int Order { get; set; }

        public bool HasChildCategory { get; set; }

    }

    public class ObjectivesGrouped : IComparable<ObjectivesGrouped>
    {
        public string Id { get; set; }

        public string ParentId { get; set; }

        public string Header { get; set; }

        public int Order { get; set; }

        public bool HasChildCategory { get; set; }

        public List<ObjectivesGrouped> ChildObjectives { get; set; }

        public int CompareTo(ObjectivesGrouped other)
        {
            return Order.CompareTo(other.Order);
        }

        public override string ToString()
        {
            return this.Header;
        }
    }
}
