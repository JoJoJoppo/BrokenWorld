using UnityEngine;
using UnityEditor;
using System.Collections;

namespace BrokenWorld.ItemSystem.Editor
{
    public partial class ISObjectEditor
    {
        Vector2 _scrollPos = Vector2.zero;
        const int LISTVIEW_WIDTH = 200;
        const int LISTVIEW_BUTTON_WIDTH = 190;
        const int LISTVIEW_BUTTON_HEIGHT= 25;

        private int _selectedIndex = -1;

        void Form_ListView()
        {
            _scrollPos = GUILayout.BeginScrollView(_scrollPos, "Box", GUILayout.ExpandHeight(true), GUILayout.Width(LISTVIEW_WIDTH));

            for (int x = 0; x < database.Count; x++)
            {
                if (GUILayout.Button(database.Get(x).Name, "Box", GUILayout.Width(LISTVIEW_BUTTON_WIDTH), GUILayout.Height(LISTVIEW_BUTTON_HEIGHT)))
                {
                    _selectedIndex = x;
                    tempWeapon = database.Get(x);
                    showNewWeaponDetails = true;
                    state = DisplayState.DETAILS;
                }
            }

            

            GUILayout.EndScrollView();
        }
    }
}