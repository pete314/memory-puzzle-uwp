using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using memory_puzzle_uwp.Models;

namespace memory_puzzle_uwp.ViewModels
{
    public class ImageViewModel : NotificationBase<ImageModel>
    {
        public ImageViewModel(ImageModel image = null) : base(image) { }
        
        public string Collection {
            get { return This.Collection; }
            set { SetProperty(This.Collection, value, () => This.Collection = value); }
        }

        public int ImageId
        {
            get { return This.ImageId; }
        }

        public string Path
        {
            get { return This.Path; }
        }

        public bool isVisible
        {
            get { return This.IsVisible; }
            set { SetProperty(This.IsVisible, value, () => This.IsVisible = value); }
        }
    }
}
