using Sandbox;
using Sandbox.Resources;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace TexGenPlus;

[Order( -9999 )]
[Title( "TextureGenerator+" )]
[Icon( "add_to_photos" )]
[ClassName( "texgenplus" )]
public class TextureGeneratorPlus : TextureGenerator
{
	[Editor( "TextureGeneratorName" ), KeyProperty, Order( -999 )]
	public string GeneratorName
	{
		get => _generatorName;
		set
		{
			if ( _generatorName == value )
				return;

			_generatorName = value;
		}
	}
	[Hide]
	private string _generatorName = "imagefile";

	[Property, Group( "Generator Settings" ), Editor( "TextureGeneratorPlus" ), WideMode( HasLabel = false ), Order( 0 ), JsonIgnore]
	private ResourceGenerator<Texture> Generator
	{
		get => _generator;
		set
		{
			if ( value is null )
			{
				SwitchGenerator( _generatorName );
				return;
			}
			_generator = value;
			_resource = _generator?.Create( Options.Default )?.GenerationData ?? new EmbeddedResource
			{
				ResourceCompiler = "texture",
				ResourceGenerator = _generatorName,
				Data = new JsonObject()
			};
			_rememberedData[_generatorName] = _generator;
		}
	}
	[Hide, JsonIgnore]
	private ResourceGenerator<Texture> _generator = null;

	[Property, Hide, JsonInclude]
	private EmbeddedResource Resource
	{
		get => _resource;
		set
		{
			if ( _generator is null )
			{
				Generator = TextureGenerator.Create<Texture>( value );
			}
			_resource = value;
		}
	}
	[Hide, JsonIgnore]
	private EmbeddedResource _resource { get; set; }

	[Property, Group( "Effects" ), Order( 50 ), WideMode( HasLabel = false ), JsonInclude]
	public List<TextureGeneratorEffect> Effects { get; set; } = new();

	[Hide, JsonIgnore]
	Dictionary<string, ResourceGenerator<Texture>> _rememberedData = new();

	[Property, Hide, JsonInclude]
	public int RandomNumber { get; set; }

	public TextureGeneratorPlus()
	{
	}

	public void SwitchGenerator( string classname )
	{
		if ( _rememberedData.TryGetValue( classname, out var cachedGenerator ) )
		{
			Generator = cachedGenerator;
			return;
		}

		Generator = TextureGenerator.Create<Texture>( classname );
		_rememberedData[classname] = Generator;
	}

	protected override async ValueTask<Texture> CreateTexture( Options options, CancellationToken ct )
	{
		if ( Generator is null )
		{
			return Texture.Invalid;
		}

		var tex = await Generator.CreateAsync( options, ct );
		var bitmap = tex.GetBitmap( 0 );

		foreach ( var effect in Effects )
		{
			bitmap = effect?.Apply( bitmap );
		}

		return bitmap.ToTexture();
	}

	public override ulong GetHash()
	{
		var hash = Generator?.GetHash() ?? 0;
		foreach ( var effect in Effects )
		{
			hash += (ulong)Math.Abs( effect?.GetHashCode() ?? 0 );
		}
		hash += (ulong)Math.Abs( RandomNumber );
		return hash;
	}

}

public static class TextureGeneratorPlusExtensions
{
	public static bool HasAttribute( this MemberInfo memberinfo, Type attribute, bool inherit = true )
	{
		return memberinfo?.IsDefined( attribute, inherit ) ?? false;
	}
}

