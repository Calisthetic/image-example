﻿using ImagesWpfApp.Pages;
using ImagesWpfApp.Utils;
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

namespace ImagesWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            PageManager.MainFrame = MainFrame;
            PageManager.MainFrame.Navigate(new SelectPage());
        }

        private void btnToListView_Click(object sender, RoutedEventArgs e)
        {
            PageManager.MainFrame.Navigate(new ListViewPage());
        }

        private void btnToDataGrid_Click(object sender, RoutedEventArgs e)
        {
            PageManager.MainFrame.Navigate(new DataGridPage());
        }

        private void btnItemsControl_Click(object sender, RoutedEventArgs e)
        {
            PageManager.MainFrame.Navigate(new ItemsControlPage());
        }
    }
}
