using UnityEngine;

namespace Zetcil
{

    public static class ZetInputHelper
    {
        private class ButtonClickInput : ZetInput.ButtonInput
        {
            public ButtonClickInput(string key) : base(key) { }

            public void OnUpdate()
            {
                if (!value)
                    value = true;
                else
                {
                    StopTracking();
                    ZetInput.OnUpdate -= OnUpdate;
                }
            }
        }

        private class MouseButtonClickInput : ZetInput.MouseButtonInput
        {
            public MouseButtonClickInput(int key) : base(key) { }

            public void OnUpdate()
            {
                if (!value)
                    value = true;
                else
                {
                    StopTracking();
                    ZetInput.OnUpdate -= OnUpdate;
                }
            }
        }

        private class KeyClickInput : ZetInput.KeyInput
        {
            public KeyClickInput(KeyCode key) : base(key) { }

            public void OnUpdate()
            {
                if (!value)
                    value = true;
                else
                {
                    StopTracking();
                    ZetInput.OnUpdate -= OnUpdate;
                }
            }
        }

        public static void TriggerButtonClick(string button)
        {
            ButtonClickInput buttonClick = new ButtonClickInput(button);
            buttonClick.StartTracking();
            ZetInput.OnUpdate += buttonClick.OnUpdate;
        }

        public static void TriggerMouseButtonClick(int button)
        {
            MouseButtonClickInput mouseButtonClick = new MouseButtonClickInput(button);
            mouseButtonClick.StartTracking();
            ZetInput.OnUpdate += mouseButtonClick.OnUpdate;
        }

        public static void TriggerKeyClick(KeyCode key)
        {
            KeyClickInput keyClick = new KeyClickInput(key);
            keyClick.StartTracking();
            ZetInput.OnUpdate += keyClick.OnUpdate;
        }
    }
}