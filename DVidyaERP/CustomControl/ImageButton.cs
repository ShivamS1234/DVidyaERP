using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DVidyaERP
{
    public class ImageButtonRefresh : Image
    {
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create("Command", typeof(ICommand), typeof(ImageButtonRefresh), null);

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create("CommandParameter", typeof(object), typeof(ImageButtonRefresh), null);

        public event EventHandler ItemTapped = (e, a) => { };

        public ImageButtonRefresh()
        {
            Initialize();
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        private ICommand TransitionCommand
        {
            get
            {
                return new Command(async () =>
                {
                    AnchorX = 0.48;
                    AnchorY = 0.48;
                    await this.ScaleTo(1.2, 10);
                    await this.RelRotateTo(360, 2000);
                    //Rotation=0;
                    await this.ScaleTo(1, 10);

                    Command?.Execute(CommandParameter);

                    ItemTapped(this, EventArgs.Empty);
                });
            }
        }

        public void Initialize()
        {
            GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = TransitionCommand
            });
        }
    }
}