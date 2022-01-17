export class Udomitelj
{
    constructor(ime,prezime,grad,ulica,postanskibroj,pas)
    {
        this.ime=ime,
        this.prezime=prezime,
        this.grad=grad,
        this.ulica=ulica,
        this.postanskibroj=postanskibroj,
        this.pas=pas
    }

    crtaj(host)
    {
        var tr=document.createElement("tr");
        host.appendChild(tr);

        var u=document.createElement("td");
        u.innerHTML=this.ime;
        tr.appendChild(u);

        var u=document.createElement("td");
        u.innerHTML=this.prezime;
        tr.appendChild(u);

        var u=document.createElement("td");
        u.innerHTML=this.grad;
        tr.appendChild(u);

        var u=document.createElement("td");
        u.innerHTML=this.ulica;
        tr.appendChild(u);

        var u=document.createElement("td");
        u.innerHTML=this.postanskibroj;
        tr.appendChild(u);

        var u=document.createElement("td");
        u.innerHTML=this.pas.ime;
        tr.appendChild(u);
    }
}