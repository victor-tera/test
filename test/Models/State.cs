namespace test.Models

{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class State
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int Cod_State { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}
