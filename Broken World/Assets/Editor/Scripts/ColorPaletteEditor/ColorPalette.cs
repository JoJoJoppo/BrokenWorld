using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class ColorPalette : ScriptableObject 
{
    public Texture2D source;
    public List<Color> palette = new List<Color>();
    public List<Color> newPalette = new List<Color>();

    [MenuItem("Assets/Create/Color Palette")]
    public static void CreateColorPalette()
    {
        if (Selection.activeObject is Texture2D)
        {
            var selectedTexture = Selection.activeObject as Texture2D;
            var selectionPath = AssetDatabase.GetAssetPath(selectedTexture);

            selectionPath = selectionPath.Replace(".png", "-color-palette.asset");

            var newPalette = CustomAssetUtility.CreateAsset<ColorPalette>(selectionPath);

            newPalette.source = selectedTexture;
            newPalette.ResetPalette();

            Debug.Log("Creating a palette " + selectionPath);
        }
        else
        {
            Debug.LogWarning("Can't create a palette");
        }
    }

    private List<Color> BuildPalette(Texture2D texture)
    {
        List<Color> palette = new List<Color>();

        var colors = texture.GetPixels();
        foreach (Color c in colors)
        {
            if (!palette.Contains(c))
            { 
                if(c.a == 1)
                    palette.Add(c);
            }
                
        }

        return palette;

    }

    public void ResetPalette()
    {
        palette = BuildPalette(source);
        newPalette = new List<Color>(palette);
    }
}





[CustomEditor(typeof(ColorPalette))]
public class ColorPaletteEditor : Editor 
{
    public ColorPalette colorPalette;

    void OnEnable() 
    {
        colorPalette = target as ColorPalette;
    }

    public override void OnInspectorGUI()
    {
        GUILayout.Label("Source Texture");

        colorPalette.source = EditorGUILayout.ObjectField(colorPalette.source, typeof(Texture2D), false) as Texture2D;

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Current Color");
        GUILayout.Label("New Color");
        EditorGUILayout.EndHorizontal();

        for (var x = 0; x < colorPalette.palette.Count; x++)
        {
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.ColorField(colorPalette.palette[x]);
            colorPalette.newPalette[x] = EditorGUILayout.ColorField(colorPalette.newPalette[x]);

            EditorGUILayout.EndHorizontal();
        }

        if (GUILayout.Button("Revert Palette"))
        {
            colorPalette.ResetPalette();
        }

        EditorUtility.SetDirty(colorPalette);
    }
}