using StepProject.Models;
using StepProject.ViewModels;
using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace StepProject.Utils.ChartHelpers
{
    public class SymbolDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate? HighValueTemplate { get; set; }
        public DataTemplate? LowValueTemplate { get; set; }
        public DataTemplate? ElseValueTemplate { get; set; }

        public override DataTemplate? SelectTemplate(object item, DependencyObject container)
        {
            if (item == null) return null;

            var adornment = item as ChartAdornment;
            if (adornment == null) return null;
            var uvm = adornment.Series.DataContext as UserViewModel;
            var workout = adornment.Item as Workout;
            if (workout == null) return null;

            if (uvm == null) return null;

            if (workout.Steps == uvm.SelectedUser.MaxSteps)
            {
                return HighValueTemplate;
            }
            else if (workout.Steps == uvm.SelectedUser.MinSteps)
            {
                return LowValueTemplate;
            }
            return ElseValueTemplate;
        }
    }
}
