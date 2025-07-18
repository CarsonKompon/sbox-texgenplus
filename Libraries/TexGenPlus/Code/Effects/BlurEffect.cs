using Sandbox;

namespace TexGenPlus.Effects;

[Icon( "blur_on" )]
public class BlurEffect : TextureGeneratorEffect
{
	[Property, KeyProperty, Range( 0, 32 )]
	public float Amount { get; set; } = 0;

	public override Bitmap Apply( Bitmap bitmap )
	{
		bitmap.Blur( Amount );
		return bitmap;
	}

	public override int GetHashCode()
	{
		return System.HashCode.Combine( Amount );
	}
}
