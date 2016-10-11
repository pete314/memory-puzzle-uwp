using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using memory_puzzle_uwp.ViewModels;
using Windows.UI.Xaml.Media.Imaging;

namespace memory_puzzle_uwp.Models
{

    /// <summary>
    /// Puzzle model is controlling the board used in the current game session
    /// </summary>
    public class PuzzleViewModel : NotificationBase<PuzzleModel>
    {
        const string IMAGE_FOLDER_NAME = "Images";

        //The image pair for the comparison
        private int[] imagePair;

        //Holds the images in a dictionary for fast access
        private Dictionary<int, ImageModel> currentPuzzleDict;

        //The image names for the current game
        string[] requiredImageNames;

        public int BoardWidth
        {
            get { return This.BoardWidth; }
            set { SetProperty(This.BoardWidth, value, () => This.BoardWidth = value); }
        }

        public int BoardHeight
        {
            get { return This.BoardHeight; }
            set { SetProperty(This.BoardHeight, value, () => This.BoardHeight = value); }
        }

        public string CollectionName
        {
            get { return This.CollectionName; }
            set { SetProperty(This.CollectionName, value, () => This.CollectionName = value); }
        }

        public int Score
        {
            get { return This.Score; }
            set { SetProperty(This.Score, value, () => This.Score = value); }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public PuzzleViewModel() {
            imagePair = new int[2] { -1, -1 };
            currentPuzzleDict = new Dictionary<int, ImageModel>();
            initPuzzleModel();
        }

        /// <summary>
        /// Constructor for custom game
        /// </summary>
        /// <param name="collectionName">The image collection name as in ~/Images/</param>
        /// <param name="boardWidth">The new puzzle width</param>
        /// <param name="boardHeight">The new puzzle height</param>
        public PuzzleViewModel(String collectionName, int boardWidth, int boardHeight)
        {
            CollectionName = collectionName;
            BoardWidth = boardWidth;
            BoardHeight = boardHeight;

            imagePair = new int[2] { -1, -1};
            currentPuzzleDict = new Dictionary<int, ImageModel>();
            initPuzzleModel();
        }

        /// <summary>
        /// Initalize the current game play
        /// </summary>
        private void initPuzzleModel() {
            //The custom image count
            int requiredImagesCnt = (BoardWidth * BoardHeight) / 2;
            requiredImageNames = new string[requiredImagesCnt*2];
            string[] collectionImages = getImageNamesFromFolder();

            //Copy the required amount of unique images
            Array.Copy(collectionImages, 0, requiredImageNames, 0, requiredImagesCnt);
            Array.Copy(collectionImages, 0, requiredImageNames, requiredImagesCnt, requiredImagesCnt);
            
            //Shuffle the images
            shuffleBoard(ref requiredImageNames);
        }


        /// <summary>
        /// Load image names from folder for current game
        /// </summary>
        /// <returns>String[] with resource names</returns>
        private string[] getImageNamesFromFolder()
        {
            return Directory.GetFiles(string.Format("{0}/{1}/", IMAGE_FOLDER_NAME, CollectionName), "*.png");
        }

        /// <summary>
        /// Shuffle image load order
        /// </summary>
        /// <param name="folderImages"></param>
        private void shuffleBoard(ref string[] folderImages) {
            Random rand = new Random();
            for (int i = folderImages.Length - 1; i > 0; i--)
            {
                int index = rand.Next(i + 1);
                string a = folderImages[index];
                folderImages[index] = folderImages[i];
                folderImages[i] = a;
            }
        }

        public bool loadPuzzleBoard(ref ObservableCollection<ImageModel> images) {
            ImageModel im;
            int iCnt = 0;
            foreach (string imageName in requiredImageNames) {
                im = new ImageModel();
                im.Collection = CollectionName;
                im.Path = imageName;
                im.RowLocation = imageName;
                im.IsFound = false;
                im.IsVisible = false;
                im.ImageId = iCnt++;

                //Push the indexes of the pictures to the dictionary for O(1) access during game play
                currentPuzzleDict.Add(im.ImageId, im);

                images.Add(im);
            }

            return true;
        }

        /// <summary>
        /// Check the tapped picture state
        /// </summary>
        /// <param name="imageId">The integer id as defined on initialization</param>
        /// <returns>true if image is revield</returns>
        public bool IsImageFound(int imageId) {
            if (currentPuzzleDict.ContainsKey(imageId))
            {
                ImageModel im = currentPuzzleDict[imageId];
                return im.IsFound;
            }
            return false;
        }

        /// <summary>
        /// Handle the image tapped event from ui
        /// </summary>
        /// <param name="imageId"></param>
        /// <param name="currentImages"></param>
        public void ImageTapped(int imageId, ref ObservableCollection<ImageModel> currentImages) {
            ImageModel im = currentPuzzleDict[imageId];
            if (im.IsFound)
                return;//If the user taps the same image twice, do nothing
            

            if (imagePair[0] == -1)
            {
                imagePair[0] = im.ImageId;
                im.IsVisible = true;
                currentImages[im.ImageId] = im;

            } else if (imagePair[1] == -1){
                imagePair[1] = im.ImageId;
                im.IsVisible = true;
                ImageModel im2 = currentPuzzleDict[imagePair[0]];

                //Check if there is pair
                if (im2.Path.Equals(im.Path) && im2.ImageId != im.ImageId)
                {
                    im2.IsFound = true;
                    im.IsFound = true;
                    //Update observable collection
                    currentImages[im.ImageId] = im;
                    currentImages[im2.ImageId] = im2;

                    //Update dict
                    currentPuzzleDict[im.ImageId] = im;
                    currentPuzzleDict[im2.ImageId] = im2;

                    imagePair[0] = -1;
                    imagePair[1] = -1;
                }else
                {
                    currentImages[im.ImageId] = im;
                }
            }
            else
            {
                //Flip images back to hidden
                ImageModel im1 = currentPuzzleDict[imagePair[0]];
                ImageModel im2 = currentPuzzleDict[imagePair[1]];
                im1.IsVisible = false;
                im2.IsVisible = false;
                currentImages[im1.ImageId] = im1;
                currentImages[im2.ImageId] = im2;

                //Flip current image
                imagePair[0] = im.ImageId;
                imagePair[1] = -1;
                im.IsVisible = true;
                currentImages[im.ImageId] = im;
            }
        }
    }
}
