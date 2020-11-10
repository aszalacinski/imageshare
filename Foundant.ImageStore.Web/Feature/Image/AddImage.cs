using Foundant.Abstract;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Foundant.AddImageToFolder;
using static Foundant.GetAlbumById;

namespace Foundant
{
    public class AddImage
    {
        public class AddImageCommand : IRequest<Album>
        {
            public Guid AlbumId { get; set; }
            public string ImageName { get; set; }
            public IFormFile File { get; set; }

            public AddImageCommand() { }

            public AddImageCommand(Guid albumId, string imageName, IFormFile file)
            {
                AlbumId = albumId;
                ImageName = imageName;
                File = file;
            }
        }

        public class AddImageCommandHandler : IRequestHandler<AddImageCommand, Album>
        {
            private IImageStoreRepository _repo;
            private IMediator _mediator;

            public AddImageCommandHandler(IImageStoreRepository repo, IMediator mediator)
            {
                _repo = repo;
                _mediator = mediator;
            }

            public async Task<Album> Handle(AddImageCommand command, CancellationToken cancellationToken)
            {

                // add image info to database
                var album = await _mediator.Send(new GetAlbumByIdQuery(command.AlbumId));
                var imageId = album.AddImage(command.ImageName, string.Empty, new List<string>(), Path.GetExtension(command.File.FileName));

                var updatedAlbum = await _repo.UpdateAlbum(album);

                // upload image to directory
                _ = _mediator.Send(new AddImageToFolderCommand(command.AlbumId, updatedAlbum.Images.OrderByDescending(x => x.Created).FirstOrDefault().Id, command.File));

                return updatedAlbum;

            }
        }
    }
}
