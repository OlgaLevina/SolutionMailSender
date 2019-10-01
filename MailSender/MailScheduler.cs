using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using MailSenderLib.Data.LinqToSQL;

namespace MailSender
{
    class MailScheduler <T>
    {
        public DispatcherTimer EmailTimer { get; set; } = new DispatcherTimer();
        public EmailSendServiceClass EmailSender { get; set; }
        public DateTime EmailDateTime;
        //public IQueryable<Recipient> Emails;
        public IEnumerable<T> Emails;
        public IResult ResultWindow;

        public TimeSpan GetSendTime(string strSendTime)
        {
            TimeSpan tsSendTime = new TimeSpan();
            try { tsSendTime = TimeSpan.Parse(strSendTime); }
            catch { }
            return tsSendTime;
        }

        public void PlanSendEmails(DateTime dtSend, EmailSendServiceClass emailSender, IEnumerable<T> emails, IResult result)
        {
            this.EmailSender = emailSender; 
            this.EmailDateTime = dtSend; this.Emails = emails; EmailTimer.Tick += Timer_Tick;
            EmailTimer.Interval = new TimeSpan(0, 0, 1); EmailTimer.Start();
            this.ResultWindow = result;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (EmailDateTime.ToShortTimeString() == DateTime.Now.ToShortTimeString())
            { EmailSender.Send(Emails); EmailTimer.Stop(); ResultWindow.Show("Was sent!"); }
        }

    }
}
