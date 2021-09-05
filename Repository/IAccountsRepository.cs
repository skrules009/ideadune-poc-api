using ideadune_pos.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ideadune_pos.Repository
{
    public interface IAccountsRepository
    {
        Task<List<BOAccounts>> GetAccounts();
        Task<List<BONote>> GetNotes(int accountId);
        Task<bool> AddNote(NoteWithMontion boNote);
        Task<bool> AddComment(BOComment comment);
    }
}
