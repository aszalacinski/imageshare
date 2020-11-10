using Foundant.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Foundant
{
    public class GetAlbumById
    {
        public class GetAlbumByIdQuery : IRequest<Album>
        {
            public Guid Id { get; set; }

            public GetAlbumByIdQuery(Guid id) => Id = id;
        }

        public class GetAlbumByIdQueryHandler : IRequestHandler<GetAlbumByIdQuery, Album>
        {
            private IImageStoreRepository _repo;

            public GetAlbumByIdQueryHandler(IImageStoreRepository repo)
            {
                _repo = repo;
            }

            public async Task<Album> Handle(GetAlbumByIdQuery query, CancellationToken cancellationToken)
            {
                return await _repo.GetAlbumById(query.Id);
            }
        }
    }
}
