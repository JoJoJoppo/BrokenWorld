using UnityEngine;
using UnityEditor;
using System.Collections;

namespace BrokenWorld.Editors.NodeEditor
{

    public abstract class BaseNode : ScriptableObject
    {
        public Rect WindowRect;
        public bool HasInputs = false;
        public string WindowTitle = "";

        public virtual void DrawWindow()
        {
            WindowTitle = EditorGUILayout.TextField("Title", WindowTitle);

        }

        public abstract void DrawCurves();

        public virtual void SetInput(BaseInputNode input, Vector2 clickPos) { }

        public virtual void NodeDeleted(BaseNode node) { }

        public virtual BaseInputNode ClickedOnInput(Vector2 pos)
        {
            return null;
        }

    }
}