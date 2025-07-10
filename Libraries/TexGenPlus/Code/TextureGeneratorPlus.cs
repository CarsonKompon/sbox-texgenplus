using Sandbox;
using Sandbox.Resources;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace TexGenPlus;

[Order( -9999 )]
[Title( "TextureGenerator+" )]
[Icon( "add_to_photos" )]
[ClassName( "spritesheetgenerator" )]
public class TextureGeneratorPlus : TextureGenerator
{
	[Editor( "TextureGeneratorName" ), KeyProperty]
	public string GeneratorName
	{
		get => _generatorName;
		set
		{
			_generatorName = value;
			SwitchGenerator( value );
		}
	}
	[Hide]
	private string _generatorName = "imagefile";

	[Property, Group( "Generator Settings" ), Editor( "TextureGeneratorPlus" ), WideMode( HasLabel = false )]
	internal ResourceGenerator<Texture> Generator { get; set; } = null;

	[Hide, JsonIgnore]
	Dictionary<string, ResourceGenerator<Texture>> _rememberedData = new();

	public TextureGeneratorPlus ()
	{
		SwitchGenerator( _generatorName );
	}

	private void SwitchGenerator ( string classname )
	{
		if ( _rememberedData.TryGetValue( classname, out var cachedGenerator ) )
		{
			Generator = cachedGenerator;
			return;
		}

		Generator = TextureGenerator.Create<Texture>( classname );
		_rememberedData[classname] = Generator;
	}

	protected override async ValueTask<Texture> CreateTexture ( Options options, CancellationToken ct )
	{
		if ( Generator is null )
		{
			return Texture.Invalid;
		}

		return await Generator.CreateAsync( options, ct );
	}
}

