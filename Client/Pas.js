
import {Udomitelj} from "./Udomitelj.js"
export class Pas
{
    constructor(ime,pol,starost,tezina,velicina,udomljen)
    {
     
        this.ime=ime;
        this.pol=pol;
        this.starost=starost;
        this.tezina=tezina;
        this.velicina=velicina;
        this.udomljen=udomljen;
       
    }
   
    crtajPse(host)
    {
      
        if(this.udomljen==false){
        let f=host.querySelector(".AZIL");
        let a=document.createElement("button");
        a.className="PasDugme";
        a.innerHTML=this.ime;
        if(this.pol=="m" || this.pol=="M")
            a.style.background="rgb(135,228,229)";
        else
        a.style.background="pink";
        f.appendChild(a);
        a.onclick=(ev)=>this.Prikazi(host);
        }
    }
     
 
    Prikazi(host)
    {
        console.log(host);
       let forma=host.querySelector(".Upitnici");
     
       forma=forma.querySelector(".OtvorenaFormica");
       forma.style.display='block';
       console.log(forma);
       
      

        let tekst=forma.querySelector("textarea");
        
        tekst.innerText="";
       
        tekst.innerHTML="Ime:" + this.ime + "\npol:" +this.pol+"\nstarost:"+this.starost+"m\ntezina:"+this.tezina+"kg\nvelicina:"+this.vrati(this.velicina);
       

   
    }
    vrati(x)
    {
        switch(x)
        {
            case 0: return "Patuljasti";
            break;
            case 1: return "Mali";
            break;
            case 2: return "Srednji";
            break;
            case 3: return "Veliki";
            break;
            case 4: return "Veoma veliki";
            break;

        }
    }

 
    Ispisi()
    {
        return this.ime;
    }
}