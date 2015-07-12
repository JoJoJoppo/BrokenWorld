using UnityEngine;
using UnityEditor;
using System.Collections;

namespace BrokenWorld.Editors.EnvironmentSystem
{
    public class EnvironmentEditor : EditorWindow
    {
        Color EnvironmentColor;
        Color NightColor;
        Color ColorTotal;


        [MenuItem("BrokenWorld/Environment Editor")]
        public static void ShowEnvironmentEditor()
        {
            var window = GetWindow<EnvironmentEditor>();
            window.titleContent.text = "Environment Editor";
        }

        void OnEnable()
        {
            EnvironmentColor = Color.white;
            NightColor = Color.white;
            ColorTotal = Color.white;


           


            
        }

        public void OnGUI()
        {

            EnvironmentColor = EditorGUILayout.ColorField("Environment Color", EnvironmentColor);
            NightColor = EditorGUILayout.ColorField("Night Color", NightColor);
            ColorTotal = EditorGUILayout.ColorField("Color Total", ColorTotal);
            ColorTotal.r = (EnvironmentColor.r + NightColor.r) / 2;
            ColorTotal.g = (EnvironmentColor.g + NightColor.g) / 2;
            ColorTotal.b = (EnvironmentColor.b + NightColor.b) / 2;

            RenderSettings.ambientLight = ColorTotal;
        }
    }
}