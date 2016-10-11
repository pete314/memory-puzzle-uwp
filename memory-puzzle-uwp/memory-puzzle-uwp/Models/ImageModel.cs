using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace memory_puzzle_uwp.Models
{
    /// <summary>
    /// Image model is holding the data about image
    /// </summary>
    public class ImageModel
    {
        const string HIDDEN_PATH = "/Images/hidden.png";
        private string collection;

        public string Collection
        {
            get { return collection; }
            set { collection = value; }
        }

        private int imageId;

        public int ImageId
        {
            get { return imageId; }
            set { imageId = value; }
        }

        public string ImageIdStr { get { return "" + imageId; }}

        private string path;
            
        public string Path
        {
            get { return isVisible || isFound ? path : HIDDEN_PATH; }
            set { path = value; }
        }

        private bool isVisible = false;

        public bool IsVisible
        {
            get { return isVisible; }
            set { isVisible = value; }
        }

        private bool isFound = false;

        public bool IsFound
        {
            get { return isFound; }
            set { isFound = value; }
        }

        //Defines the row where in which the image is located for current game
        private string rowLocation;

        public string RowLocation
        {
            get { return rowLocation; }
            set { rowLocation = value; }
        }

        //Defines the column in which the image is located for current game
        private int columnLocation;

        public int ColumnLocation
        {
            get { return columnLocation; }
            set { columnLocation = value; }
        }

        private BitmapImage imageSource;

        public BitmapImage ImageSource
        {
            get { return imageSource; }
            set { imageSource = value; }
        }

    }
}
