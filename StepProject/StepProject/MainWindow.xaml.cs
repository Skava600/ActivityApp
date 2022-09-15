using StepProject.Entities;
using StepProject.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        private void ListOfUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView list = (ListView)sender;
            UsersViewModel users = (UsersViewModel)DataContext;
            users.SelectedWorkouts = users.UserList[list.SelectedIndex].Workouts;

            
        }
    }

    public class UsersViewModel : INotifyPropertyChanged
    {
        public List<User> UserList { get; set; } = GetUsers();

        public List<Workout> SelectedWorkouts
        {
            get
            {
                return selectedWorkouts;

            }
            set
            {
                selectedWorkouts = value;
                OnPropertyChanged("SelectedWorkouts");
            }
        }

        private List<Workout> selectedWorkouts = GetUsers()[0].Workouts;

        const string dataPath = @"G:\study\work\texodeTask\StepProject\StepProject\data\";

        public event PropertyChangedEventHandler? PropertyChanged;
        private  static List<User> GetUsers()
        {
            IList<Day> days = new DataReader(dataPath).ReadAllDays();

            var users = new List<User>();
            int id = 0;
            foreach (Day day in days)
            {
                foreach (var workout in day.workouts)
                {
                    if (!users.Any(u => u.Name.Equals(workout.User)))
                    {
                        users.Add(new User(id++, workout.User));
                    }

                    var user = users.Find(u => u.Name.Equals(workout.User));

                    user!.Workouts.Add(workout);
                }
            }


            foreach (var u in users)
            {
                double stepByMonth = 0;
                int maxSteps = 0;
                int minSteps = u.Workouts[0].Steps;
                u.Workouts.ForEach(w => {
                    stepByMonth += w.Steps;
                    if (maxSteps < w.Steps)
                        maxSteps = w.Steps;
                    if (minSteps > w.Steps)
                        minSteps = w.Steps;
                    });

                u.AverageSteps = Math.Round(stepByMonth / 30, 1);
                u.MaxSteps = maxSteps;
                u.MinSteps = minSteps;
                u.Workouts.Sort((a, b) => a.Day.CompareTo(b.Day));
            }

            return users;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
