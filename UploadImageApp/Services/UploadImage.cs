using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UploadImageApp.Model;

namespace UploadImageApp.Services
{
    public class UploadImage
    {
        /// <summary>
        /// Open Media Picker
        /// </summary>
        /// <returns></returns>
        public async Task<FileResult> OpenMediaPickerAsync()
        {
            try
            {
                var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Please a pick photo"
                });

                if (result.ContentType == "image/png" ||
                    result.ContentType == "image/jpeg" ||
                    result.ContentType == "image/jpg")
                    return result;
                else
                    await App.Current.MainPage.DisplayAlert("Error Type Image", "Please choose a new image", "Ok");

                return null;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Convert FileResult to Stream
        /// </summary>
        /// <param name="fileResult">FileResult</param>
        /// <returns>Stream</returns>
        public async Task<Stream> FileResultToStream(FileResult fileResult)
        {
            if(fileResult == null)
                return null;

            return await fileResult.OpenReadAsync();
        }

        /// <summary>
        /// Convert byte[] to Stream
        /// </summary>
        /// <param name="bytes">byte[]</param>
        /// <returns>Stream</returns>
        public Stream ByteArrayToStream(byte[] bytes)
        {
            return new MemoryStream(bytes);
        }

        /// <summary>
        /// Convert byte[] to string
        /// </summary>
        /// <param name="bytes">byte[]</param>
        /// <returns>string</returns>
        public string ByteBase64ToString(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Convert string to byte[]
        /// </summary>
        /// <param name="text">string</param>
        /// <returns>byte[]</returns>
        public byte[] StringToByteBase64(string text)
        {
            return Convert.FromBase64String(text);
        }

        /// <summary>
        /// Upload a image
        /// </summary>
        /// <param name="fileResult">FileResult</param>
        /// <returns>ImageFile class</returns>
        public async Task<ImageFile> Upload(FileResult fileResult)
        {
            byte[] bytes;

            try
            {
                using (var ms = new MemoryStream())
                {
                    var stream = await FileResultToStream(fileResult);
                    stream.CopyTo(ms);
                    bytes = ms.ToArray();
                }

                return new ImageFile
                {
                    byteBase64 = ByteBase64ToString(bytes),
                    ContentType = fileResult.ContentType,
                    FileName = fileResult.FileName
                };
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
