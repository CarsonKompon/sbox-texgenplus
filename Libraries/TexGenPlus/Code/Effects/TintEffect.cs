using Sandbox;

namespace TexGenPlus.Effects;

[Icon( "colorize" )]
public class TintEffect : TextureGeneratorEffect
{
	[Property, KeyProperty]
	public Color TintColor { get; set; } = Color.White;

	public override void Apply( Bitmap bitmap )
	{
		bitmap.Tint( TintColor );
	}

	public override int GetHashCode()
	{
		return TintColor.GetHashCode();
	}
}
