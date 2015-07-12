using UnityEngine;
using System.Collections;

namespace BrokenWorld.Editors.NodeEditor
{ 

    public class BaseInputNode : BaseNode 
    {

        private BaseInputNode[] _inputs;

        public BaseInputNode[] Inputs
        {
            get { return _inputs; }
            set { _inputs = value; }
        }


    



        private BaseInputNode _input1;
        private BaseInputNode _input2;

        private Rect _input1Rect;
        private Rect _input2Rect;

        #region Getters & Setters

        public BaseInputNode Input1
        {
            get { return _input1; }
            set { _input1 = value; }
        }

        public BaseInputNode Input2
        {
            get { return _input2; }
            set { _input2 = value; }
        }

        public Rect Input1Rect
        {
            get { return _input1Rect; }
            set { _input1Rect = value; }
        }

        public Rect Input2Rect
        {
            get { return _input2Rect; }
            set { _input2Rect = value; }
        }



        #endregion

        public virtual string GetResult()
        {
            return "none";
        }


        public override void SetInput(BaseInputNode input, Vector2 clickPos)
        {
            clickPos.x -= WindowRect.x;
            clickPos.y -= WindowRect.y;

            if (_input1Rect.Contains(clickPos))
            {
                _input1 = input;

            }
            else if (_input2Rect.Contains(clickPos))
            {
                _input2 = input;
            }
        }

        public override void DrawCurves()
        {
            if (_input1)
            {
                Rect rect = WindowRect;
                rect.x += _input1Rect.x;
                rect.y += _input1Rect.y + _input2Rect.height / 2;
                rect.width = 1;
                rect.height = 1;

                NodeEditor.DrawNodeCurve(_input1.WindowRect, rect);
            }

            if (_input2)
            {
                Rect rect = WindowRect;
                rect.x += _input2Rect.x;
                rect.y += _input2Rect.y + _input2Rect.height / 2;
                rect.width = 1;
                rect.height = 1;

                NodeEditor.DrawNodeCurve(_input2.WindowRect, rect);
            }
        }

        public override void NodeDeleted(BaseNode node)
        {
            if (node.Equals(_input1))
            {
                _input1 = null;
            }

            if (node.Equals(_input2))
            {
                _input2 = null;
            }
        }

        public override BaseInputNode ClickedOnInput(Vector2 pos)
        {
            BaseInputNode retVal = null;

            pos.x -= WindowRect.x;
            pos.y -= WindowRect.y;

            if (_input1Rect.Contains(pos))
            {
                retVal = _input1;
                _input1 = null;
            }
            else if (_input2Rect.Contains(pos))
            {
                retVal = _input2;
                _input2 = null;
            }

            return retVal;
        }
    }
}