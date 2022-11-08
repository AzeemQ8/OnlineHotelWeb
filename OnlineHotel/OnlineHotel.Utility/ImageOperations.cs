using Microsoft.AspNetCore.Hosting;
using OnlineHotel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineHotel.Utility
{
    public  class ImageOperations
    {
        private IWebHostEnvironment _webHostEnvironment;
     
        public  string UploadImage(RoomViewModel vm)
        {
            string filename = null;
            if (vm.RoomPictureFile != null)
            {
                string UploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                filename = Guid.NewGuid().ToString() + vm.RoomPictureFile.FileName;
                string filePath = Path.Combine(UploadDir, filename);
                using (FileStream ms = new FileStream(filePath, FileMode.Create))
                {
                    vm.RoomPictureFile.CopyTo(ms);
                }
            }
            return filename;
        }

        public ImageOperations(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
    }
}
