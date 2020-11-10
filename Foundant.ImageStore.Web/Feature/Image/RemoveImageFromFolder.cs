using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Foundant.GetImageById;

namespace Foundant.ImageStore.Web.Feature.Image
{
    public class RemoveImageFromFolder
    {
        public class RemoveImageFromFolderCommand : IRequest<bool>
        {
            public Guid AlbumId { get; set; }
            public Guid ImageId { get; set; }
            public string Extension { get; set; }

            public RemoveImageFromFolderCommand(Guid albumId, Guid imageId, string extension)
            {
                AlbumId = albumId;
                ImageId = imageId;
                Extension = extension;
            }
        }

        public class RemoveImageFromFolderCommandHandler : IRequestHandler<RemoveImageFromFolderCommand, bool>
        {
            private IMediator _mediator;

            public RemoveImageFromFolderCommandHandler(IMediator mediator)
            {
                _mediator = mediator;
            }

            public async Task<bool> Handle(RemoveImageFromFolderCommand cmd, CancellationToken cancellationToken)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\albums", cmd.AlbumId.ToString(), $"{cmd.ImageId}{cmd.Extension}");

                File.Delete(filePath);

                if(!File.Exists(filePath))
                {
                    return true;
                }

                throw new Exception("Could not delete the specified image from the album directory");

            }
        }
    }
}
