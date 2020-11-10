using Foundant.Abstract;
using Foundant.Model.DAO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Foundant.ImageStore.Web.Model
{
    public class ImageStoreEFRepository : IImageStoreRepository
    {
        private ImageStoreDbContext _context;

        public ImageStoreEFRepository(ImageStoreDbContext context)
        {
            _context = context;
        }

        public async Task<Album> AddAlbum(Album album)
        {
            var newAlbum = album.ToSnapshot().ToDAO();
            
            _context.Albums.Add(newAlbum);
            var num = await _context.SaveChangesAsync();

            if(num > 0)
            {
                return newAlbum.ToEntity();
            }

            throw new Exception("Couldn't save data");

        }

        public ImageDAO AddImage(string name, string description = null, List<string> tags = null)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAlbumById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteImageById(Guid imageId)
        {
            var image = _context.Images.Where(x => x.Id == imageId).FirstOrDefault();

            var _r = _context.Images.Remove(image);
            await _context.SaveChangesAsync();

            return true;
            
        }

        public bool DeleteImagesByIds(List<Guid> id)
        {
            throw new NotImplementedException();
        }

        public async Task<Album> GetAlbumById(Guid id)
        {
            var album = await _context.Albums.AsNoTracking().Where(x => x.Id == id).Include(x => x.Images).FirstOrDefaultAsync();
            return album.ToEntity();
        }

        public async Task<List<Album>> GetAlbums()
        {
            var albums = await _context.Albums.Include(x => x.Images).ToListAsync();
            return albums.ToEntity();
        }

        public ImageDAO GetImageById(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<ImageDAO> GetImagesByAlbumId(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<ImageDAO> GetImagesByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<ImageDAO> GetImagesByTag(string tag)
        {
            throw new NotImplementedException();
        }

        public async Task<Album> UpdateAlbum(Album updatedAlbum)
        { 
            try
            {
                var album = await _context.Albums.Where(x => x.Id == updatedAlbum.Id).FirstOrDefaultAsync();
                album.Name = updatedAlbum.Name;
                album.Images = updatedAlbum.Images.ToSnapshot().ToDAO();
                _context.Albums.Update(album);
                await _context.SaveChangesAsync();
                return album.ToEntity();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public ImageDAO UpdateImage(Guid id, string name, string description = null, List<string> tags = null)
        {
            throw new NotImplementedException();
        }
    }
}
