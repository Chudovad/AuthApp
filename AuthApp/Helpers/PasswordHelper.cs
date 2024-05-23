using System.Windows.Controls;
using System.Windows;

namespace AuthApp.Helpers
{
    public static class PasswordHelper
    {
        public static readonly DependencyProperty PasswordProperty =
           DependencyProperty.RegisterAttached("Password", typeof(string), typeof(PasswordHelper), new FrameworkPropertyMetadata(string.Empty, OnPasswordPropertyChanged));

        public static string GetPassword(DependencyObject obj)
        {
            return (string)obj.GetValue(PasswordProperty);
        }

        public static void SetPassword(DependencyObject obj, string value)
        {
            obj.SetValue(PasswordProperty, value);
        }

        private static void OnPasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PasswordBox passwordBox && !passwordBox.Password.Equals(e.NewValue))
            {
                passwordBox.Password = (string)e.NewValue;
            }
        }

        private static void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            SetPassword(passwordBox, passwordBox.Password);
        }

        public static readonly DependencyProperty AttachProperty =
            DependencyProperty.RegisterAttached("Attach", typeof(bool), typeof(PasswordHelper), new PropertyMetadata(false, Attach));

        public static bool GetAttach(DependencyObject obj)
        {
            return (bool)obj.GetValue(AttachProperty);
        }

        public static void SetAttach(DependencyObject obj, bool value)
        {
            obj.SetValue(AttachProperty, value);
        }

        private static void Attach(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PasswordBox passwordBox)
            {
                if ((bool)e.OldValue)
                {
                    passwordBox.PasswordChanged -= OnPasswordChanged;
                }
                if ((bool)e.NewValue)
                {
                    passwordBox.PasswordChanged += OnPasswordChanged;
                }
            }
        }
    }
}
