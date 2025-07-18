using Editor;
using Sandbox;
using Sandbox.Resources;
using System;
using System.Linq;

namespace TexGenPlus;

[CustomEditor( typeof( string ), NamedEditor = "TextureGeneratorName" )]
public class TextureGeneratorNameControlWidget : ControlWidget
{
	public TextureGeneratorNameControlWidget( SerializedProperty property ) : base( property )
	{
		Layout = Layout.Row();
		Layout.Spacing = 4;

		AcceptDrops = false;

		Rebuild();
	}

	protected override void OnPaint()
	{

	}

	public void Rebuild()
	{
		Layout.Clear( true );

		var val = SerializedProperty.GetValue<string>();
		var textureGenerators = EditorTypeLibrary.GetTypes<TextureGenerator>().OrderBy( x => x.Order ).ThenBy( x => x.Name );

		if ( textureGenerators.Count() <= 0 )
		{
			Layout.Add( new Label( "None" ) );
			return;
		}

		var comboBox = new ComboBox( this );
		var v = SerializedProperty.GetValue<string>();

		for ( int i = 0; i < textureGenerators.Count(); ++i )
		{
			var gen = textureGenerators.ElementAt( i );
			if ( gen.TargetType == typeof( TextureGenerator ) || gen.TargetType == typeof( TextureGeneratorPlus ) )
				continue;

			var className = gen.ClassName;
			comboBox.AddItem( gen.Title, gen.Icon, onSelected: () =>
			{
				SerializedProperty.SetValue( className );
				var texgenplus = SerializedProperty.Parent.Targets.First() as TextureGeneratorPlus;
				texgenplus?.SwitchGenerator( className );
			}, selected: string.Equals( v, className, StringComparison.OrdinalIgnoreCase ) );
		}

		Layout.Add( comboBox );
	}

	protected override void OnValueChanged()
	{
		Rebuild();
	}
}
