using System;
using System.Collections.Generic;
using System.Text;

namespace BimCheckAgent.Models
{
    // Модель данных одного элемента
   public class ModelElement
    {
        public string Id { get; set; }
        public string Category { get; set; }
        public string Mark { get; set; }

        public ModelElement(string id, string category, string mark)
        {
            Id = id;
            Category = category;
            Mark = mark;
        }
    }
}
