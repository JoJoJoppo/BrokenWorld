﻿using UnityEngine;
using UnityEditor;
using System.Collections;

public class CustomAssetUtility
{

    public static T CreateAsset<T>(string path) where T : ScriptableObject
    {
        T asset = null;

        asset = ScriptableObject.CreateInstance<T>();

        var newPath = AssetDatabase.GenerateUniqueAssetPath(path);

        AssetDatabase.CreateAsset(asset, newPath);

        AssetDatabase.SaveAssets();

        return asset;
    }
    
}
