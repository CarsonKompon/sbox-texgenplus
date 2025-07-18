using Sandbox;
using System;

namespace TexGenPlus.Effects;

[Icon( "flip" )]
public class MirrorEffect : TextureGeneratorEffect
{
	[Flags]
	public enum MirrorFlags
	{
		None = 0,

		[Icon( "align_horizontal_center" )]
		[Title( "Horizontal Flip" )]
		[Description( "Flip the image horizontally" )]
		HorizontalFlip = 1 << 2,

		[Icon( "align_vertical_center" )]
		[Title( "Vertical Flip" )]
		[Description( "Flip the image vertically" )]
		VerticalFlip = 1 << 3
	}

	[Property, KeyProperty]
	public MirrorFlags Mirror { get; set; } = MirrorFlags.None;

	public override Bitmap Apply( Bitmap bitmap )
	{
		if ( Mirror.HasFlag( MirrorFlags.HorizontalFlip ) )
		{
			bitmap = bitmap.FlipHorizontal();
		}

		if ( Mirror.HasFlag( MirrorFlags.VerticalFlip ) )
		{
			bitmap = bitmap.FlipVertical();
		}

		return bitmap;
	}

	public override int GetHashCode()
	{
		return System.HashCode.Combine( Mirror );
	}
}
