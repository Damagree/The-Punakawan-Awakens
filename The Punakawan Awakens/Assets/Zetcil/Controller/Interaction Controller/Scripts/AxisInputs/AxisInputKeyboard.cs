using UnityEngine;

namespace Zetcil
{
	public class AxisInputKeyboard : MonoBehaviour
	{
#pragma warning disable 0649
		[SerializeField]
		private KeyCode key;
#pragma warning restore 0649

		public ZetInput.AxisInput axis = new ZetInput.AxisInput();
		public float value = 1f;

		private void OnEnable()
		{
			axis.StartTracking();
            ZetInput.OnUpdate += OnUpdate;
		}

		private void OnDisable()
		{
			axis.StopTracking();
            ZetInput.OnUpdate -= OnUpdate;
		}

		private void OnUpdate()
		{
			axis.value = Input.GetKey( key ) ? value : 0f;
		}
	}
}