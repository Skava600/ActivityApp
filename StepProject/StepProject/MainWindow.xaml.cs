using Microsoft.Win32;
using StepProject.Models;
using StepProject.Utils;
using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
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
using StepProject.Utils.Writers;
using System.Collections.ObjectModel;
using StepProject.ViewModels;
using StepProject.Interfaces;

namespace StepProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SaveUserButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            UserViewModel? uvm = DataContext as UserViewModel;
            IUserSerializer serializer;
            switch (((TextBlock)serializerComboBox.SelectedItem).Text)
            {
                case "CSV": serializer = new UserCsvWriter(); break;
                case "XML":serializer = new UserXmlWriter(); break;
                case "JSON": serializer = new UserJsonWriter(); break;
                default: serializer = new UserJsonWriter(); break;
            }
            if (uvm == null) return;

            if (saveFileDialog.ShowDialog() == true)
            {
                serializer.Write(new[] {uvm.SelectedUser}, saveFileDialog.FileName);
            }
        }
        private void LoadDataButton_Click(object sender, RoutedEventArgs e)
        {
            List<User> users = new List<User>(UserViewModel.GetUsers());
            UserViewModel? uvm = DataContext as UserViewModel;
            if (uvm == null) return;
            foreach (var user in users)
            {
                try
                {
                    var foundUser = uvm.UserList.Where(u => u.Name.Equals(user.Name)).First();
                    foreach (var newWorkout in user.Workouts.Except(foundUser.Workouts))
                    {
                        foundUser.Workouts.Add(newWorkout);
                    }
                }
                catch (InvalidOperationException)
                {
                    uvm.UserList.Add(user);
                }           
            }
        }
    }

   
   
}
