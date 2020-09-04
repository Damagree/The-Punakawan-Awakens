using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Zetcil
{
	[RequireComponent( typeof( ZetInputMultiDragListener ) )]
	public class AxisInputRotateGesture : MonoBehaviour, IZetInputDraggableMultiTouch
	{
		private const float MULTIPLIER = 180f / Mathf.PI;

		public ZetInput.AxisInput axis = new ZetInput.AxisInput();

		public float sensitivity = 0.25f;
		public bool clockwise = true;

		private ZetInputMultiDragListener eventReceiver;

		public int Priority { get { return 2; } }

		private void Awake()
		{
			eventReceiver = GetComponent<ZetInputMultiDragListener>();
		}

		private void OnEnable()
		{
			eventReceiver.AddListener( this );
			axis.StartTracking();
		}

		private void OnDisable()
		{
			eventReceiver.RemoveListener( this );
			axis.StopTracking();
		}

		public bool OnUpdate( List<PointerEventData> mousePointers, List<PointerEventData> touchPointers, IZetInputDraggableMultiTouch activeListener )
		{
			axis.value = 0f;

			if( activeListener != null && activeListener.Priority > Priority )
				return false;

			if( touchPointers.Count < 2 )
			{
				if( ReferenceEquals( activeListener, this ) && touchPointers.Count == 1 )
					touchPointers[0].pressPosition = touchPointers[0].position;

				return false;
			}

			PointerEventData touch1 = touchPointers[touchPointers.Count - 1];
			PointerEventData touch2 = touchPointers[touchPointers.Count - 2];

			Vector2 deltaPosition = touch2.position - touch1.position;
			Vector2 prevDeltaPosition = deltaPosition - ( touch2.delta - touch1.delta );

			float deltaAngle = ( Mathf.Atan2( prevDeltaPosition.y, prevDeltaPosition.x ) - Mathf.Atan2( deltaPosition.y, deltaPosition.x ) ) * MULTIPLIER;
			if( deltaAngle > 180f )
				deltaAngle -= 360f;
			else if( deltaAngle < -180f )
				deltaAngle += 360f;

			axis.value = clockwise ? deltaAngle * sensitivity : -deltaAngle * sensitivity;
			return true;
		}
	}
}