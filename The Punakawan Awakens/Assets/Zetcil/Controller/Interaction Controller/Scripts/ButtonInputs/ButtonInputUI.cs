using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Zetcil
{
	public class ButtonInputUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
	{
		public ZetInput.ButtonInput button = new ZetInput.ButtonInput();

		private void Awake()
		{
			Graphic graphic = GetComponent<Graphic>();
			if( graphic != null )
				graphic.raycastTarget = true;
		}

		private void OnEnable()
		{
			button.StartTracking();
		}

		private void OnDisable()
		{
			button.StopTracking();
		}

		public void OnPointerDown( PointerEventData eventData )
		{
			button.value = true;
		}

		public void OnPointerUp( PointerEventData eventData )
		{
			button.value = false;
		}
	}
}