using Sandbox;

namespace TexGenPlus.Effects;

[Icon( "contrast" )]
public class ContrastEffect : TextureGeneratorEffect
{
	[Property, KeyProperty, Range( 0, 2 )]
	public float Amount { get; set; } = 0;

	public override Bitmap Apply( Bitmap bitmap )
	{
		bitmap.Adjust( contrast: Amount );
		return bitmap;
	}

	public override int GetHashCode()
	{
		return System.HashCode.Combine( Amount );
	}
}
