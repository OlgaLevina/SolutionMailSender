using MailSenderLib.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace MailSender
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private List<IPlagin> _Plugins;

        //protected override async void OnStartup(StartupEventArgs e)
        //{
        //    base.OnStartup(e);
        //    _Plugins = await Task.Run(() => GetPlugins()).ConfigureAwait(false);
        //    if(_Plugins.Count==0) return;

        //    await InitializePluginsAsync(_Plugins).ConfigureAwait(false);
        //    await StartPluginsAsync(_Plugins).ConfigureAwait(false);
        //}

        private List<IPlagin> GetPlugins()
        {
            const string plugins_dir = "Plugins";
            var directory = new DirectoryInfo(plugins_dir);
            var result=new List<IPlagin>();
            if (!directory.Exists) return result;
            foreach (var dll in directory.EnumerateFiles("*.dll"))
            {
                var plugin_dll = Assembly.LoadFile(dll.FullName);
                foreach (var plugin_type in plugin_dll.GetTypes().Where(t=>t.GetInterfaces().Any(i=> i==typeof(IPlagin))))
                {
                    var plugin = Activator.CreateInstance(plugin_type) as IPlagin;
                    if (plugin is null) continue;
                    result.Add(plugin);
                }
            }

            return result;
        }

        private static async Task InitializePluginsAsync(IEnumerable<IPlagin> Plugins)
        {
            foreach (var plugin in Plugins)
            {
                try
                {
                    await plugin.InitializeAsync();
                }
                catch (Exception e)
                {
                    Trace.TraceError($"Error on initialization of {plugin.GetType()}: {e}");
                }

            }
        }

        private static async Task StartPluginsAsync(IEnumerable<IPlagin> Plugins)
        {
            foreach (var plugin in Plugins)
            {
                try
                {
                    await plugin.StartAsync();
                }
                catch (Exception e)
                {
                    Trace.TraceError($"Error on start of {plugin.GetType()}: {e}");
                }

            }
        }

        private static async Task StopPluginsAsync(IEnumerable<IPlagin> Plugins)
        {
            foreach (var plugin in Plugins)
            {
                try
                {
                    await plugin.StopAsync();
                }
                catch (Exception e)
                {
                    Trace.TraceError($"Error on finish of {plugin.GetType()}: {e}");
                }

            }
        }

        //protected override async void OnExit(ExitEventArgs e)
        //{
        //    base.OnExit(e);
        //    await StopPluginsAsync(_Plugins).ConfigureAwait(false);
        //}
    }
}
