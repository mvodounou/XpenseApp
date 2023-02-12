using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("Xpense")]
[assembly: ExportEffect(typeof(Xpense.iOS.Effects.BorderEffect), nameof(Xpense.iOS.Effects.BorderEffect))]
namespace Xpense.iOS.Effects
{
    public class BorderEffect : PlatformEffect
    {
        public BorderEffect()
        {
        }

        protected override void OnAttached()
        {
            try
            {
                Control.Layer.BorderWidth = 1f;
                Control.Layer.CornerRadius = 5;
                Control.Layer.BorderColor = Color.Gray.ToCGColor();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
            }
        }

        protected override void OnDetached()
        {
            throw new NotImplementedException();
        }
    }
}

