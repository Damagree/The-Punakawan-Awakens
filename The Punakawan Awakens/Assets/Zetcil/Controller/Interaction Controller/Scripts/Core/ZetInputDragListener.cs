using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Zetcil
{
	public class ZetInputDragListener : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
	{
		public IZetInputDraggable Listener { get; set; }

		private int pointerId = ZetInputUtils.NON_EXISTING_TOUCH;

		private void Awake()
		{
			Graphic graphic = GetComponent<Graphic>();
			if( graphic == null )
			{
				graphic = gameObject.AddComponent<Image>();
				graphic.color = Color.clear;
			}

			graphic.raycastTarget = true;
		}

		public void OnPointerDown( PointerEventData eventData )
		{
			Listener.OnPointerDown( eventData );
			pointerId = eventData.pointerId;
		}

		public void OnDrag( PointerEventData eventData )
		{
			if( pointerId != eventData.pointerId )
				return;

			Listener.OnDrag( eventData );
		}

		public void OnPointerUp( PointerEventData eventData )
		{
			if( pointerId != eventData.pointerId )
				return;

			Listener.OnPointerUp( eventData );
			pointerId = ZetInputUtils.NON_EXISTING_TOUCH;
		}

		public void Stop()
		{
			pointerId = ZetInputUtils.NON_EXISTING_TOUCH;
		}
	}
}