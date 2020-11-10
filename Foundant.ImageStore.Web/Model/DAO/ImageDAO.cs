using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Foundant.Model.DAO
{
    public class ImageDAO
    {
        public Guid Id { get; set; }
        public string Extension { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public string _Tags { get; set; }

        public List<string> Tags
        {
            get { return _Tags == null ? new List<string>() : JsonConvert.DeserializeObject<List<string>>(_Tags); }
            set { _Tags = JsonConvert.SerializeObject(value); }
        }

        public ImageDAO() { }

        public ImageDAO(Guid id, string extension, string name, string description, DateTime created, List<string> tags)
        {
            Id = id;
            Extension = extension;
            Name = name;
            Description = description;
            Created = created;
            Tags = tags;
        }

        public static ImageDAO Create(Guid id, string extension, string name, string description, DateTime created, List<string> tags)
            => new ImageDAO(id, extension, name, description, created, tags);

    }

    public static class ImageDAOExtensions
    {
        public static ImageDAO ToDAO(this ImageSnapshot ss)
            => ImageDAO.Create(ss.Id, ss.Extension, ss.Name, ss.Description, ss.Created, ss.Tags);

        public static List<ImageDAO> ToDAO(this List<ImageSnapshot> ss)
            => ss.Select(c => c.ToDAO()).ToList();
    }
}
