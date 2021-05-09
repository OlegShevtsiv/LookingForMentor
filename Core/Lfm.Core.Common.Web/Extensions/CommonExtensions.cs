using System.IO;
using Microsoft.AspNetCore.Http;

namespace Lfm.Core.Common.Web.Extensions
{
    public static class CommonExtensions
    {
        public static byte[] GetBytes(this IFormFile formFile)
        {
            byte[] bytes = null;
            try
            {
                if (formFile != null)
                {
                    using var binaryReader = new BinaryReader(formFile.OpenReadStream());
                    bytes = binaryReader.ReadBytes((int) formFile.Length);
                }
            }
            catch
            { }
            return bytes;
        }
    }
}