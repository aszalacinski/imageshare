using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static Foundant.AddImage;
using static Foundant.GetAlbumById;
using static Foundant.ImageStore.Web.Feature.Image.GetImageTags;
using static Foundant.RemoveImageFromAlbumById;

namespace Foundant.ImageStore.Web.Pages.Album
{
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;
        public AlbumDTO AlbumDetails { get; private set; }
        public List<string> Tags { get; private set; }
        public string TagToSearch { get; private set; }
        [BindProperty]
        public Guid AlbumId { get; private set; }

        public IndexModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task OnGetAsync(Guid id)
        {
            AlbumId = id;
            var album = await _mediator.Send(new GetAlbumByIdQuery(id));
            AlbumDetails = new AlbumDTO
            {
                Id = album.Id,
                Name = album.Name,
                Images = album.Images?.Select(c => new ImageDTO { Id = c.Id, Name = c.Name, Created = c.Created, Extension = c.Extension }).ToList()
            };

            Data = new AddImageCommand(id, string.Empty, null);

            Tags = await _mediator.Send(new GetImageTagsQuery());
        }

        public async Task OnPostSearchByTagAsync(Guid albumId, string tagToSearch)
        {
            AlbumId = albumId;
            var album = await _mediator.Send(new GetAlbumByIdQuery(albumId));
            AlbumDetails = new AlbumDTO
            {
                Id = album.Id,
                Name = album.Name
            };

            AlbumDetails.Images = album.Images?.Where(c => c.Tags.Contains(tagToSearch)).Select(c => new ImageDTO { Id = c.Id, Name = c.Name, Created = c.Created, Extension = c.Extension }).ToList();

            Data = new AddImageCommand(albumId, string.Empty, null);
            Tags = await _mediator.Send(new GetImageTagsQuery());

        }

        public async Task<IActionResult> OnGetDeleteImageAsync(Guid albumId, Guid imageId)
        {
            var album = await _mediator.Send(new RemoveImageFromAlbumByIdCommand(albumId, imageId));

            return RedirectToPage("/Album/Index", new { id = albumId });
        }

        [BindProperty]
        public AddImageCommand Data { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            // TODO: validate mime type

            var value = await _mediator.Send(Data);

            return RedirectToPage("/Album/Index", new { id = Data.AlbumId });
        }

        public class AlbumDTO
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public List<ImageDTO> Images { get; set; }
        }

        public class ImageDTO
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Extension { get; set; }
            public DateTime Created { get; set; }
        }
    }
}
