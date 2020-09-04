using UnityEditor;

namespace Zetcil
{
	[CustomPropertyDrawer( typeof( ZetInput.ButtonInput ) )]
	public class ButtonInputDrawer : BaseInputDrawer
	{
		public override string ValueToString( SerializedProperty valueProperty )
		{
			return valueProperty.boolValue.ToString();
		}
	}
}