using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Interactivity;
using WorkTrackingLib.Models;

namespace NewWorkTracking.Behavior
{
    /// <summary>
    /// Combobox behavior when searching in an active directory 
    /// </summary>
    class ComboboxBehavior : Behavior<ComboBox>
    {
        private string search;

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.AddHandler(TextBoxBase.TextChangedEvent, new TextChangedEventHandler(ComboboxSearch));

            AssociatedObject.MouseUp += AssociatedObject_MouseUp;
        }

        private void AssociatedObject_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var combo = sender as ComboBox;
            combo.IsDropDownOpen = true;
            combo.Focus();
        }

        private async void ComboboxSearch(object sender, TextChangedEventArgs e)
        {
            var combo = sender as ComboBox;           
            var tb = (TextBox)e.OriginalSource;
            var temp = tb.Text;

            combo.IsDropDownOpen = true;

            if (tb.SelectionStart != 0)
            {
                combo.SelectedItem = null;
                tb.Text = temp;
            }

            if(!string.IsNullOrEmpty(tb.Text)) 
                tb.Select(tb.Text.Length, 0);
            else
                combo.IsDropDownOpen = false;

            if (combo.SelectedItem == null)
            {
                search = tb.Text;
                CollectionView collectionView = CollectionViewSource.GetDefaultView(combo.ItemsSource) as CollectionView;
                await Dispatcher.InvokeAsync(() => collectionView.Filter = s => ((BaseTableModel)s).Name.ToLower().Contains(tb.Text.ToLower()));
            }
        }
    }
}
