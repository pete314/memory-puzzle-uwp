using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using memory_puzzle_uwp.Helpers;
using memory_puzzle_uwp.Models;
using memory_puzzle_uwp.ViewModels;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace memory_puzzle_uwp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ObservableCollection<CollectionModel> collectionList;
        ObservableCollection<ScoreModel> scoreListLocal;
        ObservableCollection<ScoreModel> scoreListRemote;
        MainViewModel mainViewModel;
        public PuzzleModel puzzleModel { get; set; }

        public MainPage()
        {
            puzzleModel = new PuzzleModel();
            mainViewModel = new MainViewModel();
            this.DataContext = this;
            this.InitializeComponent();

            //Load available collections
            collectionList = new ObservableCollection<CollectionModel>();
            mainViewModel.LoadCollections(ref collectionList);

            //Load available local scores
            scoreListLocal = new ObservableCollection<ScoreModel>();
            mainViewModel.LoadBestScores(ref scoreListLocal, true);

            //Load available remote scores
            scoreListRemote = new ObservableCollection<ScoreModel>();
            mainViewModel.LoadBestScores(ref scoreListRemote, false);

            ScoreLocalCollectionList.Source = scoreListLocal;
            ScoreRemoteCollectionList.Source = scoreListRemote;
            CollectionList.Source = collectionList;
            checkUsername();
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

        /// <summary>
        /// Check username in local storage
        /// </summary>
        private void checkUsername() {
            if (null == SHelperUtil.GetLocalStorageValue("username"))
                routeToSettingsDialog();
        }

        /// <summary>
        /// Create and show dialog for settings
        /// </summary>
        private async void routeToSettingsDialog() {
            var dialog = new Windows.UI.Popups.MessageDialog(
                "Welcome to Memory Puzzle!\nIt seams you did not set a username yet. Please visit the settings and set a username.",
                "Settings");

            dialog.Commands.Add(new Windows.UI.Popups.UICommand("Set username") { Id = 0 });
            dialog.Commands.Add(new Windows.UI.Popups.UICommand("Remind me later") { Id = 1 });

            dialog.DefaultCommandIndex = 0;
            dialog.CancelCommandIndex = 1;

            var result = await dialog.ShowAsync();

            if (0 == (int)result.Id)
            {
                ((Frame)Window.Current.Content).Navigate(typeof(Settings));
            }
        }

        /// <summary>
        /// Handle collection tapped event from collection list
        /// </summary>
        /// <param name="sender">StackPanel</param>
        /// <param name="e"></param>
        private void CollectionPanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (SizeSelectorPanel.Visibility == Visibility.Visible)
            {
                puzzleModel.CollectionName= null;
                SizeSelectorPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                FindSelectItem((Panel)sender);
                SizeSelectorPanel.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Find selected collection path
        /// </summary>
        /// <param name="stackPanel"></param>
        private void FindSelectItem(Panel stackPanel) {
            foreach (var child in stackPanel.Children) {
                //This is really risky, only possible with current ui (should refactor into selectedIndex)
                if (child is TextBox) {
                    puzzleModel.CollectionName = ((TextBox)child).Text;
                    return;
                }
            }
        }

        /// <summary>
        /// Handles Custom puzzle size input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomPuzzleSizeIB_TextChanged(object sender, TextChangedEventArgs e)
        {
            tryParseText(((TextBox)sender).Text);
        }
        

        /// <summary>
        /// Handles SizeSelectorTBTapped textbox tapped event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SizeSelectorTBTapped(object sender, TappedRoutedEventArgs e)
        {
            tryParseText(((TextBlock)sender).Text);
        }

        /// <summary>
        /// Parse size string into puzzle size integer
        /// </summary>
        /// <param name="text">UI element text</param>
        private void tryParseText(string text) {
            int size;
            if (Int32.TryParse(text, out size))
                puzzleModel.BoardSize = size;
        }

        /// <summary>
        /// Start new game with given parameters
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (puzzleModel.BoardSize > 0 && puzzleModel.CollectionName != null) {
                ((Frame)Window.Current.Content).Navigate(typeof(PlayGamePage), puzzleModel);
            }
            
        }
    }
}
