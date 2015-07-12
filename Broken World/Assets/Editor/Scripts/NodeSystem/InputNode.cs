using UnityEngine;
using UnityEditor;
using System.Collections;

namespace BrokenWorld.Editors.NodeEditor
{

    public class InputNode : BaseInputNode
    {

        public enum InputType
        {
            Number,
            Randomization
        }

        private InputType _inputType;
        private float randomFrom = 0;
        private float randomTo = 0;
        private string inputValue = "";

        public InputNode()
        {
            WindowTitle = "Input Node";
        }

        public override void DrawWindow()
        {
            base.DrawWindow();

            _inputType = (InputType)EditorGUILayout.EnumPopup("Input type: ", _inputType);

            switch (_inputType)
            {
                case InputType.Number:
                    inputValue = EditorGUILayout.TextField("Value: ", inputValue);
                    break;

                case InputType.Randomization:
                    randomFrom = EditorGUILayout.FloatField("From", randomFrom);
                    randomTo = EditorGUILayout.FloatField("To", randomTo);

                    if (GUILayout.Button("Calculate random"))
                        inputValue = UnityEngine.Random.Range(randomFrom, randomTo).ToString();
                    break;
            }

        }

        public override void DrawCurves()
        {

        }


        public override string GetResult()
        {
            return inputValue;
        }

    }
}