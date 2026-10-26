using System;
using System.Collections.Generic;
using System.Text;


namespace BimCheckAgent.Models
{
    public class SpecificationRow
    {
        public string Id { get; set; }
        public string Category { get; set; }
        public string Mark { get; set; }

        public SpecificationRow(string id, string category, string mark)
        {
            Id = id;
            Category = category;
            Mark = mark;
        }
    }
}

