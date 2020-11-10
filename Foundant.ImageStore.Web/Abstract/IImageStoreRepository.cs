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

        Task<bool> DeleteImageById(Guid imageId);

    }
}
