using ideadune_pos.Entities;
using ideadune_pos.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ideadune_pos.Repository
{
    public class LoginRepository : ILoginRepository
    {
        dbEntity db;
        public LoginRepository(dbEntity _db)
        {
            db = _db;
        }
        public async Task<List<Login>> GetUsers()
        {
            if (db != null)
            {
                return await (from p in db.login
                              select new Login
                              {
                                  id = p.id,
                                  lastName = p.lastName,
                                  firstName = p.firstName,
                                  userId = p.userId,
                                  createDt = p.createDt,
                                  role = p.role
                              }).ToListAsync();
            }
            return null;
        }

        public async Task<bool> Login(Login user)
        {
            if (db != null)
            {
                return db.login.Any(x => x.userId == user.userId && x.password==user.password); 
               
            }
            return false;
        }

        public async Task<bool> SaveAccount(Login user)
        {
            if (db != null)
            {
                user.createDt = System.DateTime.Now;
                await db.login.AddAsync(user);
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<Login>> GetAccounts()
        {
            if (db != null)
            {
                return await (from p in db.login
                              select new Login
                              {
                                  id = p.id,
                                  lastName = p.lastName,
                                  firstName = p.firstName,
                                  userId = p.userId,
                                  createDt = p.createDt,
                                  role = p.role
                              }).ToListAsync();
            }
            return null;
        }

    }
}
