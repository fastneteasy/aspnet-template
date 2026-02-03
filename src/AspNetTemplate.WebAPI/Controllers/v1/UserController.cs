using AspNetTemplate.DAL.Models;
using AspNetTemplate.DAL.Repositories;
using AspNetTemplate.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetTemplate.WebAPI.Controllers.v1
{
    [Route("api/v1/users")]
    [ApiController]
    public class UserController(
        ILoggerFactory loggerFactory,
        AspNetTemplateDbContext dbContext)
        : BaseController(loggerFactory)
    {

        [HttpGet]
        public async Task<PageCollection<ListUserModel>> Page([FromQuery] PageParameter query)
        {
            var page = await dbContext.Users
                .OrderByDescending(x => x.id)
                .ToPageAsync(query, x => new ListUserModel(x));

            var ids = page.Collection.Select(x => x.Id);

            page.Collection = page.Collection.ToList();

            return page;
        }

        [HttpPut("{id}/disable")]
        public async Task Disable(long id)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(x => x.id == id) ?? throw new ArgumentException();
            user.disabled = true;

            await dbContext.SaveChangesAsync();
        }

        [HttpPut("{id}/enable")]
        public async Task Enable(long id)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(x => x.id == id) ?? throw new ArgumentException();
            user.disabled = false;

            await dbContext.SaveChangesAsync();
        }
    }
}
