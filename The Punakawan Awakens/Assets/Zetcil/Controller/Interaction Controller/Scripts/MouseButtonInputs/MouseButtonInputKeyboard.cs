using UnityEngine;

namespace Zetcil
{
	public class MouseButtonInputKeyboard : MonoBehaviour
	{
#pragma warning disable 0649
		[SerializeField]
		private KeyCode key;
#pragma warning restore 0649

		public ZetInput.MouseButtonInput mouseButton = new ZetInput.MouseButtonInput();

		private void OnEnable()
		{
			mouseButton.StartTracking();
			ZetInput.OnUpdate += OnUpdate;
		}

		private void OnDisable()
		{
			mouseButton.StopTracking();
			ZetInput.OnUpdate -= OnUpdate;
		}

		private void OnUpdate()
		{
			mouseButton.value = Input.GetKey( key );
		}
	}
}