using Foundant.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Foundant.ImageStore.Web.Feature.Image
{
    public class UpdateImage
    {
        public class UpdateImageCommand : IRequest<Album>
        {
            public Guid AlbumId { get; set; }
            public Guid ImageId { get; set; }
            public string ImageName { get; set; }
            public string ImageDescription { get; set; }

            public UpdateImageCommand(Guid albumId, Guid imageId, string imageName, string imageDescription)
            {
                AlbumId = albumId;
                ImageId = imageId;
                ImageName = imageName;
                ImageDescription = imageDescription;
            }
        }

        public class UpdateImageCommandHandler : IRequestHandler<UpdateImageCommand, Album>
        {
            private IImageStoreRepository _repo;

            public UpdateImageCommandHandler(IImageStoreRepository repo)
            {
                _repo = repo;
            }

            public async Task<Album> Handle(UpdateImageCommand cmd, CancellationToken cancellationToken)
            {
                var album = await _repo.GetAlbumById(cmd.AlbumId);
                album.UpdateImageName(cmd.ImageId, cmd.ImageName);
                album.UpdateImageDescription(cmd.ImageId, cmd.ImageDescription);

                return await _repo.UpdateAlbum(album);
            }
        }
    }
}
