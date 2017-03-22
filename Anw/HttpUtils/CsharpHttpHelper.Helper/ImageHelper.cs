using System.Drawing;
using System.IO;

namespace Anw.HttpUtils.CsharpHttpHelper.Helper
{
    internal class ImageHelper
    {
        internal static Image ByteToImage(byte[] b)
        {
            Image result;
            try
            {
                var stream = new MemoryStream(b);
                result = Image.FromStream(stream, true);
            }
            catch
            {
                result = null;
            }
            return result;
        }
    }
}