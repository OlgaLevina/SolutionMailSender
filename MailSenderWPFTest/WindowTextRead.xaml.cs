using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO.Compression;//для работы с архивами
using System.Threading;

namespace MailSenderWPFTest
{
    /// <summary>
    /// Логика взаимодействия для WindowTextRead.xaml
    /// </summary>
    public partial class WindowTextRead : Window
    {
        public WindowTextRead()
        {
            InitializeComponent();
        }

        private CancellationTokenSource ProcessCancel;

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var cts = new CancellationTokenSource();
            Interlocked.Exchange(ref ProcessCancel, cts)?.Cancel();//если захотим сменить файл в процессе, то созданный новый тоукен обновит общий тоукен, что отменит предыдущую операцию 
            var cancel = cts.Token;
            var file_dialog = new OpenFileDialog
            {
                Title = "File of data",
                Filter = "zip-архивы(*.zip)|*.zip|Все файлы(*.*)|*.*",
                InitialDirectory = Environment.CurrentDirectory
            };
            if (file_dialog.ShowDialog() != true) return;
            var data_file_name = file_dialog.FileName;
            if (!File.Exists(data_file_name)) return;
            var tasks = new List<Task<int>>();
            int[] result;
            IProgress<double> progress = new Progress<double>(p => Progress.Value = p * 100);//особенность - всегда выполняет свой делегат только в том потоке, где создан - не нужно думать о потоках
            try
            {
                using (var zip = new ZipArchive(file_dialog.OpenFile()))
                {
                    var total_length = zip.Entries.Sum(el => el.Length);
                    foreach (var zip_entry in zip.Entries.Where(el => el.Name.EndsWith(".txt")).Take(1)) // тоолько в 1м файле
                        tasks.Add(GetWordsCountAsync(zip_entry.Open(),zip_entry.Length,progress, cancel));
                    result = await Task.WhenAll(tasks).ConfigureAwait(true);//здесь нужно отправить в основной поток, чтобы не было проблем
                }

                ResultText.Text = $"Количество {result.Sum()}";
            }
            catch (OperationCanceledException)
            {
                ResultText.Text="Canceled";
            }            
        }
        private async void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            ProcessCancel.Cancel();//вызов метода отмены преыдущей операции
        }
        private static async Task<int> GetWordsCountAsync(Stream stream,long length, IProgress<double> progress, CancellationToken cancelToken)
        {
            var reader = new StreamReader(stream);
            var words_count = 0;
            var seperators = new[] { ' ' };
            var position = 01;
            while(!reader.EndOfStream)
            {
                cancelToken.ThrowIfCancellationRequested();//отмена выполнения ассинхронных задач
                var line =await reader.ReadLineAsync().ConfigureAwait(true);//если не добавлять конфигурацию, то то что после эвэйта будет выполняться в том потоке, где была запущена асинхронная команда - замедление. Установив конфигурацию - дальнейшее выполноение кода после эвэйта - в произвольном потоке - ускорение. но обратиться к основному потоку (и кнопка) соответственно уже будет нельзя. Здесь - это не проблема, если бы мы не работали с зип-архивом - который не терпит, чтобы его читали асинхронно-параллельно
                position += line.Length;
                await Task.Delay(1, cancelToken).ConfigureAwait(true);//асинхронный аналог слип
                if (string.IsNullOrWhiteSpace(line)) continue;
                words_count += line.Split(seperators, StringSplitOptions.RemoveEmptyEntries).Length;
                progress?.Report((double)position / length );
            }
            return words_count;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
