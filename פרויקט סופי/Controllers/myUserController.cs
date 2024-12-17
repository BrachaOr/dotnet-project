using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using שיעור_3.Interfaces;
using שיעור_3.Services;
using שיעור_3.Models;


namespace שיעור_3.Controllers{

[ApiController]
[Route("[controller]")]
public class myUserController : ControllerBase
{
        private IUserServices UserService;
        public myUserController(IUserServices UserService)
        {
            this.UserService = UserService;
        }

        [HttpGet]
      
        public ActionResult<List<MyUser>> GetAll()
        {
            return UserService.GetAll();
        }
       

        [HttpGet("{id}")]
        public ActionResult<MyUser> Get(int id)
        {
            var user = UserService.Get(id);

            if (user == null)
                return NotFound();

            return user;
        }

        [HttpPost] 
        public IActionResult Create(MyUser user)
        {
            UserService.Add(user);
            return CreatedAtAction(nameof(Create), new {id=user.Id}, user);

        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, MyUser user)
        {
            if (id != user.Id)
                return BadRequest();

            var existingUser = UserService.Get(id);
            if (existingUser is null)
                return  NotFound();

            UserService.Update(user);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = UserService.Get(id);
            if (user is null)
                return  NotFound();

            UserService.Delete(id);

            return Content(UserService.Count.ToString());
        }
        [HttpPost]
        [Route("/login")]
        public ActionResult<objectToReturn> login([FromBody] MyUser user){
           int UserExistID = UserService.ExistUser(user.Name, user.Password);
           Console.WriteLine(UserExistID);
           if(UserExistID==-1)
            return Unauthorized();
              var claims =new List<Claim>{ };
            if(user.Password=="123")
              claims.Add(new Claim("type","Admin"));
            else
              claims.Add(new Claim("type","User")); 
            claims.Add(new Claim("id",UserExistID.ToString()));
            var token=clothesTokenServices.GetToken(claims);
            return new OkObjectResult(new{Id=UserExistID,token=clothesTokenServices.WriteToken(token)});
        }
    

}}

public class objectToReturn{
    public int id{get; set;}
    public string token{get; set;}
}
