namespace test.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Center
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int Cod_Center { get; set; }

        // Foreign key to Municipality
        public int Cod_Parish { get; set; }

        [ForeignKey("Cod_Parish")]
        public Parish Parish { get; set; }
    }
}