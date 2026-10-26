using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace BimCheckAgent.Services
{
    public class KnowledgeBaseService
    {
        public string LoadKnowledge()
        {
            string filePath = Path.Combine(
                AppContext.BaseDirectory,
                "KnowledgeBase",
                "bim_rules.txt");

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(
                    "Файл базы знаний не найден.",
                    filePath);
            }

            return File.ReadAllText(filePath);
        }
    }
}
