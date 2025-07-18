using Sandbox;

namespace TexGenPlus.Effects;

[Icon( "filter_tilt_shift" )]
public class HueEffect : TextureGeneratorEffect
{
	[Property, KeyProperty, Range( 0, 360 )]
	public float Amount { get; set; } = 0;

	public override Bitmap Apply( Bitmap bitmap )
	{
		bitmap.Adjust( hueDegrees: Amount );
		return bitmap;
	}

	public override int GetHashCode()
	{
		return System.HashCode.Combine( Amount );
	}
}
