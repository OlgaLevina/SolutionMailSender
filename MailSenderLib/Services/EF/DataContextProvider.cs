using MailSenderLib.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSenderLib.Services.EF
{
   public class DataContextProvider
    {
        public string ConnectionString { get; set; } = "MailSenderDB";
        public MailSenderDB CreateContext() => new MailSenderDB(ConnectionString); 
    }
}
