using Microsoft.Win32;
using StepProject.Models;
using StepProject.Utils.Readers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StepProject.ViewModels
{
    public class UserViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<User> UserList { get; set; } = new ObservableCollection<User>();

        public User SelectedUser
        {
            get
            {
                return selectedUser;
            }
            set
            {
                selectedUser = value;
                OnPropertyChanged("SelectedUser");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private User selectedUser = new User();

        public static ObservableCollection<User> GetUsers()
        {
            IList<Day> days = new List<Day>();
            try
            {
                days = new DataReader(ChooseDataSetFiles()).ReadAllDays();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during data importing." + ex.Message);
            }

            var users = new List<User>();
            foreach (Day day in days)
            {
                foreach (var workout in day.workouts)
                {
                    if (!users.Any(u => u.Name.Equals(workout.User)))
                    {
                        User newUser = new User();
                        newUser.Name = workout.User;
                        users.Add(newUser);
                    }

                    var user = users.Find(u => u.Name.Equals(workout.User));

                    user!.Workouts.Add(workout);
                }
            }

            return new ObservableCollection<User>(users);
        }

        private static string[] ChooseDataSetFiles()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Data (*.json)|*.json";
            ofd.Title = "Choose initial data set";
            ofd.Multiselect = true;
            ofd.InitialDirectory = Environment.CurrentDirectory;
            bool? dialogResult = ofd.ShowDialog();
            if (dialogResult == true)
            {
                return ofd.FileNames;
            }

            return Array.Empty<string>();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
