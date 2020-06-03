﻿using System;
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
        QuestionPoolManager questionManager = new QuestionPoolManager();
        public List<Question> questionsPool = new List<Question>();
        public MainWindow()
        {
            InitializeComponent();
            questionManager.LoadQuestionsToList(questionsPool);
            questionManager.DisplayQuestionAndRandomiseAnswerLocation(1);
        }

        public void Correct(object sender, RoutedEventArgs e)
        {

        }

        public void Incorrect(object sender, RoutedEventArgs e)
        {

        }
    }
}
