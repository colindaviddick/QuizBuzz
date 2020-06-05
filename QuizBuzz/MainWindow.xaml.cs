using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuizBuzz
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random r = new Random();
        public List<Question> questionPool = new List<Question>();
        public QuestionLoader questionManager = new QuestionLoader();
        public GameManager gm = new GameManager();
        public MainWindow()
        {
            InitializeComponent();
        }

        public void Correct(object sender, RoutedEventArgs e)
        {
            // Do something interesting before progressing.
            CorrectAnswer();
        }

        public void Incorrect(object sender, RoutedEventArgs e)
        {
            // Do something interesting before progressing.
            MessageBox.Show("Incorrect");
            NextQuestion();
        }
        public void NewGame(object sender, RoutedEventArgs e)
        {
            questionManager.LoadQuestionsToList(questionPool);
        }

        public void CorrectAnswer()
        {
            gm.Score++;
            NextQuestion();
            scoreDisplay.Text = gm.Score.ToString();
            MessageBox.Show("Correct");
        }

        public void NextQuestion()
        {
            int rInt = r.Next(0, questionPool.Count());
            DisplayQuestionAndRandomiseAnswerLocation(questionPool, rInt);
        }

        public List<Question> RemoveQuestionFromPool(List<Question> questionPool, int currentQuestion)
        {
            questionPool.RemoveAt(currentQuestion);
            return questionPool;
        }

        public void DisplayQuestionAndRandomiseAnswerLocation(List<Question> questionPool, int currentQuestion)
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
                    AnswerButton1.Click += Incorrect;
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

        private void DebugNextQuestion(object sender, RoutedEventArgs e)
        {
            NextQuestion();
        }
    }
}

