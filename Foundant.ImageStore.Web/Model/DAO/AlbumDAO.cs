using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Foundant.Model.DAO
{
    public class AlbumDAO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public List<ImageDAO> Images { get; set; }

        public AlbumDAO() { }

        public AlbumDAO(Guid id, string name, DateTime created, List<ImageDAO> images)
        {
            Id = id;
            Name = name;
            Created = created;
            Images = images;
        }

        public static AlbumDAO Create(Guid id, string name, DateTime created, List<ImageDAO> images)
            => new AlbumDAO(id, name, created, images);
    }

    public static class AlbumDAOExtensions
    {
        public static AlbumDAO ToDAO(this AlbumSnapshot ss)
            => AlbumDAO.Create(ss.Id, ss.Name, ss.Created, ss.Images.ToDAO());

        public static List<AlbumDAO> ToDAO(this List<AlbumSnapshot> ss)
            => ss.Select(c => c.ToDAO()).ToList();

    }
}
