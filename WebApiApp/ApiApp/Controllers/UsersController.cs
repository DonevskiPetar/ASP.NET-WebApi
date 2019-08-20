using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ApiApp.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiApp.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        public class UserController : ControllerBase
        {
            public static List<User> Users = new List<User>()
        {
            new User ("Petar" , "Donevski" , 32),
            new User ("Dario" , "Kostov" , 25),
            new User ("Martin" , "Petrovski" , 25),
            new User ("Dragan" , "Todorovski" , 15),
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
                    return Users[id - 1];
                }
                catch (ArgumentOutOfRangeException)
                {
                    return NotFound($"The user id {id} is not found");
                }
                catch (Exception ex)
                {
                    return BadRequest($"Error: {ex.Message}");
                }
            }

            [HttpGet("{id}/val")]
            public ActionResult<string> ValidateUser(int id)
            {
                try
                {
                    if (Users[id - 1].Age < 18)
                    {
                        return $"The user {Users[id - 1].FirstName} {Users[id - 1].LastName} is under 18";
                    }
                    return $"The user {Users[id - 1].FirstName} {Users[id - 1].LastName} is above 18";
                }
                catch (ArgumentOutOfRangeException)
                {
                    return NotFound($"The user id {id} is not found");
                }
                catch (Exception ex)
                {
                    return BadRequest($"Error: {ex.Message}");
                }
            }

            [HttpPost]
            public IActionResult Post()
            {
                string body;
                using (StreamReader sr = new StreamReader(Request.Body))
                {
                    body = sr.ReadToEnd();
                }
                User user = JsonConvert.DeserializeObject<User>(body);
                Users.Add(user);
                return Ok($"User with id {Users.Count} added!");
            }

        }
    }
