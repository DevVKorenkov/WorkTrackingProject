using NewWorkTracking.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Interactivity;


namespace NewWorkTracking.Behavior
{
    /// <summary>
    /// Behavior of the DataGrid table 
    /// </summary>
    class DataGridBehavior : Behavior<DataGrid>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            // Subscribe to the dataGrid selection change event 
            AssociatedObject.SelectionChanged += AssociatedObject_SelectionChanged;
        }

        // Handler for the DataGrid selection change event 
        private void AssociatedObject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is DataGrid grid)
            {
                // Check for an empty object
                if (grid.SelectedItem != null)
                {
                    grid.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        //grid.ScrollIntoView(grid.SelectedItem, null);
                    }));
                }

                // Forming a collection of selected objects
                AllWorksViewModel.SelectionChanged(grid.SelectedItems);
            }
        }
    }
}
