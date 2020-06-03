using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace QuizBuzz
{
    public class QuestionManager : MainWindow
    {
        public List<Question> LoadQuestionsToList(List<Question> QuestionsPool)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("Questions.xml");

            var root = doc.DocumentElement;
            if (root == null)
            {
                MessageBox.Show("Fail");
                return QuestionsPool;
            }

            var questions = root.SelectNodes("Module");
            if (questions == null)
            {
                MessageBox.Show("Fail");
                return QuestionsPool;
            }


            foreach (XmlNode q in questions)
            {
                string questiontext = q.SelectSingleNode("Question").InnerText;
                string correctanswer = q.SelectSingleNode("CorrectAnswer").InnerText;
                string incorrectanswer1 = q.SelectSingleNode("IncorrectAnswer1").InnerText;
                string incorrectanswer2 = q.SelectSingleNode("IncorrectAnswer2").InnerText;
                string incorrectanswer3 = q.SelectSingleNode("IncorrectAnswer3").InnerText;
                string category = q.SelectSingleNode("Category").InnerText;
                Question question = new Question();
                question.AddQuestion(questiontext, correctanswer, incorrectanswer1, incorrectanswer2, incorrectanswer3, category);
                QuestionsPool.Add(question);
            }
            return QuestionsPool;
        }

        public void RemoveQuestionFromPool(int currentQuestion)
        {
            questionPool.RemoveAt(currentQuestion);
        }

        public void DisplayQuestionAndRandomiseAnswerLocation(int currentQuestion)
        {
            QuestionText.Text = questionPool[currentQuestion].QuestionString;
            Random randomAnswerPos = new Random();
          
            int correctAnswerPos = randomAnswerPos.Next(1, 4);

            switch (correctAnswerPos)
            {

                case 1:
                    AnswerButton1.Content = questionPool[currentQuestion].CorrectAnswer;
                    AnswerButton2.Content = questionPool[currentQuestion].IncorrectAnswer1;
                    AnswerButton3.Content = questionPool[currentQuestion].IncorrectAnswer2;
                    AnswerButton4.Content = questionPool[currentQuestion].IncorrectAnswer3;
                    AnswerButton1.Click += Correct;
                    AnswerButton2.Click += Incorrect;
                    AnswerButton3.Click += Incorrect;
                    AnswerButton4.Click += Incorrect;
                    break;
                case 2:
                    AnswerButton1.Content = questionPool[currentQuestion].IncorrectAnswer3;
                    AnswerButton2.Content = questionPool[currentQuestion].CorrectAnswer;
                    AnswerButton3.Content = questionPool[currentQuestion].IncorrectAnswer1;
                    AnswerButton4.Content = questionPool[currentQuestion].IncorrectAnswer2;
                    AnswerButton1.Click += Incorrect;
                    AnswerButton2.Click += Correct;
                    AnswerButton3.Click += Incorrect;
                    AnswerButton4.Click += Incorrect;
                    break;
                case 3:
                    AnswerButton1.Content = questionPool[currentQuestion].IncorrectAnswer2;
                    AnswerButton2.Content = questionPool[currentQuestion].IncorrectAnswer3;
                    AnswerButton3.Content = questionPool[currentQuestion].CorrectAnswer;
                    AnswerButton4.Content = questionPool[currentQuestion].IncorrectAnswer1;
                    AnswerButton1.Click += Incorrect;
                    AnswerButton2.Click += Incorrect;
                    AnswerButton3.Click += Correct;
                    AnswerButton4.Click += Incorrect;
                    break;
                case 4:
                    AnswerButton1.Content = questionPool[currentQuestion].IncorrectAnswer1;
                    AnswerButton2.Content = questionPool[currentQuestion].IncorrectAnswer2;
                    AnswerButton3.Content = questionPool[currentQuestion].IncorrectAnswer3;
                    AnswerButton4.Content = questionPool[currentQuestion].CorrectAnswer;
                    AnswerButton1.Click -= Incorrect;
                    AnswerButton2.Click += Incorrect;
                    AnswerButton3.Click += Incorrect;
                    AnswerButton4.Click += Correct;
                    break;
                default:
                    AnswerButton1.Content = questionPool[currentQuestion].CorrectAnswer;
                    AnswerButton2.Content = questionPool[currentQuestion].IncorrectAnswer1;
                    AnswerButton3.Content = questionPool[currentQuestion].IncorrectAnswer2;
                    AnswerButton4.Content = questionPool[currentQuestion].IncorrectAnswer3;
                    AnswerButton1.Click += Correct;
                    AnswerButton2.Click += Incorrect;
                    AnswerButton3.Click += Incorrect;
                    AnswerButton4.Click += Incorrect;
                    break;
            }
        }
    }
}
