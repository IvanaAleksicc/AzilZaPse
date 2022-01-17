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

    public class UdomiteljController : ControllerBase
    {
        public AzilContext Context {get;set;}

        public UdomiteljController(AzilContext context)
        {
            Context=context;
        }

         [Route("DodajUdomitelja/{Ime}/{Grad}/{Prezime}/{Ulica}/{PostanskiBroj}")]
        [HttpPost]
        public async Task<ActionResult> DodajUdomitelja(string Ime,string Grad,string Prezime,string Ulica,int PostanskiBroj)
        {
            
             if(string.IsNullOrWhiteSpace(Ime)|| Ime.Length>50)
            {
                return BadRequest("Ime je predugacko!");
            }
            if(string.IsNullOrWhiteSpace(Grad))
            {
                return BadRequest("Ime je predugacko!");
            }
             if(string.IsNullOrWhiteSpace(Prezime)|| Prezime.Length>50)
            {
                return BadRequest("Ime je predugacko!");
            }
             if(string.IsNullOrWhiteSpace(Ulica))
            {
                return BadRequest("Ime je predugacko!");
            }
            if(PostanskiBroj<0)
            {
                return BadRequest("Ime je predugacko!");
            }

       
                var u = await Context.Udomitelji.Where(p => p.Ime == Ime && p.Grad == Grad && p.Prezime == Prezime && p.Ulica == Ulica && p.PostanskiBroj == PostanskiBroj).FirstOrDefaultAsync();
                if (u != null)
                  return BadRequest("Korisnik moze usvojiti samo jednog psa");
                       try
            {
                
                    var udomitelj = new Udomitelj
                    {
                        Ime = Ime,
                        Grad = Grad,
                        Prezime = Prezime,
                        Ulica = Ulica,
                        PostanskiBroj = PostanskiBroj

                    };
                    Context.Udomitelji.Add(udomitelj);
                    await Context.SaveChangesAsync();
                    return Ok(udomitelj.ID);
                }
                
            
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
           
        }


      [Route("PreuzmiID/{Ime}/{Grad}/{Prezime}/{Ulica}/{PostanskiBroj}")]
      [HttpGet]

    public async Task<ActionResult> PreuzmiID(string Ime,string Grad,string Prezime,string Ulica,int PostanskiBroj)
    {
        var udomitelj= await Context.Udomitelji
           .Where(p=>p.Ime==Ime && p.Prezime==Prezime && p.Grad==Grad && p.Ulica==Ulica && p.PostanskiBroj==PostanskiBroj).FirstOrDefaultAsync();

            
        if(udomitelj!=null)
            return Ok ( udomitelj.ID );
        else return BadRequest("Nije naso udomitelja");
    }


       

        [Route("DodeliAzil/{idudomitelja}/{idazila}")]
        [HttpPost]
        public async Task<ActionResult> DodeliAzil(int idudomitelja,int idazila)
        {
            
           
            if(idudomitelja<=0)
            {
                return BadRequest("Nepostojeci ID");
            }
             if(idazila<=0)
            {
                return BadRequest("Nepostojeci ID");
            }
           
            try
            {
                var p= Context.Udomitelji.Where(p => p.ID == idudomitelja).FirstOrDefault();
                var azil=Context.Azili.Where(q => q.ID == idazila).FirstOrDefault();
                if(p!=null  && azil!=null)
                {
                    p.azil=azil;

                    await Context.SaveChangesAsync();
                    return Ok("Uspesno dodat azil!");
                }
                else
                return BadRequest("Nije pronadjen pas ili azil!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
           
        }
         [Route("DodeliPsa/{idudomitelja}/{idpsa}")]
        [HttpPost]
        public async Task<ActionResult> DodeliPsa(int idudomitelja,int idpsa)
        {
            
           
            if(idudomitelja<=0)
            {
                return BadRequest("Nepostojeci ID!");
            }
             if(idpsa<=0)
            {
                return BadRequest("Nepostojeci ID!");
            }
           
            try
            {
                var p= Context.Udomitelji.Where(p => p.ID == idudomitelja).FirstOrDefault();
                var pas=Context.Psi.Where(q => q.ID == idpsa).FirstOrDefault();
                var azil=Context.Azili.Include(q=>q.psi).Where(q=>q.ID==pas.ID).FirstOrDefault();
                if(p!=null  && pas!=null)
                {
                    p.pas=pas;

                    await Context.SaveChangesAsync();
                    return Ok("Uspesno dodat pas!");
                }
                else
                return BadRequest("Nije pronadjen pas ili udomitelj!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
           
        }
        [Route("DodeliPsaPrekoInfo/{idudomitelja}/{Ime}/{Pol}/{Tezina}/{Starost}/{Velicina}")]
        [HttpPost]
        public async Task<ActionResult> DodeliPsaPrekoInfo(int idudomitelja,string Ime,char Pol,int Tezina,int Starost,Veliko Velicina)
        {
            
           
        
            try
            {
                var p= Context.Udomitelji.Where(p => p.ID == idudomitelja).FirstOrDefault();
               if(p!=null && p.pas==null)
               {
                var pas=Context.Psi.Include(q=>q.azil).Where(q => q.Ime == Ime && q.Pol==Pol && q.Tezina==q.Tezina && q.Starost==Starost && q.Velicina==Velicina).FirstOrDefault();
                
        
                if(   pas!=null && pas.Udomljen==false)
                {
                    pas.Udomljen=true;
                    p.pas=pas;
                    p.azil=pas.azil;
                    await Context.SaveChangesAsync();

                    return Ok(pas.azil);
                }
                else
                return BadRequest(pas);
               }
               else return BadRequest("Vec ste udomili psa!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
           
        }
 
         [Route("IzbrisiUdomitelja/{id}")]
        [HttpDelete]
        public async Task<ActionResult> IzbrisiUdomitelja(int id)
        {
            if(id<=0)
            {
                return BadRequest("Pogresan ID!");
            }
            try
            {
               var p=await Context.Udomitelji.FindAsync(id);
               Context.Udomitelji.Remove(p);
               await Context.SaveChangesAsync();
                return Ok("Uspesno izbrisan udomitelj!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
           
        }
    
        [Route("DodajUdomitelja1")]
        [HttpPost]
        public async Task<ActionResult> DodajUdomiteljaKrozBody([FromBody]Udomitelj udomitelj)
        {
            
             if(string.IsNullOrWhiteSpace(udomitelj.Ime)|| udomitelj.Ime.Length>50)
            {
                return BadRequest("Ime je predugacko!");
            }
            if(string.IsNullOrWhiteSpace(udomitelj.Grad))
            {
                return BadRequest("Ime je predugacko!");
            }
             if(string.IsNullOrWhiteSpace(udomitelj.Prezime)|| udomitelj.Prezime.Length>50)
            {
                return BadRequest("Ime je predugacko!");
            }
             if(string.IsNullOrWhiteSpace(udomitelj.Ulica))
            {
                return BadRequest("Ime je predugacko!");
            }
            if(udomitelj.PostanskiBroj<0)
            {
                return BadRequest("Ime je predugacko!");
            }
           
            try
            {
                Context.Udomitelji.Add(udomitelj);
                await Context.SaveChangesAsync();
                return Ok("Uspesno unet azil!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
           
        }
    }
}
    