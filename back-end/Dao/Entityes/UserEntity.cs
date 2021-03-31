using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEPractices4ML.Dao.Entityes
{
    [Table("SEPractices4ML_USER")]
    public class UserEntity
    {
        public virtual ICollection<MembersEntity> Members { get; set; }
        public UserEntity()
        {
            Members = new HashSet<MembersEntity>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("IdUser")]
        public int Id { get; set; }
      
        [Column("Email")]
        public string Email { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Password")]
        public string Password { get; set; }

        [Column("UserImage")]
        public string UserImage { get; set; }

        [Column("UserName")]
        public string UserName { get; set; }

        [Column("TipoUsuario")]
        public int? TipoUsuario { get; set; }

        [Column("UsuarioHabilitado")]
        public int? UsuarioHabilitado { get; set; }

    }
}
