using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Business.Helpers
{
    public class FileSettings
    {
        private readonly byte[] fileBytes;
        MemoryStream stream;

        public FileSettings(byte[] fileBytes, string filename = null, string extension = "")
        {
            this.fileBytes = fileBytes;
            this.FileName = filename;
            this.ContentType = $"image/{extension}";
            this.stream = new MemoryStream(fileBytes);
        }
        public int ContentLength => fileBytes.Length;
        public string FileName { get; }
        public Stream InputStream
        {
            get { return stream; }
        }
        public string ContentType { get; }

        public void SaveAs(string filename)
        {
            using (var file = File.Open(filename, FileMode.CreateNew))
                stream.WriteTo(file);
        }

    }
}
