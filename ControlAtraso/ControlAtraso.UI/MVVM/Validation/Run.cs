using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ControlAtraso.UI.MVVM.Validation
{
    public class Run : ValidationRule
    {
        public string Message
        {
            get;
            set;
        }

        public string RunCuerpo
        {
            get;
            set;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrEmpty(value.ToString()))
            {
                return new System.Windows.Controls.ValidationResult(false, this.Message);
            }
            else
            {
                return System.Windows.Controls.ValidationResult.ValidResult;
            }
        }
    }
}
