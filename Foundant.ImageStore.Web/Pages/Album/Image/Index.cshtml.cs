using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Foundant.AddImageTag;
using static Foundant.GetImageById;
using static Foundant.ImageStore.Web.Feature.Image.UpdateImage;

namespace Foundant.ImageStore.Web.Pages.Album.Image
{
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;
        [BindProperty]
        public ImageEditDTO ImageToEdit { get; set; }

        public IndexModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task OnGetAsync(Guid albumId, Guid imageId)
        {
            var image = await _mediator.Send(new GetImageByIdQuery(albumId, imageId));
            ImageToEdit = new ImageEditDTO { AlbumId = albumId, ImageId = imageId, ImageDescription = image.Description, ImageName = image.Name, Tags = image.Tags, Extension = image.Extension };
            AddTag = new AddTagDTO { AlbumId = albumId, ImageId = imageId };
        }

        public async Task<IActionResult> OnPostAsync(ImageEditDTO imageToEdit)
        {
            _ = await _mediator.Send(new UpdateImageCommand(imageToEdit.AlbumId, imageToEdit.ImageId, imageToEdit.ImageName, imageToEdit.ImageDescription));

            return RedirectToPage("/Album/Image/Index", new { albumId = imageToEdit.AlbumId, imageId = imageToEdit.ImageId });
        }

        public class ImageEditDTO
        {
            public Guid AlbumId { get; set; }
            public Guid ImageId { get; set; }
            public string ImageName { get; set; }
            public string ImageDescription { get; set; }
            public List<string> Tags { get; set; }
            public string Extension { get; set; }
        }

        [BindProperty]
        public AddTagDTO AddTag { get; set; }

        public async Task<IActionResult> OnPostAddTagAsync(AddTagDTO tagDetails )
        {
            _ = await _mediator.Send(new AddImageTagCommand(tagDetails.AlbumId, tagDetails.ImageId, tagDetails.Tag));

            return RedirectToPage("/Album/Image/Index", new { albumId = tagDetails.AlbumId, imageId = tagDetails.ImageId });
        }

        [BindProperty]
        public Guid AlbumId { get; set; }

        [BindProperty]
        public Guid ImageId { get; set; }

        [BindProperty]
        public string Tag { get; set; }

        public class AddTagDTO
        {
            public Guid AlbumId { get; set; }
            public Guid ImageId { get; set; }
            public string Tag { get; set; }
        }
    }
}
