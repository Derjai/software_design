using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Update.Models
{
    [Table("persona")]
    public class Persona
    {
        [Column("tipo_doc")]
        public string Tipo_Doc { get; set; } = null!;

        [Key]
        [StringLength(10)]
        [Column("num_doc")]
        public string Num_Doc { get; set; } = null!;

        [StringLength(30)]
        [Column("nombre")]
        public string Nombre { get; set; } = null!;

        [StringLength(30)]
        [Column("seg_nombre")]
        public string? Seg_Nombre { get; set; }

        [StringLength(60)]
        [Column("appelidos")]
        public string Apellidos { get; set; } = null!;

        [Column("fecha_nacimiento")]
        public DateTime Fecha_Nacimiento { get; set; }

        [Column("genero")]
        public string Genero { get; set; } = null!;

        [StringLength(255)]
        [Column("correo")]
        public string Correo { get; set; } = null!;

        [StringLength(10)]
        [Column("celular")]
        public string Celular { get; set; } = null!;

        [Column("foto")]
        public byte[]? Foto { get; set; }
    }
}
