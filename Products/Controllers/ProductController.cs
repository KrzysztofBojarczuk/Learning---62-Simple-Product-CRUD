using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Products.Data;
using Products.Dtos;
using Products.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DataContext _ctx;
        private readonly IMapper _mapper;

        public ProductController(DataContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetProduct()
        {
            var product = await _ctx.Products.ToListAsync();
            var productGet = _mapper.Map<List<ProductGetDto>>(product);
            return Ok(productGet);
        }
        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _ctx.Products.FirstOrDefaultAsync(h => h.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            var productGet = _mapper.Map<ProductGetDto>(product);
            return Ok(productGet);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDto product)
        {
            var domainProduct = _mapper.Map<Product>(product);

            _ctx.Products.Add(domainProduct);

            await _ctx.SaveChangesAsync();

            var productGet = _mapper.Map<ProductGetDto>(domainProduct);

            return CreatedAtAction(nameof(GetProductById), new {id = domainProduct.Id}, productGet);
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductCreateDto update, int id)
        {
            var toUpdate = _mapper.Map<Product>(update);
            toUpdate.Id = id;

            _ctx.Products.Update(toUpdate);

            await _ctx.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _ctx.Products.FirstOrDefaultAsync(h => h.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            _ctx.Products.Remove(product);
            await _ctx.SaveChangesAsync();
            return NoContent();
        }
    }
}
