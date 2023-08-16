namespace Catstagram.Server.Features.Cats
{
    using Catstagram.Server.Data;
    using Catstagram.Server.Data.Models;
    using Catstagram.Server.Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class CatsController : ApiController
    {
        private readonly CatstagramDbContext data;

        public CatsController(CatstagramDbContext data)
        {
            this.data = data;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateCatRequestModel model)
        {
            try
            {
                var userId = this.User.GetId();

                var ca = new Cat
                {
                    Description = model.Description,
                    ImageUrl = model.ImageUrl,
                    UserId = userId
                };

                this.data.Add(ca);

                await this.data.SaveChangesAsync();

                return Created(nameof(this.Create), ca.Id);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}
