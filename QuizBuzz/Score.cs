using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBuzz
{
    public class Score
    {
        string Name { get; set; }
        string ScorePercentage { get; set;}
        string Category { get; set; }
        string DateAndTime { get; set; }

        public void AddScore(string name,
            string scorePercentage,
            string category,
            string dateAndTime)
        {
            Name = name;
            ScorePercentage = scorePercentage;
            Category = category;
            DateAndTime = dateAndTime;
        }

    }
}
