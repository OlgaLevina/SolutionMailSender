using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MailSender.ConsoleTest.Data
{
    [Table("Artists")]
    public class Artist
    {
        public int Id { get; set; }
        [Required, MinLength(3)]
        public string Name { get; set; }
        public DateTime BirthDay { get; set; }
        public virtual ICollection<Treck> Trecks { get; set; }
    }

    [Table("Album")]
    public class Album
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Treck> Trecks { get; set; }
    }
}
