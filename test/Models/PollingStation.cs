namespace test.Models

{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class PollingStation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int Cod_PollingStation { get; set; }

        [Required]
        public int PollingNumber { get; set; }

        [Required]
        public int ValidVotes { get; set; }

        [Required]
        public int NullVotes { get; set; }

        [Required]
        public int EG { get; set; }

        [Required]
        public int NM { get; set; }

        [Required]
        public int LB { get; set; }

        [Required]
        public int JABE { get; set; }

        [Required]
        public int JOBR { get; set; }

        [Required]
        public int AE { get; set; }

        [Required]
        public int CF { get; set; }

        [Required]
        public int DC { get; set; }

        [Required]
        public int EM { get; set; }

        [Required]
        public int BERA { get; set; }

        [Required]
        [MaxLength(255)]
        public string Record { get; set; }

        [ForeignKey("Cod_Center")]
        public Center Center { get; set; }
    }
}
