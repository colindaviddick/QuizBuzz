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
        public QuestionManager questionManager = new QuestionManager();
        public List<Question> questionPool = new List<Question>();
        public Question q1 = new Question();
        public MainWindow()
        {
            InitializeComponent();
            q1.AddQuestion("What is wrong with my program?", "You suck", "You're awesome at programming", "You're awesome at programming", "You're awesome at programming", "Can't do the basics");
            questionPool.Add(q1);
            MessageBox.Show(questionPool[0].CorrectAnswer);
            //questionManager.LoadQuestionsToList(questionPool);
            //questionManager.DisplayQuestionAndRandomiseAnswerLocation(1);
        }

        public void Correct(object sender, RoutedEventArgs e)
        {

        }

        public void Incorrect(object sender, RoutedEventArgs e)
        {

        }
    }
}
