using UnityEngine;
using UnityEditor;
using System.Collections;

namespace BrokenWorld.ItemSystem.Editor
{
    public partial class ISQualityDatabaseEditor
    {
        
        /// <summary>
        /// Lists all of the stored qualities in the database
        /// </summary>
        void ListView()
        {

            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, GUILayout.ExpandHeight(true));

            DisplayQualities();

            EditorGUILayout.EndScrollView();
        }

        void DisplayQualities()
        {

            for (int x = 0; x < qualityDatabase.Count; x++)
            {
                GUILayout.BeginHorizontal("Box");


                selectedTexture = (qualityDatabase.Get(x).Icon)? qualityDatabase.Get(x).Icon.texture : null;

                if (GUILayout.Button(selectedTexture, GUILayout.Width(SPRITE_BUTTON_SIZE), GUILayout.Height(SPRITE_BUTTON_SIZE)))
                {
                    int controlerID = EditorGUIUtility.GetControlID(FocusType.Passive);
                    EditorGUIUtility.ShowObjectPicker<Sprite>(null, true, null, controlerID);
                    selectedIndex = x;
                }

                if (Event.current.commandName == "ObjectSelectorUpdated")
                {
                    if (selectedIndex != -1)
                    { 
                        qualityDatabase.Get(selectedIndex).Icon = (Sprite)EditorGUIUtility.GetObjectPickerObject();
                        selectedIndex = -1;
                        Repaint();
                    }
                }
                GUILayout.BeginVertical();
                qualityDatabase.Get(x).Name = GUILayout.TextField(qualityDatabase.Get(x).Name);

                if (GUILayout.Button("x", GUILayout.Width(30), GUILayout.Height(25)))
                { 
                    if(EditorUtility.DisplayDialog("Delete Quality", "Are you shour you want to delete " + qualityDatabase.Get(x).Name + " from the database?",
                                                    "Delete", 
                                                    "Cancel"))
                    {
                        qualityDatabase.RemoveAt(x);
                    }
                }
                GUILayout.EndVertical();
                GUILayout.EndHorizontal();
            }


        }
	}
}
