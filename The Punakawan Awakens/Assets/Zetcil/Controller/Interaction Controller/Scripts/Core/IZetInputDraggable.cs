using UnityEngine.EventSystems;

namespace Zetcil
{
	public interface IZetInputDraggable
	{
		void OnPointerDown( PointerEventData eventData );
		void OnDrag( PointerEventData eventData );
		void OnPointerUp( PointerEventData eventData );
	}
}