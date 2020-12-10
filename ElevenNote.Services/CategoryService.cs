using ElevenNote.Data;
using ElevenNote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Services
{
    public class CategoryService
    {
        public bool CreateCatagory(CategoryCreate model)//post
        {
            var entity =
                new Category()
                {
                    Name = model.Name
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Categorys.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<CategoryListItem> GetCategorys()//get
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Categorys.Select(
                            e =>
                                new CategoryListItem
                                {
                                    Id = e.Id,
                                    Name = e.Name
                                });

                return query.ToArray();
            }
        }
        public CategoryDetail GetCategoryById(int id)//get
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Categorys
                        .Single(e => e.Id == id);
                return
                    new CategoryDetail
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                    };
            }
        }
        public bool UpdateCategory(CategoryEdit model)//put
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Categorys
                        .Single(e => e.Id == model.Id);

                entity.Id = model.Id;
                entity.Name = model.Name;
                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteCategory(int categoryId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Categorys
                        .Single(e => e.Id == categoryId);

                ctx.Categorys.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
