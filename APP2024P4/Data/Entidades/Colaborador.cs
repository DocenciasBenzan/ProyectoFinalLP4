﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APP2024P4.Data.Entidades
{
    [Table("Colaboradores")]
    public class Colaborador
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public virtual ICollection<Tarea>? Tareas { get; set; }
        public string CreadorEmail { get; set; } = null!;
        public string ColaboradorEmail { get; set; } = null!;
        public bool IsApproved { get; set; }

        public static Colaborador Create(string userId, string creadorEmail, string colaboradorEmail, bool isApproved)
     => new()
     {
         UserId = userId,
         CreadorEmail = creadorEmail,
         ColaboradorEmail = colaboradorEmail,
         IsApproved = isApproved
     };

        public bool Update(string userId, string creadorEmail, string colaboradorEmail, bool isApproved)
        {
            var save = false;

            if (UserId != userId)
            {
                this.UserId = userId;
                save = true;
            }
            if (CreadorEmail != creadorEmail)
            {
                this.CreadorEmail = creadorEmail;
                save = true;
            }
            if (ColaboradorEmail != colaboradorEmail)
            {
                this.ColaboradorEmail = colaboradorEmail;
                save = true;
            }
            if (IsApproved != isApproved)
            {
                this.IsApproved = isApproved;
                save = true;
            }
            return save;
        }
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser? User { get; set; }
    }
}