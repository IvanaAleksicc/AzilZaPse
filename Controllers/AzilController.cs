using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;

namespace WEB_PROJEKAT.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class AzilController : ControllerBase
    {
        public AzilContext Context {get;set;}

        public AzilController(AzilContext context)
        {
            Context=context;
        }

 [Route("PrikaziPseNaOsnovuImenaAzila/{ime}")]
        [HttpGet]
      
        public async Task<ActionResult> PrikaziPse3( string ime)
        {

            try
            {
            
                var azil =await  Context.Azili.Include(p => p.psi).Where(p => p.Naziv == ime).FirstOrDefaultAsync();
                
                   return Ok
                 (
                     
                          azil.psi.Select(q =>
                         new
                         {
                             Ime = q.Ime,
                             Starost = q.Starost,
                             Pol = q.Pol,
                             Tezina=q.Tezina,
                             Velicina = q.Velicina,
                             Udomljen=q.Udomljen
                         }) .ToList()      
                 );   
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


  

        [Route("PrikaziRaseIzAzila/{id}")]
        [HttpGet]
      
        public async Task<ActionResult> PrikaziRase(int id )
        {

           try
            {
            
            
                 var azil =await  Context.Azili.Include(p => p.psi).ThenInclude(p=>p.rasa).Where(p => p.ID == id).FirstOrDefaultAsync();
                 var azil1= azil.psi.Select(q => q.rasa).ToList();
                 var rase=azil1.Distinct().ToList();
                
                   return Ok
                 (
                      rase. Select(p=>
                          new
                          {
                              naziv=p.Naziv
                             

                      }).ToList()
                 );

                 
            
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

  
      
        [Route("PrikaziPse/{idazila}")]
        [HttpGet]
      
        public async Task<ActionResult> PrikaziPse( int idazila)
        {

            try
            {
            
                var azil =await  Context.Azili.Include(p => p.psi).Where(p => p.ID == idazila).FirstOrDefaultAsync();
                
                   return Ok
                 (
                     
                          azil.psi.Select(q =>
                         new
                         {
                             Ime = q.Ime,
                             Starost = q.Starost,
                             Pol = q.Pol,
                             Tezina=q.Tezina,
                             Velicina = q.Velicina,
                             Udomljen=q.Udomljen
                            
                         }) .ToList()      
                 );   
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("PrikaziPse2/{idazila}")]
        [HttpGet]

     
        public async Task<ActionResult> PrikaziPse2( int idazila)
        {
           
            try
            {
                var azil = await Context.Azili.FindAsync(idazila);
               
                var litapasa = await Context.Azili
                    .Include(p => p.psi)
                    .Where(p => p.ID == idazila)
                    .Select(p =>
        new
        {
            Naziv = p.Naziv,
            Psi = p.psi.Select(q =>
          new
          {
              Ime = q.Ime,
              Rasa = q.rasa,
              STarost = q.Starost,
              Tezina = q.Tezina,
              Pol = q.Pol,
             Rodjendan = q.datum_rodjenja,
              Velicina = q.Velicina
          })
        }).ToListAsync();

                return Ok(litapasa);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
[Route("PrikaziUdomitelje/{idazila}")]
        [HttpGet]

        public async Task<ActionResult> PrikaziUdomitelje( int idazila)
        {
          
            try
            {
                var azil =await  Context.Azili.Include(p => p.udomitelji).ThenInclude(p=>p.pas).Where(p => p.ID == idazila).FirstOrDefaultAsync();

            return Ok(

                azil.udomitelji.ToList()
                
                    
            );

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
        var azil= await Context.Azili
           .Where(p=>p.Naziv==Naziv).FirstOrDefaultAsync();

            
        if(azil!=null)
            return Ok ( azil.ID );
        else return BadRequest("Nije naso azil");
    }
        [Route("PrikaziAzile")]
        [HttpGet]
        public async Task<ActionResult> Preuzmi()
        {
            try
            {
                return Ok(await Context.Azili.Select(p => new { p.ID, p.Naziv,p.Grad,p.BrojPasa,p.Kapacitet }).ToListAsync());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
[Route("PrikaziAzile2")]
        [HttpGet]
        public async Task<ActionResult> Preuzmi2()
        {
            try
            {
               var gradovi=await Context.Azili.Select(p => p.Grad).ToListAsync();
               var gradoviStr = gradovi.Distinct().ToList();

               var gradoviIAzili= Context.Azili.AsEnumerable().GroupBy(p => p.Grad).ToList();
            
              return Ok(gradoviIAzili);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

         [Route("PronadjiAzil/{grad}")]
        [HttpGet]
        public async Task<ActionResult> Nadji(string grad)
        {
            try
            {
               return Ok(await Context.Azili.Where(p=>p.Grad==grad).ToListAsync());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [Route("DodajAzil")]
        [HttpPost]
        public async Task<ActionResult> DodajAzil([FromBody]Azil azil)
        {
            
             if(string.IsNullOrWhiteSpace(azil.Naziv)|| azil.Naziv.Length>50)
            {
                return BadRequest("Ime je predugacko!");
            }
            if(string.IsNullOrWhiteSpace(azil.Grad))
            {
                return BadRequest("Ime je predugacko!");
            }
             if(string.IsNullOrWhiteSpace(azil.Ulica))
            {
                return BadRequest("Ime je predugacko!");
            }
            if(azil.PostanskiBroj<0)
            {
                return BadRequest("Ime je predugacko!");
            }
            if(azil.BrojPasa<0)
            {
                return BadRequest("Ime je predugacko!");
            }
             if(azil.Kapacitet<0)
            {
                return BadRequest("Ime je predugacko!");
            }
            try
            {
                Context.Azili.Add(azil);
                await Context.SaveChangesAsync();
                return Ok("Uspesno unet azil!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
           
        }
        [Route("VratiKapacitet/{i}/{id}")]
        [HttpPut]
        public async Task<ActionResult> VratiKapacitet(int i,int id)
        {
            
            
            try
            {
              var azil=await Context.Azili.Where(p=>p.ID==id).FirstOrDefaultAsync();
              if(azil==null)
               return BadRequest("Nepostojece azil!");
               else{
                if(azil.BrojPasa<azil.Kapacitet){
                    azil.BrojPasa=azil.BrojPasa+i;
                    if(azil.BrojPasa<azil.Kapacitet)
                         await  Context.SaveChangesAsync();
                 

                    else
                    return BadRequest(0);
                }
                return Ok("Uspesno promenjena");
               }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("IzbrisiAzil/{id}")]
        [HttpDelete]
        public async Task<ActionResult> IzbrisiAzil(int id)
        {
            if(id<=0)
            {
                return BadRequest("Pogresan ID!");
            }
            try
            {
               var p=await Context.Azili.FindAsync(id);
               Context.Azili.Remove(p);
               await Context.SaveChangesAsync();
                return Ok("Uspesno izbrisan azil!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            
            }}

    }
}