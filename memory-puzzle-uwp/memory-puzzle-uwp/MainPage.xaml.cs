using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using memory_puzzle_uwp.Models;
using memory_puzzle_uwp.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
        MainViewModel mainViewModel;
        public PuzzleModel puzzleModel { get; set; }
        public MainPage()
        {
            puzzleModel = new PuzzleModel();
            mainViewModel = new MainViewModel();
            this.DataContext = this;
            this.InitializeComponent();
            collectionList = new ObservableCollection<CollectionModel>();
            mainViewModel.LoadCollections(ref collectionList);
            CollectionList.Source = collectionList;
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
