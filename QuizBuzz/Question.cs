using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBuzz
{
    public class Question : MainWindow
    {
        public string QuestionString { get; set; }
        public string CorrectAnswer { get; set; }
        public string IncorrectAnswer1 { get; set; }
        public string IncorrectAnswer2 { get; set; }
        public string IncorrectAnswer3 { get; set; }
        public string Category { get; set; }

        public void AddQuestion(string questionText,
            string correctAnswer,
            string incorrectAnswerA,
            string incorrectAnswerB,
            string incorrectAnswerC,
            string category)
        {
            QuestionString = questionText;
            CorrectAnswer = correctAnswer;
            IncorrectAnswer1 = incorrectAnswerA;
            IncorrectAnswer2 = incorrectAnswerB;
            IncorrectAnswer3 = incorrectAnswerC;
            Category = category;
        }
    }
}