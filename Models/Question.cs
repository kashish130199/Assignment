using System.Collections.Generic;
using System.Text.Json.Serialization;

public class Question
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("QuestionText")]
    public string QuestionText { get; set; }

    [JsonPropertyName("options")]
    public List<Option> Options { get; set; }

    [JsonPropertyName("answerKey")]
    public string AnswerKey { get; set; }
}

public class Option
{
    [JsonPropertyName("key")]
    public string Key { get; set; }

    [JsonPropertyName("text")]
    public string Text { get; set; }
}
