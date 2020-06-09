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
        public List<Question> LoadQuestionsToList(
            List<Question> gnQPool,
            List<Question> musicqPool,
            List<Question> movieqPool,
            List<Question> geographyqPool,
            List<Question> scienceqPool,
            List<Question> historyqPool,
            List<Question> sportqPool,
            List<Question> tvqPool,
            List<Question> naturalqPool,
            List<Question> foodqPool)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("Questions.xml");

            var root = doc.DocumentElement;
            if (root == null)
            {
                MessageBox.Show("Fail");
                return gnQPool;
            }

            var questions = root.SelectNodes("Module");
            if (questions == null)
            {
                MessageBox.Show("Fail");
                return gnQPool;
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
                gnQPool.Add(question);

                if(category.Contains("Music"))
                {
                    musicqPool.Add(question);
                }
                if (category.Contains("Movies"))
                {
                    movieqPool.Add(question);
                }
                if (category.Contains("Science"))
                {
                    scienceqPool.Add(question);
                }
                if (category.Contains("History"))
                {
                    historyqPool.Add(question);
                }
                if (category.Contains("Geography"))
                {
                    geographyqPool.Add(question);
                }
                if (category.Contains("Sport"))
                {
                    sportqPool.Add(question);
                }
                if (category.Contains("TV"))
                {
                    tvqPool.Add(question);
                }
                if (category.Contains("Natural"))
                {
                    naturalqPool.Add(question);
                }
                if (category.Contains("Food"))
                {
                    foodqPool.Add(question);
                }
            }


            return gnQPool;
        }
    }
}
