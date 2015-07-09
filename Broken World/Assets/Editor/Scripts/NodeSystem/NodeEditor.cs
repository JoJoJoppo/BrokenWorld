//using UnityEngine;
//using UnityEditor;
//using System.Collections;
//using System.Collections.Generic;

//public class NodeEditor : EditorWindow
//{
//    private List<BaseNode> windows = new List<BaseNode>();
//    private Vector2 mousePos;
//    private BaseNode selectedNode;
//    private bool makeTransisistionMode = false;

//    [MenuItem("BrokenWorld/Node Editor")]
//    static void ShowNodeEditor()
//    {
//        NodeEditor editor = EditorWindow.GetWindow<NodeEditor>();
//    }

//    void OnGUI()
//    {
//        Event e = Event.current;
//        mousePos = e.mousePosition;

//        if (e.button == 1 && !makeTransisistionMode)
//        {
//            if (e.type == EventType.MouseDown)
//            {
//                bool clickedOnWindow = false;
//                int selectedIndex = -1;

//                for (int x = 0; x < windows.Count; x++)
//                {
//                    if (windows[x].WindowRect.Contains(mousePos))
//                    {
//                        selectedIndex = x;
//                        clickedOnWindow = true;
//                        break;
//                    }
//                }

//                GenericMenu menu = new GenericMenu();

//                if (!clickedOnWindow)
//                {
//                    menu.AddItem(new GUIContent("Add Input Node"), false, ContextCallback, "inputNode");
//                    menu.AddItem(new GUIContent("Add Output Node"), false, ContextCallback, "outputNode");
//                    menu.AddItem(new GUIContent("Add Calculation Node"), false, ContextCallback, "calcNode");
//                    menu.AddItem(new GUIContent("Add Comparison Node"), false, ContextCallback, "compNode");

//                    menu.ShowAsContext();
//                    e.Use();
//                }
//                else
//                {
//                    menu.AddItem(new GUIContent("Make Transition"), false, ContextCallback, "makeTransition");
//                    menu.AddSeparator("");
//                    menu.AddItem(new GUIContent("Delete Node"), false, ContextCallback, "deleteNode");
//                }
//                menu.ShowAsContext();
//                e.Use();
//            }
//        }
//        else if (e.button == 0 && e.type == EventType.MouseDown && makeTransisistionMode)
//        {
//            bool clickedOnWindow = false;
//            int selectedIndex = -1;

//            for (int x = 0; x < windows.Count; x++)
//            {
//                if (windows[x].WindowRect.Contains(mousePos))
//                {
//                    selectedIndex = x;
//                    clickedOnWindow = true;
//                    break;
//                }
//            }

//            if (clickedOnWindow && !windows[selectedIndex].Equals(selectedNode))
//            {
//                windows[selectedIndex].SetInput((BaseInputNode)selectedNode, mousePos);
//                makeTransisistionMode = false;
//                selectedNode = null;
//            }

//            if (!clickedOnWindow)
//            {
//                makeTransisistionMode = false;
//                selectedNode = null;
//            }
//            e.Use();
//        }
//        else if (e.button == 0 && e.type == EventType.MouseDown && !makeTransisistionMode)
//        {
//            bool clickedOnWindow = false;
//            int selectedIndex = -1;

//            for (int x = 0; x < windows.Count; x++)
//            {
//                if (windows[x].WindowRect.Contains(mousePos))
//                {
//                    selectedIndex = x;
//                    clickedOnWindow = true;
//                    break;
//                }
//            }

//            if (clickedOnWindow)
//            {
//                BaseInputNode nodeToChange = windows[selectedIndex].ClickedOnInput(mousePos);

//                if (nodeToChange)
//                {
//                    selectedNode = nodeToChange;
//                    makeTransisistionMode = true;
//                }
//            }
//        }

//        if (makeTransisistionMode && selectedNode != null)
//        {
//            Rect mouseRect = new Rect(e.mousePosition, new Vector2(10, 10));
//            DrawNodeCurve(selectedNode.WindowRect, mouseRect);
//            Repaint();

//        }

//        foreach (BaseNode n in windows)
//        {
//            n.DrawWindow();
//        }
//        BeginWindows();

//        for (int i = 0; i < windows.Count; i++)
//        {
//            windows[i].WindowRect = GUI.Window(i, windows[i].WindowRect, DrawNodeWindow, windows[i].WindowTitle);
//        }

//        EndWindows();

//    }

//    void ContextCallback(object obj)
//    {
//        string clb = obj.ToString();
//        bool clickedOnWindow = false;
//        bool isAction = false;

//        int selectIndex = -1;
//        BaseNode currentNode = null;

//        switch (obj.ToString())
//        {
//            case "inputNode":
//                currentNode = ScriptableObject.CreateInstance<InputNode>();
//                break;

//            case "outputNode":
//                currentNode = ScriptableObject.CreateInstance<OutputNode>();
//                break;

//            case "calcNode":
//                currentNode = ScriptableObject.CreateInstance<CalcNode>();

//                break;

//            case "compNode":
//                currentNode = ScriptableObject.CreateInstance<ComparisonNode>();
//                break;

//            case "makeTransition":
//                isAction = true;

//                for (int i = 0; i < windows.Count; i++)
//                {
//                    if (windows[i].WindowRect.Contains(mousePos))
//                    {
//                        selectIndex = i;
//                        clickedOnWindow = true;
//                        break;
//                    }
//                }

//                if (clickedOnWindow)
//                {
//                    selectedNode = windows[selectIndex];
//                    makeTransisistionMode = true;
//                }
//                break;

//            case "deleteNode":
//                isAction = true;

//                for (int i = 0; i < windows.Count; i++)
//                {
//                    if (windows[i].WindowRect.Contains(mousePos))
//                    {
//                        selectIndex = i;
//                        clickedOnWindow = true;
//                        break;
//                    }
//                }

//                if (clickedOnWindow)
//                {
//                    BaseNode selNode = windows[selectIndex];
//                    windows.RemoveAt(selectIndex);

//                    foreach (BaseNode n in windows)
//                    {
//                        n.NodeDeleted(selNode);
//                    }
//                }
//                break;

//            default:
//                Debug.LogError("Unknown action: " + obj.ToString());
//                break;

//        }
//        if (!isAction && currentNode != null)
//        {
//            currentNode.WindowRect = new Rect(mousePos.x, mousePos.y, 200, 100);
//            windows.Add(currentNode);
//        }

//    }

//    void DrawNodeWindow(int id)
//    {
//        windows[id].DrawWindow();
//        GUI.DragWindow();
//    }

//    public static void DrawNodeCurve(Rect start, Rect end)
//    {
//        Vector3 startPos = new Vector3(start.x + start.width / 2, start.y + start.height / 2, 0);
//        Vector3 endPos = new Vector3(end.x + end.width / 2, end.y + end.height / 2, 0);
//        Vector3 startTan = startPos + Vector3.right * 50;
//        Vector3 endTan = endPos + Vector3.left * 50;
//        Color shadowCol = new Color(0, 0, 0, .06f);

//        for (int i = 0; i < 3; i++)
//        {
//            Handles.DrawBezier(startPos, endPos, startTan, endTan, shadowCol, null, (i + 1) * 5);
//        }

//        Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.black, null, 1);
//    }
//}
using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class NodeEditor : EditorWindow
{

    private List<BaseNode> windows = new List<BaseNode>();

    private Vector2 mousePos;

    private BaseNode selectednode;

    private bool makeTransitionMode = false;

    [MenuItem("BrokenWorld/Node Editor")]
    static void ShowEditor()
    {
        NodeEditor editor = EditorWindow.GetWindow<NodeEditor>();
    }

    void OnGUI()
    {
        Event e = Event.current;

        mousePos = e.mousePosition;

        if (e.button == 1 && !makeTransitionMode)
        {
            if (e.type == EventType.MouseDown)
            {
                bool clickedOnWindow = false;
                int selectIndex = -1;

                for (int i = 0; i < windows.Count; i++)
                {
                    if (windows[i].WindowRect.Contains(mousePos))
                    {
                        selectIndex = i;
                        clickedOnWindow = true;
                        break;
                    }
                }

                if (!clickedOnWindow)
                {
                    GenericMenu menu = new GenericMenu();

                    menu.AddItem(new GUIContent("Add Input Node"), false, ContextCallback, "inputNode");
                    menu.AddItem(new GUIContent("Add Output Node"), false, ContextCallback, "outputNode");
                    menu.AddItem(new GUIContent("Add Calculation Node"), false, ContextCallback, "calcNode");
                    menu.AddItem(new GUIContent("Add Comparison Node"), false, ContextCallback, "compNode");

                    menu.ShowAsContext();
                    e.Use();
                }
                else
                {
                    GenericMenu menu = new GenericMenu();

                    menu.AddItem(new GUIContent("Make Transition"), false, ContextCallback, "makeTransition");
                    menu.AddSeparator("");
                    menu.AddItem(new GUIContent("Delete Node"), false, ContextCallback, "deleteNode");

                    menu.ShowAsContext();
                    e.Use();
                }
            }
        }
        else if (e.button == 0 && e.type == EventType.MouseDown && makeTransitionMode)
        {
            bool clickedOnWindow = false;
            int selectIndex = -1;

            for (int i = 0; i < windows.Count; i++)
            {
                if (windows[i].WindowRect.Contains(mousePos))
                {
                    selectIndex = i;
                    clickedOnWindow = true;
                    break;
                }
            }

            if (clickedOnWindow && !windows[selectIndex].Equals(selectednode))
            {
                windows[selectIndex].SetInput((BaseInputNode)selectednode, mousePos);
                makeTransitionMode = false;
                selectednode = null;
            }

            if (!clickedOnWindow)
            {
                makeTransitionMode = false;
                selectednode = null;
            }

            e.Use();
        }
        else if (e.button == 0 && e.type == EventType.MouseDown && !makeTransitionMode)
        {
            bool clickedOnWindow = false;
            int selectIndex = -1;

            for (int i = 0; i < windows.Count; i++)
            {
                if (windows[i].WindowRect.Contains(mousePos))
                {
                    selectIndex = i;
                    clickedOnWindow = true;
                    break;
                }
            }

            if (clickedOnWindow)
            {
                BaseInputNode nodeTochange = windows[selectIndex].ClickedOnInput(mousePos);

                if (nodeTochange != null)
                {
                    selectednode = nodeTochange;
                    makeTransitionMode = true;
                }
            }
        }

        if (makeTransitionMode && selectednode != null)
        {
            Rect mouseRect = new Rect(e.mousePosition.x, e.mousePosition.y, 10, 10);

            DrawNodeCurve(selectednode.WindowRect, mouseRect);

            Repaint();
        }

        foreach (BaseNode n in windows)
        {
            n.DrawCurves();
        }

        BeginWindows();

        for (int i = 0; i < windows.Count; i++)
        {
            windows[i].WindowRect = GUI.Window(i, windows[i].WindowRect, DrawNodeWindow, windows[i].WindowTitle);
        }

        EndWindows();

    }

    void DrawNodeWindow(int id)
    {
        windows[id].DrawWindow();
        GUI.DragWindow();
    }

    void ContextCallback(object obj)
    {
        string clb = obj.ToString();

        if (clb.Equals("inputNode"))
        {
            InputNode inputNode = new InputNode();
            inputNode.WindowRect = new Rect(mousePos.x, mousePos.y, 200, 150);

            windows.Add(inputNode);
        }
        else if (clb.Equals("outputNode"))
        {
            OutputNode outputNode = new OutputNode();
            outputNode.WindowRect = new Rect(mousePos.x, mousePos.y, 200, 100);

            windows.Add(outputNode);
        }
        else if (clb.Equals("calcNode"))
        {
            CalcNode calcNode = new CalcNode();
            calcNode.WindowRect = new Rect(mousePos.x, mousePos.y, 200, 100);

            windows.Add(calcNode);
        }
        else if (clb.Equals("compNode"))
        {
            ComparisonNode comparisonNode = new ComparisonNode();
            comparisonNode.WindowRect = new Rect(mousePos.x, mousePos.y, 200, 100);

            windows.Add(comparisonNode);
        }
        else if (clb.Equals("makeTransition"))
        {
            bool clickedOnWindow = false;
            int selectIndex = -1;

            for (int i = 0; i < windows.Count; i++)
            {
                if (windows[i].WindowRect.Contains(mousePos))
                {
                    selectIndex = i;
                    clickedOnWindow = true;
                    break;
                }
            }

            if (clickedOnWindow)
            {
                selectednode = windows[selectIndex];
                makeTransitionMode = true;
            }
        }
        else if (clb.Equals("deleteNode"))
        {
            bool clickedOnWindow = false;
            int selectIndex = -1;

            for (int i = 0; i < windows.Count; i++)
            {
                if (windows[i].WindowRect.Contains(mousePos))
                {
                    selectIndex = i;
                    clickedOnWindow = true;
                    break;
                }
            }

            if (clickedOnWindow)
            {
                BaseNode selNode = windows[selectIndex];
                windows.RemoveAt(selectIndex);

                foreach (BaseNode n in windows)
                {
                    n.NodeDeleted(selNode);
                }
            }
        }
    }

    public static void DrawNodeCurve(Rect start, Rect end)
    {

        Vector3 startPos = new Vector3(start.x + start.width / 2, start.y + start.height / 2, 0);
        Vector3 endPos = new Vector3(end.x + end.width / 2, end.y + end.height / 2, 0);
        Vector3 startTan = startPos + Vector3.right * 50;
        Vector3 endTan = endPos + Vector3.left * 50;
        Color shadowCol = new Color(0, 0, 0, .06f);

        for (int i = 0; i < 3; i++)
        {
            Handles.DrawBezier(startPos, endPos, startTan, endTan, shadowCol, null, (i + 1) * 5);
        }

        Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.black, null, 1);
    }
}
