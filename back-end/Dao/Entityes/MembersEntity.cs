using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEPractices4ML.Dao.Entityes
{
    [Table("SEPractices4ML_MEMBERS")]
    public class MembersEntity 
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("IdMember")]
        public int Id { get; set; }

        public virtual UserEntity User { get; set; }
        [Required]
        [Column("FK_IdUser")]
        [ForeignKey(nameof(User))]
        public int IdUsuario { get; set; }

        [Column("Name")]
        [Required]
        public string Name { get; set; }

        [Column("Email")]
        [Required]
        public string Email { get; set; }

        [Column("Degree")]
        public string Degree { get; set; }

        [Column("Organization")]
        public string Organization { get; set; }

        [Column("CurrentlyWork")]
        public string CurrentlyWork { get; set; }

        [Column("AreaActuationRole")]
        public string AreaActuationRole { get; set; }

        [Column("WebSite")]
        public string WebSite { get; set; }

        [Column("AnaliseFinalizada")]
        public int? AnaliseFinalizada { get; set; }

    }
}
