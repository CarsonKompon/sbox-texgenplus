using Sandbox;

namespace TexGenPlus.Effects;

[Icon( "refresh" )]
public class RotateEffect : TextureGeneratorEffect
{
	[Property, KeyProperty]
	public float Amount { get; set; } = 0;

	public override Bitmap Apply( Bitmap bitmap )
	{
		return bitmap.Rotate( Amount );
	}

	public override int GetHashCode()
	{
		return System.HashCode.Combine( Amount );
	}
}
