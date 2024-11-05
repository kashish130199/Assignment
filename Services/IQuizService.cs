using QuizApp.Models;
using System.Collections.Generic;

namespace QuizApp.Services
{
    public interface IQuizService
    {
        List<Question> LoadQuestions();
        int CalculateScore(List<UserAnswer> userAnswers);
        List<QuizResult> GetResults(List<UserAnswer> userAnswers);
    }
}
