namespace Zetcil
{
	public class ButtonInputSwipeGesture : SwipeGestureBase<string, bool>
	{
		public ZetInput.ButtonInput button = new ZetInput.ButtonInput();

		protected override BaseInput<string, bool> Input { get { return button; } }
		protected override bool Value { get { return true; } }

		public override int Priority { get { return 1; } }
	}
}