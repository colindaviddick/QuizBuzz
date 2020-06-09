using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBuzz
{
    public class GameManager
    {
        public int Score { get; set; }
        public string Category { get; set; }
        public int CurrentGameQuestionNumber { get; set; }
        public int NumberOfGameQuestions { get; set; }
        public bool PlaySounds { get; set; }
        public float Percentage { get; set; }
    }
}
