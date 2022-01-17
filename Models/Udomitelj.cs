using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models{
public class Udomitelj
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(50)]
        public string Ime { get; set; }
         [MaxLength(50)]

        public string Prezime { get; set; }
        public string Grad { get; set; }
        public string Ulica { get; set; }

        
        public int PostanskiBroj { get; set; }
        
        //[JsonIgnore]
        public Azil azil{get; set;}

    //[JsonIgnore]
        public Pas pas {get;set;}

    }}