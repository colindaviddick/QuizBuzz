using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using System.Xml;

namespace QuizBuzz
{
    public class QuestionLoader
    {
        public List<Question> LoadQuestionsToList(List<Question> qPool)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("Questions.xml");

            var root = doc.DocumentElement;
            if (root == null)
            {
                MessageBox.Show("Fail");
                return qPool;
            }

            var questions = root.SelectNodes("Module");
            if (questions == null)
            {
                MessageBox.Show("Fail");
                return qPool;
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
                qPool.Add(question);
            }
            

            return qPool;
        }
    }
}
