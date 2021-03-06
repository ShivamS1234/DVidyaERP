﻿using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace DVidyaERP.CustomControl
{
    public class CustomPicker : Picker
    {
       

        public static readonly BindableProperty ImageProperty =
            BindableProperty.Create(nameof(Image), typeof(string), typeof(CustomPicker), string.Empty);

        public string Image
        {
            get { return (string)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }
    }
}