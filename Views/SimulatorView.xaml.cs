using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using AutoCarSim.ViewModels;
using AutoCarSim.Models;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AutoCarSim.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SimulatorView : Page
    {

        public SimulationViewModel SVM { get; set; }
        public SimulatorView()
        {

            this.SVM = new SimulationViewModel();
            //this.DataContext = new SimulationViewModel();
            this.InitializeComponent();
        }
        /*
         * Trying to pass parameters
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Parameters parameters = e.Parameter as Parameters;
            //int threads = parameters.threads;
            //int spawns = parameters.spawns * 1000;
            
            this.SVM = new SimulationViewModel(parameters.spawns, parameters.threads * 1000, 1);
            this.InitializeComponent();

        }
        */
    }
}