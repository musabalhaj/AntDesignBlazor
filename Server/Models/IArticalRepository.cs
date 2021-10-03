using Project.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Server.Models
{
    public interface IArticalRepository
    {
        Task<IEnumerable<Artical>> GetArticals();
        Task<Artical> GetArtical(int articalId);
        Task<Artical> AddArtical(Artical artical);
        Task<Artical> UpdateArtical(Artical artical);
        Task<Artical> DeleteArtical(int articalId);
        Task<IEnumerable<Artical>> Search(string title);
    }
}
