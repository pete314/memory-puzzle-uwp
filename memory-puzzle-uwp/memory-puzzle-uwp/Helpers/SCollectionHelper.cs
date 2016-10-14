using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace memory_puzzle_uwp.Helpers
{
    public class SCollectionHelper
    {
        const string IMAGE_FOLDER_NAME = "Images";
        
        /// <summary>
        /// Load image names from folder for current game
        /// </summary>
        /// <returns>String[] with resource names</returns>
        public static string[] getImageNamesFromFolder(string collectionName)
        {
            return Directory.GetFiles(collectionName, "*.png");
        }

        /// <summary>
        /// Get folder names fomr image folder (collection names)
        /// </summary>
        /// <returns>String </returns>
        public static string[] getCollectionNames() {
            return Directory.GetDirectories(IMAGE_FOLDER_NAME);
        }

        /// <summary>
        /// Shuffle image load order
        /// </summary>
        /// <param name="folderImages"></param>
        public static void shuffleBoard(ref string[] folderImages)
        {
            Random rand = new Random();
            for (int i = folderImages.Length - 1; i > 0; i--)
            {
                int index = rand.Next(i + 1);
                string a = folderImages[index];
                folderImages[index] = folderImages[i];
                folderImages[i] = a;
            }
        }
    }
}
