using UnityEngine;
using System.Collections;
using UnityEditor;

namespace BrokenWorld.Editors.NodeEditor
{

    public class ComparisonNode : BaseInputNode
    {

        private ComparisonType _comparisonType;

        public enum ComparisonType
        {
            Greater,
            Less,
            Equal
        }

        private string compareText = "";

        public ComparisonNode()
        {
            WindowTitle = "Comparison Node";
            HasInputs = true;
        }

        public override void DrawWindow()
        {
            base.DrawWindow();

            Event e = Event.current;
            _comparisonType = (ComparisonType)EditorGUILayout.EnumPopup("Comparison Type", _comparisonType);

            string input1Title = "None";

            if (Input1)
            {
                input1Title = Input1.GetResult();
            }

            //draw a label
            GUILayout.Label("Input 1: " + input1Title);

            //same as before
            if (e.type == EventType.Repaint)
            {
                Input1Rect = GUILayoutUtility.GetLastRect();

            }

            string input2Title = "None";

            if (Input2)
            {
                input2Title = Input2.GetResult();
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

            string result = "false";

            switch (_comparisonType)
            {
                case ComparisonType.Equal:
                    if (input1Value == input2Value)
                    {
                        result = "true";
                    }
                    break;
                case ComparisonType.Greater:
                    if (input1Value > input2Value)
                    {
                        result = "true";
                    }
                    break;
                case ComparisonType.Less:
                    if (input1Value < input2Value)
                    {
                        result = "true";
                    }
                    break;
            }

            return result;
        }

    }

}

//using UnityEngine;
//using UnityEditor;
//using System.Collections;

//public class ComparisonNode : BaseInputNode
//{

//    public enum ComparisonType { Greater, Less, Equal }

//    private ComparisonType comparisonType;

//    private BaseInputNode input1;
//    private Rect input1Rect;

//    private BaseInputNode input2;
//    private Rect input2Rect;

//    private string compareText = "";

//    public ComparisonNode()
//    {
//        WindowTitle = "Comparison Node";
//        HasInputs = true;
//    }

//    public override void DrawWindow()
//    {
//        base.DrawWindow();

//        comparisonType = (ComparisonType)EditorGUILayout.EnumPopup("Calculation Type: ", comparisonType);

//        string input1Title = (input1) ? input1.GetResult() : "None";
//        string input2Title = (input2) ? input2.GetResult() : "None";

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
//            input1 = input;
//        else if (input2Rect.Contains(clickPos))
//            input2 = input;
//    }

//    public override string GetResult()
//    {
//        float input1Value = 0;
//        float input2Value = 0;
//        bool result = false;

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

//        switch (comparisonType)
//        {
//            case ComparisonType.Equal:
//                result = input1Value == input2Value;
//                break;
//            case ComparisonType.Greater:
//                result = input1Value > input2Value;
//                break;
//            case ComparisonType.Less:
//                result = input1Value < input2Value;
//                break;
//        }

//        return result.ToString();
//    }

//    public override void NodeDeleted(BaseNode node)
//    {
//        //If the inputNode is attached to it self, delete it
//        if (node.Equals(input1))
//            input1 = null;
//        if (node.Equals(input2))
//            input2 = null;
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


//}









