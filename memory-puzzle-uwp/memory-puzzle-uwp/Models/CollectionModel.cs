using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace memory_puzzle_uwp.Models
{
    /// <summary>
    /// CollectionModel holds data about image collection used in gameplay
    /// </summary>
    public class CollectionModel
    {
        private string sampleImgPath1;

        public string SampleImgPath1
        {
            get { return sampleImgPath1; }
            set { sampleImgPath1 = value; }
        }

        private string sampleImgPath2;

        public string SampleImgPath2
        {
            get { return sampleImgPath2; }
            set { sampleImgPath2 = value; }
        }

        private string sampleImgPath3;

        public string SampleImgPath3
        {
            get { return sampleImgPath3; }
            set { sampleImgPath3 = value; }
        }

        private string sampleImgPath4;

        public string SampleImgPath4
        {
            get { return sampleImgPath4; }
            set { sampleImgPath4 = value; }
        }

        private string collectionName;

        public string CollectionName
        {
            get { return collectionName; }
            set { collectionName = value; }
        }

    }
}
