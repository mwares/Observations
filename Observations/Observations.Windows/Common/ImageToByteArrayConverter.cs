using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
//using Windows.UI.Xaml.Media.Imaging;

namespace Observations.WindowsRT.Common
{
    class ImageToByteArrayConverter
    {
        //public static byte[] ConvertToBytes(BitmapImage bitmapImage)
        //{
            
        //    byte[] pixeBuffer = null; 

        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        WriteableBitmap wb = new WriteableBitmap(bitmapImage);
        //        Stream s1 = wb.PixelBuffer.AsStream();
        //        s1.CopyTo(ms);

        //        pixeBuffer = ms.ToArray();
        //    }
        //}

        /// <summary>
        /// Loads the byte data from a StorageFile
        /// </summary>
        /// <param name="file">The file to read</param>
        public async Task<byte[]> GetByteArray(StorageFile file)
        {
            byte[] fileBytes = null;
            using (IRandomAccessStreamWithContentType stream = await file.OpenReadAsync())
            {
                fileBytes = new byte[stream.Size];
                using (DataReader reader = new DataReader(stream))
                {
                    await reader.LoadAsync((uint)stream.Size);
                    reader.ReadBytes(fileBytes);
                }
            }

            return fileBytes;
        }
    }
}
