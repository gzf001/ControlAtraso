using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ControlAtraso.Controls
{
    public class TextBox : System.Windows.Controls.TextBox
    {
        public bool OnlyNumbers
        {
            get;
            set;
        }

        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            if (this.OnlyNumbers)
            {
                long ascci = Convert.ToInt64(Convert.ToChar(e.Text));

                if (ascci >= 48 && ascci <= 57)
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }

            base.OnPreviewTextInput(e);
        }

        protected override void OnIsKeyboardFocusedChanged(DependencyPropertyChangedEventArgs e)
        {
            System.Windows.Media.SolidColorBrush scb = new System.Windows.Media.SolidColorBrush();

            scb.Color = new System.Windows.Media.Color
            {
                A = 255,
                R = 255,
                G = 255,
                B = 204
            };

            this.Background = scb;

            base.OnIsKeyboardFocusedChanged(e);
        }

        protected override void OnLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            System.Windows.Media.SolidColorBrush scb = new System.Windows.Media.SolidColorBrush();

            scb.Color = new System.Windows.Media.Color
            {
                A = 255,
                R = 255,
                G = 255,
                B = 255
            };

            this.Background = scb;

            base.OnLostKeyboardFocus(e);
        }
    }
}
