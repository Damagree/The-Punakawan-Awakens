using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Zetcil
{
	public class AxisInputUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
	{
		public ZetInput.AxisInput axis = new ZetInput.AxisInput();
		public float value = 1f;
        public bool isPress = false;

		private void Awake()
		{
			Graphic graphic = GetComponent<Graphic>();
			if( graphic != null )
				graphic.raycastTarget = true;
		}

		private void OnEnable()
		{
			axis.StartTracking();
		}

		private void OnDisable()
		{
			axis.StopTracking();
		}

		public void OnPointerDown( PointerEventData eventData )
		{
			axis.value = value;
            isPress = true;
        }

		public void OnPointerUp( PointerEventData eventData )
		{
			axis.value = 0f;
            isPress = false;
        }
	}
}