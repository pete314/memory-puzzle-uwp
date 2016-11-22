using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace memory_puzzle_uwp.Models
{
    /// <summary>
    /// ScoreModel is the data model for scores
    /// </summary>
    public class ScoreModel
    {
        
        private string id = System.Guid.NewGuid().ToString();//generate the default new guid to simplify insertion

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        private bool isLocal = true;

        public bool IsLocal
        {
            get { return isLocal; }
            set { isLocal = value; }
        }
        
        //Holds the collection name for the puzzle completed
        private string collection;

        public string Collection
        {
            get { return collection; }
            set { collection = value; }
        }

        //Holds the username as defined in local storage["username"]
        private string username;

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        //Holds the seconds of puzzle completion
        private int total_seconds;

        public int TotalSeconds
        {
            get { return total_seconds; }
            set { total_seconds = value; }
        }

        //Holds the score archived in game 
        private int score;

        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        private int puzzle_size;

        public int PuzzleSize
        {
            get { return puzzle_size; }
            set { puzzle_size = value; }
        }

        //Holds the gameplay date
        private DateTime createdAt = DateTime.Now;

        public DateTime Created
        {
            get { return createdAt; }
            set { createdAt = value; }
        }

    }
}
