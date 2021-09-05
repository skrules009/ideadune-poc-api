using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ideadune_pos.Model;
using ideadune_pos.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ideadune_pos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        IAccountsRepository accountsRepository;
        public AccountsController(IAccountsRepository _accountsRepository)
        {
            accountsRepository = _accountsRepository;
        }
        [HttpGet]
        [Route("GetAccounts")]
        public async Task<IActionResult> GetAccounts()
        {
            try
            {
                var accounts = await accountsRepository.GetAccounts();
                if (accounts == null)
                {
                    return NotFound();
                }
                return Ok(accounts);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
        [HttpGet]
        [Route("GetNotes")]
        public async Task<IActionResult> GetNotes(int accountId)
        {
            try
            {
                if (accountId <1)
                {
                    return NotFound();
                }
                // int accountId = 1;
                var notes = await accountsRepository.GetNotes(accountId);
                if (notes == null)
                {
                    return NotFound();
                }
                return Ok(notes);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }
        [HttpPost]
        [Route("addNote")]
        public async Task<IActionResult> AddNote([FromBody]NoteWithMontion note)
        {
            try
            {
                var isAdded = await accountsRepository.AddNote(note);
                if (isAdded == null)
                {
                    return NotFound();
                }
                return Ok(isAdded);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }
        [HttpPost]
        [Route("addComment")]
        public async Task<IActionResult> AddComment(BOComment comment)
        {
            try
            {
                var isAdded = await accountsRepository.AddComment(comment);
                if (isAdded == null)
                {
                    return NotFound();
                }
                return Ok(isAdded);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }
    }
}
