using Sandbox;

namespace TexGenPlus.Effects;

[Icon( "details" )]
public class SharpenEffect : TextureGeneratorEffect
{
	[Property, KeyProperty, Range( 0, 10 )]
	public float Amount { get; set; } = 0;

	public override Bitmap Apply( Bitmap bitmap )
	{
		bitmap.Sharpen( Amount );
		return bitmap;
	}

	public override int GetHashCode()
	{
		return System.HashCode.Combine( Amount );
	}
}
