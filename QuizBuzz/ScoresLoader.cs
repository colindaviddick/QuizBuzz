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
        public List<Score> LoadScoresToList(List<Score> scoresPool)
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
            //    < PlayerScoreBoard >
            //        < PlayerScore >
            //            < PlayerName > Dada </ PlayerName >
            //            < Score > General Knowledge </ Score >
            //            < Category > General Knowledge </ Category >
            //            < PlayerScore > 07 / 06 / 2020 03:13:09 </ PlayerScore >
            //        </ PlayerScore >
            //        < PlayerScore >
            //           < PlayerName > Colin </ PlayerName >
            //           < Score > General Knowledge </ Score >
            //           < Category > General Knowledge </ Category >               
            //           < PlayerScore > 07 / 06 / 2020 03:13:03 </ PlayerScore >
            //       </ PlayerScore >
            //   </ PlayerScoreBoard >

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
    }
}