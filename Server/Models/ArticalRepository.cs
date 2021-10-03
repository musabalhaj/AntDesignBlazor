using Microsoft.EntityFrameworkCore;
using Project.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Server.Models
{
    public class ArticalRepository : IArticalRepository
    {
        private readonly ApplicationDbContext context;

        public ArticalRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<Artical> AddArtical(Artical artical)
        {
            var result = await context.Articals.AddAsync(artical);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Artical> DeleteArtical(int articalId)
        {
            var result = await context.Articals.FirstOrDefaultAsync(a => a.Id == articalId);
            if (result != null)
            {
                context.Articals.Remove(result);
                await context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Artical> GetArtical(int articalId)
        {
            return await context.Articals
                .Include(a => a.Category)
                .FirstOrDefaultAsync(a => a.Id == articalId);

        }

        public async Task<IEnumerable<Artical>> GetArticals()
        {
            return await context.Articals
                .Include(a => a.Category)
                .OrderByDescending(a => a.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<Artical>> Search(string title)
        {
            IQueryable<Artical> query = context.Articals;
            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(e => e.Title.Contains(title));
            }
            return await query.ToListAsync();
        }

        public async Task<Artical> UpdateArtical(Artical artical)
        {
            var result = await context.Articals.FirstOrDefaultAsync(e => e.Id == artical.Id);
            if (result != null)
            {
                result.Title = artical.Title;
                result.Body = artical.Body;
                result.Author = artical.Author;
                result.CategoryId = artical.CategoryId;
                result.Image = artical.Image;
                await context.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}
