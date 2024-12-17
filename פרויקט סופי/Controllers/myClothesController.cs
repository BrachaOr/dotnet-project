using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using שיעור_3.Interfaces;
using שיעור_3.Services;
using שיעור_3.Models;
using Microsoft.AspNetCore.Authorization;

namespace שיעור_3.Controllers{

[ApiController]
[Route("[controller]")]
public class myClothesController : ControllerBase
{
        private IClothesServices clothesService;
        public myClothesController(IClothesServices clothesService)
        {
            this.clothesService = clothesService;
        }

        [HttpGet]
         [Authorize(Policy ="User")]
        public ActionResult<List<MyClothes>> GetAll()
        {
            if( User.FindFirst("type")?.Value == "Admin")
                return clothesService.GetAll(-1);
            return clothesService.GetAll(int.Parse(User.FindFirst("id")?.Value!));
        }

        [HttpGet("{id}")]
        [Authorize(Policy ="User")]
        public ActionResult<MyClothes> Get(int id)
        {
            var clothes = clothesService.Get(id);

            if (clothes == null)
                return NotFound();

            return clothes;
        }

        [HttpPost] 
        [Authorize(Policy ="User")]
        public IActionResult Create(MyClothes clothes)
        {
            clothesService.Add(clothes);
            return CreatedAtAction(nameof(Create), new {id=clothes.Id}, clothes);

        }

        [HttpPut("{id}")]
        [Authorize(Policy ="User")]
        public IActionResult Update(int id, MyClothes clothes)
        {
            if (id != clothes.Id)
                return BadRequest();

            var existingClothes = clothesService.Get(id);
            if (existingClothes is null)
                return  NotFound();

            clothesService.Update(clothes);

            return NoContent();
        }

        [HttpDelete("{id}")]
         [Authorize(Policy ="User")]
        public IActionResult Delete(int id)
        {
            var clothes = clothesService.Get(id);
            if (clothes is null)
                return  NotFound();

            clothesService.Delete(id);

            return Content(clothesService.Count.ToString());
        }

}}