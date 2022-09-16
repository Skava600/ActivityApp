using Microsoft.Win32;
using StepProject.Entities;
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
            users.SelectedWorkouts = UsersViewModel.UserList[list.SelectedIndex].Workouts;       
        }

        private void SaveUserButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            UsersViewModel? users = DataContext as UsersViewModel;
            if (users == null) return;

            try
            {
                if (saveFileDialog.ShowDialog() == true)
                {
                    switch (((TextBlock)serializerComboBox.SelectedItem).Text)
                    {
                        case "CSV":
                            {
                                saveFileDialog.DefaultExt = "csv";
                                using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName))
                                {
                                    WorkoutCsvWriter writer = new WorkoutCsvWriter(sw);
                                    foreach (var workout in users.SelectedWorkouts)
                                    {
                                        writer.Write(workout);
                                    }
                                }
                                break;
                            }
                        case "XML":
                            {
                                saveFileDialog.DefaultExt = "xml";
                                UserXmlWriter writer = new UserXmlWriter(saveFileDialog.FileName);
                                writer.Write(UsersViewModel.UserList.Find(u => u.Name.Equals(users.SelectedWorkouts[0].User))!);
                                break;
                            }
                        case "JSON":
                            {
                                saveFileDialog.DefaultExt = "json";
                                UserJsonWriter writer = new UserJsonWriter(saveFileDialog.FileName);
                                writer.Write(UsersViewModel.UserList.Find(u => u.Name.Equals(users.SelectedWorkouts[0].User))!);
                                break;
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to save file, try again. {ex.Message}");
            }
        }
    }

    public class SymbolDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate HighValueTemplate { get; set; }
        public DataTemplate LowValueTemplate { get; set; }
        public DataTemplate ElseValueTemplate { get; set; }
        public Binding Workouts { get; set; }

        public override DataTemplate? SelectTemplate(object item, DependencyObject container)
        {
            if (item == null) return null;

            var adornment = item as ChartAdornment;
            if (adornment == null) return null;

            var workout = adornment.Item as Workout;
            if (workout == null) return null;

            var user = UsersViewModel.UserList.Find(u => u.Name.Equals(workout.User));
            if (user == null) return null;
            
            if (workout.Steps == user.MaxSteps)
            {
                return HighValueTemplate;
            }
            else if (workout.Steps == user.MinSteps)
            {
                return LowValueTemplate;
            }
            return ElseValueTemplate;
        }
    }
    public class UsersViewModel : INotifyPropertyChanged
    {
        public static List<User> UserList { get; set; } = GetUsers();

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
            IList<Entities.Day> days = new DataReader(dataPath).ReadAllDays();

            var users = new List<User>();
            int id = 0;
            foreach (Entities.Day day in days)
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
                u.MinSteps = u.Workouts.Count == 30? minSteps:0;
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
