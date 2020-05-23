using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace login.web.Data.Entity
{
    public class TaxiEntity
    {
        public int Id { get; set; }

        [StringLength(20, MinimumLength =6,ErrorMessage ="the {0} field not can must have than {1} character" )]
        [Required(ErrorMessage = "the field {0} is mandatory")]
        public string  Plque { get; set; }

        public ICollection<TripEntity> Trips { get; set; }
        public UserEntity User { get; set; }
    }
}
