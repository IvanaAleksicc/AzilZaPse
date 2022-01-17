
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Models
{
 
    //[Table("Pas")]
    public class Pas
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(50)]
        public string Ime { get; set; }

       
       public int Starost { get; set; }
        
        public int Tezina { get; set; }
         
        public Rasa rasa { get; set; }
 
          public char Pol { get; set; }
        public DateTime datum_rodjenja {get;set;}

        public Veliko Velicina { get; set; }

        public bool Udomljen {get;set;}

        [JsonIgnore]
        public Azil azil{get;set;}



    }
}