using Blog.Data;
using Blog.Extensions;
using Blog.Models;
using Blog.ViewModels;
using BlogWeb.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Blog.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpGet("v1/categories")]
        public async Task<IActionResult> GetAsync(
            [FromServices] IMemoryCache cache,
            [FromServices] BlogDataContext context)
        {  
            try
            {
                var categories = cache.GetOrCreate("CategoriesCache", entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
                    return GetCategories(context);
                });

                //var categories = await context.Categories.AsNoTracking().ToListAsync();
                return Ok(new ResultViewModel<List<Category>>(categories));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Category>>("05XE04 - Falha interna no servidor"));

            }
        }

        private List<Category> GetCategories(BlogDataContext context) 
        {
            return context.Categories.AsNoTracking().ToList();
        }

        [HttpGet("v1/categories/{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id, [FromServices] BlogDataContext context)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == id);
                if (category == null)
                    return NotFound(new ResultViewModel<Category>("Conteudo não encontrado"));

                return Ok(new ResultViewModel<Category>(category));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Category>("Falha interna no servidor"));
            }
        }

        [HttpPost("v1/categories")]
        public async Task<IActionResult> PostAsync([FromBody] EditorCategoryViewModel categoryViewModel, [FromServices] BlogDataContext context)
        {
            if(!ModelState.IsValid)
               return BadRequest(new ResultViewModel<Category>(ModelState.GetErrors()));

            var category = new Category
            {
                Name = categoryViewModel.Name,
                Slug = categoryViewModel.Slug.ToLower(),
            };
            try
            {
                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Category>("05XE9 - Não foi Possível incluir a categoria"));

            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Category>("05XE10 - Falha interna no servidor"));
            }


            return Created($"v1/categories/{category.Id}", new ResultViewModel<Category>(category));
        }

        [HttpPut("v1/categories/{id:int}")]
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] EditorCategoryViewModel categoryNova, [FromServices] BlogDataContext context)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == id);
                if (category == null)
                    return NotFound(new ResultViewModel<Category>("Conteudo nao encontrado"));

                category.Name = categoryNova.Name;
                category.Slug = categoryNova.Slug;

                context.Categories.Update(category);
                await context.SaveChangesAsync();


                return Ok(new ResultViewModel<Category>(category));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Category>("05XE8 - Não foi Possível Alterar a categoria"));

            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultViewModel<Category>("05XE11 - Falha interna no servidor"));
            }
        }

        [HttpDelete("v1/categories/{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id, [FromServices] BlogDataContext context)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == id);
                if (category == null)
                    return NotFound(new ResultViewModel<Category>("Conteudo nao encontrado"));


                context.Categories.Remove(category);
                await context.SaveChangesAsync();


                return Ok(new ResultViewModel<Category>(category));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Category>("05XE7 - Não foi Possível Excluir a categoria"));

            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultViewModel<Category>("05XE12 - Falha interna no servidor") );
            }
        }
    }
}
