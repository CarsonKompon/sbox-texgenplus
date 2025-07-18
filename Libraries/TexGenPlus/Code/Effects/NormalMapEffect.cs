using Sandbox;

namespace TexGenPlus.Effects;

[Icon( "terrain" )]
public class NormalMapEffect : TextureGeneratorEffect
{
	[Property, KeyProperty]
	public float Scale { get; set; } = 1;

	public override Bitmap Apply( Bitmap bitmap )
	{
		return bitmap.HeightmapToNormalMap( Scale );
	}

	public override int GetHashCode()
	{
		return System.HashCode.Combine( Scale );
	}
}
