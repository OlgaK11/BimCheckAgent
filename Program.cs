
using BimCheckAgent.Import;
using BimCheckAgent.Models;
using BimCheckAgent.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

string basePath = AppContext.BaseDirectory;
string inputPath = Path.Combine(
    AppContext.BaseDirectory,
    "elements.csv");
string specificationPath = Path.Combine(basePath, "specification.csv");

if (!File.Exists(inputPath))
{
    Console.WriteLine($"Файл не найден: {inputPath}");
    return;
}

var elements = CsvImporter.Load(inputPath);
List<SpecificationRow> specification =SpecificationImporter.Load(specificationPath);

var checkService = new CheckService();
var findings = checkService.Check(elements);

var comparisonService = new ComparisonService();
findings.AddRange(comparisonService.Compare(elements, specification));

string reportPath = Path.Combine(basePath, "report.csv");
ReportService.Save(findings, reportPath);

Console.WriteLine("=== BimCheckAgent ===");
Console.WriteLine($"Элементов модели: {elements.Count}");
Console.WriteLine($"Строк спецификации: {specification.Count}");
Console.WriteLine($"Всего замечаний: {findings.Count}");

var ai = new AiExplanationService();
var knowledgeBase = new KnowledgeBaseService();
string knowledge = knowledgeBase.LoadKnowledge();

Console.WriteLine("База инженерных знаний загружена.");

if (findings.Count > 0)
{
    Console.WriteLine();
    Console.WriteLine("=== Объяснения ИИ ===");

    foreach (var finding in findings)
    {
        Console.WriteLine();
        Console.WriteLine($"Замечание: {finding.Description}");
        Console.WriteLine($"Элемент: {finding.ElementId}");
        Console.WriteLine($"Правило: {finding.RuleId}");
        Console.WriteLine($"Серьёзность: {finding.Severity}");

        string problem =
            $"Описание: {finding.Description}\n" +
            $"Элемент: {finding.ElementId}\n" +
            $"Правило: {finding.RuleId}\n" +
            $"Серьёзность: {finding.Severity}\n" +
            $"Рекомендация: {finding.Recommendation}";

        string answer = await ai.ExplainAsync(problem, knowledge);

        Console.WriteLine("Объяснение ИИ:");
        Console.WriteLine(answer);
        Console.WriteLine("----------------------------------");
    }
}
else
{
    Console.WriteLine("Замечаний нет — объяснение не требуется.");
}

Console.WriteLine($"Отчёт: {reportPath}");






