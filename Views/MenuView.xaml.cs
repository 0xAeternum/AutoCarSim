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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AutoCarSim.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MenuView : Page
    {
        public MenuView()
        {
            this.InitializeComponent();
        }

        public void simulateButton_Click( object sender, RoutedEventArgs e)
        {

            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            //store input values
            if (numberOfThreadsTxt.SelectedValue != null) localSettings.Values["threads"] = (int)numberOfThreadsTxt.SelectedValue; //store threads amount to local settings
            if (numberOfSpawnsTxt.Text != null)localSettings.Values["spawns"] = Int32.Parse(numberOfSpawnsTxt.Text);               //store spawn time interval to local settings

            Frame.Navigate(typeof(SimulatorView));
        }
    }
}
