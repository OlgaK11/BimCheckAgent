using BimCheckAgent.Models;
using BimCheckAgent.Rules;

namespace BimCheckAgent.Services
{
    //единая точка запуска проверок.
    public class CheckService
    {

        private readonly RequiredParameterRule _requiredRule = new();//проверяет обязательные параметры.
        private readonly DuplicateMarkRule _duplicateRule = new();//ищет повторяющиеся марки

        public List<Finding> Check(List<ModelElement> elements)
        {
            var findings = new List<Finding>();

            // Проверяем каждый элемент на пустую марку
            foreach (ModelElement element in elements)
            {
                Finding? finding = _requiredRule.Check(element);

                if (finding != null)
                {
                    findings.Add(finding);
                }
            }

            // Проверяем повторяющиеся марки
            findings.AddRange(_duplicateRule.Check(elements));

            return findings;
        }
    }


}
