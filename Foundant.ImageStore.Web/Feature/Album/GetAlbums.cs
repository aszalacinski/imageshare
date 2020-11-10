using Foundant.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Foundant
{
    public class GetAlbums
    {
        public class GetAlbumsQuery : IRequest<List<Album>>
        {

        }

        public class GetAlbumsQueryHandler : IRequestHandler<GetAlbumsQuery, List<Album>>
        {
            private IImageStoreRepository _repo;

            public GetAlbumsQueryHandler(IImageStoreRepository repo)
            {
                _repo = repo;
            }

            public async Task<List<Album>> Handle(GetAlbumsQuery query, CancellationToken cancellationToken)
            {
                return await _repo.GetAlbums();
            }
        }
    }
}
