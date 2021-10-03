using Project.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Client.Services
{
    public interface IArticalService
    {
        Task<IEnumerable<Artical>> GetArticals();

        Task<Artical> GetArtical(int id);

        Task<Artical> CreateArtical(Artical newArtical);

        Task<Artical> UpdateArtical(Artical updatedArtical);

        Task DeleteArtical(int Id);
    }
}
