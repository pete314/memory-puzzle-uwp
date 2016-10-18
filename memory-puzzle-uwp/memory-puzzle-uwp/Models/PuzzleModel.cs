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
        private int boardSize = 2;
        
        public int BoardSize
        {
            get { return boardSize; }
            set { boardSize = value; }
        }

        //Hold the collection name for images to use for current puzzle
        private string collectionName = "Images/default";

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
