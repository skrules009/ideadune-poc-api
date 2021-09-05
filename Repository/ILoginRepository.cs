using ideadune_pos.Entities;
using ideadune_pos.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ideadune_pos.Repository
{
    public interface ILoginRepository
    {
        Task<List<Login>> GetUsers();
        Task<bool> SaveAccount(Login user);
        Task<bool> Login(Login user);
    }
}
