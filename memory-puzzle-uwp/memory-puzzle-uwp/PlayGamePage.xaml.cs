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
        PuzzleModel puzzleModel;
        ObservableCollection<ImageModel> images;

        /// <summary>
        /// Page constructor loading all resources
        /// </summary>
        public PlayGamePage()
        {
            this.InitializeComponent();
            puzzleModel = new PuzzleModel();
            images = new ObservableCollection<ImageModel>();
            ImageList.Source = images;

            tryLoadImages();
        }

        public void tryLoadImages()
        {
            //Show loading
            ProgressRing progressRing1 = new ProgressRing();
            progressRing1.IsActive = true;
            loaderRing.Children.Add(progressRing1);

            puzzleModel.loadPuzzleBoard(ref images);

            //@todo: Should be moved
            progressRing1.IsActive = false;
        }

        private void TopNavMenuClicked(object sender, RoutedEventArgs e)
        {
            TopNavMenu.TopNavSplitView.IsPaneOpen = !TopNavMenu.TopNavSplitView.IsPaneOpen;
        }
    }
}