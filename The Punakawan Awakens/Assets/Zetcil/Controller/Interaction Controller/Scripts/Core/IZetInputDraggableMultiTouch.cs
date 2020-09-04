using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace Zetcil
{
	public interface IZetInputDraggableMultiTouch
	{
		int Priority { get; }

		bool OnUpdate( List<PointerEventData> mousePointers, List<PointerEventData> touchPointers, IZetInputDraggableMultiTouch activeListener );
	}
}