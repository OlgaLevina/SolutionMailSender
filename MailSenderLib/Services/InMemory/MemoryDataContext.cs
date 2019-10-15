using MailSenderLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSenderLib.Services.InMemory
{
    public class MemoryDataContext
    {
        public List<Recipient> Recipients { get; }
        public List<Sender> Senders { get; }
        public List<Server> Servers { get; }
        public List<RecipientsList> RecipientsLists { get; }
        public List<Email> Emails { get; }
        public List<SchedularTask> SchedularTasks { get; }

        public MemoryDataContext()
        {
            Recipients = Enumerable.Range(1, 10).Select(i => new Recipient
            {
                Id = i, Name=$"Получатель {i}", Address=$"recipient{i}@server.com"
            }).ToList();
            Senders = Enumerable.Range(1, 10).Select(i => new Sender
            {
                Id = i,
                Name = $"Отправитель {i}",
                Address = $"sender{i}@server.com"
            }).ToList();
            Emails = Enumerable.Range(1, 10).Select(i => new Email
            {
                Id = i,
                Name = $"Письмо {i}",
                Body = $"Сообщение {i}"
            }).ToList();
            Servers = Enumerable.Range(1, 10).Select(i => new Server
            {
                Id = i,
                Host = $"smtp.server{i}.com",
                UserName = $"user{i}@server.com",
                Password=$"user-{}-password"
            }).ToList();

            var rnd = new Random();
            T GetRandom<T>(IList<T> items) => items[rnd.Next(0, items.Count)];
            IEnumerable<T> GetRandomItems<T>(IList<T> items, int count) => Enumerable.Range(0, count).Select(i => GetRandom(items));
            RecipientsLists = Enumerable.Range(1, 10).Select(i => new RecipientsList
            {
                Id = i,
                Name = $"Mail list {i}",
                Recipients=GetRandomItems(Recipients,rnd.Next(i,Recipients.Count)).ToList()
            }).ToList();

            SchedularTasks = Enumerable.Range(1, 10).Select(i => new SchedularTask
            {
                Id=i,
                Time=DateTime.Now.Add(TimeSpan.FromMinutes(rnd.Next(10,100))),
                Server=GetRandom(Servers),
                Sender=GetRandom(Senders),
                Email=GetRandom(Emails),
                Recipients=GetRandom(RecipientsLists)
            }).ToList();
        }
    }
}
