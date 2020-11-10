using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Foundant.ImageStore.Web.Abstract
{
    public interface IFileStorageService
    {
        bool StoreFile(Stream file, string filePath);
    }
}
