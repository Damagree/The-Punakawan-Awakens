using UnityEngine;

namespace Zetcil
{
	public class KeyInputSwipeGesture : SwipeGestureBase<KeyCode, bool>
	{
		public ZetInput.KeyInput key = new ZetInput.KeyInput();

		protected override BaseInput<KeyCode, bool> Input { get { return key; } }
		protected override bool Value { get { return true; } }

		public override int Priority { get { return 1; } }
	}
}