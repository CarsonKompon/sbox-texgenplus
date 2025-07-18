using Sandbox;

namespace TexGenPlus.Effects;

[Icon( "colorize" )]
public class TintEffect : TextureGeneratorEffect
{
	[Property, KeyProperty]
	public Color TintColor { get; set; } = Color.White;

	public override Bitmap Apply( Bitmap bitmap )
	{
		bitmap.Tint( TintColor );
		return bitmap;
	}

	public override int GetHashCode()
	{
		return System.HashCode.Combine( TintColor.Hex );
	}
}
