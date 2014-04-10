using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Text.RegularExpressions;

namespace Nosc.UI
{
    public class ControlsValidator : FrameworkElement
    {
        static IDictionary<TextBox, Brush> _textBox = new Dictionary<TextBox, Brush>();

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("TextMail", typeof(TextBox), typeof(ControlsValidator),
            new PropertyMetadata(TextChanged));

        public TextBox TextMail
        {
            get { return (TextBox)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        private static void TextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pv = (ControlsValidator)d;
            _textBox[pv.TextMail] = pv.TextMail.BorderBrush;
            pv.TextMail.LostFocus += (obj, evt) =>
            {
                if (!Regex.IsMatch(pv.TextMail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
                {
                    pv.TextMail.BorderBrush = new SolidColorBrush(Colors.Red);
                    pv.TextMail.ToolTip = " El correo electrónico introducido no es válido..";

                }
                else
                {
                    pv.TextMail.BorderBrush = _textBox[pv.TextMail];
                    pv.TextMail.ToolTip = null;
                }
            };
        }
    }
}
