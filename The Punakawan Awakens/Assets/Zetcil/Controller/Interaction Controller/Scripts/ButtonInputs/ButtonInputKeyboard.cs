using UnityEngine;

namespace Zetcil
{
	public class ButtonInputKeyboard : MonoBehaviour
	{
#pragma warning disable 0649
		[SerializeField]
		private KeyCode key;
#pragma warning restore 0649

		public ZetInput.ButtonInput button = new ZetInput.ButtonInput();

		private void OnEnable()
		{
			button.StartTracking();
            ZetInput.OnUpdate += OnUpdate;
		}

		private void OnDisable()
		{
			button.StopTracking();
            ZetInput.OnUpdate -= OnUpdate;
		}

		private void OnUpdate()
		{
			button.value = Input.GetKey( key );
		}
	}
}