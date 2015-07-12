using UnityEngine;
using System.Collections;
using UnityEditor;

namespace BrokenWorld.Editors.NodeEditor
{

    public class CalcNode : BaseInputNode
    {
        public enum CalculationType
        {
            Addition,
            Subtraction,
            Multiplication,
            Division
        }

        private CalculationType _currentCalculationType;

        public CalcNode()
        {
            WindowTitle = "Calculation Node";
            HasInputs = true;
        }

        public override void DrawWindow()
        {
            base.DrawWindow();
            Event e = Event.current;

            _currentCalculationType = (CalculationType)EditorGUILayout.EnumPopup("Calculation Type: ", _currentCalculationType);

            string input1Title = (Input1) ? Input1.GetResult() : "None";
            string input2Title = (Input2) ? Input2.GetResult() : "None";

            GUILayout.Label("Input 1: " + input1Title);

            if (e.type == EventType.Repaint)
            {
                Input1Rect = GUILayoutUtility.GetLastRect();
            }


            GUILayout.Label("Input 2: " + input2Title);

            if (e.type == EventType.Repaint)
            {
                Input2Rect = GUILayoutUtility.GetLastRect();
            }

        }


        public override string GetResult()
        {
            float input1Value = 0;
            float input2Value = 0;

            if (Input1)
            {
                string input1Raw = Input1.GetResult();
                float.TryParse(input1Raw, out input1Value);
            }

            if (Input2)
            {
                string input2Raw = Input2.GetResult();
                float.TryParse(input2Raw, out input2Value);
            }


            switch (_currentCalculationType)
            {
                case CalculationType.Addition:
                    return (input1Value + input2Value).ToString();
                case CalculationType.Division:
                    return (input1Value / input2Value).ToString();
                case CalculationType.Multiplication:
                    return (input1Value * input2Value).ToString();
                case CalculationType.Subtraction:
                    return (input1Value - input2Value).ToString();
                default:
                    return "";
            }
        }




    }

}

//using UnityEngine;
//using UnityEditor;
//using System.Collections;

//public class CalcNode : BaseInputNode 
//{
//    public enum CalculationType { Addition, Substraction, Multiplication, Division }

//    private BaseInputNode input1;
//    private Rect input1Rect;

//    private BaseInputNode input2;
//    private Rect input2Rect;

//    private CalculationType calculationType;

//    public CalcNode()
//    {
//        WindowTitle = "Calculation Node";
//        HasInputs = true;
//    }

//    public override void DrawWindow()
//    {
//        base.DrawWindow();

//        calculationType = (CalculationType) EditorGUILayout.EnumPopup("Calculation Type: ", calculationType);

//        string input1Title = (input1)?  input1.GetResult() : "None";
//        string input2Title = (input2)? input2.GetResult() : "None";

//        GUILayout.Label("Input 1: " + input1Title);
//        GUILayout.Label("Input 2: " + input2Title);

//        if (Event.current.type == EventType.Repaint)
//        {
//            input1Rect = GUILayoutUtility.GetLastRect();
//            input2Rect = GUILayoutUtility.GetLastRect();
//        }

//    }

//    public override void SetInput(BaseInputNode input, Vector2 clickPos)
//    {
//        clickPos.x -= WindowRect.x;
//        clickPos.y -= WindowRect.y;

//        if (input1Rect.Contains(clickPos))
//        {
//            input1 = input;
//        }
//        else if (input2Rect.Contains(clickPos))
//        {
//            input2 = input;
//        }
//    }

//    public override void DrawCurves()
//    {
//        Rect rect;
//        if (input1)
//        {
//            rect = WindowRect;
//            rect.x += input1Rect.x;
//            rect.y += input1Rect.y + input1Rect.height/2;

//            rect.width = 1;
//            rect.height = 1;

//            NodeEditor.DrawNodeCurve(input1.WindowRect, rect);
//        }

//        if (input2)
//        {
//            rect = WindowRect;
//            rect.x += input2Rect.x;
//            rect.y += input2Rect.y + input2Rect.height / 2;

//            rect.width = 1;
//            rect.height = 1;

//            NodeEditor.DrawNodeCurve(input2.WindowRect, rect);
//        }
//    }

//    public override string GetResult()
//    {
//        float input1Value = 0;
//        float input2Value = 0;
//        float result = 0;

//        if (input1)
//        {
//            string input1Raw = input1.GetResult();
//            float.TryParse(input1Raw, out input1Value);
//        }

//        if (input2)
//        {
//            string input2Raw = input2.GetResult();
//            float.TryParse(input2Raw, out input2Value);
//        }

//        switch (calculationType)
//        { 
//            case CalculationType.Addition:
//                result = input1Value + input2Value;
//                break;

//            case CalculationType.Division:
//                result = input1Value / input2Value;
//                break;

//            case CalculationType.Multiplication:
//                result = input1Value * input2Value;
//                break;

//            case CalculationType.Substraction:
//                result = input1Value - input2Value;
//                break;
//        }

//        return result.ToString();
//    }

//    public override BaseInputNode ClickedOnInput(Vector2 pos)
//    {
//        BaseInputNode retVal = null;

//        //Set to local pos
//        pos.x -= WindowRect.x;
//        pos.y -= WindowRect.y;

//        if (input1Rect.Contains(pos))
//        {
//            retVal = input1;
//            input1 = null;
//        }
//        else if (input2Rect.Contains(pos))
//        {
//            retVal = input2;
//            input2 = null;
//        }

//        return retVal;
//    }

//    public override void NodeDeleted(BaseNode node)
//    {
//        //If the inputNode is attached to it self, delete it
//        if (node.Equals(input1))
//            input1 = null;
//        if (node.Equals(input2))
//            input2 = null;
//    }
//}










