using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Zetcil
{
	[RequireComponent( typeof( ZetInputMultiDragListener ) )]
	public class Touchpad : SelectivePointerInput, IZetInputDraggableMultiTouch
	{
		public ZetInput.AxisInput xAxis = new ZetInput.AxisInput( "Horizontal" );
		public ZetInput.AxisInput yAxis = new ZetInput.AxisInput( "Vertical" );

		public bool invertHorizontal, invertVertical;
		public float sensitivity = 1f;

		private ZetInputMultiDragListener eventReceiver;

		public int Priority { get { return 1; } }

		private Vector2 m_value = Vector2.zero;
		public Vector2 Value { get { return m_value; } }

		private void Awake()
		{
			eventReceiver = GetComponent<ZetInputMultiDragListener>();
		}

		private void OnEnable()
		{
			eventReceiver.AddListener( this );

			xAxis.StartTracking();
			yAxis.StartTracking();
		}

		private void OnDisable()
		{
			eventReceiver.RemoveListener( this );

			xAxis.StopTracking();
			yAxis.StopTracking();
		}

		public bool OnUpdate( List<PointerEventData> mousePointers, List<PointerEventData> touchPointers, IZetInputDraggableMultiTouch activeListener )
		{
			xAxis.value = 0f;
			yAxis.value = 0f;

			if( activeListener != null && activeListener.Priority > Priority )
				return false;

			PointerEventData pointer = GetSatisfyingPointer( mousePointers, touchPointers );
			if( pointer == null )
				return false;

			m_value = pointer.delta * ZetInputUtils.ResolutionMultiplier * sensitivity;

			xAxis.value = invertHorizontal ? -m_value.x : m_value.x;
			yAxis.value = invertVertical ? -m_value.y : m_value.y;

			return true;
		}
	}
}