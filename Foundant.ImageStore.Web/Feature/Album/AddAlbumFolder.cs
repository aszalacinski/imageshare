using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Foundant
{
    public class AddAlbumFolder
    {
        public class AddAlbumFolderCommand : IRequest<string>
        {
            public Guid Id { get; set; }
            public AddAlbumFolderCommand(Guid id)
            {
                Id = id;
            }
        }

        public class AddAlbumFolderCommandHandler : IRequestHandler<AddAlbumFolderCommand, string>
        {
            public async Task<string> Handle(AddAlbumFolderCommand command, CancellationToken cancellationToken)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\albums", command.Id.ToString());
                Directory.CreateDirectory(filePath);
                if (Directory.Exists(filePath))
                {
                    return filePath;
                }

                throw new Exception("Directory could not be created");
            }
        }
    }
}
