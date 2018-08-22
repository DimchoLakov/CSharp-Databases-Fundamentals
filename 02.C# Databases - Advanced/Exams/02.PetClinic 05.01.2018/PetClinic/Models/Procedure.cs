using System;
using System.Collections.Generic;
using System.Linq;

namespace PetClinic.Models
{
    public class Procedure
    {
        public Procedure()
        {
            this.ProcedureAnimalAids = new List<ProcedureAnimalAid>();
        }

        public int Id { get; set; }

        public int AnimalId { get; set; }
        public Animal Animal { get; set; }

        public int VetId { get; set; }
        public Vet Vet { get; set; }

        public decimal Cost => this.ProcedureAnimalAids.Sum(x => x.AnimalAid.Price);

        public DateTime DateTime { get; set; }

        public ICollection<ProcedureAnimalAid> ProcedureAnimalAids { get; set; }
    }
}
