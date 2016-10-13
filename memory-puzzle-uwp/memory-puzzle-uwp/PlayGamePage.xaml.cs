﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using memory_puzzle_uwp.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
        public PuzzleViewModel PuzzleModel { get; set; }
        ObservableCollection<ImageModel> images;

        /// <summary>
        /// Page constructor loading all resources
        /// </summary>
        public PlayGamePage()
        {
            PuzzleModel = new PuzzleViewModel();
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

            PuzzleModel.loadPuzzleBoard(ref images);

            progressRing1.IsActive = false;
        }

        private void TopNavMenuClicked(object sender, RoutedEventArgs e)
        {
            TopNavMenu.TopNavSplitView.IsPaneOpen = !TopNavMenu.TopNavSplitView.IsPaneOpen;
        }

        private void PuzzleImage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            int imageId = ExtractImageId((Panel)sender);
            PuzzleModel.ImageTapped(imageId, ref images);
        }

        /// <summary>
        /// Extract the id from hidden textbox
        /// </summary>
        /// <param name="sender">Stack panel from xaml ui datatempalte</param>
        /// <returns></returns>
        private int ExtractImageId(Panel sender) {
            foreach (var element in ((Panel)sender).Children)
            {
                if (element is TextBlock)
                    return Int32.Parse(((TextBlock)element).Text);
            }
            return -1;
        }
    }
}
