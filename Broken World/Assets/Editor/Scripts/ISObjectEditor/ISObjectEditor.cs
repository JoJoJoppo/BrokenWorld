using UnityEngine;
using UnityEditor;
using System.Collections;

namespace BrokenWorld.ItemSystem.Editor
{
    public partial class ISObjectEditor : EditorWindow
    {
        ISWeaponDatabase database;
        Texture2D selectedTexture;

        const string DATABASE_NAME = @"bwWeaponDatabase.asset";
        const string DATABASE_PATH = @"Database";
        const string DATABASE_FULL_PATH = @"Assets/" + DATABASE_PATH + "/" + DATABASE_NAME;

        [MenuItem("BrokenWorld/Database/Item System Editor %#i")]
        public static void Init()
        {
            ISObjectEditor window = ISObjectEditor.GetWindow<ISObjectEditor>();
            window.minSize = new Vector2(800, 600);
            window.titleContent.text = "Item System Database";
            window.Show();
        }

        void OnEnable()
        {
            if (database == null)
                database = ISWeaponDatabase.GetDatabase<ISWeaponDatabase>(DATABASE_PATH, DATABASE_NAME);
        }


        void OnGUI()
        {
            Form_TopTabBar();

            GUILayout.BeginHorizontal();
            Form_ListView();
            Form_ItemDetails();
            GUILayout.EndHorizontal();

            Form_StatusBar();
        }
    }
}