import {Azil} from "./Azil.js";
import {Pas} from "./Pas.js";

var listaAzila1=[]

 fetch("https://localhost:5001/Azil/PrikaziAzile2")
.then(p=>{
    p.json().then(niz=>{
        console.log(niz);
        niz.forEach(listaAzila => {
            console.log(listaAzila);
            listaAzila1=[];    
            listaAzila.forEach(azil=>{
                var a=new Azil(azil.id,azil.naziv,azil.kapacitet,azil.brojPasa,azil.grad);
                listaAzila1.push(a);
            })
            console.log(listaAzila1);
            let div =document.createElement("div");
            document.body.append(div);
            let l=document.createElement("label");
            l.className="Titl";
            l.innerHTML="Azil u "+listaAzila1[0].grad+"u:";
            div.append(l);
            listaAzila1[0].crtaj(document.body,listaAzila1);
        });
        /*niz.forEach(azil => {
          azil.crtaj(document.body,listaAzila);
          azil.forEach(b=>{
           
            listaAzila.push(a);
         })
         azil.crtaj()
            
           // a.crtaj()
        });
        listaAzila.forEach(q=>{
      
       q.crtaj(document.body,listaAzila)
    }); */
//})
    })
}) 





