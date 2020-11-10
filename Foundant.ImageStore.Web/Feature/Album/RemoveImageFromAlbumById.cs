using Foundant.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Foundant.GetAlbumById;
using static Foundant.GetImageById;
using static Foundant.ImageStore.Web.Feature.Image.RemoveImageFromFolder;

namespace Foundant
{
    public class RemoveImageFromAlbumById
    {
        public class RemoveImageFromAlbumByIdCommand : IRequest<Album>
        {
            public Guid AlbumId { get; set; }
            public Guid ImageId { get; set; }

            public RemoveImageFromAlbumByIdCommand(Guid albumId, Guid imageId)
            {
                AlbumId = albumId;
                ImageId = imageId;
            }
        }

        public class RemoveImageFromAlbumByIdCommandHandler : IRequestHandler<RemoveImageFromAlbumByIdCommand, Album>
        {
            private IImageStoreRepository _repo;
            private IMediator _mediator;

            public RemoveImageFromAlbumByIdCommandHandler(IImageStoreRepository repo, IMediator mediator)
            {
                _repo = repo;
                _mediator = mediator;
            }

            public async Task<Album> Handle(RemoveImageFromAlbumByIdCommand cmd, CancellationToken cancellationToken)
            {
                var image = await _mediator.Send(new GetImageByIdQuery(cmd.AlbumId, cmd.ImageId));

                _ = _repo.DeleteImageById(cmd.ImageId);

                // remove image from directory
                _ = _mediator.Send(new RemoveImageFromFolderCommand(cmd.AlbumId, cmd.ImageId, image.Extension));

                var album = await _mediator.Send(new GetAlbumByIdQuery(cmd.AlbumId));

                return album;

            }
        }
    }
}
