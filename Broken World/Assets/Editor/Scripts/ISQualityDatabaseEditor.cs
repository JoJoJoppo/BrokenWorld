using UnityEngine;
using UnityEditor;
using System.Collections;

namespace BrokenWorld.ItemSystem.Editor 
{ 
    public partial class ISQualityDatabaseEditor : EditorWindow 
    {
        ISQualityDatabase qualityDatabase;
        Texture2D selectedTexture;
        
        //ListView variables
        Vector2 _scrollPos;
        int selectedIndex = -1;

        const int SPRITE_BUTTON_SIZE = 46;
        const string DATABASE_NAME = @"bwQualityDatabase.asset";
        const string DATABASE_PATH = @"Database";
        //const string DATABASE_FULL_PATH = @"Assets/" + DATABASE_PATH + "/" + DATABASE_NAME;

        [MenuItem("BrokenWorld/Database/Quality Editor %#w")]
        public static void Init()
        {
            ISQualityDatabaseEditor window = ISQualityDatabaseEditor.GetWindow<ISQualityDatabaseEditor>();
            window.minSize = new Vector2(400, 300);
            window.titleContent.text = "Quality Database";
            window.Show();
        }


        void OnEnable()
        {
            if( qualityDatabase == null)
                qualityDatabase = ISQualityDatabase.GetDatabase<ISQualityDatabase>(DATABASE_PATH, DATABASE_NAME);
        }

        void OnGUI()
        {
            if (qualityDatabase == null)
                Debug.LogWarning("QualityDatabase not loaded!");
            
            ListView();
            
            GUILayout.BeginHorizontal("Box", GUILayout.ExpandWidth(true));
            BottomBar();
            GUILayout.EndHorizontal();
        }

        void BottomBar()
        {
            GUILayout.Label("Quality count: " + qualityDatabase.Count);

            if(GUILayout.Button("Add"))
                qualityDatabase.Add(new ISQuality());
        }

        

    }
}
