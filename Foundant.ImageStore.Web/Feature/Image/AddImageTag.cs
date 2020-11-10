using Foundant.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Foundant
{
    public class AddImageTag
    {
        public class AddImageTagCommand : IRequest<Album>
        {
            public Guid AlbumId { get; set; }
            public Guid Imageid { get; set; }
            public string Tag { get; set; }

            public AddImageTagCommand() { }

            public AddImageTagCommand(Guid albumId, Guid imageId, string tag)
            {
                AlbumId = albumId;
                Imageid = imageId;
                Tag = tag;
            }
        }

        public class AddImageTagCommandHandler : IRequestHandler<AddImageTagCommand, Album>
        {
            private IImageStoreRepository _repo;

            public AddImageTagCommandHandler(IImageStoreRepository repo)
            {
                _repo = repo;
            }
            
            public async Task<Album> Handle(AddImageTagCommand cmd, CancellationToken cancellationToken)
            {
                var album = await _repo.GetAlbumById(cmd.AlbumId);
                album.AddImageTag(cmd.Imageid, cmd.Tag);

                return await _repo.UpdateAlbum(album);

            }
        }
    }
}
