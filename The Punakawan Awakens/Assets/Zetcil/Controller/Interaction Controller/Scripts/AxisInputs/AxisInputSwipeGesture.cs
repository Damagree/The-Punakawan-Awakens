namespace Zetcil
{
	public class AxisInputSwipeGesture : SwipeGestureBase<string, float>
	{
		public ZetInput.AxisInput axis = new ZetInput.AxisInput();
		public float value = 1f;

		protected override BaseInput<string, float> Input { get { return axis; } }
		protected override float Value { get { return value; } }

		public override int Priority { get { return 1; } }
	}
}