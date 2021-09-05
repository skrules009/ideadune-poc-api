using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ideadune_pos.Entities;
using ideadune_pos.Model;
using ideadune_pos.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ideadune_pos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        ILoginRepository loginRepository;
        public HomeController(ILoginRepository _loginRepository)
        {
            loginRepository = _loginRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Welcome to API<br/> API is running now..");
        }
        [HttpGet]
        [Route("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var accounts = await loginRepository.GetUsers();
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
       
        [HttpPost]
        [Route("AddUser")]
        public async Task<IActionResult> SaveAccount(Login user)
        {
            try
            {
                bool isSaved = await loginRepository.SaveAccount(user);              
                return Ok(isSaved);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(Login user)
        {
            try
            {
                bool isSaved = await loginRepository.Login(user);
                return Ok(isSaved);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
    }
}
