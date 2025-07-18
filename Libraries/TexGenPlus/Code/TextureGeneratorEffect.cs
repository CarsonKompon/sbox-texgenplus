using Sandbox;
using System;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace TexGenPlus;

public abstract class TextureGeneratorEffect : IJsonConvert
{
	public bool Enabled { get; set; } = true;

	public abstract Bitmap Apply( Bitmap bitmap );

	static void IJsonConvert.JsonWrite( object value, Utf8JsonWriter writer )
	{
		// Serialize the type name (to support polymorphism) and then serialize the object itself
		if ( value is TextureGeneratorEffect effect )
		{
			writer.WriteStartObject();
			writer.WriteString( "Type", effect.GetType().FullName );
			writer.WritePropertyName( "Data" );
			JsonSerializer.Serialize( writer, effect, effect.GetType() );
			writer.WriteEndObject();
		}
		else
		{
			throw new InvalidOperationException( "Cannot serialize non-effect type." );
		}
	}

	static object IJsonConvert.JsonRead( ref Utf8JsonReader reader, Type typeToConvert )
	{
		if ( reader.TokenType != JsonTokenType.StartObject )
			throw new JsonException( "Expected start of object." );
		string typeName = null;
		JsonObject data = null;
		while ( reader.Read() && reader.TokenType != JsonTokenType.EndObject )
		{
			if ( reader.TokenType == JsonTokenType.PropertyName )
			{
				var propertyName = reader.GetString();
				reader.Read();
				if ( propertyName == "Type" )
				{
					typeName = reader.GetString();
				}
				else if ( propertyName == "Data" )
				{
					data = JsonSerializer.Deserialize<JsonObject>( ref reader );
				}
			}
		}
		if ( typeName == null || data == null )
			throw new JsonException( "Missing required properties." );
		var effectTypeDesc = TypeLibrary.GetType( typeName );
		var effectType = effectTypeDesc?.TargetType;
		if ( effectType == null )
			throw new JsonException( $"Unknown effect type: {typeName}" );
		return JsonSerializer.Deserialize( data, effectType );
	}
}
