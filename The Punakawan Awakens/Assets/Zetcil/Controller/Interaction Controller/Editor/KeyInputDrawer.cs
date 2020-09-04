using UnityEditor;

namespace Zetcil
{
	[CustomPropertyDrawer( typeof(ZetInput.KeyInput ) )]
	public class KeyInputDrawer : BaseInputDrawer
	{
		public override string ValueToString( SerializedProperty valueProperty )
		{
			return valueProperty.boolValue.ToString();
		}
	}
}