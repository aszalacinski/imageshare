using Foundant.Model.DAO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Foundant.Abstract
{
    public interface IImageStoreRepository
    {
        Task<Album> AddAlbum(Album album);
        Task<Album> UpdateAlbum(Album updatedAlbum);
        Task<Album> GetAlbumById(Guid id);
        Task<List<Album>> GetAlbums();
        bool DeleteAlbumById(Guid id);

        ImageDAO AddImage(string name, string description = null, List<string> tags = null);
        ImageDAO UpdateImage(Guid id, string name, string description = null, List<string> tags = null);
        List<ImageDAO> GetImagesByAlbumId(Guid id);
        ImageDAO GetImageById(Guid id);
        List<ImageDAO> GetImagesByTag(string tag);
        List<ImageDAO> GetImagesByName(string name);
        bool DeleteImage(Guid id);
        bool DeleteImagesByIds(List<Guid> id);

    }
}
