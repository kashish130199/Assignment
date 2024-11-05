namespace QuizApp.Models
{
    public class QuizResult
    {
        public string QuestionText { get; set; }
        public string CorrectAnswer { get; set; }
        public string UserAnswer { get; set; }
        public bool IsCorrect { get; set; }
    }
}
