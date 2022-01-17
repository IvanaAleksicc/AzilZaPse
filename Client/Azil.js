import {Pas} from "./Pas.js";
import {Rase} from "./Rase.js";
import { Udomitelj } from "./Udomitelj.js";

export class Azil
{
    constructor(id,naziv,kapacitet,brojPasa,grad)
    {
        this.id=id;
        this.naziv=naziv;
       this.kapacitet=kapacitet;
       this.brojPasa=brojPasa;
       this.grad=grad;
        this.kontejner=null;
    }



    crtaj(host,lista)
    {
        this.kontejner=document.createElement("div");
        this.kontejner.className="Glavni";
        host.appendChild(this.kontejner);

     

        let kontrole=document.createElement("div");
        kontrole.className="Kontrole";
        this.kontejner.appendChild(kontrole);

        let AzilITabela=document.createElement("div");
        AzilITabela.className="AzilITabela";
        this.kontejner.appendChild(AzilITabela);

        let azil=document.createElement("div");
         azil.className="AZIL";
       
         AzilITabela.appendChild(azil);

       let tabela=document.createElement("div");
       tabela.className="TABELA";
       AzilITabela.appendChild(tabela);

       let upitnici=document.createElement("div");
       upitnici.className="Upitnici";
    
       AzilITabela.appendChild(upitnici);

        this.crtajKontrole(kontrole,lista);
        this.crtajUpitnike(upitnici);
    

       
    }
    crtajUpitnike(host)
    {

    
    let  div=document.createElement("div");
    div.className="OtvorenaFormica";
    host.appendChild(div);

    let tekst=document.createElement("textarea");
  
    div.appendChild(tekst);

    let div1=document.createElement("div")
    div1.className="DUGMICI";
    div.appendChild(div1);
    let dugme=document.createElement("button");
    dugme.className="usvojibtn";
    dugme.innerHTML="Usvoji";
    div1.appendChild(dugme);

    let  divf=document.createElement("div");
    divf.className="FormaZaValidaciju";
    host.appendChild(divf);
   

    dugme.onclick=(ev)=>this.OtvoriFormuZaValidaciju(host);

   

    let dugme3=document.createElement("button");
    dugme3.innerHTML="Promeni tezinu";
    dugme3.className="usvojibtn";
    div1.appendChild(dugme3);
   dugme3.onclick=(ev)=>this.PromeniTezinu(tekst);

   let dugme4=document.createElement("button");
    dugme4.innerHTML="Promeni starost";
    dugme4.className="usvojibtn";
    div1.appendChild(dugme4);
  dugme4.onclick=(ev)=>this.PromeniStarost(tekst);
  let dugme6=document.createElement("button");
  dugme6.innerHTML="Obrisi";
  dugme6.className="usvojibtn";
  div1.appendChild(dugme6);
    dugme6.onclick=(ev)=>this.Obrisi(tekst);

    this.NacrtajFormuZaValidaciju(divf,dugme);
    let dugme2=document.createElement("button");
    dugme2.innerHTML="Cancel";
    dugme2.className="usvojibtn";
    div1.appendChild(dugme2);
    dugme2.onclick=(ev)=>this.ZatvoriP(host);

    let div5=document.createElement("div");
    div5.className="FormaZaRegistraciju";
    host.appendChild(div5);
    this.NacrtajFormuZaRegistraciju(div5)
   
}
Obrisi(tekst) //brisanje psa
{
    let odg=prompt("Da li sigurno zelite da obrisete psa? --Unesite no ili yes--");
    if(odg=='no')
        return;
    let niz=[];
    let podaci=tekst.value;
   
    niz= this.srediString(podaci);
  
    fetch("https://localhost:5001/Pas/IzbrisiPsa/" +  niz[0] + "/" + niz[1] + "/" + niz[3] + "/" + niz[2] + "/" + niz[4],
    {
        method: "DELETE",
  
    }).then(p => {
        if (p.ok) {
           alert("Uspesno obrisan pas!");  
          }
          }) 
}
PromeniTezinu(tekst)
{
    let niz=[];
    let podaci=tekst.value;
    let t=prompt('Unesite željenu težinu u kilogramima:');
   
    niz= this.srediString(podaci);
  
    fetch("https://localhost:5001/Pas/PromeniTezinu/" + t + "/" + niz[0] + "/" + niz[1] + "/" + niz[3] + "/" + niz[2] + "/" + niz[4],
    {
        method: "PUT",
  
    }).then(p => {
        if (p.ok) {
           alert("Uspesno promenjena tezina!");  
          }
          }) 
   
}
PromeniStarost(tekst)
{
    let niz=[];
    let podaci=tekst.value;
    let t=prompt('Unesite željenu starost u mesecima:');
   
    niz= this.srediString(podaci);
  
    fetch("https://localhost:5001/Pas/PromeniStarost/" + t + "/" + niz[0] + "/" + niz[1] + "/" + niz[3] + "/" + niz[2] + "/" + niz[4],
    {
        method: "PUT",
  
    }).then(p => {
        if (p.ok) {
           alert("Uspesno promenjena starost!");  
          }
          }) 
   
}
NacrtajFormuZaValidaciju(divf,dugme)
{
    let div=this.crtajDiv(divf);
    let r=document.createElement("input");
    r.type='radio';
    r.name='kuca';
    r.onclick=(ev)=>this.OnemoguciS(divf);
    div.appendChild(r);

    let l=document.createElement("label");
    l.innerText="Kuca";
    div.appendChild(l);

     r=document.createElement("input");
    r.type='radio';
    r.name='stan';
    r.onclick=(ev)=>this.OnemoguciK(divf);
    div.appendChild(r);

     l=document.createElement("label");
    l.innerText="Stan";
    div.appendChild(l);

    div=this.crtajDiv(divf);
    r=document.createElement("input");
    r.type='checkbox';
    r.name='dvpriste';
    div.appendChild(r);

     l=document.createElement("label");
    l.innerText="Dvoriste";
    div.appendChild(l);

    div=this.crtajDiv(divf);
    l=document.createElement("label");
    l.innerText="Slobodno vreme:";
    div.appendChild(l);

    r=document.createElement("input");
    r.name='vreme';
    div.appendChild(r);

    div=this.crtajDiv(divf);
    let b=document.createElement("button");
    b.innerHTML="Submit";
    b.onclick=(ev)=>this.Validacija(divf,dugme);
    div.appendChild(b);

    
    

     b=document.createElement("button");
    b.innerHTML="Close";
    b.onclick=(ev)=>this.Zatvori1P(divf);
    div.appendChild(b);

    console.log(divf);

}
OnemoguciK(host)
{
    let k=host.querySelector('input[name="kuca"]');
  
    k.disabled=true;
}
OnemoguciS(host)
{
    let k=host.querySelector('input[name="stan"]');
    
    k.disabled=true;
}
Validacija(host)
{
    let poeni;
    let k=host.querySelector('input[name="kuca"]');
    if(k.checked)
        poeni=10;
    k=host.querySelector('input[name="stan"]');
    if(k.checked)
        poeni=5;

     k=host.querySelector('input[name="dvpriste"]');
    if(k.checked)
        poeni+=20

    k=host.querySelector('input[name="vreme"]');
    poeni+=k.value;

    if( poeni>=6)
    {
        alert("Cestitamo!\nIspunjavate sve uslove!\nReistrujte se da biste usvojili psa!\n");
        this.OtvoriRegistraciju();
     
       
        
    }

    if(poeni<6)
    {
        alert("Ne ispunjavate sve uslove!\n");
       
        
    }

    

}
OtvoriRegistraciju()
{
    let k=this.kontejner.querySelector(".FormaZaRegistraciju");
    k.style.display="block";
}
NacrtajFormuZaRegistraciju(divf)
{
    let div=this.crtajDiv(divf);
  let  l=document.createElement("label");
    l.innerText="Unesite Ime:";
    div.appendChild(l);

  let  r=document.createElement("input");
    r.name='ime';
    div.appendChild(r);

    div.className="DIV"
    
     div=this.crtajDiv(divf);
    l=document.createElement("label");
    l.innerText="Unesite prezime:";
    div.appendChild(l);

    r=document.createElement("input");
    r.name='prezime';
    div.appendChild(r);

     div=this.crtajDiv(divf);
    l=document.createElement("label");
    l.innerText="Unesite Grad:";
    div.appendChild(l);

    r=document.createElement("input");
    r.name='grad';
    div.appendChild(r);

     div=this.crtajDiv(divf);
    l=document.createElement("label");
    l.innerText="Unesite ulicu:";
    div.appendChild(l);

    r=document.createElement("input");
    r.name='ulica';
    div.appendChild(r);
  
     div=this.crtajDiv(divf);
    l=document.createElement("label");
    l.innerText="Unesite postanski broj:";
    div.appendChild(l);

    r=document.createElement("input");
    r.name='postanskibroj';
    div.appendChild(r);

     div=this.crtajDiv(divf);
    let b=document.createElement("button");
    b.innerHTML="Submit";
    b.onclick=(ev)=>this.UnesiKorisnika(divf);
    div.appendChild(b);

     b=document.createElement("button");
    b.innerHTML="Close";
    b.onclick=(ev)=>this.Zatvori2P(divf);
    div.appendChild(b); 

     div.className="DIV"
  //  div.style.display='inline-block';

}

UnesiKorisnika(m)
{
    
    console.log(m);
     let  ime=m.querySelector('input[name="ime"]').value;
     let  prezime=m.querySelector('input[name="prezime"]').value;
     let  grad=m.querySelector('input[name="grad"]').value;
     let  ulica=m.querySelector('input[name="ulica"]').value;
     let  postanskibroj=m.querySelector('input[name="postanskibroj"]').value;
    console.log(postanskibroj);
     var udomitelj= new Udomitelj(ime,prezime,grad,ulica,postanskibroj);
    console.log(udomitelj);
     fetch("https://localhost:5001/Udomitelj/DodajUdomitelja/" + ime + "/" + grad + "/" + prezime + "/" + ulica + "/" + postanskibroj,
  {
      method: "POST",

  }).then(p => {
      if (p.ok) {
         console.log("Uspesno unet udomitelj!");   
         p.json().then(c => {
            let  ID = c;            
             console.log(ID);
             this.UsvojiPsa(ID);
        });
    }
        else
        {
            alert("Udomitelj Vec postoji!"); 
      }

      
                 
        });

}




UsvojiPsa(udomiteljID)
{
    
   
   let k=this.kontejner.querySelector(".OtvorenaFormica");
   let t=k.querySelector("textarea");
   let podaci=t.value;
   

   podaci=this.srediString(podaci)
    console.log(podaci);
        fetch("https://localhost:5001/Udomitelj/DodeliPsaPrekoInfo/" + udomiteljID + "/" + podaci[0] + "/" + podaci[1] + "/" + podaci[3] + "/" + podaci[2] + "/" + podaci[4],
  {
      method: "POST",

  }).then(p => {
      if (p.ok) {
          p.json().then(op1=>{
            alert("Uspesno povezan pas, azil i udomitelj!"); 
            console.log(op1); 
             fetch("https://localhost:5001/Azil/VratiKapacitet/" +(-1) + "/" + op1.id ,
            {
                method: "PUT",
    
            }).then(p => {
                if (p.ok) {
                    console.log(p);
                   alert("Uspesno smanjen broj pasa");
                }
            });  
   
          })
       
        }
        }) 

}
srediString(podaci)
{
  
    let podaci1,podaci2=[],vracamo=[];
    podaci1=podaci.split("\n");
  
    podaci1.forEach(p=>{
        podaci2=p.split(":");
     
        vracamo.push(podaci2[1]);
        console.log("podaci2"+podaci2);

    
    })
    console.log(vracamo);
    console.log(vracamo[4]);
   vracamo[4]=this.vrati(vracamo[4]);
   console.log(vracamo[4]);
 
 
    return vracamo;
}
vrati(x)
    {
        console.log(x);
        switch(x)
      
        {
            case 'Patuljasti': return 0;
            break;
            case 'Mali': return 1;
            break;
            case 'Srednji': return 2;
            break;
            case 'Veliki': return 3;
            break;
            case 'Veoma veliki': return 4;
            break;

        }
    }
Zatvori1P(host)
{
    console.log(host);
    console.log(host.className);
    this.Zatvori2P(host);
    if(host.className!="FormaZaValidaciju")
        host=host.querySelector(".FormaZaValidaciju");
   
    console.log(host);
    
   
   host.style.display='none';
}
ZatvoriP(host)
{
    console.log(host);
    this.Zatvori1P(host);
    let z=host.querySelector(".OtvorenaFormica"); 
    if(host!=null)   
    z.style.display='none';
    console.log(z);
   
}
Zatvori2P(host)
{
   
    console.log(host);
    console.log(host.className);
    if(host.className!="FormaZaRegistraciju")
       host=host.querySelector(".FormaZaRegistraciju");
    
       if(host!=null)
       host.style.display='none';
   
}

Ocisti(z)
{
    let dete=z.querySelector("textarea");
    dete.innerText="";
}
OtvoriFormuZaValidaciju(host)
{
    
    let forma=host.querySelector(".FormaZaValidaciju");
    
    this.OcistiFormu(forma);

    forma.style.display='block';

}

OcistiFormu(host)
{
    console.log(host);
    let k=host.querySelector('input[name="kuca"]');
    k.disabled=false;
    k.checked=false;

     k=host.querySelector('input[name="stan"]');
    k.disabled=false;
    k.checked=false;

     k=host.querySelector('input[name="dvpriste"]');
    k.checked=false;

     k=host.querySelector('input[name="vreme"]');
    k.value="";

}



    crtajDiv(host)
    {
        let red=document.createElement("div");
        host.appendChild(red);

        return red;
    }
    crtajKontrole(host,lista)
    {
        let div=this.crtajDiv(host)
        let labela=document.createElement("label");
        labela.innerHTML="Azili:";
        div.appendChild(labela);

        
        let s=document.createElement("select");
        s.className="SelektZaAzile";
        div.appendChild(s);

        let opcija;
        
        lista.forEach(i=>{
            opcija=document.createElement("option");
            opcija.innerHTML=i.naziv;
            opcija.value=i.id;
            s.appendChild(opcija);
        })
       
        div=this.crtajDiv(host)
        let Prikaz=document.createElement("button");
        Prikaz.innerHTML="Prikazi Udomitelje";
      
        div.appendChild(Prikaz);
        Prikaz.onclick=(ev)=>this.Popuni();//OtvoriTabeluUdomitelja

        this.NacrtajTablicu();
        

    
        div=this.crtajDiv(host)
        let Prikazi=document.createElement("button");
        Prikazi.innerHTML="Prikazi pse";
        Prikazi.className="Prikaz";
        div.appendChild(Prikazi);
        Prikazi.onclick=(ev)=>this.OtvoriAzil();
        
 

        div=this.crtajDiv(host);
        let l=document.createElement("label");
        l.innerHTML="Unesite naziv azila:";
        div.appendChild(l);

      
         l=document.createElement("input");   
        
        host.appendChild(l);

         Prikazi=document.createElement("button");
        Prikazi.innerHTML="OK";
        Prikazi.className="Prikaz";
        host.appendChild(Prikazi);
        Prikazi.onclick=(ev)=>this.Unesi(l);

        div=this.crtajDiv(host)
        Prikazi=document.createElement("button");
        Prikazi.innerHTML="Nadji Psa";
        div.appendChild(Prikazi);
       Prikazi.onclick=(ev)=>this.Omoguci1();

        let divPas=this.crtajDiv(host);
        divPas.className="FormaOdgovarajuciPas"

           //this.NacrtajFormuPsa(divPas);

       div=this.crtajDiv(host)
       Prikazi=document.createElement("button");
       Prikazi.innerHTML="Dodaj Psa";
       div.appendChild(Prikazi);
      Prikazi.onclick=(ev)=>this.OtvoriFormuZaDodavanjePsa(lista);
    

     let divDodaj=this.crtajDiv(host);
    divDodaj.className="FormaDodajPsa";
    this.NacrtajDodajPsa(divDodaj,lista);
   
       
    }
    OtvoriAzil()
    {
         
            let divTabela=this.kontejner.querySelector(".AZIL");
            console.log(divTabela);
            console.log(divTabela.style.display);
        
        if( divTabela.hasChildNodes())
        {
            while(divTabela.firstChild)
                 divTabela.removeChild(divTabela.lastChild);
            console.log("Zatvaram");
           divTabela.style.display='none';
           divTabela.style.border='none';
        }
        else
        {
            console.log("Otvaram");
           divTabela.style.display='grid';
           console.log(divTabela.style.display);
           divTabela.style.border='1px solid black'; 
         
           this.Prikaz();
           

       }
       console.log(divTabela);
    }
  
    OtvoriFormuZaDodavanjePsa(lista)
    {
        let divTabela=this.kontejner.querySelector(".FormaDodajPsa");
        if( divTabela.style.display=='block')
        {
           divTabela.style.display='none';
           
        this.OcistiFormuZaDodavanjePsa(lista);
        }

        else
        divTabela.style.display='block';
       
       
    }
    OcistiFormuZaDodavanjePsa(lista)
    {
        console.log("udjoh");
        let f=this.kontejner.querySelector(".FormaDodajPsa");

        let a=f.querySelector(".InputZaIme");
        a.value="";
        a=f.querySelector(".InputZaTezinu");
        a.value="";
        a=f.querySelector(".InputZaStarost");
        a.value="";
        a=f.querySelector(".InputZaRasu");
        a.value="";

        console.log(lista);
       let lista1=lista.filter(b=>b.brojPasa<b.kapacitet);
       console.log(lista1);
       a=f.querySelector(".SelektZaAzil1");
       while (a.firstChild) {
         
       a.lastChild.innerHTML=" ";
        a.removeChild(a.lastChild);
        
      }
       let opcija1;
        
       lista1.forEach(i=>{
           opcija1=document.createElement("option");
           opcija1.innerHTML=i.naziv;
           opcija1.value=i.id;
           a.appendChild(opcija1);
       })


    }
    NacrtajDodajPsa(forma,lista)
    {
        
        let s=document.createElement("select");
        s.className="SelektZaDodavanjePola";
        forma.appendChild(s);

        let opcija=document.createElement("option");
        opcija.innerHTML="Muski";
        opcija.value="m";
        s.appendChild(opcija);

        opcija=document.createElement("option");
        opcija.innerHTML="Zenski";
        opcija.value="z";
        s.appendChild(opcija);

      let   div=this.crtajDiv(forma);
      let   l=document.createElement("label");
        l.innerHTML="Unesite ime:";
        div.appendChild(l);

         div=this.crtajDiv(forma);
        let t=document.createElement("input");
        t.className="InputZaIme";
        t.name="I";
        div.appendChild(t);

         div=this.crtajDiv(forma);
         l=document.createElement("label");
        l.innerHTML="Unesite tezinu:";
        div.appendChild(l);

         div=this.crtajDiv(forma);
         t=document.createElement("input");
        t.className="InputZaTezinu";
        t.name="T";
        div.appendChild(t);

         div=this.crtajDiv(forma);
         l=document.createElement("label");
        l.innerHTML="Unesite starost:";
        div.appendChild(l);

         div=this.crtajDiv(forma);
         t=document.createElement("input");
         t.name="S";
        t.className="InputZaStarost";
        div.appendChild(t);

        div=this.crtajDiv(forma);
         l=document.createElement("label");
        l.innerHTML="Unesite velicinu:";
        div.appendChild(l);

         s=document.createElement("select");
        s.className="SelektZaDodavanjeVelicine";
        forma.appendChild(s);

        const en=["Patuljasti","Mali","Srednji","Veliki","Veoma veliki"];
        en.forEach(p=>{
            opcija=document.createElement("option");
            
            opcija.innerHTML=p;
            opcija.value=en.indexOf(p);
            console.log(opcija.value);
            s.appendChild(opcija);
        })
        div=this.crtajDiv(forma);
        l=document.createElement("label");
        l.innerHTML="Unesi Azil:"
        div.append(l);

        let q=document.createElement("select");
        q.className="SelektZaAzil1";
        div.appendChild(q);

        let opcija1;
        
        lista.forEach(i=>{
            opcija1=document.createElement("option");
            opcija1.innerHTML=i.naziv;
            opcija1.value=i.id;
            q.appendChild(opcija1);
        }) 

        div=this.crtajDiv(forma);
        l=document.createElement("label");
        l.innerHTML="Unesi Rasu:"
        div.append(l);

        div=this.crtajDiv(forma);
       let w=document.createElement("input");
        w.name="W";
       w.className="InputZaRasu";
       div.appendChild(w);
       
        div=this.crtajDiv(forma);
        let OK=document.createElement("button");
        OK.className="dugmence";
        OK.innerHTML="OK";
        forma.appendChild(OK);
        OK.onclick=(ev)=>this.DodajPsa();

        
        let Cancel=document.createElement("button");
        Cancel.className="dugmence";
        Cancel.innerHTML="Cancel";
        forma.appendChild(Cancel);
        Cancel.onclick=(ev)=>this.Zatvori2();




    }
    DodajPsa()
    {
        let forma=this.kontejner.querySelector(".FormaDodajPsa");
        let sel=forma.querySelector(".SelektZaDodavanjePola");
        let opcija=sel.options[sel.selectedIndex].value;

        sel=this.kontejner.querySelector(".InputZaIme").value;
         let im=sel;
         console.log("ime "+im);

         sel=this.kontejner.querySelector(".InputZaTezinu").value;
    
         let tez=sel;
         console.log("tezina"+sel);

         sel=forma.querySelector(".InputZaStarost").value;
         let star=sel;

         sel=forma.querySelector(".SelektZaDodavanjeVelicine");
       let  vel=sel.options[sel.selectedIndex].value;

       sel=forma.querySelector(".SelektZaAzil1");
       let op=sel.options[sel.selectedIndex].innerHTML;
        console.log(op);
        let op1=sel.options[sel.selectedIndex].value;
        console.log(op1);
        fetch("https://localhost:5001/Azil/VratiKapacitet/" + 1 + "/" + op1 ,
        {
            method: "PUT",

        }).then(p => {
            if (p.ok) {
                console.log(p);
               alert("Uspesno sve");
            }
        });  
       
       sel=forma.querySelector(".InputZaRasu").value;
       let rasa=sel;
        console.log(op,rasa);
       
        vel=parseInt(vel);
      
        let a=new Pas(im,opcija,star,tez,vel);
        console.log(a);
     
        let ID;
        let IDazila;
        let IDrase;
      
            fetch("https://localhost:5001/Pas/DodajPsaUzmiIDDodeliAzil/" + im + "/" + opcija + "/" + star + "/" + tez + "/" + vel+"/" + op+"/" + rasa,
            {
                method: "POST",

            }).then(p => {
                if (p.ok) {
                    alert("Uspesno sve");
                }
            });
            
           
          
        
    }
    PronadjiPsa()
    {
        let forma=this.kontejner.querySelector(".FormaOdgovarajuciPas");
       let pol=forma.querySelectorAll(".RadioPol");
       
        let selectedPol;
              pol.forEach(p=>{
                if (p.checked) {
                   
                    selectedPol = p.value;
                    
                } 
            });
            if(selectedPol===0){
                selectedPol='\0';
            console.log("Nije selektovan pol");
            }
            pol=forma.querySelector(".InputZaStarost");
            let selectedStarost=pol.value;
             
            console.log("Selektovana starost"+selectedStarost);
            if(selectedStarost===0){
                selectedStarost=0;
            console.log("Nije selektovana staarost");
            }
            pol=forma.querySelector("select");
           
            let opcija=pol.options[pol.selectedIndex].value;
            if(opcija===null){
                opcija="";
            console.log("Nije selektovana rasa");

            }
          
            fetch("https://localhost:5001/Pas/PronadjiPsa?pol="+selectedPol+"&meseci="+selectedStarost+"&rasa="+opcija,
            {
                method:"GET"
            }).then(p=>{
                if(p.ok)
                {
                    this.ocisti();
                    p.json().then(psi=>{
                        console.log(psi);
                        psi.forEach(i=>{
                            var a=new Pas(i.ime,i.pol,i.starost,i.tezina,i.velicina,i.udomljen);
                            console.log(a);
                            a.crtajPse(this.kontejner);
                        });
                    })
                }
            })

    }
    Omoguci1()
    {
       
        let divTabela=this.kontejner.querySelector(".FormaOdgovarajuciPas");
        if( divTabela.style.display=='block')
        {
           divTabela.style.display='none';
           this.OcistiSve(divTabela);
        }
        else
        {
        divTabela.style.display='block';
       
        this.NacrtajFormuPsa(divTabela);
        }
        
      
    
    }
    NacrtajTablicu()
    {
        
        let divTabela=this.kontejner.querySelector(".TABELA");
        console.log(divTabela);
         let tabela=document.createElement("table");
        tabela.className="Tablica";
        divTabela.appendChild(tabela);

        var head=document.createElement("thead");
        tabela.appendChild(head);

        var tr=document.createElement("tr");
        head.appendChild(tr);

        var body=document.createElement("tbody");
        body.className="Telo";
        tabela.appendChild(body);

        let td;
        var niz=["Ime","Prezime","Grad","Ulica","Postanski broj","Pas"];
        niz.forEach(i=>{
            td=document.createElement("td");
            td.innerHTML=i;
            head.appendChild(td);
        });

        
      }
      
    Popuni()
    { 
        let a=this.kontejner.querySelector(".Kontrole");
        a=a.querySelector(".SelektZaAzile");
        let opcija=a.options[a.selectedIndex].value;
        let k
        

        fetch("https://localhost:5001/Azil/PrikaziUdomitelje/" + opcija,
            {
                method: "GET"

            }).then(p => {
                if (p.ok) {
                    p.json().then(udomitelji => {

                        
                        if (udomitelji.length === 0)
                            alert("U selektvanom azilu nema udomitelja!");
                       
                        else{
                            let body = this.OcistiTablicu();
                            udomitelji.forEach(udomitelj => {

                                let a = new Udomitelj(udomitelj.ime, udomitelj.prezime, udomitelj.grad, udomitelj.ulica, udomitelj.postanskiBroj, udomitelj.pas)
                                console.log(a);
                                a.crtaj(body);
                            
                })
                this.OtvoriTabeluUdomitelja();
                        }   
                })
             
            }
            })
         
           
    }

    OtvoriTabeluUdomitelja()
    {
        
        
       
       
        let divTabela=this.kontejner.querySelector(".TABELA");
        if( divTabela.style.display=='block')
           divTabela.style.display='none';
        else
        divTabela.style.display='block';
        
     
      
        
    }
    OcistiTablicu(host)
    {
        let divTabela=this.kontejner.querySelector(".TABELA");
        let t=divTabela.querySelector(".Telo");
        while(t.firstChild)
            t.removeChild(t.lastChild);
        return t;
        
    }
    NacrtajFormuPsa(forma)
    {
        
        let l=document.createElement("label");
        l.innerHTML="Pol:";
        forma.appendChild(l);

        let div=this.crtajDiv(forma);
         l=document.createElement("label");
        l.innerHTML="M";
        forma.appendChild(l);

        let s=document.createElement("input");
        s.className="RadioPol";
        s.type='radio';
        s.value="m";
        s.name="M";
        forma.appendChild(s);
        s.onclick=(ev)=>this.OnemoguciZ(forma);
       



        l=document.createElement("label");
        l.innerHTML="Z";
       
        forma.appendChild(l);

        let z=document.createElement("input");
        z.className="RadioPol";
        z.type='radio';
        z.name="Z";
        z.value="z";
        
        forma.appendChild(z);
        z.onclick=(ev)=>this.OnemoguciM(forma);
       

         div=this.crtajDiv(forma);
         l=document.createElement("label");
        l.innerHTML="Starost:";
        forma.appendChild(l);

        div=this.crtajDiv(forma);
        let i=document.createElement("input");
        i.className="InputZaStarost";
        forma.appendChild(i);

        div=this.crtajDiv(forma);
         l=document.createElement("label");
        l.innerHTML="Rasa:";
        forma.appendChild(l);
        

        div=this.crtajDiv(forma);
        l=document.createElement("select");
        l.className="SelektZaPse";
        forma.appendChild(l);
        this.NacrtajSelekcijuRase();

       

        div=this.crtajDiv(forma);
        let OK=document.createElement("button");
        OK.className="dugmence";
        OK.innerHTML="OK";
        forma.appendChild(OK);
        OK.onclick=(ev)=>this.PronadjiPsa(s);

        
        let Cancel=document.createElement("button");
        Cancel.className="dugmence";
        Cancel.innerHTML="Cancel";
        forma.appendChild(Cancel);
        Cancel.onclick=(ev)=>this.Zatvori1();
      
        this.NacrtajSelekcijuRase(forma);
        
    }
    OcistiSve(z)//cisti formu za nalazenje psa
    {
       
        let p=z.querySelectorAll(".RadioPol");
        p.forEach(i=>{
            i.disabled=false;
            i.checked=false;
        })
         p=z.querySelector(".InputZaStarost");
        p.value="";
        let roditelj=z.parentNode;
        console.log(roditelj);
        roditelj.removeChild(z);

        let div=document.createElement("div");
        div.className="FormaOdgovarajuciPas";
        roditelj.appendChild(div);
        return div;

    }
    OnemoguciZ(forma)
    {
       let r= forma.querySelector('input[name="Z"]');
       
                r.disabled= true;
        
    }
    OnemoguciM(forma)
    {
       let r= forma.querySelector('input[name="M"]');
      r.disabled=true;
    }

    NacrtajSelekcijuRase()
    {
        let l=this.kontejner.querySelector(".FormaOdgovarajuciPas");
        l=l.querySelector("select");
        
         let q=this.kontejner.querySelector("select");

         var AzilID=q.options[q.selectedIndex].value;

        fetch("https://localhost:5001/Azil/PrikaziRaseIzAzila/"+AzilID,
        {
            method:"GET"
        }).then(p=>{
            if(p.ok)
            {
                this.OcistiSelekcijuRase(l);
                p.json().then(rase=>{
                    console.log(rase);
                 
                  rase.forEach(i => {
                      console.log(i);
                        let opcija=document.createElement("option");
                       
                        opcija.innerHTML=i.naziv;
                        console.log("Ubacio sam"+ i.naziv);
                       
                       
                        l.appendChild(opcija);

                    })
                })
            }
        })
    }
  
    OcistiSelekcijuRase(l)
    {
       
        
        while (l.firstChild) {
         
            l.lastChild.innerHTML=" ";
            l.removeChild(l.lastChild);
            
          }
          console.log(l);

      
    }

    Unesi(s)
    {

        if(s.value==="")
        alert("Morate da unesete naziv azila!");
        else{
           
        fetch("https://localhost:5001/Azil/PrikaziPseNaOsnovuImenaAzila/"+s.value,
        {
            method:"GET"
        }).then(p=>{
            if(p.ok)
            {
                this.ocisti();
                p.json().then(psi=>{
                    if(psi.length===0)
                    alert("Nema nijednog psa u zadatom azilu");
                    psi.forEach(p=>{
                       
                       let a=new Pas(p.ime,p.pol,p.starost,p.tezina,p.velicina,p.udomljen);
                        console.log(a);
                        a.crtajPse(this.kontejner);                    
                    })
                })
            }
            else
            alert("Nepostojeci azil!");
        })
    }
    } 
    Prikaz()
    {
    
        
        let q=this.kontejner.querySelector("select");
        var AzilID=q.options[q.selectedIndex].value;
        console.log("selektovan azil je"+ AzilID);

        fetch("https://localhost:5001/Azil/PrikaziPse/"+AzilID,
        {
            method:"GET"

        }).then(p=>{
            if(p.ok)
            {
               this.ocisti();
                p.json().then(psi=>{
                    
                    psi.forEach(p=>{
                       
                        console.log(p);
                       let a=new Pas(p.ime,p.pol,p.starost,p.tezina,p.velicina,p.udomljen);
                        console.log(a);
                        a.crtajPse(this.kontejner);                    
                    })
                })
            }
           
        })


    }
    ocisti()
    {
        var host=this.kontejner.querySelector(".AZIL");
        var parent=host.parentNode;
        parent.removeChild(host);
        
        host=document.createElement("div");
        host.className="AZIL";
        parent.appendChild(host);

        return host;
    }

    Zatvori1()
    {
        let z=this.kontejner.querySelector(".FormaOdgovarajuciPas");
        
       
        z.style.display='none';

        this.OcistiSve(z);
      
    }
    Zatvori2()
    {
        let z=this.kontejner.querySelector(".FormaDodajPsa");
        
       
        z.style.display='none';
    }

}

