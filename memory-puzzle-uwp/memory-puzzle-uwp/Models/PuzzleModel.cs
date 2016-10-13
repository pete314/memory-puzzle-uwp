using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace memory_puzzle_uwp.Models
{
    public class PuzzleModel
    {
        //Hold the board width
        private int boardWidth = 6;
        
        public int BoardWidth
        {
            get { return boardWidth; }
            set { boardWidth = value; }
        }

        private int boardHeight = 4;

        public int BoardHeight
        {
            get { return boardHeight; }
            set { boardHeight = value; }
        }

        //Hold the collection name for images to use for current puzzle
        private string collectionName = "default_color";

        public string CollectionName
        {
            get { return collectionName; }
            set { collectionName = value; }
        }

        //Holds the current score
        private string score = "0";

        public string Score
        {
            get { return score; }
            set { score = value; }
        }

        private string time = "0";

        public string Time
        {
            get { return time; }
            set { time = value; }
        }

    }
}
