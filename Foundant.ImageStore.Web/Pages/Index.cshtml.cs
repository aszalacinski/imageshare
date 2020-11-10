using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using static Foundant.AddAlbum;
using static Foundant.GetAlbums;

namespace Foundant.ImageStore.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly ILogger<IndexModel> _logger;
        public List<AlbumListDTO> AlbumDetails { get; private set; }

        public IndexModel(IMediator mediator, ILogger<IndexModel> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            var albums = await _mediator.Send(new GetAlbumsQuery());
            AlbumDetails = albums.Select(c => new AlbumListDTO { AlbumId = c.Id, AlbumName = c.Name, NumberOfImages = c.Images == null ? 0 : c.Images.Count() }).ToList();
        }

        [BindProperty]
        public AddAlbumCommand Data { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var value = await _mediator.Send(Data);

            return RedirectToPage("/Index");
        }

        public class AlbumListDTO
        {
            public Guid AlbumId { get; set; }
            public string AlbumName { get; set; }
            public int NumberOfImages { get; set; }
        }
    }
}
