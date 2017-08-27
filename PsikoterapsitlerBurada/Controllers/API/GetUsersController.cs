using AutoMapper;
using PsikoterapsitlerBurada.DTOs;
using PsikoterapsitlerBurada.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace PsikoterapsitlerBurada.Controllers.API
{
    public class GetUsersController : ApiController
    {
        private ApplicationDbContext _context;

        public GetUsersController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpGet]
        public IEnumerable<UserDto> Users(string query)
        {
            var users = _context.Users
                .Where(u => u.UserName.StartsWith(query))
                .Select(Mapper.Map<UserDto>);

            return users;
        }
    }
}
