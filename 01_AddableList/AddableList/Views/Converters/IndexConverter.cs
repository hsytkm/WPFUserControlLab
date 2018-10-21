using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace AddableList.Views.Converters
{
    // How to display row numbers in a ListView?
    // https://stackoverflow.com/questions/660528/how-to-display-row-numbers-in-a-listview
    public class IndexConverter : IValueConverter
    {
        public object Convert(object value, Type TargetType, object parameter, CultureInfo culture)
        {
#if false   //Original
            var item = (ListViewItem)value;
            var listView = ItemsControl.ItemsControlFromItemContainer(item) as ListView;
            int index = listView.ItemContainerGenerator.IndexFromContainer(item);
            return index.ToString();
#else
            var listView = value as ListView;
            return listView?.ItemContainerGenerator.Items.Count;
#endif
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
