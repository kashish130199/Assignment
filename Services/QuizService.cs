using QuizApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;

namespace QuizApp.Services
{
    public class QuizService : IQuizService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly string _filePath;

        public QuizService(IWebHostEnvironment environment)
        {
            _environment = environment;
            // Assuming "Data" is the folder where the JSON file is stored.
            _filePath = Path.Combine(_environment.ContentRootPath, "App_Data", "questions.json");
        }

        public List<Question> LoadQuestions()
        {
            try
            {
                // Check if the JSON file exists at the specified path
                if (!File.Exists(_filePath))
                    throw new FileNotFoundException($"Quiz questions file not found at {_filePath}.");

                // Read JSON file content
                string jsonData = File.ReadAllText(_filePath);

                // Deserialize JSON data into a list of Question objects
                var questions = JsonConvert.DeserializeObject<List<Question>>(jsonData);

                // Check if deserialization resulted in a null or empty list
                if (questions == null || !questions.Any())
                    throw new InvalidDataException("Quiz questions file is empty or has invalid data.");

                return questions;
            }
            catch (JsonException ex)
            {
                throw new ApplicationException("Error deserializing JSON data. Check the file structure and format.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An unexpected error occurred while loading quiz questions.", ex);
            }
        }

        public int CalculateScore(List<UserAnswer> userAnswers)
        {
            try
            {
                // Load questions to check answers against
                var questions = LoadQuestions();
                int score = 0;

                // Compare each user answer with the correct answer in the questions
                foreach (var userAnswer in userAnswers)
                {
                    var question = questions.FirstOrDefault(q => q.Id == userAnswer.QuestionId);
                    if (question != null && userAnswer.SelectedAnswer == question.AnswerKey)
                    {
                        score++;
                    }
                }
                return score;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while calculating the quiz score.", ex);
            }
        }

        public List<QuizResult> GetResults(List<UserAnswer> userAnswers)
        {
            var questions = LoadQuestions();

            return userAnswers.Select(userAnswer => new QuizResult
            {
                QuestionText = questions.First(q => q.Id == userAnswer.QuestionId).QuestionText,
                CorrectAnswer = questions.First(q => q.Id == userAnswer.QuestionId).AnswerKey,
                UserAnswer = userAnswer.SelectedAnswer,
                IsCorrect = userAnswer.SelectedAnswer == questions.First(q => q.Id == userAnswer.QuestionId).AnswerKey
            }).ToList();
        }
    }
}
