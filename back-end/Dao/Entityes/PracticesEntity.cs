using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEPractices4ML.Dao.Entityes
{
    [Table("SEPractices4ML_PRACTICES")]
    public class PracticesEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("IdPractice")]
        public int Id { get; set; }
               
        [Column("Name")]
        [Required]
        public string Name { get; set; }

        [Column("Description")]
        [Required]
        public string Description { get; set; }

        [Column("TypesAiMlApplications")]
        [Required]
        public string TypesAiMlApplications { get; set; }

        [Column("OrganizationContext")]
        [Required]
        public string OrganizationContext { get; set; }

        [Column("SeKnowLedge")]
        [Required]
        public string SeKnowLedge { get; set; }

        [Column("ContribuitionTypes")]
        [Required]
        public string ContribuitionTypes { get; set; }

        [Column("Link")]
        [Required]
        public string Link { get; set; }

        [Column("ReferencesDescribing")]
        [Required]
        public string ReferencesDescribing { get; set; }

        public virtual UserEntity User { get; set; }
        [Required]
        [Column("IdUser")]
        [ForeignKey(nameof(User))]
        public int IdUser { get; set; }

    }
}
