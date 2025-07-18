using Sandbox;

namespace TexGenPlus.Effects;

[Icon( "opacity" )]
public class SaturationEffect : TextureGeneratorEffect
{
	[Property, KeyProperty, Range( 0, 2 )]
	public float Amount { get; set; } = 0;

	public override Bitmap Apply( Bitmap bitmap )
	{
		bitmap.Adjust( saturation: Amount );
		return bitmap;
	}

	public override int GetHashCode()
	{
		return System.HashCode.Combine( Amount );
	}
}
