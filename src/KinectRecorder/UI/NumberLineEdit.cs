using Godot;
using System;
using System.Text.RegularExpressions;

public partial class NumberLineEdit : LineEdit
{
	private String oldText;
	private readonly Regex regex = new Regex(@"^[0-9]*$");

	public override void _Ready()
	{
		oldText = Text;
		TextChanged += ValidateTextIsInt;
	}

	private void ValidateTextIsInt(string newText)
	{
		//Logs.Print("Validating Text Is Int");
		if(regex.IsMatch(newText))
		{
			oldText = newText;
		}
		Text = oldText;
		CaretColumn = Text.Length;
	}

	public int Input()
	{
		return Int32.Parse(string.IsNullOrWhiteSpace(Text) ? PlaceholderText : Text);
	}
}
