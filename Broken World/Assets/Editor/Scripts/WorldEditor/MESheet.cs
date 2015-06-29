using UnityEngine;
using UnityEngine.Audio;
using UnityEditor;
using System.Collections;
using System.Linq;

namespace BrokenWorld.WorldEditor
{

    public partial class MapEditorWindow
    {
        
       

        private void DisplayMapEditor()
        {
            //Controls
            GUILayout.BeginVertical("box", GUILayout.Width(TOOLBAR_WIDTH), GUILayout.ExpandHeight(true));
            _currentTab = GUILayout.Toolbar(_currentTab, toolbarStrings);
            MapEditorControls();
            GUILayout.EndVertical();

            //Canvas
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));
                MapEditorCanvas();
            GUILayout.EndScrollView();

           
        }
        private void MapEditorCanvas()
        {
            GUILayout.FlexibleSpace();
        }

        private void MapEditorControls()
        {
            _targetSprite = EditorGUILayout.TextField("Target Sprite", _targetSprite);
            texture = EditorGUILayout.ObjectField("Target Sheet", texture, typeof(Texture2D), true) as Texture2D;

            selectedTileSetIndex = EditorGUILayout.Popup("TileSet", selectedTileSetIndex, tileSetList);
            
            GUIStyle style = new GUIStyle();
            style.normal.background = texture;

            GUILayout.BeginHorizontal(GUILayout.Width(PREVIEW_TILESET_SIZE), GUILayout.Height(PREVIEW_TILESET_SIZE));
            EditorGUI.LabelField(new Rect(12, 160, PREVIEW_TILESET_SIZE, PREVIEW_TILESET_SIZE), GUIContent.none, style);

            GUIStyle buttonStyle = new GUIStyle();
            buttonStyle.padding = new RectOffset(0,0,0,0);
            for (int x = 0; x < 16; x++)
            { 
                GUILayout.BeginVertical();
                for (int y = 0; y < 16; y++)
                {
                    if (GUILayout.Button("", buttonStyle, GUILayout.Width(PREVIEW_TILESET_SIZE / 16), GUILayout.Height(PREVIEW_TILESET_SIZE / 16)))
                    {
                        ActiveTileIndexX = x;
                        ActiveTileIndexY = y;
                        Debug.Log(ActiveTileIndexX + ":" + ActiveTileIndexY);
                    }
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();


            if (GUILayout.Button("Edit"))
            {
                //Set asset to readable
                EditorFunctions.SetTextureImporterFormat(texture, true);

                _pixels = EditorFunctions.RotateColorArray(texture.GetPixels(ActiveTileIndexX * REAL_TILE_SIZE, ActiveTileIndexY * REAL_TILE_SIZE, REAL_TILE_SIZE, REAL_TILE_SIZE), REAL_TILE_SIZE, -90);
            }

            //Save the current enable-state
            var oldEnabled = GUI.enabled;

            if (_targetSprite == "") GUI.enabled = false;

            if (GUILayout.Button("Load Tile Set"))
            {
                EditorFunctions.SliceSprite(_targetSprite);
            }
            GUI.enabled = oldEnabled;
            GUILayout.FlexibleSpace();

            

            //ToDo
            //Skapa en box som går att scrolla som innehållar alla slices av ett tileset 

            //Get previous enable-state
        }
	}
}
