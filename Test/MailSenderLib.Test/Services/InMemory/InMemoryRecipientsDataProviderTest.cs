using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MailSenderLib.Services;
using MailSenderLib.Entities;

namespace MailSenderLib.Test.Services.InMemory
{
    /// <summary>
    /// Сводное описание для InMemoryRecipientsDataProviderTest
    /// </summary>
    [TestClass]
    public class InMemoryRecipientsDataProviderTest
    {
        [TestMethod]
        public void CreateNewRecipientInEmpryProvider()
        {
            //1
            var expected_recipient_id = 6;
            var expected_recipient_name = "Recipient 6";
            var expected_recipient_address = "recipient6@server.com";
            var expected_recipient_description = "desc";
            var data_provider = new InMemoryRecipientsDataProvider();
            var new_recipient = new Recipient
            {
                Name = "Recipient 6",
                Address = "recipient6@server.com",
                Description = "desc"
            };
            //2
            data_provider.Create(new_recipient);
            var actual_id = new_recipient.Id;
            var actual_recipient = data_provider.GetById(expected_recipient_id);

            //3
            Assert.AreEqual(expected_recipient_id, actual_id);
            Assert.AreEqual(expected_recipient_name, actual_recipient.Name);
            Assert.AreEqual(expected_recipient_address, actual_recipient.Address);
            Assert.AreEqual(expected_recipient_description, actual_recipient.Description);
        }

    }
}
