using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using MailSenderLib.Data.LinqToSQL;

namespace MailSender
{
    class MailScheduler
    {
        public DispatcherTimer EamilTimer { get; set; } = new DispatcherTimer();
        public EmailSendServiceClass EmailSender { get; set; }
        public DateTime EnailDateTime;
        public IQueryable<Recipient> emails;

        public TimeSpan GetSendTime(string strSendTime)
        {
            TimeSpan tsSendTime = new TimeSpan();
            try { tsSendTime = TimeSpan.Parse(strSendTime); }
            catch { }
            return tsSendTime;
        }

        public void SendEmails(DateTime dtSend, EmailSendServiceClass emailSender, IQueryable<Recipient> emails)
        {
            this.EmailSender = emailSender; 
           // this.dtSend = dtSend; this.emails = emails; timer.Tick += Timer_Tick;
           // timer.Interval = new TimeSpan(0, 0, 1); timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
          //  if (dtSend.ToShortTimeString() == DateTime.Now.ToShortTimeString())
          //  { emailSender.SendMails(emails); timer.Stop(); MessageBox.Show("Отправлены."); }
        }

    }
}
