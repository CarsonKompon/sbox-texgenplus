using Editor;
using Sandbox;
using Sandbox.Resources;

namespace TexGenPlus;

[CustomEditor( typeof( ResourceGenerator<Texture> ), NamedEditor = "TextureGeneratorPlus" )]
public class TextureGeneratorPlusControlWidget : ControlWidget
{
	ResourceGenerator<Texture> Generator
	{
		get => SerializedProperty.GetValue<ResourceGenerator<Texture>>();
		set => SerializedProperty.SetValue( value );
	}

	public TextureGeneratorPlusControlWidget( SerializedProperty property ) : base( property )
	{
		Layout = Layout.Row();
		Layout.Spacing = 4;

		AcceptDrops = false;

		Rebuild();
	}

	protected override void OnPaint()
	{
		Paint.SetBrushAndPen( Theme.ControlBackground );
		Paint.DrawRect( LocalRect, 8 );
	}

	public void Rebuild()
	{
		Layout.Clear( true );

		if ( SerializedProperty is null )
			return;

		var scrollable = new ScrollArea( this );
		scrollable.Canvas = new Widget();
		scrollable.MaximumHeight = 250;
		scrollable.Canvas.Layout = Layout.Column();

		var val = SerializedProperty?.GetValue<object>();
		if ( val is not null )
		{
			var serializedObject = val?.GetSerialized();
			var sheet = new ControlSheet();
			serializedObject.OnPropertyChanged = OnPropertyChanged;
			sheet.AddObject( serializedObject, _ => true );
			scrollable.Canvas.Layout.Add( sheet );
		}

		Layout.Add( scrollable );
	}

	protected override void OnValueChanged()
	{
		Rebuild();
	}

	private void OnPropertyChanged( SerializedProperty property )
	{
		Generator = Generator;
	}
}
