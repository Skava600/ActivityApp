using Microsoft.Win32;
using StepProjectModels;
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
using StepsAnylyzerSerialize.Serializers;
using StepProject.Cmds;

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
    } 
}
