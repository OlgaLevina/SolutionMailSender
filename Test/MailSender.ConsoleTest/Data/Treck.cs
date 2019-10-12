using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MailSender.ConsoleTest.Data
{
    [Table("Trecks")]
    public class Treck
    {
        [Key]//насильное назначение свойства идентификатором, если имя не удовлетворяет условиям, в данном случае не требуется
        public int Id { get; set; }//св-ва с именами Id или ИмяТаблицыId (TreckId) - автоматически определяюсь фрэймворком как идентификатор. При этом он может быть целым либо строкой
        [Required]
        public string Name { get; set; }
        public int Length { get; set; }
        public virtual Artist Artist{get;set;}
        //либо через свойство и атрибут навигационного св-ва
        //public int ArtistId { get; set; }
        //[ForeignKey(nameof(ArtistId))]
        //public virtual Artist Artist { get; set; }
    }
}
