//using UnityEngine;
//using UnityEditor;
//using System.Collections;

//public class OutputNode : BaseNode {

//    private string result = "";
//    private BaseInputNode inputNode;
//    private Rect inputNodeRect;

//    public OutputNode()
//    {
//        WindowTitle = "Output Node";
//        HasInputs = true;
//    }

//    public override void DrawWindow()
//    {
//        base.DrawWindow();

//        Event e = Event.current;
//        string input1Title = "none";

//        if (inputNode)
//        {
//            input1Title = inputNode.GetResult();
//        }

//        GUILayout.Label("Input 1: " + input1Title);

//        if (e.type == EventType.Repaint)
//        {
//            inputNodeRect = GUILayoutUtility.GetLastRect();
//        }

//        GUILayout.Label("Result: " + result);
//    }

//    public override void DrawCurves()
//    {
//        if (inputNode)
//        {
//            Rect rect = WindowRect;
//            rect.x += inputNodeRect.x;
//            rect.y += inputNodeRect.y + inputNodeRect.height / 2;

//            rect.width = 1;
//            rect.height = 1;

//            NodeEditor.DrawNodeCurve(inputNode.WindowRect, rect);
//        }
//    }

//    public override void NodeDeleted(BaseNode node)
//    {
//        //If the inputNode is attached to it self, delete it
//        if (node.Equals(inputNode))
//            inputNode = null;
//    }

//    public override BaseInputNode ClickedOnInput(Vector2 pos)
//    {
//        BaseInputNode retVal = null;

//        //Set to local pos
//        pos.x -= WindowRect.x;
//        pos.y -= WindowRect.y;

//        if (inputNodeRect.Contains(pos))
//        {
//            retVal = inputNode;
//            inputNode = null;
//        }

//        return retVal;
//    }

//    public override void SetInput(BaseInputNode input, Vector2 clickPos)
//    {
//        clickPos.x -= WindowRect.x;
//        clickPos.y -= WindowRect.y;

//        if (inputNodeRect.Contains(clickPos))
//        {
//            inputNode = input;
//        }
//    }
//}

using UnityEngine;
using System.Collections;
using UnityEditor;

public class OutputNode : BaseNode
{
    private string result = "";

    private BaseInputNode _inputNode;
    private Rect _inputNodeRect;

    public OutputNode()
    {
        WindowTitle = "Output Node";
        HasInputs = true;
    }

    public override void DrawWindow()
    {
        base.DrawWindow();

        Event e = Event.current;

        string input1Title = "None";

        if (_inputNode)
        {
            input1Title = _inputNode.GetResult();
        }

        GUILayout.Label("Input 1: " + input1Title);

        if (e.type == EventType.Repaint)
        {
            _inputNodeRect = GUILayoutUtility.GetLastRect();
        }

        GUILayout.Label("Result: " + result);
    }

    public override void DrawCurves()
    {
        if (_inputNode)
        {
            Rect rect = WindowRect;
            rect.x += _inputNodeRect.x;
            rect.y += _inputNodeRect.y + _inputNodeRect.height / 2;
            rect.width = 1;
            rect.height = 1;

            NodeEditor.DrawNodeCurve(_inputNode.WindowRect, rect);
        }
    }

    public override void NodeDeleted(BaseNode node)
    {
        if (node.Equals(_inputNode))
        {
            _inputNode = null;
        }
    }

    public override BaseInputNode ClickedOnInput(Vector2 pos)
    {
        BaseInputNode retVal = null;

        pos.x -= WindowRect.x;
        pos.y -= WindowRect.y;

        if (_inputNodeRect.Contains(pos))
        {
            retVal = _inputNode;
            _inputNode = null;
        }

        return retVal;
    }

    public override void SetInput(BaseInputNode input, Vector2 clickPos)
    {
        clickPos.x -= WindowRect.x;
        clickPos.y -= WindowRect.y;

        if (_inputNodeRect.Contains(clickPos))
        {
            _inputNode = input;
        }
    }

}






