using Sandbox;

namespace TexGenPlus.Effects;

[Icon( "palette" )]
public class ColorizeEffect : TextureGeneratorEffect
{
	[Property, KeyProperty]
	public Color Color { get; set; } = Color.White;

	public override Bitmap Apply( Bitmap bitmap )
	{
		bitmap.Colorize( Color );
		return bitmap;
	}

	public override int GetHashCode()
	{
		return System.HashCode.Combine( Color.Hex );
	}
}
