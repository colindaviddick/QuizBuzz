using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace QuizBuzz
{
    public class ScoresLoader
    {
        public List<Score> LoadLocalScoresToList(List<Score> scoresPool)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("Scores.xml");
            var root = doc.DocumentElement;
            if (root == null)
            {
                MessageBox.Show("Fail");
                return scoresPool;
            }

            var scores = root.SelectNodes("Module");
            if (scores == null)
            {
                MessageBox.Show("Fail");
                return scoresPool;
            }

            foreach (XmlNode s in scores)
            {
                string scoreboardBuilderplayerName = s.SelectSingleNode("PlayerName").InnerText;
                string scoreboardBuilderscore = s.SelectSingleNode("Score").InnerText;
                string scoreboardBuildercategory = s.SelectSingleNode("Category").InnerText;
                string scoreboardBuilderdateTime = s.SelectSingleNode("DateTime").InnerText;

                Score score = new Score();
                score.AddScore(scoreboardBuilderplayerName, scoreboardBuilderscore, scoreboardBuildercategory, scoreboardBuilderdateTime);
                scoresPool.Add(score);

                //  Add categories in the same way as the Questions... More pools for each category.
                //    if (category.Contains("Music"))
                //    {
                //        musicqPool.Add(question);
                //    }
                //    if (category.Contains("Movies"))
                //    {
                //        movieqPool.Add(question);
                //    }
                //    if (category.Contains("Science"))
                //    {
                //        scienceqPool.Add(question);
                //    }
                //    if (category.Contains("History"))
                //    {
                //        historyqPool.Add(question);
                //    }
                //    if (category.Contains("Geography"))
                //    {
                //        geographyqPool.Add(question);
                //    }
                //    if (category.Contains("Sport"))
                //    {
                //        sportqPool.Add(question);
                //    }
                //    if (category.Contains("TV"))
                //    {
                //        tvqPool.Add(question);
                //    }
                //    if (category.Contains("Natural"))
                //    {
                //        naturalqPool.Add(question);
                //    }
                //}
            }
            return scoresPool;
        }

        public List<Score> LoadOnlineScoresToList(List<Score> scoresPool)
        {
            DB db = new DB();
            db.OpenConnection();
            MySqlCommand mySqlCommand = new MySqlCommand("SELECT * FROM `QuizBuzzScores`", db.GetConnection());
            using (MySqlDataReader reader = mySqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.WriteLine("Row " + i + " :" + reader.GetValue(i));
                    }
                    Console.WriteLine();
                }
            }
            return scoresPool;
        }
    }
}