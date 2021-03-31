using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEPractices4ML.Dao.Entityes
{
    [Table("SEPractices4ML_PRACTICES_ANEXOS")]
    public class PracticesAnexoEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("IdAnexoPractice")]
        public int Id { get; set; }

        [Column("Name")]
        [Required]
        public string Name { get; set; }

        [Column("ObjetoAnexo")]
        [Required]
        public string ObjetoAnexo { get; set; }

        public virtual PracticesEntity Practices { get; set; }
        [Required]
        [Column("IdPractice")]
        [ForeignKey(nameof(Practices))]
        public int IdPractice { get; set; }

        [Column("ExtensaoAnexo")]
        [Required]
        public string ExtensaoAnexo { get; set; }

    }
}
