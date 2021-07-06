using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace NewWorkTracking.Behavior
{
    /// <summary>
    /// Window drag behavior
    /// </summary>
    class WindowBehavior : Behavior<Window>
    {
        // Dialog box variable 
        public static Window window;

        protected override void OnAttached()
        {
            base.OnAttached();

            // Subscribe to the event of holding the left mouse button on the top panel 
            this.AssociatedObject.MouseLeftButtonDown += TopPanel_MouseLeftButtonDown;
        }

        // Event handler for holding the left mouse button on the top panel 
        private void TopPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Window)
            {
                window = sender as Window;
            }

            // Method for dragging the window
            window.DragMove();
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
        }
    }
}
