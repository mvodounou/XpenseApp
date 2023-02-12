using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.MacOS;

[assembly: ResolutionGroupName("Xpense")]
[assembly: ExportEffect(typeof(Xpense.MacOs.Effects.BorderEffect), nameof(Xpense.MacOs.Effects.BorderEffect))]
namespace Xpense.MacOs.Effects
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

