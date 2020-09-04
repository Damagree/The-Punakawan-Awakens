namespace Zetcil
{
	public class MouseButtonInputSwipeGesture : SwipeGestureBase<int, bool>
	{
		public ZetInput.MouseButtonInput mouseButton = new ZetInput.MouseButtonInput();

		protected override BaseInput<int, bool> Input { get { return mouseButton; } }
		protected override bool Value { get { return true; } }

		public override int Priority { get { return 1; } }
	}
}