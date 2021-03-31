using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEPractices4ML.Dao.Entityes
{
    [Table("SEPractices4ML_NOTIFICATIONS")]
    public class NotificationEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("IdNotification")]
        public int Id { get; set; }

        public virtual UserEntity User { get; set; }
        [Column("IdUser")]
        [ForeignKey(nameof(User))]
        public int? IdUsuario { get; set; }

        [Column("LevelNotification")]
        public int? LevelNotification { get; set; }

        [Column("DescriptionNotification")]
        public String DescricaoNotificacao { get; set; }

        [Column("IsConcluida")]
        public int? IsConcluida { get; set; }

        [Column("TypeNotification")]
        public int? TipoNotificacao { get; set; }

    }
}
