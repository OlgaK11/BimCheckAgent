using System.Net.Http.Json;
using System.Text.Json;

namespace BimCheckAgent.Services
{
    public class AiExplanationService
    {
        private readonly HttpClient _httpClient = new();
        public async Task<string> ExplainAsync(
    string problem,
    string knowledge)
        {
            var request = new
            {
                model = "qwen2.5:7b",
                stream = false,
                options = new
                {
                    temperature = 0.2
                },
                messages = new[]
                {
                new
                {
                    role = "system",
                  content =
    "Ты помощник инженера по проверке BIM-моделей. " +
    "Твоя задача — понятно объяснять замечания, найденные программой. " +

    "СТРОГИЕ ПРАВИЛА: " +
    "1. Используй только факты из переданного замечания. " +
    "2. Не придумывай материал, размеры, назначение элемента, " +
    "причины ошибки или нормативные требования. " +
    "3. Если информации недостаточно, прямо скажи об этом. " +
    "4. Не утверждай, что нарушен норматив, если это не подтверждено " +
    "переданными данными и ссылкой на требование. " +
    "5. Различай обнаруженный факт и возможные последствия. " +
    "6. Не изменяй результат проверки программы и не объявляй " +
    "элемент ошибочным без достаточных оснований. " +

    "ФОРМАТ ОТВЕТА: " +
    "Что обнаружено: кратко опиши факт. " +
    "Почему это важно: объясни возможное влияние на работу с моделью. " +
    "Что проверить: предложи конкретные действия инженеру. " +
    "Ограничения: укажи, чего нельзя установить по имеющимся данным. " +

    "Отвечай на русском языке, простыми словами, " +
    "без длинных вступлений. " +
    "Не повторяй одно и то же разными словами."
                },
                new
                {
                    role = "user",
    content =
        "ИНЖЕНЕРНАЯ БАЗА ЗНАНИЙ:\n" +
        knowledge +
        "\n\nЗАМЕЧАНИЕ ПРОГРАММЫ:\n" +
        problem +
        "\n\nИспользуй базу знаний для объяснения замечания. " +
        "Не добавляй факты и нормативные требования, " +
        "которых нет в предоставленных данных. " +
        "Если сведений недостаточно, укажи это явно."
                }
            }
            };

            try
            {
                using var response = await _httpClient.PostAsJsonAsync(
                    "http://localhost:11434/api/chat",
                    request);

                string json =
                    await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return $"Ошибка Ollama ({(int)response.StatusCode}): {json}";
                }

                using var document = JsonDocument.Parse(json);

                return document.RootElement
                    .GetProperty("message")
                    .GetProperty("content")
                    .GetString() ?? "ИИ не вернул текстовый ответ.";
            }
            catch (HttpRequestException)
            {
                return "Не удалось подключиться к Ollama. " +
                       "Проверь, запущена ли Ollama на компьютере.";
            }
            catch (JsonException)
            {
                return "Не удалось прочитать ответ Ollama.";
            }
        }

        //public async Task<string> ExplainAsync(string problem)
        //{
        //    string? apiKey =
        //        Environment.GetEnvironmentVariable("GEMINI_API_KEY");

        //    if (string.IsNullOrWhiteSpace(apiKey))
        //    {
        //        return "Не найден ключ GEMINI_API_KEY.";
        //    }

        //    var request = new
        //    {
        //        model = "gemini-3.8-flash",
        //        input =
        //            "Ты помощник инженера по проверке BIM-моделей. " +
        //            "Объясни найденное замечание простым языком. " +
        //            "Не выдумывай требования и не заменяй проверку инженера. " +
        //            "Замечание: " + problem
        //    };

        //    using var message = new HttpRequestMessage(
        //        HttpMethod.Post,
        //        "https://generativelanguage.googleapis.com/v1beta/interactions");

        //    message.Headers.Add("x-goog-api-key", apiKey);
        //    message.Content = JsonContent.Create(request);

        //    using var response = await _httpClient.SendAsync(message);
        //    string json = await response.Content.ReadAsStringAsync();

        //    if (!response.IsSuccessStatusCode)
        //    {
        //        return $"Ошибка API ({(int)response.StatusCode}): {json}";
        //    }

        //    using var document = JsonDocument.Parse(json);
        //    var root = document.RootElement;

        //    if (root.TryGetProperty("steps", out var steps))
        //    {
        //        foreach (var step in steps.EnumerateArray())
        //        {
        //            if (step.TryGetProperty("type", out var type)
        //                && type.GetString() == "model_output"
        //                && step.TryGetProperty("content", out var content))
        //            {
        //                foreach (var item in content.EnumerateArray())
        //                {
        //                    if (item.TryGetProperty("text", out var text))
        //                    {
        //                        return text.GetString() ?? "Пустой ответ ИИ.";
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    return "ИИ не вернул текстовый ответ.";
        //}
    }
}
