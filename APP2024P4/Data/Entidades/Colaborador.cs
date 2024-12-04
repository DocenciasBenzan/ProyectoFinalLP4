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
        public int TareaId { get; set; }
        public string CreadorEmail { get; set; } = null!;
        public string ColaboradorEmail { get; set; } = null!;
        public bool IsApproved { get; set; }
        public static Colaborador Create(string creadorEmail, string colaboradorEmail, bool isApproved,int tareaId, string userId = null!)
     => new()
     {
         UserId = userId,
         TareaId = tareaId,
         CreadorEmail = creadorEmail,
         ColaboradorEmail = colaboradorEmail,
         IsApproved = isApproved
     };
        public Colaborador ToDto() => new()
        {
            Id = this.Id,
            TareaId = this.TareaId,
            UserId = this.UserId,
            CreadorEmail = this.CreadorEmail,
            ColaboradorEmail = this.ColaboradorEmail,
            IsApproved = this.IsApproved
        };

        public bool Update(string userId, string creadorEmail, int tareaId, string colaboradorEmail, bool isApproved)
        {
            var save = false;

            if (UserId != userId)
            {
                this.UserId = userId;
                save = true;
            }
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

        [ForeignKey(nameof(TareaId))]
        public virtual Tarea? Tareas { get; set; }
    }
}