using UnityEngine;

namespace Zetcil
{
	public class KeyInputKeyboard : MonoBehaviour
	{
#pragma warning disable 0649
		[SerializeField]
		private KeyCode realKey;
#pragma warning restore 0649

		public ZetInput.KeyInput key = new ZetInput.KeyInput();

		private void OnEnable()
		{
			key.StartTracking();
			ZetInput.OnUpdate += OnUpdate;
		}

		private void OnDisable()
		{
			key.StopTracking();
			ZetInput.OnUpdate -= OnUpdate;
		}

		private void OnUpdate()
		{
			key.value = Input.GetKey( realKey );
		}
	}
}