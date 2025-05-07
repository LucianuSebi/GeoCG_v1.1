using System;
using System.IO;
using System.Windows.Controls;

namespace GeoCG
{
    public static class MWLib
    {
        public static void AddStatusMessage(ListBox statusBox, string message, int maxMessages)
        {
            if (statusBox.Items.Count >= maxMessages)
                statusBox.Items.RemoveAt(0);

            statusBox.Items.Add($"[{DateTime.Now:HH:mm:ss}] {message}");
            statusBox.ScrollIntoView(statusBox.Items[statusBox.Items.Count - 1]);
        }

        public static void PopulateTree(string path, TreeViewItem parentItem)
        {
            foreach (var dir in Directory.GetDirectories(path))
            {
                var subdirItem = new TreeViewItem
                {
                    Header = Path.GetFileName(dir),
                    Tag = dir,
                    IsExpanded = false
                };
                parentItem.Items.Add(subdirItem);
                PopulateTree(dir, subdirItem);
            }

            foreach (var file in Directory.GetFiles(path))
            {
                parentItem.Items.Add(new TreeViewItem
                {
                    Header = Path.GetFileName(file),
                    Tag = file
                });
            }
        }
    }
}
