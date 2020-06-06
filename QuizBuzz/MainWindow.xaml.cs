using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
        MediaPlayer mp = new MediaPlayer();
        Random r = new Random();
        public List<Question> questionPool = new List<Question>();
        public QuestionLoader questionManager = new QuestionLoader();
        public GameManager gm = new GameManager();
        public bool button1;
        public bool button2;
        public bool button3;
        public bool button4;
        string correctSoundFilePath = "Correct.wav";
        string incorrectSoundFilePath = "Incorrect.wav";
        string introSoundFilePath = "Intro.wav";
        bool gameInProgress = false;
        int numberofQuestions = 10;

        public MainWindow()
        {
            mp.Open(new Uri(introSoundFilePath, UriKind.RelativeOrAbsolute));
            gm.PlaySounds = true;
            mp.Play();
            InitializeComponent();
            questionManager.LoadQuestionsToList(questionPool);
            MessageBox.Show(questionPool.Count + " Questions loaded");
        }

        public void Button1Clicked(object sender, RoutedEventArgs e)
        {
            // Do something interesting before progressing.
            if (button1)
            {
                CorrectAnswer();
            }
            else
            {
                IncorrectAnswer();
            }
        }
        public void Button2Clicked(object sender, RoutedEventArgs e)
        {
            // Do something interesting before progressing.
            if (button2)
            {
                CorrectAnswer();
            }
            else
            {
                IncorrectAnswer();
            }
        }
        public void Button3Clicked(object sender, RoutedEventArgs e)
        {
            // Do something interesting before progressing.
            if (button3)
            {
                CorrectAnswer();
            }
            else
            {
                IncorrectAnswer();
            }
        }
        public void Button4Clicked(object sender, RoutedEventArgs e)
        {
            // Do something interesting before progressing.
            if (button4)
            {
                CorrectAnswer();
            }
            else
            {
                IncorrectAnswer();
            }
        }

        public void ResetGame()
        {
            gm.Score = 0;

            gm.NumberOfGameQuestions = numberofQuestions;
            StartPage.Visibility = Visibility.Hidden;
            QuestionBoard.Visibility = Visibility.Visible;
            DisplayFirstQuestion();
            scoreDisplay.Text = gm.Score.ToString();
            gameInProgress = true;
        }

        public void NewGame(object sender, RoutedEventArgs e)
        {
            if (gameInProgress)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    ResetGame();
                }
            }
            else
            {
                ResetGame();
            }
        }

        public void CheckGameProgress()
        {
            // I want to invert the way this works so that I can tie in the Current Question to a display on the screen: eg. Question 1 of 10.

            if (gm.NumberOfGameQuestions == 0)
            {
                GameOverPage.Visibility = Visibility.Visible;
                QuestionBoard.Visibility = Visibility.Hidden;
                GameOverText.Text = ("You answered " + gm.Score.ToString() + " out of " + numberofQuestions + " questions correctly.");
                gameInProgress = false;
            }
        }

        public void CorrectAnswer()
        {
            if (gm.PlaySounds)
            {
                mp.Open(new Uri(correctSoundFilePath, UriKind.RelativeOrAbsolute));
                mp.Play();
            }
            gm.Score++;
            CheckGameProgress();
            NextQuestion();
            scoreDisplay.Text = gm.Score.ToString();
        }
        public void IncorrectAnswer()
        {
            if (gm.PlaySounds)
            {
                mp.Open(new Uri(incorrectSoundFilePath, UriKind.RelativeOrAbsolute));
                mp.Play();
            }
            CheckGameProgress();
            NextQuestion();
        }

        public void NextQuestion()
        {
            gm.NumberOfGameQuestions--;
            int rInt = r.Next(0, questionPool.Count());
            DisplayQuestionAndRandomiseAnswerLocation(questionPool, rInt);
            RemoveQuestionFromPool(questionPool, rInt);
        }

        public void DisplayFirstQuestion()
        {
            int rInt = r.Next(0, questionPool.Count());
            DisplayQuestionAndRandomiseAnswerLocation(questionPool, rInt);
            RemoveQuestionFromPool(questionPool, rInt);
        }

        public List<Question> RemoveQuestionFromPool(List<Question> questionPool, int currentQuestion)
        {
            questionPool.RemoveAt(currentQuestion);
            CheckGameProgress();
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
                    button1 = true;
                    button2 = false;
                    button3 = false;
                    button4 = false;
                    break;
                case 2:
                    AnswerButton1.Content = questionPool[currentQuestion].IncorrectAnswer3;
                    AnswerButton2.Content = questionPool[currentQuestion].CorrectAnswer;
                    AnswerButton3.Content = questionPool[currentQuestion].IncorrectAnswer1;
                    AnswerButton4.Content = questionPool[currentQuestion].IncorrectAnswer2;
                    button1 = false;
                    button2 = true;
                    button3 = false;
                    button4 = false;
                    break;
                case 3:
                    AnswerButton1.Content = questionPool[currentQuestion].IncorrectAnswer2;
                    AnswerButton2.Content = questionPool[currentQuestion].IncorrectAnswer3;
                    AnswerButton3.Content = questionPool[currentQuestion].CorrectAnswer;
                    AnswerButton4.Content = questionPool[currentQuestion].IncorrectAnswer1;
                    button1 = false;
                    button2 = false;
                    button3 = true;
                    button4 = false;
                    break;
                case 4:
                    AnswerButton1.Content = questionPool[currentQuestion].IncorrectAnswer1;
                    AnswerButton2.Content = questionPool[currentQuestion].IncorrectAnswer2;
                    AnswerButton3.Content = questionPool[currentQuestion].IncorrectAnswer3;
                    AnswerButton4.Content = questionPool[currentQuestion].CorrectAnswer;
                    button1 = false;
                    button2 = false;
                    button3 = false;
                    button4 = true;
                    break;
                default:
                    AnswerButton1.Content = questionPool[currentQuestion].CorrectAnswer;
                    AnswerButton2.Content = questionPool[currentQuestion].IncorrectAnswer1;
                    AnswerButton3.Content = questionPool[currentQuestion].IncorrectAnswer2;
                    AnswerButton4.Content = questionPool[currentQuestion].IncorrectAnswer3;
                    button1 = true;
                    button2 = false;
                    button3 = false;
                    button4 = false;
                    break;
            }
        }

        private void DebugNextQuestion(object sender, RoutedEventArgs e)
        {
            NextQuestion();
        }

        private void IncrementScore(object sender, RoutedEventArgs e)
        {
            gm.Score++;
            scoreDisplay.Text = gm.Score.ToString();
        }

        private void BackToMainMenu(object sender, RoutedEventArgs e)
        {
            QuestionBoard.Visibility = Visibility.Hidden;
            StartPage.Visibility = Visibility.Visible;
            GameOverPage.Visibility = Visibility.Hidden;
        }
        private void CloseOptions(object sender, RoutedEventArgs e)
        {
            CloseOptionsProcess();
        }
        private void CloseOptionsProcess()
        {
            if (gameInProgress)
            {
                QuestionBoard.Visibility = Visibility.Visible;
                StartPage.Visibility = Visibility.Hidden;
                GameOverPage.Visibility = Visibility.Hidden;
                OptionsPage.Visibility = Visibility.Hidden;

            }
            else
            {
                QuestionBoard.Visibility = Visibility.Hidden;
                StartPage.Visibility = Visibility.Visible;
                GameOverPage.Visibility = Visibility.Hidden;
                OptionsPage.Visibility = Visibility.Hidden;
            }
        }

        private void OptionsMenuOpen(object sender, RoutedEventArgs e)
        {
            QuestionBoard.Visibility = Visibility.Hidden;
            StartPage.Visibility = Visibility.Hidden;
            GameOverPage.Visibility = Visibility.Hidden;
            OptionsPage.Visibility = Visibility.Visible;
            if (gm.PlaySounds)
            {
                SoundOn.Background = Brushes.Green;
                SoundOff.Background = Brushes.DarkBlue;
            }
            else
            {
                SoundOn.Background = Brushes.DarkBlue;
                SoundOff.Background = Brushes.Red;
            }
        }

        private void SoundOn_Click(object sender, RoutedEventArgs e)
        {
            SoundOn.Background = Brushes.Green;
            SoundOff.Background = Brushes.DarkBlue;
            gm.PlaySounds = true;
        }

        private void SoundOff_Click(object sender, RoutedEventArgs e)
        {
            SoundOn.Background = Brushes.DarkBlue;
            SoundOff.Background = Brushes.Red;
            gm.PlaySounds = false;
        }
    }
}
