using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MailSender.ValidationRules
{
    public class RegExValidationRule : ValidationRule
    {
        private Regex _Regex;
        public string Pattern
        {
            get => _Regex?.ToString();
            set => _Regex = value is null ? null : value == string.Empty ? null : new Regex(value);
        }

        public bool AllowNul { get; set; } = true;

        public string ErrorMessage { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo) //вводимые данные, прежде чем поступить в привязку, поступают сюда
        {
            if (value is null)
                return AllowNul
                    ? ValidationResult.ValidResult
                    : new ValidationResult(false, ErrorMessage ?? "Отсутствует ссыдка на строку");
            if (_Regex is null) return ValidationResult.ValidResult;
            if (!(value is string str)) str = value.ToString();

            return _Regex.IsMatch(str)
                ? ValidationResult.ValidResult
                : new ValidationResult(false, ErrorMessage ?? $"Строка не удовлетворяет регулярному выражению {Pattern}");
            //return new ValidationResult(true, "Сообщение об ошибке"); - считаем валидным
            //return new ValidationResult(false, "Сообщение об ошибке"); - если что-то не так
            //return ValidationResult.ValidResult;
        }
    }
}
