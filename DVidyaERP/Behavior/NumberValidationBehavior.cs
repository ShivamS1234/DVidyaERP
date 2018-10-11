using Xamarin.Forms;  
  
namespace DVidyaERP
{  
    public class NumberValidationBehavior : Behavior<Entry>  
    {  
        protected override void OnAttachedTo(Entry entry)
        {  
            entry.TextChanged += OnEntryTextChanged;  
            base.OnAttachedTo(entry);  
        }  
  
        protected override void OnDetachingFrom(Entry entry)
        {  
            entry.TextChanged -= OnEntryTextChanged;  
            base.OnDetachingFrom(entry);  
        }  
  
        void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {  
            long result;  
             
            bool isValid = long.TryParse(args.NewTextValue, out result);  
  
            ((Entry)sender).TextColor = isValid? Color.WhiteSmoke : Color.Red;  
        }  
    }  
}  

   