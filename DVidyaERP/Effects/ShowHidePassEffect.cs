using System;
using Xamarin.Forms;

namespace DVidyaERP
{
	public class ShowHidePassEffect : RoutingEffect
	{
        public string EntryText { get; set; }
		public ShowHidePassEffect() : base("Xamarin.ShowHidePassEffect"){}
	}

}
