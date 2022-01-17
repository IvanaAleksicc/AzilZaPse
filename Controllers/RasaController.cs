using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;

namespace WEB_PROJEKAT.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class RasaController : ControllerBase
    {
        public AzilContext Context {get;set;}

        public RasaController(AzilContext context)
        {
            Context=context;
        }

 [Route("DodajRasu")]
        [HttpPost]
        public async Task<ActionResult> DodajRasu([FromBody]Rasa r)
        {
            
             if(string.IsNullOrWhiteSpace(r.Naziv)|| r.Naziv.Length>50)
            {
                return BadRequest("Ime je predugacko!");
            }
            
            try
            {
                Context.Rase.Add(r);
                await Context.SaveChangesAsync();
                return Ok("Uspesno unet azil!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
           
        }
 [Route("PrikaziRase")]
        [HttpGet]
        public async Task<ActionResult> Preuzmi()
        {
            try
            {
                return Ok(await Context.Rase.Select(p => new { p.ID, p.Naziv}).ToListAsync());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
         [Route("PreuzmiID/{Naziv}")]
    [HttpGet]

    public async Task<ActionResult> PreuzmiID(string Naziv)
    {
        var rasa= await Context.Rase
           .Where(p=>p.Naziv==Naziv).FirstOrDefaultAsync();

            
        if(rasa!=null)
            return Ok ( rasa.ID );
        else return BadRequest("Nije naso rasu");
    }

        [Route("ProveirRasu/{id}")]
        [HttpDelete]
        public async Task<ActionResult> IzbrisiRasu(string ime)
        {
           
            try
            {
               var p=await Context.Rase.Where(p=>p.Naziv==ime).FirstOrDefaultAsync();
               
              
               Context.Rase.Remove(p);
               await Context.SaveChangesAsync();
                return Ok("Uspesno izbrisana rasa!");
               
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            
            }}

    }
}