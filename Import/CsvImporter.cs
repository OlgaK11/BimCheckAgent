
using System;
using System.Collections.Generic;
using System.IO;
using BimCheckAgent.Models;

namespace BimCheckAgent.Import
{
    public class CsvImporter
    {
        public static List<ModelElement> Load(string filePath)
        {
            var elements = new List<ModelElement>();
            string[] lines = File.ReadAllLines(filePath);

            if (lines.Length == 0)
            {
                Console.WriteLine("CSV-файл пуст.");
                return elements;
            }

            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];

                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                string[] columns = line.Split(';');

                if (columns.Length != 3)
                {
                    Console.WriteLine(
                        $"Строка {i + 1} пропущена: ожидалось 3 столбца.");
                    continue;
                }

                string id = columns[0].Trim();
                string category = columns[1].Trim();
                string mark = columns[2].Trim();

                if (string.IsNullOrWhiteSpace(id))
                {
                    Console.WriteLine(
                        $"Строка {i + 1} пропущена: отсутствует Id.");
                    continue;
                }

                if (string.IsNullOrWhiteSpace(category))
                {
                    Console.WriteLine(
                        $"Строка {i + 1} пропущена: отсутствует Category.");
                    continue;
                }

                elements.Add(new ModelElement(id, category, mark));
            }

            return elements;
        }
    }
}
