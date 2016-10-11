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
        private int boardWidth = 5;
        
        public int BoardWidth
        {
            get { return boardWidth; }
            set { boardWidth = value; }
        }

        private int boardHeight = 5;

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
        private int score = 0;

        public int Score
        {
            get { return score; }
            set { score = value; }
        }

    }
}
