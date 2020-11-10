using Foundant.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Foundant.ImageStore.Web.Feature.Image
{
    public class GetImageTags
    {
        public class GetImageTagsQuery : IRequest<List<string>>
        {

        }

        public class GetImageTagQueryHandler : IRequestHandler<GetImageTagsQuery,List<string>>
        {
            private IImageStoreRepository _repo;

            public GetImageTagQueryHandler(IImageStoreRepository repo)
            {
                _repo = repo;
            }

            public async Task<List<string>> Handle(GetImageTagsQuery request, CancellationToken cancellationToken)
            {
                var tags = await _repo.GetImageTags();

                return tags;
            }
        }
    }
}
