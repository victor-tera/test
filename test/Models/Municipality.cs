namespace test.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Municipality
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int Cod_Municipality { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        // Foreign key to State
        public int Cod_State { get; set; }

        [ForeignKey("Cod_State")]
        public State State { get; set; }
    }
}
