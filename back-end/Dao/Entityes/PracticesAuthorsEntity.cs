using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEPractices4ML.Dao.Entityes
{
    [Table("SEPractices4ML_PRACTICES_AUTHORS")]
    public class PracticesAuthorsEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }

        public virtual PracticesEntity Practices { get; set; }
        [Required]
        [Column("IdPractice")]
        [ForeignKey(nameof(Practices))]
        public int IdPractice { get; set; }

        public virtual UserEntity User { get; set; }
        [Required]
        [Column("IdUser")]
        [ForeignKey(nameof(User))]
        public int IdUser { get; set; }

    }
}
