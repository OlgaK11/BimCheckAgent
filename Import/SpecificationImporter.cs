using BimCheckAgent.Models;

namespace BimCheckAgent.Import
{


    public class SpecificationImporter
    {
        public static List<SpecificationRow> Load(string filePath)
        {
            var rows = new List<SpecificationRow>();
            string[] lines = File.ReadAllLines(filePath);

            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                    continue;

                string[] columns = lines[i].Split(';');

                if (columns.Length != 3 ||
                    string.IsNullOrWhiteSpace(columns[0]) ||
                    string.IsNullOrWhiteSpace(columns[1]))
                {
                    Console.WriteLine(
                        $"Строка спецификации {i + 1} пропущена.");
                    continue;
                }

                rows.Add(new SpecificationRow(
                    columns[0].Trim(),
                    columns[1].Trim(),
                    columns[2].Trim()));
            }

            return rows;
        }
    }


}
