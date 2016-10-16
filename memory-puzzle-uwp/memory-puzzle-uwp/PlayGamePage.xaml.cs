using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using memory_puzzle_uwp.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
    public sealed partial class PlayGamePage : Page
    {
        private PuzzleModel puzzleModel { get; set; }
        public PuzzleViewModel puzzleViewModel { get; set; }
        ObservableCollection<ImageModel> images;


        /// <summary>
        /// Page constructor loading all resources
        /// </summary>
        public PlayGamePage()
        {

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            puzzleModel = e.Parameter as PuzzleModel;
            if (puzzleModel == null)
                puzzleModel = new PuzzleModel();

            puzzleViewModel = new PuzzleViewModel(puzzleModel);
            this.DataContext = this;
            this.InitializeComponent();
            images = new ObservableCollection<ImageModel>();
            ImageList.Source = images;
            tryLoadImages();
        }
        /// <summary>
        /// Load images with progress ring
        /// </summary>
        public void tryLoadImages()
        {
            ProgressRing progressRing1 = new ProgressRing();
            progressRing1.IsActive = true;
            loaderRing.Children.Add(progressRing1);

            puzzleViewModel.loadPuzzleBoard(ref images);

            progressRing1.IsActive = false;
        }

        /// <summary>
        /// Handles top manu tapped event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TopNavMenuClicked(object sender, RoutedEventArgs e)
        {
            TopNavMenu.TopNavSplitView.IsPaneOpen = !TopNavMenu.TopNavSplitView.IsPaneOpen;
        }

        /// <summary>
        /// Handles image tapped event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PuzzleImage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            int imageId = ExtractImageId((Panel)sender);
            puzzleViewModel.ImageTapped(imageId, ref images);
            if (puzzleViewModel.IsComplete) {
                ShowResults();
            }
        }

        /// <summary>
        /// Show game result in runtime dialog box
        /// </summary>
        private async void ShowResults() {
            var dialog = new Windows.UI.Popups.MessageDialog(
                "Score: " + puzzleViewModel.Score +"\n" +
                "Time: " + puzzleViewModel.Time ,
                "Puzzle completed!");

            dialog.Commands.Add(new Windows.UI.Popups.UICommand("Play again") { Id = 0 });
            dialog.Commands.Add(new Windows.UI.Popups.UICommand("Return home") { Id = 1 });

            dialog.DefaultCommandIndex = 0;
            dialog.CancelCommandIndex = 1;

            var result = await dialog.ShowAsync();

            if (0 == (int)result.Id)
            {
                ((Frame)Window.Current.Content).Navigate(typeof(PlayGamePage), puzzleModel);
            }
            else {
                ((Frame)Window.Current.Content).Navigate(typeof(MainPage));
            }
        }

        /// <summary>
        /// Extract the id from hidden textbox
        /// </summary>
        /// <param name="sender">Stack panel from xaml ui datatempalte</param>
        /// <returns></returns>
        private int ExtractImageId(Panel sender) {
            foreach (var element in sender.Children)
                if (element is TextBlock)
                    return Int32.Parse(((TextBlock)element).Text);

            return -1;
        }

        private void ReturnHome_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ((Frame)Window.Current.Content).Navigate(typeof(MainPage));
        }
    }
}
