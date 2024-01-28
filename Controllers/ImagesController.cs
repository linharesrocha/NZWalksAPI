using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        // POST
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto imageUploadRequestDto) 
        {
            ValidateFileUpload(imageUploadRequestDto);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // convert to dto
            var imageDomainModel = new Image
            {
                File = imageUploadRequestDto.File,
                FileExtension = Path.GetExtension(imageUploadRequestDto.File.FileName),
                FileSizeInBytes = imageUploadRequestDto.File.Length,
                FileName = imageUploadRequestDto.FileName,
                FileDescription = imageUploadRequestDto.FileDescription,
            };

            await imageRepository.Upload(imageDomainModel);

            return Ok(imageDomainModel);

        }

        private void ValidateFileUpload(ImageUploadRequestDto imageUploadRequestDto)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(imageUploadRequestDto.File.FileName))) {
                ModelState.AddModelError("file", "Unsupported file extension");
            }

            if(imageUploadRequestDto.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size more than 10MB, please upload a smaller size file.");
            }
        }
    }
}
