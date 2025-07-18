using Sandbox;
using Sandbox.UI;

namespace TexGenPlus.Effects;

[Icon( "border_outer" )]
public class PaddingEffect : TextureGeneratorEffect
{
	[Property, KeyProperty, Step( 1 )]
	public Margin Padding { get; set; }

	public override Bitmap Apply( Bitmap bitmap )
	{
		bitmap.InsertPadding( Padding );
		return bitmap;
	}

	public override int GetHashCode()
	{
		return System.HashCode.Combine( Padding );
	}
}
