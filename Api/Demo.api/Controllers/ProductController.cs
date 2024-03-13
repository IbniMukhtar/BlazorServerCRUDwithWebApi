using Demo.api.Data;
using Demo.api.Dmain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DemoDContext _demoDbcontext;
        public ProductController(DemoDContext demoDbContext) => _demoDbcontext = demoDbContext;

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            return _demoDbcontext.Products;
        }

        [HttpGet("{id}")]
        async public Task<ActionResult<Product?>> GetById(int id)
        {
            return await _demoDbcontext.Products.Where(x => x.Id == id).SingleOrDefaultAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Product product)
        {
            await _demoDbcontext.Products.AddAsync(product);
            await _demoDbcontext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpPut]
        public async Task<ActionResult> Update(Product product)
        {
            _demoDbcontext.Products.Update(product);
            await _demoDbcontext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var productGetByIdResult = await GetById(id);
            if (productGetByIdResult.Value is null)
            {
                return NotFound();
            }
            _demoDbcontext.Products.Remove(productGetByIdResult.Value);
            await _demoDbcontext.SaveChangesAsync();
            return Ok();
        }
    }
}
