using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;
using BimCheckAgent.Models;

namespace BimCheckAgent.Services
{
    /// <summary>
    /// Сравненеие
    /// </summary>
    public class ComparisonService
    {
        public List<Finding> Compare(
            List<ModelElement> elements,
            List<SpecificationRow> specification)
        {
            var findings = new List<Finding>();

            var modelIds = elements
                .Select(e => e.Id)
                .ToHashSet();

            var specificationIds = specification
                .Select(s => s.Id)
                .ToHashSet();

            foreach (ModelElement element in elements)
            {
                if (!specificationIds.Contains(element.Id))
                {
                    findings.Add(new Finding
                    {
                        RuleId = "CMP-001",
                        ElementId = element.Id,
                        Description = $"Элемент {element.Id} отсутствует в спецификации.",
                        Severity = "Warning",
                        Recommendation = "Проверьте, должен ли элемент присутствовать в спецификации."
                    });
                }
            }

            foreach (SpecificationRow row in specification)
            {
                if (!modelIds.Contains(row.Id))
                {
                    findings.Add(new Finding
                    {
                        RuleId = "CMP-002",
                        ElementId = row.Id,
                        Description = $"Строка {row.Id} отсутствует в данных модели.",
                        Severity = "Warning",
                        Recommendation = "Проверьте актуальность спецификации и модели."
                    });
                }
            }

            return findings;
        }
    }
}

