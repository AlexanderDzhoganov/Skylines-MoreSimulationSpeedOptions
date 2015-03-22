using System;
using System.Reflection;
using ColossalFramework.UI;
using UnityEngine;

namespace MoreSimulationSpeedOptions
{
    public class Hook : MonoBehaviour
    {
        private FieldInfo simulationSpeedField;

        private UIButton speedButton;

        private Color32 white = new Color32(255, 255, 255, 255);
        private Color32 red = new Color32(255, 0, 0, 255);

        void Awake()
        {
            simulationSpeedField = Util.FindField(SimulationManager.instance, "m_simulationSpeed");
                
            UIMultiStateButton speedBar = null;
            var multiStateButtons = GameObject.FindObjectsOfType<UIMultiStateButton>();
            foreach (var button in multiStateButtons)
            {
                if (button.name == "Speed")
                {
                    speedBar = button;
                    break;
                }
            }

            speedBar.isVisible = false;

            // Create a GameObject with a ColossalFramework.UI.UIButton component.
            var buttonObject = new GameObject("MoreSimulationSpeedOptionsButton", typeof(UIButton));

            // Make the buttonObject a child of the uiView.
            buttonObject.transform.parent = speedBar.transform.parent.transform;

            // Get the button component.
            speedButton = buttonObject.GetComponent<UIButton>();

            // Set the text to show on the button.
            speedButton.text = "x1";

            // Set the button dimensions.
            speedButton.width = speedBar.width;
            speedButton.height = speedBar.height;

            // Style the button to look like a menu button.
            speedButton.normalBgSprite = "ButtonMenu";
            speedButton.disabledBgSprite = "ButtonMenuDisabled";
            speedButton.hoveredBgSprite = "ButtonMenuHovered";
            speedButton.focusedBgSprite = "ButtonMenu";
            speedButton.pressedBgSprite = "ButtonMenuPressed";
            speedButton.textColor = new Color32(255, 255, 255, 255);
            speedButton.disabledTextColor = new Color32(7, 7, 7, 255);
            speedButton.hoveredTextColor = new Color32(7, 132, 255, 255);
            speedButton.focusedTextColor = new Color32(255, 255, 255, 255);
            speedButton.pressedTextColor = new Color32(30, 30, 44, 255);

            // Place the button.
            speedButton.transformPosition = speedBar.transformPosition;

            // Respond to button click.
            speedButton.eventClick += (component, param) =>
            {
                var speed = Util.GetFieldValue<int>(simulationSpeedField, SimulationManager.instance);

                switch (speed)
                {
                    case 1:
                        Util.SetFieldValue(simulationSpeedField, SimulationManager.instance, 2);
                        break;
                    case 2:
                        Util.SetFieldValue(simulationSpeedField, SimulationManager.instance, 3);
                        break;
                    case 3:
                        Util.SetFieldValue(simulationSpeedField, SimulationManager.instance, 5);
                        break;
                    case 5:
                        Util.SetFieldValue(simulationSpeedField, SimulationManager.instance, 8);
                        break;
                    case 8:
                        Util.SetFieldValue(simulationSpeedField, SimulationManager.instance, 1);
                        break;
                }
            };
        }

        void Update()
        {
            var speed = Util.GetFieldValue<int>(simulationSpeedField, SimulationManager.instance);
            speedButton.text = "x" + speed.ToString();
            if (speed > 3)
            {
                speedButton.textColor = red;
            }
            else
            {
                speedButton.textColor = white;
            }
        }

    }
}
