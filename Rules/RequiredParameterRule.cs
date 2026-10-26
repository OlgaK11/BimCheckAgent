using System;
using System.Collections.Generic;
using System.Text;
using BimCheckAgent.Models;


namespace BimCheckAgent.Rules
{

        public class RequiredParameterRule
        {
            public Finding? Check(ModelElement element)
            {
                if (string.IsNullOrWhiteSpace(element.Mark))
                {
                    return new Finding
                    {
                        RuleId = "REQ-001",
                        ElementId = element.Id,
                        Description = $"У элемента «{element.Category}» не заполнена марка.",
                        Severity = "Error",
                        Recommendation = "Проверьте марку элемента в модели."
                    };
                }

                return null;
            }
        }
    }

