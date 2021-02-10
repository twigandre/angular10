using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_end_net_core_5.Dao.Entityes
{
    [Table("LOGIN_SENHA")]
    public class LoginEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("LOGIN")]
        public string Login { get; set; }

        [Required]
        [Column("SENHA")]
        public string Senha { get; set; }

    }
}
