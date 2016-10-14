using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace memory_puzzle_uwp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Settings : Page
    {
        public Settings()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Handle settings saved
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveBtn_Tapped(object sender, TappedRoutedEventArgs e)
        {
            string usrName = UsrNameTB.Text;
            var localStorage = ApplicationData.Current.LocalSettings;
            localStorage.Values["username"] = usrName;
        }

        /// <summary>
        /// Handle shared menu controller tap
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TopNavMenuClicked(object sender, RoutedEventArgs e)
        {
            TopNavMenu.TopNavSplitView.IsPaneOpen = !TopNavMenu.TopNavSplitView.IsPaneOpen;
        }
    }
}
