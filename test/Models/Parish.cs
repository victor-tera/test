namespace test.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Parish
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int Cod_Parish { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        // Foreign key to Municipality
        public int Cod_Municipality { get; set; }

        [ForeignKey("Cod_Municipality")]
        public Municipality Municipality { get; set; }
    }
}