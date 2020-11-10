using Foundant.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static Foundant.AddAlbumFolder;

namespace Foundant
{
    public class AddAlbum
    {
        public class AddAlbumCommand : IRequest<Album>
        {
            public string AlbumName { get; set; }
            public AddAlbumCommand() { }
            public AddAlbumCommand(string albumName)
            {
                AlbumName = albumName;
            }
        }

        public class AddAlbumCommandHandler : IRequestHandler<AddAlbumCommand, Album>
        {
            private IImageStoreRepository _repo;
            private IMediator _mediator;

            public AddAlbumCommandHandler(IImageStoreRepository repo, IMediator mediator)
            {
                _repo = repo;
                _mediator = mediator;
            }

            public async Task<Album> Handle(AddAlbumCommand cmd, CancellationToken cancellationToken)
            {
                var album = Album.Create(Guid.Empty, cmd.AlbumName, DateTime.UtcNow, new List<Image>());
                var newAlbum = await _repo.AddAlbum(album);
                _ = _mediator.Send(new AddAlbumFolderCommand(newAlbum.Id));

                return newAlbum;
            }
        }
    }
}
