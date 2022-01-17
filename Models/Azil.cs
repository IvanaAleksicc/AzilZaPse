using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("Azil")]
    public class Azil
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(50)]
        public string Naziv { get; set; }

        public string Grad { get; set; }
        public string Ulica { get; set; }

        
        public int PostanskiBroj { get; set; }
 
        public int BrojPasa { get; set; }


     
        public int Kapacitet { get; set; }

[JsonIgnore]
         public List<Pas> psi{get; set;}

    [JsonIgnore]
        public List<Udomitelj> udomitelji{get; set;}




    }
}