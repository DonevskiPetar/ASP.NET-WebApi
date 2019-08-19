using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiApp.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public static List<User> Users = new List<User>()
        {
            new User ("Petar" , "Donevski" , 15),
            new User ("Dario" , "Kostov" , 25),
            new User ("Martin" , "Petrovski" , 25),
            new User ("Dragana" , "Siskovska" , 20),
            new User ("Goran" , "Todorovski" , 28),
            new User ("Viktor" , "Mijalcev" , 29)

        };


        [HttpGet]
        public ActionResult<List<User>> Get()
        {
            return Users;
        }

        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            try
            {
                if(Users[id - 1].Age < 18)
                {
                    return NotFound($"The user is under 18");
                }
                return Users[id - 1];
            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound($"The user id {id} is not found");
            }
            catch(Exception ex)
            {
                return BadRequest($"Broken {ex.Message}");
            }

        }



    }
}