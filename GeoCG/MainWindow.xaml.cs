using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using ConfigLoader;
using MahApps.Metro.Controls;
using Microsoft.Win32;
using Ookii.Dialogs.Wpf;

namespace GeoCG
{
    public partial class MainWindow : MetroWindow
    {
        private const int MaxStatusMessages = 100;

        public MainWindow()
        {
            InitializeComponent();
            Console.SetOut(new StatusTextWriter(this));
        }

        internal void AddStatusMessage(string message)
        {
            MWLib.AddStatusMessage(statusMessages, message, MaxStatusMessages);
        }

        private void OnDeschideDistrict(object sender, RoutedEventArgs e)
        {
            var dlg = new VistaFolderBrowserDialog
            {
                Description = "Selectează folderul 'district'",
                UseDescriptionForTitle = true
            };

            if (dlg.ShowDialog() == true)
            {
                string districtPath = dlg.SelectedPath;
                Console.WriteLine($"Deschis district: {districtPath}");

                treeDistrict.Items.Clear();

                var root = new TreeViewItem
                {
                    Header = Path.GetFileName(districtPath),
                    Tag = districtPath,
                    IsExpanded = true
                };
                treeDistrict.Items.Add(root);

                MWLib.PopulateTree(districtPath, root);

                try
                {
                    Default.Initialize(districtPath + "/defaultValue.xml");
                    Config.Initialize(districtPath + "/config.xml");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in DxfProcessor: " + ex.Message);
                }

                menuUnelte.Visibility = Visibility.Visible;
                optDefault.Visibility = Visibility.Visible;
                optSave.Visibility = Visibility.Visible;
                optSettings.Visibility = Visibility.Visible;
                optDeschideDistrict.Visibility = Visibility.Collapsed;
            }
        }

        private void OnCreeazaDistrict(object sender, RoutedEventArgs e)
            => Console.WriteLine("Creare district nou...");

        private void OnSalveazaDistrict(object sender, RoutedEventArgs e)
        {
            var dlg = new SaveFileDialog { Filter = "District Files (*.dist)|*.dist" };
            if (dlg.ShowDialog() == true)
                Console.WriteLine($"Salvat district în: {dlg.FileName}");
        }

        private void OnSetari(object sender, RoutedEventArgs e)
            => Console.WriteLine("Deschidere setări...");

        private void OnValoriDefault(object sender, RoutedEventArgs e)
            => Console.WriteLine("Restaurare valori implicite...");

        private void OnPresets(object sender, RoutedEventArgs e)
            => Console.WriteLine("Presets selectate...");

        private void OnFind(object sender, RoutedEventArgs e)
            => Console.WriteLine("Căutare în district...");

        private void OnTutoriale(object sender, RoutedEventArgs e)
            => Console.WriteLine("Deschidere tutoriale...");

        private void OnRaporteazaEroare(object sender, RoutedEventArgs e)
            => Console.WriteLine("Raportare eroare...");

        private void GridSplitter_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e) { }

        private class StatusTextWriter : System.IO.TextWriter
        {
            private readonly MainWindow _window;

            public StatusTextWriter(MainWindow window) => _window = window;

            public override System.Text.Encoding Encoding => System.Text.Encoding.UTF8;

            public override void WriteLine(string value)
            {
                _window.Dispatcher.BeginInvoke(
                    new Action(() => _window.AddStatusMessage(value)),
                    System.Windows.Threading.DispatcherPriority.Background);
            }
        }
    }
}
