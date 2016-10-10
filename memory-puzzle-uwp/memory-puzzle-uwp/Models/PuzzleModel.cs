using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace memory_puzzle_uwp.Models
{

    /// <summary>
    /// Puzzle model is controlling the board used in the current game session
    /// </summary>
    public class PuzzleModel
    {
        const string IMAGE_FOLDER_NAME = "Images";
        string[] requiredImageNames;
        //Hold the board width
        private int boardWidth = 5;

        //No public set as the board size can not change runtime
        public int BoardWidth
        {
            get { return boardWidth; }
        }

        private int boardHeight = 5;
                
        public int BoardHeight
        {
            get { return boardHeight; }
        }

        //Hold the collection name for images to use for current puzzle
        private string collectionName = "default_color";

        public string CollectionName
        {
            get { return collectionName; }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public PuzzleModel() {
            initPuzzleModel();
        }

        /// <summary>
        /// Constructor for custom game
        /// </summary>
        /// <param name="collectionName">The image collection name as in ~/Images/</param>
        /// <param name="boardWidth">The new puzzle width</param>
        /// <param name="boardHeight">The new puzzle height</param>
        public PuzzleModel(String collectionName, int boardWidth, int boardHeight)
        {
            this.collectionName = collectionName;
            this.boardWidth = boardWidth;
            this.boardHeight = boardHeight;

            initPuzzleModel();
        }

        private void initPuzzleModel() {
            //The custom image count
            int requiredImagesCnt = (boardWidth * boardHeight) / 2;
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
            return Directory.GetFiles(string.Format("{0}/{1}/", IMAGE_FOLDER_NAME, collectionName), "*.png");
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
            BitmapImage src;
            foreach (string imageName in requiredImageNames) {
                im = new ImageModel();
                im.Collection = collectionName;
                im.Path = string.Format("Images/{0}/{1}", collectionName, imageName);
                im.RowLocation = imageName;
                im.IsFound = false;
                im.IsVisible = false;

                //set the image
                //src = new BitmapImage();
                //src.UriSource = new Uri(string.Format("Images/{0}/{1}", collectionName, imageName), UriKind.Relative);
                //im.ImageSource = src;


                images.Add(im);
            }

            return true;
        }
    }
}
