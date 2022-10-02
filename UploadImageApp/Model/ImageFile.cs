using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UploadImageApp.Model
{
    public class ImageFile
    {
        public string byteBase64 { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
    }
}
