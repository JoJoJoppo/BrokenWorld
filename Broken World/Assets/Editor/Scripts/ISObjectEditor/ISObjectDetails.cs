using UnityEngine;
using UnityEditor;
using System.Collections;

namespace BrokenWorld.ItemSystem.Editor
{
    public partial class ISObjectEditor
    {
        enum DisplayState {NONE, DETAILS };
        ISWeapon tempWeapon = new ISWeapon();
        bool showNewWeaponDetails = false;
        DisplayState state = DisplayState.NONE;

        void Form_ItemDetails()
        {
            GUILayout.BeginVertical("Box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));

            EditorGUILayout.LabelField("State: " + state);
            switch (state)
            { 
                case DisplayState.DETAILS:
                    if (showNewWeaponDetails)
                        DisplayNewWeapon();
                    break;

                default:

                    break;
            }
            

            DisplayButtons();
            
            GUILayout.EndVertical();
        }

        void DisplayNewWeapon()
        {
            GUILayout.BeginVertical(GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
            

            tempWeapon.OnGUI();


            GUILayout.EndVertical();
        }

        void DisplayButtons()
        {
            GUILayout.Space(50);
            GUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));

            if (showNewWeaponDetails)
            {
                if (GUILayout.Button("Save"))
                {
                    showNewWeaponDetails = false;
                    string DATABASE_NAME = @"bwQualityDatabase.asset";
                    string DATABASE_PATH = @"Database";

                    ISQualityDatabase qdb;

                    qdb = ISQualityDatabase.GetDatabase<ISQualityDatabase>(DATABASE_PATH, DATABASE_NAME);

                    tempWeapon.Quality = qdb.Get(tempWeapon.SelectedQualityID);


                    database.Add(tempWeapon);


                    tempWeapon = null;
                    state = DisplayState.NONE;
                }

                if (GUILayout.Button("Cancel"))
                {
                    showNewWeaponDetails = false;
                    tempWeapon = null;
                    state = DisplayState.NONE;
                }
               
            }
            else
            {
                if (GUILayout.Button("Create Weapon"))
                {
                    tempWeapon = new ISWeapon();
                    showNewWeaponDetails = true;
                    state = DisplayState.DETAILS;
                }

            }

            GUILayout.EndHorizontal();
        }

    }
}