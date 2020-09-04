using UnityEngine;

namespace Zetcil
{
	public class AxisInputMouse : MonoBehaviour
	{
		public ZetInput.AxisInput xAxis = new ZetInput.AxisInput();
		public ZetInput.AxisInput yAxis = new ZetInput.AxisInput();

		//#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL || UNITY_FACEBOOK || UNITY_WSA || UNITY_WSA_10_0
		private void OnEnable()
		{
			xAxis.StartTracking();
			yAxis.StartTracking();

			ZetInput.OnUpdate += OnUpdate;
		}

		private void OnDisable()
		{
			xAxis.StopTracking();
			yAxis.StopTracking();

			ZetInput.OnUpdate -= OnUpdate;
		}

		private void OnUpdate()
		{
			if( Input.touchCount == 0 )
			{
				xAxis.value = Input.GetAxisRaw( "Mouse X" );
				yAxis.value = Input.GetAxisRaw( "Mouse Y" );
			}
			else
			{
				xAxis.value = 0f;
				yAxis.value = 0f;
			}
		}
		//#endif
	}
}