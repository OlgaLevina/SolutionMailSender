using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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


        [DefaultValue(typeof(TreckStyle), "None")]//пробывал установить значение по умолчанию - не сработало
        public TreckStyle? Style { get; set; } // ? - значит, что стиль может отсутствовать

        //public TreckDescription? Description { get; set; } = TreckDescription.None; //столбец добавился, но в данных его нет. Обновила конструктор таблицы треков, потом еще раз зашла в данные таблицы - description появился со значениями по умолчанию, но при этом добавились новые строки и значения по умолчанию - только для них, появились и новые артисты (3 шт)

        //public TreckElse? Else { get; set; } // ? - значит, что стиль может отсутствовать

        public virtual ICollection<Album> Albums { get; set; }//отношение многие ко многим - в 6ке - автоматически
    }

    public enum TreckStyle: byte
    {
        None, Pop, Reggi, Rock, Metal, Classc
    }

    //public enum TreckDescription : byte
    //{
    //    None, Pop, Reggi, Rock, Metal, Classc
    //}
    //public enum TreckElse : byte
    //{
    //    None, Pop, Reggi, Rock, Metal, Classc
    //}
}
