using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Linq;

namespace BrokenWorld.WorldEditor
{

    public partial class MapEditorWindow
    {
        private void DisplayTileEditor()
        {
            //Controls
            GUILayout.BeginVertical("box", GUILayout.Width(TOOLBAR_WIDTH), GUILayout.ExpandHeight(true));
            _currentTab = GUILayout.Toolbar(_currentTab, toolbarStrings);
            TileEditorControls();
            GUILayout.EndVertical();

            //Canvas
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));
                TileEditorCanvas();
            GUILayout.EndScrollView();

           
        }

        private void TileEditorCanvas()
        {
            Event evt = Event.current;

            var oldColor = GUI.color;

            GUILayout.BeginHorizontal(GUILayout.Width(_tileWidth * _pixelSize));
            //Generate pixel display
            for (int i = 0; i < _tileWidth; i++)
            {
                GUILayout.BeginVertical(GUILayout.Height(_tileHeight * _pixelSize));
                for (int j = 0; j < _tileHeight; j++)
                {
                    var index = j + i * _tileHeight;

                    GUI.color = _pixels[index];
                    var pixelRect = GUILayoutUtility.GetRect(GUIContent.none, GUIStyle.none, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
                    GUI.DrawTexture(pixelRect, _pixelBGTexture);

                    //If mouse down and if rectangle have the mouse inside event
                    if ((evt.type == EventType.mouseDown || evt.type == EventType.mouseDrag) && pixelRect.Contains(evt.mousePosition))
                    {
                        _pixels[index] = (evt.button == 0) ? _selectedColor : _eraseColor;
                        evt.Use();
                    }

                }
                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();

            GUI.color = oldColor;

        }

        private void TileEditorControls()
        {
            _pixelSize = EditorGUILayout.IntField("Zoom", _pixelSize);    

            //Tile Size
            _tileWidth = EditorGUILayout.IntField("Width", _tileWidth);
            _tileHeight = EditorGUILayout.IntField("Height", _tileHeight);

            //Current colors used
            _selectedColor = EditorGUILayout.ColorField("Paint Color", _selectedColor);
            _eraseColor = EditorGUILayout.ColorField("Erase Color", _eraseColor);

            //Name of texture when saved to file
            _textureName = EditorGUILayout.TextField("Texture Name", _textureName);

            if (GUILayout.Button("Fill All"))
                _pixels = _pixels.Select(o => o = _selectedColor).ToArray();

            GUILayout.FlexibleSpace();

            //Target to attach texture to 
            _textureTarget = EditorGUILayout.ObjectField("Output Renderer", _textureTarget, typeof(Renderer), true) as Renderer;

            //Save the current enable-state
            var oldEnabled = GUI.enabled;

            if (GUILayout.Button("Reset"))
                Reset();

            if (GUILayout.Button("Load Image"))
                LoadImage();

            if (GUILayout.Button("Save"))
                Save();

            //Get previous enable-state
            GUI.enabled = oldEnabled;

            
        }

        private void Reset()
        {
            _tileWidth = _tileHeight = 16;
            _pixels = new Color[_tileWidth * _tileHeight];

            for (int i = 0; i < _pixels.Length; i++)
                _pixels[i] = EditorFunctions.RandomColor();

            _textureName = "";
        }

        private void LoadImage()
        {
            string path = EditorUtility.OpenFilePanel("Choose target texturefile", "", "png");

            if (path != "")
            {
                Texture2D tex = EditorFunctions.LoadTextureFromFile(path);

                Color[] texBytes = tex.GetPixels();

                _tileHeight = tex.height;
                _tileWidth = tex.width;

                _pixels = new Color[_tileWidth * _tileHeight];
                //Texture2D texRotated = new Texture2D(_tileHeight, _tileWidth);

                ////Overwrite pixels in texture according to current pixel array
                //for (int i = 0; i < _tileWidth; i++)
                //{
                //    for (int j = 0; j < _tileHeight; j++)
                //        texRotated.SetPixel(_tileHeight - 1 - i, j, texBytes[j + i * _tileHeight]);
                //}
                //_pixels = texRotated.GetPixels();
                _pixels = texBytes;

                //if (texBytes.Length <= _pixels.Length)
                //{
                //    _pixels = texBytes;
                //}
                //else
                //{
                //    Debug.LogWarning("The file was to large");
                //}

            }
        }

        private void Save()
        {
            if (texture == null)
                return;
            Color[] tempPixels = EditorFunctions.RotateColorArray(_pixels, REAL_TILE_SIZE, 90);
            texture.SetPixels(0, 0, 16, 16, tempPixels);
            texture.Apply();
            EditorUtility.SetDirty(texture);


            //Texture2D texture = new Texture2D(_tileWidth, _tileHeight, TextureFormat.RGBA32, false);
            //texture.filterMode = FilterMode.Point;

            //if (_textureTarget != null)
            //{
            //    _textureTarget.material = new Material(Shader.Find("Diffuse"));
            //    _textureTarget.sharedMaterial.mainTexture = texture;
            //}

            ////Overwrite pixels in texture according to current pixel array
            //for (int i = 0; i < _tileWidth; i++)
            //{
            //    for (int j = 0; j < _tileHeight; j++)
            //        texture.SetPixel(i, _tileHeight - 1 - j, _pixels[j + i * _tileHeight]);
            //}

            //texture.name = _textureName;
            //texture.Apply();

            //if (_textureName != "")
            //    EditorFunctions.SaveTextureToFile(texture, _textureName);
            //else
            //    Debug.LogWarning("Name not set");

        }
    }
}
