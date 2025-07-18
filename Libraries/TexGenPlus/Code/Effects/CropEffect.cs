using Sandbox;
using Sandbox.UI;

namespace TexGenPlus.Effects;

[Icon( "crop" )]
public class CropEffect : TextureGeneratorEffect
{
	[Property, KeyProperty, Step( 1 )]
	public Margin Cropping { get; set; }

	public override Bitmap Apply( Bitmap bitmap )
	{
		var newRect = new Rect( Cropping.Left, Cropping.Top, bitmap.Width - Cropping.Right - Cropping.Left, bitmap.Height - Cropping.Bottom - Cropping.Top );
		if ( newRect.Width > 0 && newRect.Height > 0 )
		{
			bitmap = bitmap.Crop( newRect.SnapToGrid() );
		}
		return bitmap;
	}

	public override int GetHashCode()
	{
		return System.HashCode.Combine( Cropping );
	}
}
