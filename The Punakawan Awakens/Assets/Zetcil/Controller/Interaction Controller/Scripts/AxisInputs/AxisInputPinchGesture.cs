using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Zetcil
{
	[RequireComponent( typeof( ZetInputMultiDragListener ) )]
	public class AxisInputPinchGesture : MonoBehaviour, IZetInputDraggableMultiTouch
	{
		public ZetInput.AxisInput axis = new ZetInput.AxisInput();

		public float sensitivity = 1f;
		public bool invertValue;

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

			float pinchAmount = ( touch2.delta - touch1.delta ).magnitude;
			bool zoomingOut = ( touch2.position - touch1.position ).sqrMagnitude < ( ( touch2.position - touch2.delta ) - ( touch1.position - touch1.delta ) ).sqrMagnitude;
			if( invertValue != zoomingOut )
				pinchAmount = -pinchAmount;

			axis.value = pinchAmount * sensitivity * ZetInputUtils.ResolutionMultiplier;
			return true;
		}
	}
}