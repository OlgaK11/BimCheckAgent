using System;
using System.Collections.Generic;
using System.Text;

namespace BimCheckAgent.Models
{
    //класс будет хранить одно замечание, обнаруженное при проверке BIM-модели
    public class Finding
    {
        public string RuleId { get; set; } = "";
        public string ElementId { get; set; } = "";
        public string Description { get; set; } = "";
        public string Severity { get; set; } = "Warning";
        public string Recommendation { get; set; } = "";
    }
}
