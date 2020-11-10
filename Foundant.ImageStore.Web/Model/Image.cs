using Foundant.Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foundant
{
    public class Image
    {
        public Guid Id { get; private set; }
        public string Extension { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public List<string> Tags { get; private set; }
        public DateTime Created { get; private set; }

        private Image() { }

        private Image(Guid id, string extension, string name, string description, List<string> tags, DateTime created)
        {
            Id = id;
            Extension = extension;
            Name = name;
            Description = description;
            Tags = tags;
            Created = created;
        }

        public static Image Create(Guid id, string extension, string name, string description, List<string> tags, DateTime created)
            => new Image(id, extension, name, description, tags, created);

        public void UpdateName(string name)
        {
            Name = name;
        }

        public void UpdateDescription(string description)
        {
            Description = description;
        }

        public void AddTag(string tag)
        {
            if(!Tags.Contains(tag))
            {
                Tags.Add(tag);
            }
        }

        public void RemoveTag(string tag)
        {
            if (Tags.Contains(tag))
            {
                Tags.Remove(tag);
            }
        }

    }

    public class ImageSnapshot
    {
        public Guid Id { get; set; }
        public string Extension { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Tags { get; set; }
        public DateTime Created { get; set; }

        private ImageSnapshot() { }
        
        private ImageSnapshot(Guid id, string extension, string name, string description, List<string> tags, DateTime created)
        {
            Id = id;
            Extension = extension;
            Name = name;
            Description = description;
            Tags = tags;
            Created = created;
        }

        public static ImageSnapshot Create(Guid id, string extension, string name, string description, List<string> tags, DateTime created)
            => new ImageSnapshot(id, extension, name, description, tags, created);
    }

    public static class ImageExtensions
    {
        public static ImageSnapshot ToSnapshot(this Image image)
            => ImageSnapshot.Create(image.Id, image.Extension, image.Name, image.Description, image.Tags, image.Created);

        public static List<ImageSnapshot> ToSnapshot(this List<Image> image)
            => image.Select(c => c.ToSnapshot()).ToList();

        public static Image ToEntity(this ImageDAO dao)
            => Image.Create(dao.Id, dao.Extension, dao.Name, dao.Description, dao.Tags, dao.Created);

        public static List<Image> ToEntity(this List<ImageDAO> dao)
            => dao.Select(c => c.ToEntity()).ToList();
    }
}
