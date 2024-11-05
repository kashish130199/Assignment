using QuizApp.Models;
using QuizApp.Services;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;

namespace QuizApp.Controllers
{
    public class QuizController : Controller
    {
        private readonly IQuizService _quizService;

        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        public IActionResult Index()
        {
            try
            {
                var questions = _quizService.LoadQuestions();
                return View(questions);
            }
            catch (ApplicationException ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult SubmitQuiz(List<UserAnswer> userAnswers)
        {
            try
            {
                int score = _quizService.CalculateScore(userAnswers);
                var results = _quizService.GetResults(userAnswers);

                ViewBag.Score = score;
                ViewBag.Total = userAnswers.Count;
                ViewBag.Results = results;

                return View("Result");
            }
            catch (ApplicationException ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }
    }
}
