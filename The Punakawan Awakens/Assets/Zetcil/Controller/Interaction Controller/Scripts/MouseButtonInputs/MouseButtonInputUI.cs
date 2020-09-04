using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Zetcil
{
	public class MouseButtonInputUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
	{
		public ZetInput.MouseButtonInput mouseButton = new ZetInput.MouseButtonInput();

		private void Awake()
		{
			Graphic graphic = GetComponent<Graphic>();
			if( graphic != null )
				graphic.raycastTarget = true;
		}

		private void OnEnable()
		{
			mouseButton.StartTracking();
		}

		private void OnDisable()
		{
			mouseButton.StopTracking();
		}

		public void OnPointerDown( PointerEventData eventData )
		{
			mouseButton.value = true;
		}

		public void OnPointerUp( PointerEventData eventData )
		{
			mouseButton.value = false;
		}
	}
}