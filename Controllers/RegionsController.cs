using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            var regions = new List<Region>
            {
                new Region
                {
                    Id = Guid.NewGuid(),
                    Name = "Auckland Region",
                    Code = "AUK",
                    RegionImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1c/Auckland_in_New_Zealand.svg/1200px-Auckland_in_New_Zealand.svg.png",
                },
                new Region
                {
                    Id = Guid.NewGuid(),
                    Name = "Wellignton Region",
                    Code = "WLG",
                    RegionImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1c/Auckland_in_New_Zealand.svg/1200px-Auckland_in_New_Zealand.svg.png",
                }
            };
            return Ok(regions);
        }
    }
}
