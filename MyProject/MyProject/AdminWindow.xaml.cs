﻿using Oracle.DataAccess.Client;
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
using System.Windows.Shapes;

namespace MyProject
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        OracleConnection connection;
        public AdminWindow(OracleConnection con)
        {
            InitializeComponent();
            connection = con;
        }

        private void CreateTrip_Click(object sender, RoutedEventArgs e)
        {
            CreateTripWindow wind = new CreateTripWindow(connection);
            wind.Show();
            Close();
        }

        private void CreateEmp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow wind = new MainWindow();
            wind.Show();
            Close();
        }
    }
}