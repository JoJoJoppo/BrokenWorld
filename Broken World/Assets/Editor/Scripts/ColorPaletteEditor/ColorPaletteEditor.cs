using UnityEditor;
using UnityEngine;
using System.Collections;
using BrokenWorld.ColorPalette;

namespace BrokenWorld.Editors.ColorPaletteEditor
{

    [CustomEditor(typeof(Palette))]
    public class ColorPaletteEditor : Editor
    {

        public Palette colorPalette;

        void OnEnable()
        {
            colorPalette = target as Palette;
        }

        override public void OnInspectorGUI()
        {

            GUILayout.Label("Source Texture");

            colorPalette.Source = EditorGUILayout.ObjectField(colorPalette.Source, typeof(Texture2D), false) as Texture2D;


            EditorGUILayout.BeginHorizontal();

            GUILayout.Label("Current Color");
            GUILayout.Label("New Color");

            EditorGUILayout.EndHorizontal();

            for (var i = 0; i < colorPalette.CurrentPalette.Count; i++)
            {

                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.ColorField(colorPalette.CurrentPalette[i]);

                colorPalette.NewPalette[i] = EditorGUILayout.ColorField(colorPalette.NewPalette[i]);

                EditorGUILayout.EndHorizontal();
            }

            if (GUILayout.Button("Revert Palette"))
            {
                colorPalette.ResetPalette();
            }

            //DisplayPixels();

            EditorUtility.SetDirty(colorPalette);

        }

        [MenuItem("Assets/Create/Color Palette")]
        public static void CreateColorPalette()
        {

            if (Selection.activeObject is Texture2D)
            {

                var selectedTexture = Selection.activeObject as Texture2D;

                var selectionPath = AssetDatabase.GetAssetPath(selectedTexture);

                selectionPath = selectionPath.Replace(".png", "-color-palette.asset");

                var newPalette = CustomAssetUtil.CreateAsset<Palette>(selectionPath);

                newPalette.Source = selectedTexture;
                newPalette.ResetPalette();

                Debug.Log("Creating a Palette " + selectionPath);
            }
            else
            {
                Debug.Log("Can't create a Palette");
            }

        }

        private void DisplayPixels()
        {
            int _pixelSize = 16;
            int tileWidth = 16;
            int _tileHeight = 2;
            Event evt = Event.current;
            Texture2D _pixelBGTexture = EditorGUIUtility.whiteTexture;

            int rows = Mathf.RoundToInt(colorPalette.CurrentPalette.Count / 16)+1;
            int lastRowCount = colorPalette.CurrentPalette.Count % 16;

            int pixelsX = 16;
            int pixelsY = 2;

            GUILayout.BeginHorizontal(GUILayout.MaxWidth(200));
            //Generate pixel display
            for (int i = 0; i < 16; i++)
            {
                GUILayout.BeginVertical(GUILayout.Height(_tileHeight * _pixelSize));
                for (int j = 0; j < pixelsY; j++)
                {
                    var index = j + i * _tileHeight;

                        //var index = i * _tileHeight;
                        GUI.color = colorPalette.CurrentPalette[index];
                        ColorPickerHDRConfig p = new ColorPickerHDRConfig(0,0,0,0);
                        //colorPalette.Palette[index] = EditorGUILayout.ColorField("", colorPalette.Palette[index], false, false, false, p,params new GUILayoutOption()[0]);
                        var pixelRect = GUILayoutUtility.GetRect(GUIContent.none, GUIStyle.none, GUILayout.Width(_pixelSize), GUILayout.Height(_pixelSize));
                        GUI.DrawTexture(pixelRect, _pixelBGTexture);

                    //If mouse down and if rectangle have the mouse inside event
                    //if ((evt.type == EventType.mouseDown || evt.type == EventType.mouseDrag) && pixelRect.Contains(evt.mousePosition))
                    //{
                    //    colorPalette.Palette[index] = EditorGUILayout.ColorField(colorPalette.Palette[index], GUILayout.Width(_pixelSize), GUILayout.Height(_pixelSize));
                    //    evt.Use();
                    //    //colorPalette.Palette[index] = (evt.button == 0) ? _selectedColor : _eraseColor;
                    //    //evt.Use();
                    //}

                }
                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();
        }

    }
}
//using UnityEditor;
//using UnityEngine;
//using System.Collections;

//namespace BrokenWorld.Editors.ColorPaletteEditor
//{

//    [CustomEditor(typeof(ColorPalette))]
//    public class ColorPaletteEditor : Editor
//    {
//        public ColorPalette colorPalette;

//        void OnEnable()
//        {
//            colorPalette = target as ColorPalette;
//        }

//        public override void OnInspectorGUI()
//        {
//            GUILayout.Label("Source Texture");

//            colorPalette.Source = EditorGUILayout.ObjectField(colorPalette.Source, typeof(Texture2D), false) as Texture2D;


//            EditorGUILayout.BeginHorizontal();

//            GUILayout.Label("Current Color");
//            GUILayout.Label("New Color");

//            EditorGUILayout.EndHorizontal();

//            for (var i = 0; i < colorPalette.Palette.Length; i++)
//            {

//                EditorGUILayout.BeginHorizontal();

//                EditorGUILayout.ColorField(colorPalette.Palette[i]);

//                colorPalette.NewPalette[i] = EditorGUILayout.ColorField(colorPalette.NewPalette[i]);

//                EditorGUILayout.EndHorizontal();
//            }

//            if (GUILayout.Button("Revert Palette"))
//            {
//                colorPalette.ResetPalette();
//            }

//            EditorUtility.SetDirty(colorPalette);
//        }



//        [MenuItem("Assets/Create/Color Palette")]
//        public static void CreateColorPalette()
//        {

//            if (Selection.activeObject is Texture2D)
//            {
//                var selectedTexture = Selection.activeObject as Texture2D;
//                var selectionPath = AssetDatabase.GetAssetPath(selectedTexture);

//                selectionPath = selectionPath.Replace(".png", "-color-palette.asset");

//                var newPalette = EditorFunctions.CreateAsset<ColorPalette>(selectionPath);

//                newPalette.Source = selectedTexture;
//                newPalette.ResetPalette();

//                Debug.Log("Creating a palette " + selectionPath);
//                Debug.Log("Source: " + newPalette.Source);
//            }
//            else
//            {
//                Debug.LogWarning("Can't create a palette");
//            }
//        }


        
        
//    }

//}