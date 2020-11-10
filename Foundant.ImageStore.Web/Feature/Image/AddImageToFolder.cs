using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Foundant
{
    public class AddImageToFolder
    {
        public class AddImageToFolderCommand : IRequest<string>
        {
            public Guid AlbumId { get; set; }
            public Guid ImageId { get; set; }
            public IFormFile File { get; set; }

            public AddImageToFolderCommand(Guid albumId, Guid imageId, IFormFile file)
            {
                AlbumId = albumId;
                ImageId = imageId;
                File = file;
            }
        }

        public class AddImageToFolderCommandHandler : IRequestHandler<AddImageToFolderCommand, string>
        {
            public async Task<string> Handle(AddImageToFolderCommand command, CancellationToken cancellationToken)
            {

                var filePath = string.Empty;
                if(command.File.Length > 0)
                {
                    // get file extension
                    var extension = Path.GetExtension(command.File.FileName);

                    filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\albums", command.AlbumId.ToString(), $"{command.ImageId}{extension}"); 
                    using (var stream = File.Create(filePath))
                    {
                        await command.File.CopyToAsync(stream);
                    }
                }

                if(File.Exists(filePath))
                {
                    return filePath;
                }

                throw new Exception("Image could not be uploaded");
            }
        }
    }
}
