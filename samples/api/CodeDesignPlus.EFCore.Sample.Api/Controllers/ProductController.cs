using AutoMapper;
using CodeDesignPlus.Core.Models.Pager;
using CodeDesignPlus.EfCore.Sample.Api.Abstractions;
using CodeDesignPlus.EFCore.Extensions;
using CodeDesignPlus.EFCore.Sample.Api.Dto;
using CodeDesignPlus.EFCore.Sample.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeDesignPlus.EFCore.Sample.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository repository;
        private readonly IMapper mapper;

        public ProductController(IProductRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("[action]/{currentPage}/{pageSize}")]
        public async Task<IActionResult> Page(int currentPage, int pageSize)
        {
            if (currentPage < 1 || pageSize < 1)
                return NotFound();

            var entities = await this.repository.GetEntity<Category>().ToPageAsync(currentPage, pageSize);

            var data = this.mapper.Map<Pager<ProductDto>>(entities);

            return Ok(data);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Get()
        {
            var entites = await this.repository.GetEntity<Product>().Include(x => x.Category).ToListAsync();

            var data = this.mapper.Map<List<ProductDto>>(entites);

            return Ok(data);
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Get(long id)
        {
            if (id < 1)
                return NotFound();

            var entity = await this.repository.GetEntity<Product>().Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);

            var data = this.mapper.Map<ProductDto>(entity);

            return Ok(data);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Post([FromBody] ProductDto product)
        {
            var entity = this.mapper.Map<Product>(product);

            var id = await this.repository.CreateAsync(entity);

            product.Id = id;

            return Ok(product);
        }

        [HttpPut]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] ProductDto Product)
        {
            if (id < 1)
                return NotFound();

            var entity = this.mapper.Map<Product>(Product);

            var success = await this.repository.UpdateAsync(id, entity);

            return Ok(success);
        }

        [HttpPut]
        [Route("[action]/{id}")]
        public async Task<IActionResult> ChangeState(long id, bool state)
        {
            if (id < 1)
                return NotFound();

            var success = await this.repository.ChangeStateAsync<Product>(id, state);

            return Ok(success);
        }

        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            if (id < 1)
                return NotFound();

            var success = await this.repository.DeleteAsync(id);

            return Ok(success);
        }
    }
}
