using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace AppUserPanel.Pages
{
    class PasswordBoxHelper
    {
        // DependencyProperty for binding the password
        public static readonly DependencyProperty BoundPasswordProperty =
            DependencyProperty.RegisterAttached("BoundPassword", typeof(string), typeof(PasswordBoxHelper),
                new PropertyMetadata(string.Empty, OnBoundPasswordChanged));

        // Getter for the BoundPassword property
        public static string GetBoundPassword(DependencyObject d)
        {
            return (string)d.GetValue(BoundPasswordProperty);
        }

        // Setter for the BoundPassword property
        public static void SetBoundPassword(DependencyObject d, string value)
        {
            d.SetValue(BoundPasswordProperty, value);
        }

        // Handler for when the BoundPassword property changes
        private static void OnBoundPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PasswordBox box)
            {
                // Unsubscribe from the PasswordChanged event to prevent recursion
                box.PasswordChanged -= HandlePasswordChanged;

                var newPassword = (string)e.NewValue;
                if (box.Password != newPassword)
                {
                    // Update the PasswordBox's password if it doesn't match the new value
                    box.Password = newPassword;
                }

                // Resubscribe to the PasswordChanged event
                box.PasswordChanged += HandlePasswordChanged;
            }
        }

        // Handler for when the PasswordBox's password changes
        private static void HandlePasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox box)
            {
                // Update the BoundPassword property to match the PasswordBox's password
                box.SetCurrentValue(BoundPasswordProperty, box.Password);
            }
        }
    }
}
