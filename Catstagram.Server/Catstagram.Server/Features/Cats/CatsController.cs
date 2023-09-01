namespace Catstagram.Server.Features.Cats
{
    using Catstagram.Server.Features.Cats.Models;
    using Catstagram.Server.Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Authorize]
    public class CatsController : ApiController
    {
        private readonly ICatService catService;

        public CatsController(ICatService catService)
        {
            this.catService = catService;
        }

        [HttpGet]
        public async Task<IEnumerable<CatListingServiceModel>> Mine()
        {
            var userId = this.User.GetId();
            var cats = await this.catService.ByUser(userId);

            return cats;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<CatDetailsServiceModel>> Details(int id)
        {
            var cat = await this.catService.Details(id);

            return cat;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateCatRequestModel model)
        {
            try
            {
                var userId = this.User.GetId();

                var id = await this.catService.Create(model.ImageUrl, model.Description, userId);

                return Created(nameof(this.Create), id);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}
