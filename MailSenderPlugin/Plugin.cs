using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSenderLib.Services.Interfaces;
using System.Windows.Forms;

namespace MailSenderPlugin
{
    public class Plugin: IPlagin
    {
        public async Task InitializeAsync()
        {
            await Task.Run(()=>MessageBox.Show("Initialized"));
        }

        public async Task StartAsync()
        {
            await Task.Run(() => MessageBox.Show("Started"));
        }

        public async Task StopAsync()
        {
            await Task.Run(() => MessageBox.Show("Stopped"));
        }
    }
}
