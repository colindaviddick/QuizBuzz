using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBuzz
{
    public class Score
    {
        public string Award { get; set; }
        public string Name { get; set; }
        public string ScorePercentage { get; set;}
        public string Category { get; set; }
        public string DateAndTime { get; set; }

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
