using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using memory_puzzle_uwp.Helpers;
using memory_puzzle_uwp.Models;

namespace memory_puzzle_uwp.ViewModels
{
    public class MainViewModel : NotificationBase
    {
        public MainViewModel() { }

        /// <summary>
        /// Load the available collections
        /// </summary>
        /// <param name="collectionList">The observable collection consumed by xaml</param>
        public void LoadCollections(ref ObservableCollection<CollectionModel> collectionList) {
            string[] collectionNames = SCollectionHelper.getCollectionNames();
            Random random = new Random();

            foreach (string collection in collectionNames) {
                string[] collectionImages = SCollectionHelper.getImageNamesFromFolder(collection);

                collectionList.Add(new CollectionModel() {
                    SampleImgPath1 = collectionImages[random.Next(collectionImages.Length)],
                    SampleImgPath2 = collectionImages[random.Next(collectionImages.Length)],
                    SampleImgPath3 = collectionImages[random.Next(collectionImages.Length)],
                    SampleImgPath4 = collectionImages[random.Next(collectionImages.Length)],
                    CollectionName = collection
                });
            }
        }
    }
}
