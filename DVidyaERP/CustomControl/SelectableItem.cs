using Xamarin.Forms;

namespace DVidyaERP.CustomControl
{
    public class SelectableItem : BindableObject
    {
        public static readonly BindableProperty DataProperty =
            BindableProperty.Create(nameof(Data), typeof(object), typeof(SelectableItem), null);

        public static readonly BindableProperty IsSelectedProperty =
            BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(SelectableItem), false);

        public static readonly BindableProperty BackgroundColorProperty =
            BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(SelectableItem), Color.White );
        
		public SelectableItem(object data)
		{
			Data = data;

		}

		public SelectableItem(object data, bool isSelected)
		{
			Data = data;
			IsSelected = isSelected;
            if (isSelected == true) { BackgroundColor = Color.White; } else { BackgroundColor = Color.WhiteSmoke; }; 
		}
        public SelectableItem(object data, bool isSelected, Color backgroundColor)
        {
            Data = data;
            IsSelected = isSelected;
            if (isSelected == true)  { BackgroundColor = Color.White; } else { BackgroundColor = Color.WhiteSmoke; } ; 
            //BackgroundColor = backgroundColor;
        }

		public object Data
		{
			get { return GetValue(DataProperty); }
			set { SetValue(DataProperty, value); }
		}

		public bool IsSelected
		{
			get { return (bool)GetValue(IsSelectedProperty); }
			set { SetValue(IsSelectedProperty, value); }
		}
        public Color BackgroundColor
        {
            get { return (Color)GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); }
        }
	}

	public class SelectableItem<T> : SelectableItem
	{
		public SelectableItem(T data)
			: base(data)
		{
		}

		public SelectableItem(T data, bool isSelected)
			: base(data, isSelected)
		{
		}
        public SelectableItem(T data, bool isSelected,Color backgroundColor)
            : base(data, isSelected,backgroundColor)
        {
        }

		public new T Data
		{
			get { return (T)base.Data; }
			set { base.Data = value; }
		}
	}
}
