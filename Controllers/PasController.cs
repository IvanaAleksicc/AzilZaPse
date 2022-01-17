using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using  Models;


namespace Pas_Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class PasController : ControllerBase
    {
        public AzilContext Context {get;set;}

        public PasController(AzilContext context)
        {
            Context=context;
        }
       

   
[Route("PronadjiPsa")]
    [HttpGet]

    public async Task<ActionResult> Nadji([FromQuery] char? pol,[FromQuery] int? meseci,[FromQuery] string? rasa)
    {
           

            var psi=  Context.Psi
            .Include(p => p.rasa)
            .Include(p=>p.azil)
            .Where(p=>(p.Pol==pol || pol==null)  && (p.Starost<=meseci || meseci==null)&& (p.rasa.Naziv==rasa || rasa==null));

           var pas=await psi.ToListAsync();
            
           return Ok(pas);
    }
 //    [Route("DodajUdomiteljaPoId/{Ime}/{Pol}/{Tezina}/{Starost}/{Velicina}/{idudomitelja}")]
  //  [HttpGet]


     [Route("PreuzmiID/{Ime}/{Pol}/{Tezina}/{Starost}/{Velicina}")]
    [HttpGet]

    public async Task<ActionResult> PreuzmiID(string Ime,char Pol,int Tezina,int Starost,Veliko Velicina)
    {
        var pas= await Context.Psi
           .Where(p=>p.Ime==Ime && p.Pol==Pol && p.Tezina==Tezina && p.Starost==Starost && p.Velicina==Velicina).FirstOrDefaultAsync();

            
        if(pas!=null)
            return Ok ( pas.ID );
        else return BadRequest("Nije naso psa");
    }


    [Route("Preuzmi")]
    [HttpGet]

    public async Task<ActionResult> Preuzmi()
    {
        var psi=  Context.Psi
            .Include(p => p.azil)
            .Include(p => p.rasa);
           
            
           var pas=await psi.ToListAsync();
            return Ok
            (
                pas.Select(p =>
                new
                {
                    Ime=p.Ime,
                    Starost=p.Starost,
                    Tezina=p.Tezina,
                    Pol=p.Pol,
                    Rodjendan=p.datum_rodjenja,
                    Velicina=p.Velicina,
                    Rasa=p.rasa.Naziv,
                    Azil=p.azil.Naziv,
                    Grad=p.azil.Grad,
                    Ulica=p.azil.Ulica,
                    PostanskiBroj=p.azil.PostanskiBroj,

                }).ToList()
            );

          



    }

        [Route("DodajPsa/{Ime}/{Pol}/{Tezina}/{Starost}/{Velicina}")]
        [HttpPost]
        public async Task<ActionResult> DodajPsa(string Ime,char Pol,int Tezina,int Starost,Veliko Velicina,bool Udomljen)
        {
           if(Pol=='\0')
            {
                return BadRequest("Unesite pol!");
            }
             if(Tezina<0)
            {
                return BadRequest("Unesite validnu tezinu!");
            }
             if(string.IsNullOrWhiteSpace(Ime)|| Ime.Length>20)
            {
                return BadRequest("Ime je predugacko!");
            }

            if(Starost<0 || Starost==0)
                {
                return BadRequest("Morate da unesete starost ili datum rodjenja!");
                }
    
 

            if(!Enum.IsDefined(typeof(Veliko),Velicina))
            {
              
                return BadRequest("Uneta je losa velicina!");
            }

            try
            {
                 DateTime datum_rodjenja=  DateTime.Now.AddMonths(-Starost);
              var Pas=new Pas{
                  Pol=Pol,
                   Tezina=Tezina,
                    Ime=Ime,
                    Starost=Starost,
                    Velicina=Velicina,
                    Udomljen=Udomljen,
                    datum_rodjenja=datum_rodjenja
              };
                Context.Psi.Add(Pas);
                await Context.SaveChangesAsync();
                
                return Ok(Pas.ID);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
           
        }
         [Route("DodajPsaUzmiIDDodeliAzil/{Ime}/{Pol}/{Tezina}/{Starost}/{Velicina}/{azilID}/{rasaID}")]
        [HttpPost]
        public async Task<ActionResult> DodajPsaUzmiIDDodeliAzil(string Ime,char Pol,int Tezina,int Starost,Veliko Velicina,string azilID,string rasaID)
        {
           if(Pol=='\0')
            {
                return BadRequest("Unesite pol!");
            }
             if(Tezina<0)
            {
                return BadRequest("Unesite validnu tezinu!");
            }
             if(string.IsNullOrWhiteSpace(Ime)|| Ime.Length>20)
            {
                return BadRequest("Ime je predugacko!");
            }

                if(Starost<0)
                {
                return BadRequest("Morate da unesete starost ili datum rodjenja!");
                }

            if(!Enum.IsDefined(typeof(Veliko),Velicina))
            {
              
                return BadRequest("Uneta je losa velicina!");
            }
            
           
            var azil= await Context.Azili.Where(q=>q.Naziv==azilID).FirstOrDefaultAsync();
            var rasa=await Context.Rase.Where(q=>q.Naziv==rasaID).FirstOrDefaultAsync();
            if(rasa==null)
            {
                rasa= new Rasa{
                    Naziv=rasaID
                };

                Context.Rase.Add(rasa);
                  Context.SaveChanges();

                
            }

            try
            {
                DateTime datum_rodjenja=  DateTime.Now.AddMonths(-Starost);

                
                var Pas = new Pas
                {
                    Pol = Pol,
                    Tezina = Tezina,
                    Ime = Ime,
                    Starost = Starost,
                    Velicina = Velicina,
                    rasa = rasa,
                    azil = azil,
                    datum_rodjenja = datum_rodjenja,
                    Udomljen=false



                };
                Context.Psi.Add(Pas);
                await Context.SaveChangesAsync();//da bi se obavljalo u pozadinskoj niti,neblokirajuca metoda
                //vraca broj podataka koje smo upisali
                 return Ok(Pas);
                
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
           
        }
         [Route("DodajPsaFromBody")]
        [HttpPost]
        public async Task<ActionResult> DodajPsa1([FromBody] Pas pas)
        {
             if(pas.Pol=='\0')
            {
                return BadRequest("Unesite pol!");
            }
          
             if(pas.Tezina<0)
            {
                return BadRequest("Unesite validnu tezinu!");
            }
             if(string.IsNullOrWhiteSpace(pas.Ime)|| pas.Ime.Length>20)
            {
                return BadRequest("Ime je predugacko!");
            }
            if(string.IsNullOrWhiteSpace(pas.Ime)|| pas.Ime.Length>20)
            {
                return BadRequest("Ime je predugacko!");
            }

            if(pas.datum_rodjenja==DateTime.MinValue)
            {
                if(pas.Starost<0 || pas.Starost==0)
                {
                return BadRequest("Morate da unesete starost ili datum rodjenja!");
                }
               pas.datum_rodjenja =  DateTime.Now.AddMonths(-pas.Starost);
               
            }
            if(pas.Starost<0 || pas.Starost==0)
            {
                DateTime danas=DateTime.Today;
                pas.Starost=(int)(((danas.Year-pas.datum_rodjenja.Year)*12)+danas.Month-pas.datum_rodjenja.Month);
               
            }


            if(!Enum.IsDefined(typeof(Veliko),pas.Velicina))
            {
              
                return BadRequest("Uneta je losa velicina!");
            }

            try
            {
                Context.Psi.Add(pas);
                await Context.SaveChangesAsync();
                return Ok("Uspesno unet pas!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
           
        }

        [Route("DodeliAzil/{idpsa}/{idazila}")]
        [HttpPost]
        public async Task<ActionResult> DodeliAzil(int idpsa,int idazila)
        {
            if(idpsa<=0)
            {
                return BadRequest("Nepostojeci azil!");
            }
            if(idazila<=0)
            {
                return BadRequest("Nepostojeci pas");
            }

            try
            {
              var p= Context.Psi.Where(p => p.ID == idpsa).FirstOrDefault();
              var azil=Context.Azili.Where(q => q.ID == idazila).FirstOrDefault();
              if(p!=null && azil!=null)
                {
                    p.azil=azil;

                    await Context.SaveChangesAsync();
                    return Ok("Uspesno promenjeno!");
                }
                else
                return BadRequest("Nije pronadjen pas!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
           
        }
         [Route("DodeliRasu/{idpsa}/{idrase}")]
        [HttpPut]
        public async Task<ActionResult> DodeliRasu(int idpsa,int idrase)
        {
            if(idpsa<=0)
            {
                return BadRequest("Nepostojeca rasa!");
            }
            if(idrase<=0)
            {
                return BadRequest("Nepostojeci pas");
            }

            try
            {
              var p= Context.Psi.Where(p => p.ID == idpsa).FirstOrDefault();
              var rasa=Context.Rase.Where(q => q.ID == idrase).FirstOrDefault();
              if(p!=null && rasa!=null)
                {
                    p.rasa=rasa;

                    await Context.SaveChangesAsync();
                    return Ok("Uspesno dotata rasa!");
                }
                else
                return BadRequest("Nije pronadjen pas ili rasa!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
           
        }
        [Route("PromeniStarost/{t}/{Ime}/{Pol}/{Tezina}/{Starost}/{Velicina}")]
        [HttpPut]
        public async Task<ActionResult> PromeniStarost(int t,string Ime,char Pol,int Tezina,int Starost,Veliko Velicina)
        {
            
           
            if( t==0||t<0)
            {
                return BadRequest("Nepostojeca rasa!");
            }
           
              try
            {
              var p= Context.Psi.Where(q => q.Ime == Ime && q.Pol==Pol && q.Tezina==Tezina && q.Starost==Starost && q.Velicina==Velicina).FirstOrDefault();
              if(p!=null)
                {
                    p.Starost=t;


                    await Context.SaveChangesAsync();
                    return Ok("Uspesno promenjena starost!");
                }
                else
                return BadRequest("Nije pronadjen pas!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
           
        }
        [Route("PromeniTezinu/{t}/{Ime}/{Pol}/{Tezina}/{Starost}/{Velicina}")]
        [HttpPut]
        public async Task<ActionResult> PromeniTezinu(int t,string Ime,char Pol,int Tezina,int Starost,Veliko Velicina)
        {
            
            if(t==0 || t<0)
            {
                return BadRequest("Neodgovarajuca tezina!");
            }
           
            
            try
            {
              var p= Context.Psi.Where(q => q.Ime == Ime && q.Pol==Pol && q.Tezina==Tezina && q.Starost==Starost && q.Velicina==Velicina).FirstOrDefault();
              if(p!=null)
                {
                    p.Tezina=t;

                    await Context.SaveChangesAsync();
                    return Ok("Uspesno promenjena tezina!");
                }
                else
                return BadRequest("Nije pronadjen pas!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
           
        }

        [Route("IzbrisiPsa/{Ime}/{Pol}/{Tezina}/{Starost}/{Velicina}")]
        [HttpDelete]
        public async Task<ActionResult> ObrisiPsa(string Ime,char Pol,int Tezina,int Starost,Veliko Velicina)
        {
          
            try
            {
              var p= Context.Psi.Where(q => q.Ime == Ime && q.Pol==Pol && q.Tezina==Tezina && q.Starost==Starost && q.Velicina==Velicina).FirstOrDefault();
              if(p!=null){
              Context.Psi.Remove(p);
               await Context.SaveChangesAsync();
                return Ok("Uspesno obrisan pas!");
              }
               return BadRequest("Nepostojeci pas");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
           
        }

    }
}