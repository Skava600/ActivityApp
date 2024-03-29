﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StepProjectModels
{
    [XmlRoot("User",IsNullable = false)]
    public  class User
    {
        private string name = "";
        private double averageSteps;
        private uint minSteps;
        private uint maxSteps;
        [XmlAttribute]
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        [XmlAttribute]
        public double AverageSteps 
        {
            get => averageSteps; 
            set
            {
                averageSteps = value;
                OnPropertyChanged("AverageSteps");
            }
        }
        [XmlAttribute]
        public uint MinSteps
        {
            get => minSteps;
            set
            {
                minSteps = value;
                OnPropertyChanged("MinSteps");
            }
        }
        [XmlAttribute]
        public uint MaxSteps
        {
            get => maxSteps;
            set
            {
                maxSteps = value;
                OnPropertyChanged("MaxSteps");
            }
        }
        [XmlArray("Workouts")]
        public ObservableCollection<Workout> Workouts { get; }
        public User()
        {
            Workouts = new ObservableCollection<Workout>();
            Workouts.CollectionChanged += (_, _) =>
            {
                OnPropertyChanged("Days");
                CalculateAverageSteps();
                CalculateBestStepsResult();
                CalculateWorstStepsResult();
            };
        }

        [XmlIgnoreAttribute]
        [JsonIgnore]
        public bool HasUnstableWorkouts
        {
            get
            {
                if (AverageSteps - MinSteps > AverageSteps * 0.2 ||
                    MaxSteps - AverageSteps > AverageSteps * 0.2)
                    return true;
                return false;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public override string ToString()
        {
            return Name.ToString(CultureInfo.InvariantCulture) + "," +AverageSteps + "," + MinSteps + "," + MaxSteps;
        }

        private void CalculateAverageSteps()
        {
            this.AverageSteps = (uint)Workouts.Average(workout => workout.Steps);
        }

        private void CalculateBestStepsResult()
        {
            this.MaxSteps = (uint)Workouts.Max(workout => workout.Steps);
        }

        private void CalculateWorstStepsResult()
        {
            this.MinSteps = (uint)Workouts.Min(workout => workout.Steps);
        }
    }
}
