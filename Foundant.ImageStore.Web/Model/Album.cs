using Foundant.Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foundant
{
    public class Album
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public DateTime Created { get; private set; }
        public List<Image> Images { get; private set; }

        private Album() { }

        private Album(Guid id, string name, DateTime created, List<Image> images)
        {
            Id = id;
            Name = name;
            Created = created;
            Images = images ?? new List<Image>();
        }

        public static Album Create(Guid id, string name, DateTime created, List<Image> images)
            => new Album(id, name, created, images);

        public void UpdateName(string name)
        {
            Name = name;
        }

        public Guid AddImage(string name, string description, List<string> tags, string extension)
        {
            Image newImage = Image.Create(Guid.Empty, extension, name, description, tags, DateTime.UtcNow);

            Images.Add(newImage);

            return newImage.Id;
        }

        public void UpdateImageName(Guid id, string name)
        {
            var image = Images.Where(c => c.Id == id).FirstOrDefault();
            image.UpdateName(name);
        }

        public void UpdateImageDescription(Guid id, string description)
        {
            var image = Images.Where(c => c.Id == id).FirstOrDefault();
            image.UpdateDescription(description);
        }

        public void AddImageTag(Guid id, string tag)
        {
            var image = Images.Where(c => c.Id == id).FirstOrDefault();
            image.AddTag(tag);

        }

        public void RemoveImageTag(Guid id, string tag)
        {
            var image = Images.Where(c => c.Id == id).FirstOrDefault();
            image.RemoveTag(tag);
        }

        public void RemoveImage(Guid id)
        {
            Images.RemoveAll(r => r.Id == id);
        }
    }

    public class AlbumSnapshot
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public List<ImageSnapshot> Images { get; set; }

        private AlbumSnapshot() { }

        private AlbumSnapshot(Guid id, string name, DateTime created, List<ImageSnapshot> images)
        {
            Id = id;
            Name = name;
            Created = created;
            Images = images;
        }

        public static AlbumSnapshot Create(Guid id, string name, DateTime created, List<ImageSnapshot> images)
            => new AlbumSnapshot(id, name, created, images);
    }

    public static class AlbumExtensions
    {
        public static AlbumSnapshot ToSnapshot(this Album album)
            => AlbumSnapshot.Create(album.Id, album.Name, album.Created, album.Images?.ToSnapshot());

        public static List<AlbumSnapshot> ToSnapshot(this List<Album> albums)
            => albums.Select(c => c.ToSnapshot()).ToList();

        public static Album ToEntity(this AlbumDAO dao)
            => Album.Create(dao.Id, dao.Name, dao.Created, dao.Images?.ToEntity());

        public static List<Album> ToEntity(this List<AlbumDAO> dao)
            => dao.Select(c => c.ToEntity()).ToList();
    }
}
