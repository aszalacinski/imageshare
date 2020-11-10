using Foundant.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Foundant
{
    public class UpdateAlbum
    {
        public class UpdateAlbumCommand : IRequest<Album>
        {
            public Album Album { get; set; }

            public UpdateAlbumCommand() { }

            public UpdateAlbumCommand(Album album)
            {
                Album = album;
            }
        }

        public class UpdateAlbumCommandHandler : IRequestHandler<UpdateAlbumCommand, Album>
        {
            private IImageStoreRepository _repo;
            private IMediator _mediator;

            public async Task<Album> Handle(UpdateAlbumCommand request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
