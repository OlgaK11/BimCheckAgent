using BimCheckAgent.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BimCheckAgent.Services
{
    //будет содержать идентификатор правила, элемент, серьёзность, описание проблемы и рекомендацию

    public class ReportService
        {
            public static void Save(List<Finding> findings, string filePath)
            {
                var lines = new List<string>
            {
                "RuleId;ElementId;Severity;Description;Recommendation"
            };

                foreach (Finding finding in findings)
                {
                    lines.Add(string.Join(";",
                        Escape(finding.RuleId),
                        Escape(finding.ElementId),
                        Escape(finding.Severity),
                        Escape(finding.Description),
                        Escape(finding.Recommendation)));
                }

                File.WriteAllLines(filePath, lines, Encoding.UTF8);
            }

            private static string Escape(string value)
            {
                return "\"" + value.Replace("\"", "\"\"") + "\"";
            }
        }
    

}
