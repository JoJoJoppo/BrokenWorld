using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace BrokenWorld.Editors.MapEditor
{ 

    public partial class MapEditorWindow : EditorWindow
    {

        [MenuItem("BrokenWorld/2D Map Editor")]
        public static void ShowMapEditorWindow()
        {
            var window = GetWindow<MapEditorWindow>();
            window.titleContent.text = "2D Map Editor";
        }

        private Color[] _pixels;
        private int _tileWidth;
        private int _tileHeight;
        private Texture2D _pixelBGTexture;
        private Color _selectedColor;
        private Color _eraseColor;
        private Renderer _textureTarget;
        private string _textureName;
        private int _currentTab;
        public Vector2 scrollPosition = Vector2.zero;

        private string _targetSprite;

        public int _pixelSize;
        public int ActiveTileIndexX;
        public int ActiveTileIndexY;

        string[] toolbarStrings;
        int selectedTileSetIndex = 0;
        string[] tileSetList;

        const int TOOLBAR_WIDTH = 300;
        const int REAL_TILE_SIZE = 16;
        const int PREVIEW_TILESET_SIZE = 275;
        Texture2D texture;

        const string STANDARD_FOLDER_FILE = @"Assets/Editor/StandardPaths.txt";


        

        void OnEnable()
        {
            EditorFunctions.GenerateFolderHierarchy(File.ReadAllLines(STANDARD_FOLDER_FILE));

            

            ActiveTileIndexX = 0;
            ActiveTileIndexY = 0;


            texture = new Texture2D(250,250);
            List<string> result = new List<string>();
            toolbarStrings = new string[] { "Map Editor", "Tile Editor" };
            var info = new DirectoryInfo("Assets/Game/Sprites/TileSets");
            var fileInfo = info.GetFiles();
            foreach (FileInfo file in fileInfo)
            {
                if(!file.Name.Contains("meta"))
                    result.Add(Path.GetFileNameWithoutExtension(file.Name));
            }
                


            tileSetList = result.ToArray<string>();

            _pixelSize = 32;
            _tileWidth = _tileHeight = REAL_TILE_SIZE;
            _pixels = new Color[_tileWidth * _tileHeight];

            for (int i = 0; i < _pixels.Length; i++)
                _pixels[i] = EditorFunctions.RandomColor();

            _pixelBGTexture = EditorGUIUtility.whiteTexture;
            _textureTarget = new Renderer();

            _selectedColor = Color.white;
            _eraseColor = Color.white;

            _textureName = "";
            _currentTab = 1;
            _targetSprite = "Assets/Game/Sprites/chipset01.png";


             
            
                
        }

        public void OnGUI()
        {
            GUILayout.BeginHorizontal(GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));

            switch (_currentTab)
            {
                case 0:
                    DisplayMapEditor();
                    break;
                case 1:
                    DisplayTileEditor();
                    break;
                default:

                    break;
            }

            GUILayout.EndHorizontal();
        }


    }
}