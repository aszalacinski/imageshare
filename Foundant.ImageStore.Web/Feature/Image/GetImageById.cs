using Foundant.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Foundant
{
    public class GetImageById
    {
        public class GetImageByIdQuery : IRequest<Image>
        {
            public Guid AlbumId { get; set; }
            public Guid ImageId { get; set; }

            public GetImageByIdQuery() { }

            public GetImageByIdQuery(Guid albumId, Guid imageId)
            {
                AlbumId = albumId;
                ImageId = imageId;
            }
        }

        public class GetImageByIdQueryHandler : IRequestHandler<GetImageByIdQuery, Image>
        {
            private IImageStoreRepository _repo;

            public GetImageByIdQueryHandler(IImageStoreRepository repo)
            {
                _repo = repo;
            }

            public async Task<Image> Handle(GetImageByIdQuery query, CancellationToken cancellationToken)
            {
                var album = await _repo.GetAlbumById(query.AlbumId);

                return album.Images.Where(x => x.Id == query.ImageId).FirstOrDefault();
            }
        }
    }
}
