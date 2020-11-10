using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static Foundant.AddImage;
using static Foundant.GetAlbumById;

namespace Foundant.ImageStore.Web.Pages.Album
{
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;
        public AlbumDTO AlbumDetails { get; private set; }

        public IndexModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task OnGetAsync(Guid id)
        {
            var album = await _mediator.Send(new GetAlbumByIdQuery(id));
            AlbumDetails = new AlbumDTO
            {
                Id = album.Id,
                Name = album.Name,
                Images = album.Images?.Select(c => new ImageDTO { Id = c.Id, Name = c.Name, Created = c.Created }).ToList()
            };

            Data = new AddImageCommand(id, string.Empty, null);
        }

        [BindProperty]
        public AddImageCommand Data { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            // validate mime type

            var value = await _mediator.Send(Data);

            return RedirectToPage("/Index", new { id = Data.AlbumId });
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
            public DateTime Created { get; set; }
        }
    }
}