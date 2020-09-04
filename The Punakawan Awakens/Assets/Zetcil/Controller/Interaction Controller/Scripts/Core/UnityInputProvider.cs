using System;
using UnityEngine;

namespace Zetcil
{
	public class UnityInputProvider : MonoBehaviour
	{
#pragma warning disable 0649
		[SerializeField]
		private string[] axes;

		[SerializeField]
		private string[] buttons;

		[SerializeField]
		private int[] mouseButtons;

		[SerializeField]
		private KeyCode[] keys;
#pragma warning restore 0649

		private ZetInput.AxisInput[] axisInputs;
		private ZetInput.ButtonInput[] buttonInputs;
		private ZetInput.MouseButtonInput[] mouseButtonInputs;
		private ZetInput.KeyInput[] keyInputs;

		private void Awake()
		{
			if( axes.Length > 0 )
			{
				axisInputs = new ZetInput.AxisInput[axes.Length];

				int index = 0;
				for( int i = 0; i < axisInputs.Length; i++ )
				{
					try
					{
						ZetInput.AxisInput unityAxis = new ZetInput.AxisInput( axes[i] ) { value = Input.GetAxisRaw( axes[i] ) };
						axisInputs[index++] = unityAxis;
					}
					catch { }
				}

				if( index < axisInputs.Length )
					Array.Resize( ref axisInputs, index );
			}

			if( buttons.Length > 0 )
			{
				buttonInputs = new ZetInput.ButtonInput[buttons.Length];

				int index = 0;
				for( int i = 0; i < buttonInputs.Length; i++ )
				{
					try
					{
						ZetInput.ButtonInput unityButton = new ZetInput.ButtonInput( buttons[i] ) { value = Input.GetButton( buttons[i] ) };
						buttonInputs[index++] = unityButton;
					}
					catch { }
				}

				if( index < buttonInputs.Length )
					Array.Resize( ref buttonInputs, index );
			}

			if( mouseButtons.Length > 0 )
			{
				mouseButtonInputs = new ZetInput.MouseButtonInput[mouseButtons.Length];

				int index = 0;
				for( int i = 0; i < mouseButtonInputs.Length; i++ )
				{
					try
					{
						ZetInput.MouseButtonInput unityMouseButton = new ZetInput.MouseButtonInput( mouseButtons[i] ) { value = Input.GetMouseButton( mouseButtons[i] ) };
						mouseButtonInputs[index++] = unityMouseButton;
					}
					catch { }
				}

				if( index < mouseButtonInputs.Length )
					Array.Resize( ref mouseButtonInputs, index );
			}

			if( keys.Length > 0 )
			{
				keyInputs = new ZetInput.KeyInput[keys.Length];
				for( int i = 0; i < keyInputs.Length; i++ )
				{
					ZetInput.KeyInput unityKey = new ZetInput.KeyInput( keys[i] ) { value = Input.GetKey( keys[i] ) };
					keyInputs[i] = unityKey;
				}
			}
		}

		private void OnEnable()
		{
			if( axisInputs != null )
			{
				for( int i = 0; i < axisInputs.Length; i++ )
					axisInputs[i].StartTracking();
			}

			if( buttonInputs != null )
			{
				for( int i = 0; i < buttonInputs.Length; i++ )
					buttonInputs[i].StartTracking();
			}

			if( mouseButtonInputs != null )
			{
				for( int i = 0; i < mouseButtonInputs.Length; i++ )
					mouseButtonInputs[i].StartTracking();
			}

			if( keyInputs != null )
			{
				for( int i = 0; i < keyInputs.Length; i++ )
					keyInputs[i].StartTracking();
			}

			ZetInput.OnUpdate += OnUpdate;
		}

		private void OnDisable()
		{
			if( axisInputs != null )
			{
				for( int i = 0; i < axisInputs.Length; i++ )
					axisInputs[i].StopTracking();
			}

			if( buttonInputs != null )
			{
				for( int i = 0; i < buttonInputs.Length; i++ )
					buttonInputs[i].StopTracking();
			}

			if( mouseButtonInputs != null )
			{
				for( int i = 0; i < mouseButtonInputs.Length; i++ )
					mouseButtonInputs[i].StopTracking();
			}

			if( keyInputs != null )
			{
				for( int i = 0; i < keyInputs.Length; i++ )
					keyInputs[i].StopTracking();
			}

			ZetInput.OnUpdate -= OnUpdate;
		}

		private void OnUpdate()
		{
			if( axisInputs != null )
			{
				for( int i = 0; i < axisInputs.Length; i++ )
					axisInputs[i].value = Input.GetAxisRaw( axisInputs[i].Key );
			}

			if( buttonInputs != null )
			{
				for( int i = 0; i < buttonInputs.Length; i++ )
					buttonInputs[i].value = Input.GetButton( buttonInputs[i].Key );
			}

			if( mouseButtonInputs != null )
			{
				for( int i = 0; i < mouseButtonInputs.Length; i++ )
					mouseButtonInputs[i].value = Input.GetMouseButton( mouseButtonInputs[i].Key );
			}

			if( keyInputs != null )
			{
				for( int i = 0; i < keyInputs.Length; i++ )
					keyInputs[i].value = Input.GetKey( keyInputs[i].Key );
			}
		}
	}
}