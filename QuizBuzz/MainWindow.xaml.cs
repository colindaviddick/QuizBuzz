using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using System.Xml;
using System.Xml.Linq;


namespace QuizBuzz
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Need more TV Questions. I want at least 50 questions for each topic.
        // Maybe add theming, but not vital.

        // To add more of a game element to the quiz, have a 10s timer for each question?
        // Points reduce for every half second it takes to answer
        //
        // Show scores per groups of questions for fairness.

        // Add streaks? 5+ questions in a row = a streak & earns a medal?

        // Disable the possibility of double clicking... 

        readonly MediaPlayer mp = new MediaPlayer();
        readonly MediaPlayer bgmusic = new MediaPlayer();
        public QuestionLoader questionManager = new QuestionLoader();
        public GameManager gm = new GameManager();
        public bool button1;
        public bool button2;
        public bool button3;
        public bool button4;
        readonly string correctSoundFilePath = @"Sounds\Correct.wav";
        readonly string incorrectSoundFilePath = @"Sounds\Incorrect.wav";
        readonly string introSoundFilePath = @"Sounds\Intro.wav";
        bool gameInProgress = false;
        readonly Random r = new Random();

        public List<Question> gnQuestionPool = new List<Question>();
        public List<Question> musicQuestionPool = new List<Question>();
        public List<Question> movieQuestionPool = new List<Question>();
        public List<Question> geographyQuestionPool = new List<Question>();
        public List<Question> scienceQuestionPool = new List<Question>();
        public List<Question> historyQuestionPool = new List<Question>();
        public List<Question> naturalWorldQuestionPool = new List<Question>();
        public List<Question> sportQuestionPool = new List<Question>();
        public List<Question> tvQuestionPool = new List<Question>();
        public List<Question> foodQuestionPool = new List<Question>();

        // Push Category information to Gm.Category. Don't store it in main memory.

        public MainWindow()
        {
            gm.Category = "General Knowledge";
            gm.PlaySounds = true;
            //mp.Open(new Uri(introSoundFilePath, UriKind.RelativeOrAbsolute));
            //mp.Play();
            // Play these if bg music is off
            MusicLoop();
            InitializeComponent();
            questionManager.LoadQuestionsToList(
                gnQuestionPool,
                musicQuestionPool,
                movieQuestionPool,
                geographyQuestionPool,
                scienceQuestionPool,
                historyQuestionPool,
                sportQuestionPool,
                tvQuestionPool,
                naturalWorldQuestionPool,
                foodQuestionPool);
            SetVolumeToOne();
            SetCategoryCounts();
            LoadAllOnlineScoresToList();
        }

        private async void AnswerDisplay(bool IsAnswerCorrect)
        {
            if (IsAnswerCorrect)
            {
                CorrectPage.Visibility = Visibility.Visible;
            }
            else
            {
                IncorrectPage.Visibility = Visibility.Visible;
            }

            await Task.Delay(1000);

            if (IsAnswerCorrect)
            {
                CorrectPage.Visibility = Visibility.Hidden;
            }
            else
            {
                IncorrectPage.Visibility = Visibility.Hidden;
            }
        }

        public void MusicLoop()
        {

            bgmusic.Open(new Uri(@"Sounds\BGLoop.wav", UriKind.RelativeOrAbsolute));
            bgmusic.MediaEnded += new EventHandler(Media_Ended);
            bgmusic.Play();

            return;
        }
        private void Media_Ended(object sender, EventArgs e)
        {
            bgmusic.Position = TimeSpan.Zero;
            bgmusic.Play();
        }

        public void SetCategoryCounts()
        {
            string musicCategoryLabel = (Mu.Content.ToString() + " (" + musicQuestionPool.Count().ToString() + " Questions)");
            string movieCategoryLabel = (Mo.Content.ToString() + " (" + movieQuestionPool.Count().ToString() + " Questions)");
            string geographyCategoryLabel = (Ge.Content.ToString() + " (" + geographyQuestionPool.Count().ToString() + " Questions)");
            string scienceCategoryLabel = (Sc.Content.ToString() + " (" + scienceQuestionPool.Count().ToString() + " Questions)");
            string historyCategoryLabel = (Hi.Content.ToString() + " (" + historyQuestionPool.Count().ToString() + " Questions)");
            string naturalCategoryLabel = (NW.Content.ToString() + " (" + naturalWorldQuestionPool.Count().ToString() + " Questions)");
            string sportCategoryLabel = (Sp.Content.ToString() + " (" + sportQuestionPool.Count().ToString() + " Questions)");
            string tvCategoryLabel = (Tv.Content.ToString() + " (" + tvQuestionPool.Count().ToString() + " Questions)");
            string generalCategoryLabel = (Gn.Content.ToString() + " (" + gnQuestionPool.Count().ToString() + " Questions)");
            string foodCategoryLabel = (Fo.Content.ToString() + " (" + foodQuestionPool.Count().ToString() + " Questions)");
            Mu.Content = musicCategoryLabel;
            Mo.Content = movieCategoryLabel;
            Ge.Content = geographyCategoryLabel;
            Sc.Content = scienceCategoryLabel;
            Hi.Content = historyCategoryLabel;
            NW.Content = naturalCategoryLabel;
            Sp.Content = sportCategoryLabel;
            Tv.Content = tvCategoryLabel;
            Gn.Content = generalCategoryLabel;
            Fo.Content = foodCategoryLabel;
        }

        public void Button1Clicked(object sender, RoutedEventArgs e)
        {
            gm.CurrentGameQuestionNumber++;
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
            gm.CurrentGameQuestionNumber++;
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
            gm.CurrentGameQuestionNumber++;
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
            gm.CurrentGameQuestionNumber++;
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
            gm.CurrentGameQuestionNumber = 0;
            StartPage.Visibility = Visibility.Hidden;
            QuestionBoard.Visibility = Visibility.Visible;
            CheckCategoryAndPassToNextQuestion();
            DisplayCategory();
            scoreDisplay.Text = gm.Score.ToString();
            gameInProgress = true;
            DisplayQuestionCountIndicator();
        }

        void DisplayCategory()
        {
            CategoryLabel.Text = gm.Category;

            switch (gm.Category)
            {
                case "Music":
                    CategoryLabel.Text = "Music";
                    break;
                case "Movies":
                    CategoryLabel.Text = "Movies";
                    break;
                case "Food":
                    CategoryLabel.Text = "Food & Drink";
                    break;
                case "Geography":
                    CategoryLabel.Text = "Geography";
                    break;
                case "Science":
                    CategoryLabel.Text = "Science";
                    break;
                case "History":
                    CategoryLabel.Text = "History";
                    break;
                case "Natural World":
                    CategoryLabel.Text = "Natural World";
                    break;
                case "Sport":
                    CategoryLabel.Text = "Sport";
                    break;
                case "TV":
                    CategoryLabel.Text = "TV";
                    break;
                case "General Knowledge":
                    CategoryLabel.Text = "General Knowledge";
                    break;
                default:
                    break;
            }
        }

        void DisplayQuestionCountIndicator()
        {
            QuestionNumberIndicator.Text = ("Question " + (gm.CurrentGameQuestionNumber + 1) + " of " + gm.NumberOfGameQuestions);
        }

        public void NewGame(object sender, RoutedEventArgs e)
        {
            if (gameInProgress)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Start New Game", MessageBoxButton.YesNo);

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    DisplayGameSetup();
                }
            }
            else
            {
                ResetGame();
            }
        }

        public void CheckGameProgress()
        {
            DisplayQuestionCountIndicator();
            if (gm.CurrentGameQuestionNumber == gm.NumberOfGameQuestions)
            {
                GameOver();
            }
        }

        public void GameOver()
        {
            float endScore = gm.Score;
            float endTotalQuestions = gm.NumberOfGameQuestions;

            GameOverPage.Visibility = Visibility.Visible;
            QuestionBoard.Visibility = Visibility.Hidden;
            OptionsPage.Visibility = Visibility.Hidden;
            ScoresPage.Visibility = Visibility.Hidden;
            AboutPage.Visibility = Visibility.Hidden;
            gm.Percentage = ((endScore / endTotalQuestions) * 100);
            GameOverText.Text = ("You answered " + gm.Score.ToString() + " out of " + gm.NumberOfGameQuestions + " questions correctly. (" + gm.Percentage + "%)");
            gameInProgress = false;
        }

        public void CorrectAnswer()
        {
            AnswerDisplay(true);
            if (gm.PlaySounds)
            {
                mp.Open(new Uri(correctSoundFilePath, UriKind.RelativeOrAbsolute));
                mp.Play();
            }
            gm.Score++;
            CheckGameProgress();
            CheckCategoryAndPassToNextQuestion();
            scoreDisplay.Text = gm.Score.ToString();
        }

        public void CheckCategoryAndPassToNextQuestion()
        {
            if (gm.Category.Contains("Music"))
            {
                int qPoolSize = musicQuestionPool.Count();
                DisplayQuestionAndRandomiseAnswerLocation(musicQuestionPool, qPoolSize);
            }
            else if (gm.Category.Contains("Movies"))
            {
                int qPoolSize = movieQuestionPool.Count();
                DisplayQuestionAndRandomiseAnswerLocation(movieQuestionPool, qPoolSize);
            }
            else if (gm.Category.Contains("Geography"))
            {
                int qPoolSize = geographyQuestionPool.Count();
                DisplayQuestionAndRandomiseAnswerLocation(geographyQuestionPool, qPoolSize);
            }
            else if (gm.Category.Contains("History"))
            {
                int qPoolSize = historyQuestionPool.Count();
                DisplayQuestionAndRandomiseAnswerLocation(historyQuestionPool, qPoolSize);
            }
            else if (gm.Category.Contains("TV"))
            {
                int qPoolSize = tvQuestionPool.Count();
                DisplayQuestionAndRandomiseAnswerLocation(tvQuestionPool, qPoolSize);
            }
            else if (gm.Category.Contains("Sport"))
            {
                int qPoolSize = sportQuestionPool.Count();
                DisplayQuestionAndRandomiseAnswerLocation(sportQuestionPool, qPoolSize);
            }
            else if (gm.Category.Contains("Natural"))
            {
                int qPoolSize = naturalWorldQuestionPool.Count();
                DisplayQuestionAndRandomiseAnswerLocation(naturalWorldQuestionPool, qPoolSize);
            }
            else if (gm.Category.Contains("Science"))
            {
                int qPoolSize = scienceQuestionPool.Count();
                DisplayQuestionAndRandomiseAnswerLocation(scienceQuestionPool, qPoolSize);
            }
            else if (gm.Category.Contains("General Knowledge"))
            {
                int qPoolSize = gnQuestionPool.Count();
                DisplayQuestionAndRandomiseAnswerLocation(gnQuestionPool, qPoolSize);
            }
            else if (gm.Category.Contains("Food"))
            {
                int qPoolSize = foodQuestionPool.Count();
                DisplayQuestionAndRandomiseAnswerLocation(foodQuestionPool, qPoolSize);
            }
            else
            {
                int qPoolSize = gnQuestionPool.Count();
                DisplayQuestionAndRandomiseAnswerLocation(gnQuestionPool, qPoolSize);
            }
        }

        public void IncorrectAnswer()
        {
            AnswerDisplay(false);
            if (gm.PlaySounds)
            {
                mp.Open(new Uri(incorrectSoundFilePath, UriKind.RelativeOrAbsolute));
                mp.Play();
            }
            CheckGameProgress();
            CheckCategoryAndPassToNextQuestion();
        }

        public List<Question> RemoveQuestionFromPool(List<Question> questionPool, int currentQuestion)
        {
            questionPool.RemoveAt(currentQuestion);
            CheckGameProgress();
            return questionPool;
        }

        public void DisplayQuestionAndRandomiseAnswerLocation(List<Question> questionPool, int sizeOfQuestionPool)
        {
            int currentQuestion = r.Next(0, sizeOfQuestionPool);
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
            questionPool.RemoveAt(currentQuestion);
        }
        private void IncrementScore(object sender, RoutedEventArgs e)
        {
            gm.Score++;
            scoreDisplay.Text = gm.Score.ToString();
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
                ScoresPage.Visibility = Visibility.Hidden;
                AboutPage.Visibility = Visibility.Hidden;
            }
            else
            {
                DisplayGameSetup();
            }
        }

        public void DisplayGameSetup()
        {
            QuestionBoard.Visibility = Visibility.Hidden;
            StartPage.Visibility = Visibility.Visible;
            GameOverPage.Visibility = Visibility.Hidden;
            OptionsPage.Visibility = Visibility.Hidden;
            ScoresPage.Visibility = Visibility.Hidden;
            AboutPage.Visibility = Visibility.Hidden;
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
        private void Questions_5(object sender, RoutedEventArgs e)
        {
            gm.NumberOfGameQuestions = 5;
        }
        private void Questions_10(object sender, RoutedEventArgs e)
        {
            gm.NumberOfGameQuestions = 10;
        }
        private void Questions_20(object sender, RoutedEventArgs e)
        {
            gm.NumberOfGameQuestions = 20;
        }
        private void Questions_30(object sender, RoutedEventArgs e)
        {
            gm.NumberOfGameQuestions = 30;
        }
        private void Questions_50(object sender, RoutedEventArgs e)
        {
            gm.NumberOfGameQuestions = 50;
        }
        private void Music(object sender, RoutedEventArgs e)
        {
            gm.Category = "Music";
        }
        private void Movies(object sender, RoutedEventArgs e)
        {
            gm.Category = "Movies";
        }
        private void Food(object sender, RoutedEventArgs e)
        {
            gm.Category = "Food";
        }
        private void Geography(object sender, RoutedEventArgs e)
        {
            gm.Category = "Geography";
        }
        private void Science(object sender, RoutedEventArgs e)
        {
            gm.Category = "Science";
        }
        private void History(object sender, RoutedEventArgs e)
        {
            gm.Category = "History";
        }
        private void Natural_World(object sender, RoutedEventArgs e)
        {
            gm.Category = "Natural World";
        }
        private void Sport(object sender, RoutedEventArgs e)
        {
            gm.Category = "Sport";
        }
        private void TV(object sender, RoutedEventArgs e)
        {
            gm.Category = "TV";
        }
        private void General_Knowledge(object sender, RoutedEventArgs e)
        {
            gm.Category = "General Knowledge";
        }

        private void SaveScoreAndReturnToStartPage(object sender, RoutedEventArgs e)
        {

            ///
            /// New information to be edited
            ///
            Score sc = new Score();
            if (gm.Percentage > 95)
            {
                sc.Award = "images/GoldTrophy.png";
            }
            else if (gm.Percentage >= 85)
            {
                sc.Award = "images/SilverTrophy.png";
            }
            else if (gm.Percentage >= 70)
            {
                sc.Award = "images/BronzeTrophy.png";
            }
            else if (gm.Percentage == 0)
            {
                sc.Award = "images/Poop.png";
            }
            else
            {
                sc.Award = "";
            }

            try
            {
                DB dB = new DB();
                DataTable dataTable = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();

                MySqlCommand mySqlCommand = new MySqlCommand(@"INSERT INTO `QuizBuzzScores`(`Name`, `Score`, `Topic`, `Percentage`, `Date`, `Time`, `Award`)
                  VALUES (@Name, @Score, @Topic, @Percentage, @Date, @Time, @Award)", dB.GetConnection());

                mySqlCommand.Parameters.Add("@Name", MySqlDbType.VarChar).Value = UsernameBox.Text;
                mySqlCommand.Parameters.Add("@Score", MySqlDbType.Int32).Value = gm.Score;
                mySqlCommand.Parameters.Add("@Topic", MySqlDbType.VarChar).Value = gm.Category;
                mySqlCommand.Parameters.Add("@Percentage", MySqlDbType.VarChar).Value = gm.Percentage;
                mySqlCommand.Parameters.Add("@Date", MySqlDbType.VarChar).Value = DateTime.Now.ToString("dd/MM/yyyy");
                mySqlCommand.Parameters.Add("@Time", MySqlDbType.VarChar).Value = DateTime.Now.ToString("hh:mm tt");
                mySqlCommand.Parameters.Add("@Award", MySqlDbType.VarChar).Value = sc.Award;

                adapter.SelectCommand = mySqlCommand;

                adapter.Fill(dataTable);
                MessageBox.Show("Score saved!");
            }
            catch (Exception)
            {
                MessageBox.Show("This operation failed.");
                throw;
            }
            finally
            {
                LoadAllOnlineScoresToList();
                ShowScoresPage();
            }
        }

        private void ShowScoresPageClick(object sender, RoutedEventArgs e)
        {
            LoadAllOnlineScoresToList();
            ShowScoresPage();
        }

        private void ShowScoresPage()
        {
            QuestionBoard.Visibility = Visibility.Hidden;
            StartPage.Visibility = Visibility.Hidden;
            OptionsPage.Visibility = Visibility.Hidden;
            GameOverPage.Visibility = Visibility.Hidden;
            ScoresPage.Visibility = Visibility.Visible;
            AboutPage.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult confirmShutdown = MessageBox.Show("Are you sure you want to exit the game?", "Confirm exit", MessageBoxButton.YesNo);

            if (confirmShutdown == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void BackToGameSetup(object sender, RoutedEventArgs e)
        {
            DisplayGameSetup();
        }

        private void AboutMenuOpen(object sender, RoutedEventArgs e)
        {
            QuestionBoard.Visibility = Visibility.Hidden;
            StartPage.Visibility = Visibility.Hidden;
            OptionsPage.Visibility = Visibility.Hidden;
            GameOverPage.Visibility = Visibility.Hidden;
            ScoresPage.Visibility = Visibility.Hidden;
            AboutPage.Visibility = Visibility.Visible;
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(e.Uri.AbsoluteUri);
            e.Handled = true;
        }

        private void StartGame(object sender, RoutedEventArgs e)
        {
            int qCompare;

            if (gm.Category.Contains("Music"))
            {
                qCompare = musicQuestionPool.Count();
            }
            else if (gm.Category.Contains("Movies"))
            {
                qCompare = movieQuestionPool.Count();
            }
            else if (gm.Category.Contains("Geography"))
            {
                qCompare = geographyQuestionPool.Count();
            }
            else if (gm.Category.Contains("History"))
            {
                qCompare = historyQuestionPool.Count();
            }
            else if (gm.Category.Contains("TV"))
            {
                qCompare = tvQuestionPool.Count();
            }
            else if (gm.Category.Contains("Sport"))
            {
                qCompare = sportQuestionPool.Count();
            }
            else if (gm.Category.Contains("Natural"))
            {
                qCompare = naturalWorldQuestionPool.Count();
            }
            else if (gm.Category.Contains("Science"))
            {
                qCompare = scienceQuestionPool.Count();
            }
            else if (gm.Category.Contains("General Knowledge"))
            {
                qCompare = gnQuestionPool.Count();
            }
            else if (gm.Category.Contains("Food"))
            {
                qCompare = foodQuestionPool.Count();
            }
            else
            {
                qCompare = gnQuestionPool.Count();
            }

            if (qCompare < gm.NumberOfGameQuestions)
            {
                MessageBox.Show(("Not enough questions in the " + gm.Category + " question pool to play a " + gm.NumberOfGameQuestions.ToString() + " question game. Select less questions or choose a different category."), "Game creation error!");
            }
            else
            {
                ResetGame();
            }
        }

        public void SliderReadoutChanged()
        {
            VolumeReadout.Text = ((VolumeSlider.Value * 100).ToString() + "%");
        }

        public void SetVolumeToOne()
        {
            VolumeSlider.Value = 0.5;
            MusicVolumeSlider.Value = 0.5;
            MusicVolumeReadoutChanged();
            SliderReadoutChanged();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mp.Volume = VolumeSlider.Value;
            SliderReadoutChanged();
        }

        private void PlayIntroMusic(object sender, RoutedEventArgs e)
        {
            mp.Open(new Uri(introSoundFilePath, UriKind.RelativeOrAbsolute));
            mp.Play();
        }

        private void MusicSoundOn_Click(object sender, RoutedEventArgs e)
        {
            MusicLoop();
            MusicSoundOn.Background = Brushes.Green;
            MusicSoundOff.Background = Brushes.DarkBlue;
        }

        private void MusicSoundOff_Click(object sender, RoutedEventArgs e)
        {
            bgmusic.Stop();
            MusicSoundOn.Background = Brushes.DarkBlue;
            MusicSoundOff.Background = Brushes.Red;
        }

        private void MusicVolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            bgmusic.Volume = MusicVolumeSlider.Value;
            MusicVolumeReadoutChanged();
        }

        private void MusicVolumeReadoutChanged()
        {
            MusicVolumeReadout.Text = ((MusicVolumeSlider.Value * 100).ToString() + "%");
        }

        private void PlayRandomSoundFX()
        {
            int rInt = r.Next(0, 100);
            if (rInt % 2 == 0)
            {
                mp.Open(new Uri(correctSoundFilePath, UriKind.RelativeOrAbsolute));
                mp.Play();
            }
            else
            {
                mp.Open(new Uri(incorrectSoundFilePath, UriKind.RelativeOrAbsolute));
                mp.Play();
            }
        }

        private void RandomSFX(object sender, RoutedEventArgs e)
        {
            PlayRandomSoundFX();
        }

        public void LoadAllOnlineScoresToList()
        {
            DB conn = new DB();
            try
            {
                conn.OpenConnection();
                MySqlCommand cmd = new MySqlCommand("Select Name, Score, Topic, Percentage, Date, Time, Award from QuizBuzzScores ORDER BY Percentage DESC", conn.GetConnection());
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "LoadDataBinding");
                ScoreTemplate.DataContext = ds;


                // Figure out how to mark latest entry with a marker, and remove the marker from the previous entry


            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.CloseConnection();
            }
        }

        public void LoadFoodOnlineScoresToList()
        {

            DB conn = new DB();
            try
            {
                conn.OpenConnection();
                MySqlCommand cmd = new MySqlCommand("Select Name, Score, Topic, Percentage, Date, Time, Award from QuizBuzzScores WHERE Topic = \"Food\" ORDER BY Percentage DESC", conn.GetConnection());

                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "LoadDataBinding");
                ScoreTemplate.DataContext = ds;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.CloseConnection();
            }
        }

        public void LoadScienceOnlineScoresToList()
        {

            DB conn = new DB();
            try
            {
                conn.OpenConnection();
                MySqlCommand cmd = new MySqlCommand("Select Name, Score, Topic, Percentage, Date, Time, Award from QuizBuzzScores WHERE Topic = \"Science\" ORDER BY Percentage DESC", conn.GetConnection());

                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "LoadDataBinding");
                ScoreTemplate.DataContext = ds;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.CloseConnection();
            }
        }

        public void LoadGeneralKnowledgeOnlineScoresToList()
        {

            DB conn = new DB();
            try
            {
                conn.OpenConnection();
                MySqlCommand cmd = new MySqlCommand("Select Name, Score, Topic, Percentage, Date, Time, Award from QuizBuzzScores WHERE Topic = \"General Knowledge\" ORDER BY Percentage DESC", conn.GetConnection());

                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "LoadDataBinding");
                ScoreTemplate.DataContext = ds;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.CloseConnection();
            }
        }
        public void LoadGeographyOnlineScoresToList()
        {

            DB conn = new DB();
            try
            {
                conn.OpenConnection();
                MySqlCommand cmd = new MySqlCommand("Select Name, Score, Topic, Percentage, Date, Time, Award from QuizBuzzScores WHERE Topic = \"Geography\" ORDER BY Percentage DESC", conn.GetConnection());

                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "LoadDataBinding");
                ScoreTemplate.DataContext = ds;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.CloseConnection();
            }
        }

        public void LoadHistoryOnlineScoresToList()
        {

            DB conn = new DB();
            try
            {
                conn.OpenConnection();
                MySqlCommand cmd = new MySqlCommand("Select Name, Score, Topic, Percentage, Date, Time, Award from QuizBuzzScores WHERE Topic = \"History\" ORDER BY Percentage DESC", conn.GetConnection());

                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "LoadDataBinding");
                ScoreTemplate.DataContext = ds;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.CloseConnection();
            }
        }

        public void LoadMoviesOnlineScoresToList()
        {

            DB conn = new DB();
            try
            {
                conn.OpenConnection();
                MySqlCommand cmd = new MySqlCommand("Select Name, Score, Topic, Percentage, Date, Time, Award from QuizBuzzScores WHERE Topic = \"Movies\" ORDER BY Percentage DESC", conn.GetConnection());

                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "LoadDataBinding");
                ScoreTemplate.DataContext = ds;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.CloseConnection();
            }
        }

        public void LoadMusicOnlineScoresToList()
        {

            DB conn = new DB();
            try
            {
                conn.OpenConnection();
                MySqlCommand cmd = new MySqlCommand("Select Name, Score, Topic, Percentage, Date, Time, Award from QuizBuzzScores WHERE Topic = \"Music\" ORDER BY Percentage DESC", conn.GetConnection());

                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "LoadDataBinding");
                ScoreTemplate.DataContext = ds;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.CloseConnection();
            }
        }

        public void LoadNaturalWorldOnlineScoresToList()
        {

            DB conn = new DB();
            try
            {
                conn.OpenConnection();
                MySqlCommand cmd = new MySqlCommand("Select Name, Score, Topic, Percentage, Date, Time, Award from QuizBuzzScores WHERE Topic = \"Natural World\" ORDER BY Percentage DESC", conn.GetConnection());

                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "LoadDataBinding");
                ScoreTemplate.DataContext = ds;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.CloseConnection();
            }
        }

        public void LoadSportOnlineScoresToList()
        {

            DB conn = new DB();
            try
            {
                conn.OpenConnection();
                MySqlCommand cmd = new MySqlCommand("Select Name, Score, Topic, Percentage, Date, Time, Award from QuizBuzzScores WHERE Topic = \"Sport\" ORDER BY Percentage DESC", conn.GetConnection());

                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "LoadDataBinding");
                ScoreTemplate.DataContext = ds;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.CloseConnection();
            }
        }
        public void LoadTVOnlineScoresToList()
        {

            DB conn = new DB();
            try
            {
                conn.OpenConnection();
                MySqlCommand cmd = new MySqlCommand("Select Name, Score, Topic, Percentage, Date, Time, Award from QuizBuzzScores WHERE Topic = \"TV\" ORDER BY Percentage DESC", conn.GetConnection());

                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "LoadDataBinding");
                ScoreTemplate.DataContext = ds;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.CloseConnection();
            }
        }

        private void All_Selected(object sender, RoutedEventArgs e)
        {
            LoadAllOnlineScoresToList();
        }

        private void CB_Science_Selected(object sender, RoutedEventArgs e)
        {
            LoadScienceOnlineScoresToList();
        }

        private void CB_GeneralKnowledge_Selected(object sender, RoutedEventArgs e)
        {
            LoadGeneralKnowledgeOnlineScoresToList();
        }

        private void CB_Geography_Selected(object sender, RoutedEventArgs e)
        {
            LoadGeographyOnlineScoresToList();
        }

        private void CB_Food_Selected(object sender, RoutedEventArgs e)
        {
            LoadFoodOnlineScoresToList();
        }

        private void CB_History_Selected(object sender, RoutedEventArgs e)
        {
            LoadHistoryOnlineScoresToList();
        }

        private void CB_Movies_Selected(object sender, RoutedEventArgs e)
        {
            LoadMoviesOnlineScoresToList();
        }

        private void CB_Music_Selected(object sender, RoutedEventArgs e)
        {
            LoadMusicOnlineScoresToList();
        }

        private void CB_NaturalWorld_Selected(object sender, RoutedEventArgs e)
        {
            LoadNaturalWorldOnlineScoresToList();
        }

        private void CB_Sport_Selected(object sender, RoutedEventArgs e)
        {
            LoadSportOnlineScoresToList();
        }

        private void CB_TV_Selected(object sender, RoutedEventArgs e)
        {
            LoadTVOnlineScoresToList();
        }


        // Can use the query to filter by topic... Select Name, Score.. etc. from QuizBuzzScores WHERE Topic EQUALS General...
    }
}
