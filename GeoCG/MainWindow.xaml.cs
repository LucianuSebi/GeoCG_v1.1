using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using ConfigLoader;
using MahApps.Metro.Controls;
using Microsoft.Win32;
using Ookii.Dialogs.Wpf;
using Modules;
using CGEntity;
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
                Console.WriteLine($"GeoCG: s-a deschis districtul {districtPath}");

                refreshFileTree(districtPath);


                try
                {
                    Default.Initialize(districtPath);
                    Config.Initialize(districtPath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("EROARE in ConfigLoader: " + ex.Message);
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

        private void OnExtrageDXF(object sender, RoutedEventArgs e)
        {
            DxfProcessor.ProcessDxf();
            refreshFileTree(Config.districtPath);
        }

        private void OnGenereazaCGAuto(object sender, RoutedEventArgs e)
        {
            CGDxfEntity.Initialize();
            foreach (Document document in CGDxfEntity.documents)
            {
                CgXmlEntity CgXmlEntity = new CgXmlEntity(document);
            }

            refreshFileTree(Config.districtPath);
        }

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

        private void refreshFileTree(string filePath)
        {
            treeDistrict.Items.Clear();

            var root = new TreeViewItem
            {
                Header = Path.GetFileName(filePath),
                Tag = filePath,
                IsExpanded = true
            };
            treeDistrict.Items.Add(root);

            MWLib.PopulateTree(filePath, root);

        }
    }
}
