
# TextureGenerator+

Adds a new TextureGenerator called `TextureGenerator+` which allows you to select any other TextureGenerator and apply `TextureGeneratorEffects` on top of it. Re-order the list to change the order in which they are applied and press the visibility button to toggle the effect.

https://github.com/user-attachments/assets/ede179ef-10d0-45a0-9c82-50960ef994c0

## TextureGeneratorEffect

Here's how you make an effect. GetHashCode is used like BuildHash in razor panels, anything you put in there will force the texture to re-build when changed.

```cs
[Icon( "refresh" )]
public class RotateEffect : TextureGeneratorEffect
{
	[Property, KeyProperty, Range( 0, 360 )]
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
```
