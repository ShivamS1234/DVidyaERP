
using System.Text.RegularExpressions;
using Xamarin.Forms;  
namespace DVidyaERP
{  
    public class PasswordValidationBehavior : Behavior<Entry>  
    {  
        const string passwordRegex = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{4,}$";  
  
  
        protected override void OnAttachedTo(Entry bindable)
        {  
            bindable.TextChanged += HandleTextChanged;  
            base.OnAttachedTo(bindable);  
        }  
  
        void HandleTextChanged(object sender, TextChangedEventArgs e)
        {  
            bool IsValid = false;  
            IsValid = (Regex.IsMatch(e.NewTextValue.Trim(), passwordRegex));  
            ((Entry)sender).TextColor = IsValid? Color.WhiteSmoke : Color.Red;  
        }  
  
        protected override void OnDetachingFrom(Entry bindable)
        {  
            bindable.TextChanged -= HandleTextChanged;  
            base.OnDetachingFrom(bindable);  
        }  
    }  
}  
