using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace QuizBuzz
{
    public class QuestionPoolManager : MainWindow
    {
        
        public List<Question> LoadQuestionsToList(List<Question> QuestionsPool)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("Questions.xml");

            var root = doc.DocumentElement;
            if (root == null)
            {
                MessageBox.Show("Fail");
                //return;
            }

            var questions = root.SelectNodes("Module");
            if (questions == null)
            {
                MessageBox.Show("Fail");
                //return;
            }


            foreach (XmlNode question in questions)
            {
                string questiontext = question.SelectSingleNode("Question").InnerText;
                string correctanswer = question.SelectSingleNode("CorrectAnswer").InnerText;
                string incorrectanswer1 = question.SelectSingleNode("IncorrectAnswer1").InnerText;
                string incorrectanswer2 = question.SelectSingleNode("IncorrectAnswer2").InnerText;
                string incorrectanswer3 = question.SelectSingleNode("IncorrectAnswer3").InnerText;
                string category = question.SelectSingleNode("Category").InnerText;
                Question gnincrement = new Question();
                gnincrement.AddQuestion(questiontext, correctanswer, incorrectanswer1, incorrectanswer2, incorrectanswer3, category);
                QuestionsPool.Add(gnincrement);
            }

            return QuestionsPool;
        }

        public void RemoveQuestionFromPool(int currentQuestion)
        {
            questionsPool.RemoveAt(currentQuestion);
        }

        public void DisplayQuestionAndRandomiseAnswerLocation(int currentQuestion)
        {
            QuestionText.Text = questionsPool[currentQuestion].QuestionString;
            Random randomAnswerPos = new Random();
            
            int correctAnswerPos = randomAnswerPos.Next(1, 4);

            switch (correctAnswerPos)
            {

                case 1:
                    AnswerButton1.Content = questionsPool[currentQuestion].CorrectAnswer;
                    AnswerButton2.Content = questionsPool[currentQuestion].IncorrectAnswer1;
                    AnswerButton3.Content = questionsPool[currentQuestion].IncorrectAnswer2;
                    AnswerButton4.Content = questionsPool[currentQuestion].IncorrectAnswer3;
                    AnswerButton1.Click += Correct;
                    AnswerButton2.Click += Incorrect;
                    AnswerButton3.Click += Incorrect;
                    AnswerButton4.Click += Incorrect;
                    break;
                case 2:
                    AnswerButton1.Content = questionsPool[currentQuestion].IncorrectAnswer3;
                    AnswerButton2.Content = questionsPool[currentQuestion].CorrectAnswer;
                    AnswerButton3.Content = questionsPool[currentQuestion].IncorrectAnswer1;
                    AnswerButton4.Content = questionsPool[currentQuestion].IncorrectAnswer2;
                    AnswerButton1.Click += Incorrect;
                    AnswerButton2.Click += Correct;
                    AnswerButton3.Click += Incorrect;
                    AnswerButton4.Click += Incorrect;
                    break;
                case 3:
                    AnswerButton1.Content = questionsPool[currentQuestion].IncorrectAnswer2;
                    AnswerButton2.Content = questionsPool[currentQuestion].IncorrectAnswer3;
                    AnswerButton3.Content = questionsPool[currentQuestion].CorrectAnswer;
                    AnswerButton4.Content = questionsPool[currentQuestion].IncorrectAnswer1;
                    AnswerButton1.Click += Incorrect;
                    AnswerButton2.Click += Incorrect;
                    AnswerButton3.Click += Correct;
                    AnswerButton4.Click += Incorrect;
                    break;
                case 4:
                    AnswerButton1.Content = questionsPool[currentQuestion].IncorrectAnswer1;
                    AnswerButton2.Content = questionsPool[currentQuestion].IncorrectAnswer2;
                    AnswerButton3.Content = questionsPool[currentQuestion].IncorrectAnswer3;
                    AnswerButton4.Content = questionsPool[currentQuestion].CorrectAnswer;
                    AnswerButton1.Click -= Incorrect;
                    AnswerButton2.Click += Incorrect;
                    AnswerButton3.Click += Incorrect;
                    AnswerButton4.Click += Correct;
                    break;
                default:
                    AnswerButton1.Content = questionsPool[currentQuestion].CorrectAnswer;
                    AnswerButton2.Content = questionsPool[currentQuestion].IncorrectAnswer1;
                    AnswerButton3.Content = questionsPool[currentQuestion].IncorrectAnswer2;
                    AnswerButton4.Content = questionsPool[currentQuestion].IncorrectAnswer3;
                    AnswerButton1.Click += Correct;
                    AnswerButton2.Click += Incorrect;
                    AnswerButton3.Click += Incorrect;
                    AnswerButton4.Click += Incorrect;
                    break;
            }


        }
    }
}
