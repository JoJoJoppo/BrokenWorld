using UnityEngine;
using UnityEditor;
using System.Collections;

namespace BrokenWorld.ItemSystem.Editor
{
    public partial class ISObjectEditor
    {
        int ActiveButton = 0;

        string[] toolbarStrings = new string[] { "Weapons", "Armor", "Potions", "About" };
        
        void Form_TopTabBar()
        {
            
            GUILayout.BeginHorizontal("Box", GUILayout.ExpandWidth(true));

            ActiveButton = GUILayout.Toolbar(ActiveButton, toolbarStrings);
            switch(ActiveButton)
            {
                case 1:

                    break;
                case 2:

                    break;
                case 3:

                    break;
                case 4:

                    break;
            }
            //WeaponTab();
            //ArmorTab();
            //PotionsTab();
            //AboutTab();
           
            GUILayout.EndHorizontal();
        }

      
       
    }
}