using BimCheckAgent.Models;

namespace BimCheckAgent.Rules
{
    //ищет повторяющиеся марки в одной категории.
    public class DuplicateMarkRule
    {
        public List<Finding> Check(List<ModelElement> elements)
        {
            var findings = new List<Finding>();

            var duplicates = elements
                .Where(e => !string.IsNullOrWhiteSpace(e.Mark))
                .GroupBy(e => new
                {
                    e.Category,
                    Mark = e.Mark.Trim()
                })
                .Where(g => g.Count() > 1);

            foreach (var group in duplicates)
            {
                foreach (ModelElement element in group)
                {
                    findings.Add(new Finding
                    {
                        RuleId = "DUP-001",
                        ElementId = element.Id,
                        Description = $"Марка «{group.Key.Mark}» повторяется в категории «{group.Key.Category}».",
                        Severity = "Warning",
                        Recommendation = "Проверьте, допустимо ли повторение марки."
                    });
                }
            }

            return findings;
        }

    }

}
