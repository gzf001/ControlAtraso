using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ControlAtraso.Controls
{
    public class ComboBox : System.Windows.Controls.ComboBox
    {
        public void Clear()
        {
            this.Items.Clear();

            ComboBoxItem comboBoxItem = new ComboBoxItem();

            comboBoxItem.Content = "[Seleccione]";
            comboBoxItem.Tag = string.Empty;
            comboBoxItem.IsSelected = true;

            this.Items.Add(comboBoxItem);
        }

        protected override void OnInitialized(EventArgs e)
        {
            this.SelectedIndex = 0;

            base.OnInitialized(e);
        }

        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            this.SelectedIndex = 0;

            base.OnItemsSourceChanged(oldValue, newValue);
        }
    }
}