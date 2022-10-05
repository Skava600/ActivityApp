using Microsoft.Win32;
using StepProject.Cmds;
using StepProject.Utils.Writers;
using StepProjectModels;
using StepsAnylyzerSerialize.Serializers;
using StepsDataDeserializer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace StepProject.ViewModels
{
    public class UserViewModel : INotifyPropertyChanged
    {
        private User? selectedUser;
        private TextBlock? serializerFormat;
        private RelayCommandT<User?>? _saveUserCommand = null;
        private RelayCommandT<UserViewModel?>? _loadDataCommand = null;

        public ObservableCollection<User> UserList { get; set; } = new ObservableCollection<User>();

        public User? SelectedUser
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

        public ITemplateSerializer<User>? Serializer 
        {
            get
            {
                if (SerializerFormat == null) return null;
                switch (SerializerFormat.Text)
                {
                    case "CSV": return new UserCsvSerializer<User>();
                    case "XML": return new TemplateXmlSerializer<User>();
                    case "JSON":return new TemplateJsonSerializer<User>(); 
                }

                return null;

            }
        }

        public TextBlock? SerializerFormat { get => serializerFormat; set => serializerFormat = value; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public RelayCommandT<User?> SaveUserCmd =>
             _saveUserCommand ??= new RelayCommandT<User?>(SaveUser, CanSaveUser);

        public RelayCommandT<UserViewModel?>? LoadDataCmd =>
            _loadDataCommand ??= new RelayCommandT<UserViewModel?>(LoadData, null);

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

        private bool CanSaveUser(User? user) => user != null && Serializer != null;
        private void SaveUser(User? user)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = SerializerFormat!.Text.ToLower();
            if (saveFileDialog.ShowDialog() == true)
            {
                Serializer!.Write(new[] { user! }, saveFileDialog.FileName);
            }
        }

        private void LoadData(UserViewModel? uvm)
        {
            if (uvm == null) return;

            IList<User> users = new List<User>();
            try
            {
                users = new UserDeserializer(ChooseDataSetFiles()).ReadAllData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during data importing." + ex.Message);
            }

            foreach (var user in users)
            {
                try
                {
                    User foundUser = uvm.UserList.Where(u => u.Name.Equals(user.Name)).First();
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
