using UnityEditor;

namespace Zetcil
{
	[CustomPropertyDrawer( typeof(ZetInput.AxisInput ) )]
	public class AxisInputDrawer : BaseInputDrawer
	{
		public override string ValueToString( SerializedProperty valueProperty )
		{
			return valueProperty.floatValue.ToString();
		}
	}
}