using UnityEditor;

namespace Zetcil
{
	[CustomPropertyDrawer( typeof(ZetInput.MouseButtonInput ) )]
	public class MouseButtonInputDrawer : BaseInputDrawer
	{
		public override string ValueToString( SerializedProperty valueProperty )
		{
			return valueProperty.boolValue.ToString();
		}
	}
}