///Author: Jonathan Bark

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
namespace BrokenWorld
{

    /// <summary>
    /// Functions usable to expand Unity editor
    /// </summary>
    public static class EditorFunctions
    {
        /// <summary>
        /// Load a imagefile from disk, and returns it as an texture2D
        /// </summary>
        /// <param name="filePath">the exact path to the file</param>
        /// <returns>Texture2D version of the imagefile</returns>
        public static Texture2D LoadTextureFromFile(string filePath)
        {
            Texture2D loadedTex = new Texture2D(2, 2);

            if (System.IO.File.Exists(filePath))
                loadedTex.LoadImage(System.IO.File.ReadAllBytes(filePath));
            else
                Debug.LogWarning("File was not found");

            return loadedTex;
        }

        /// <summary>
        /// Save target named texture to assets folder
        /// </summary>
        /// <param name="texture">A texture2D</param>
        public static void SaveTextureToFile(Texture2D texture, string fileName)
        {
            SaveTextureToFile(texture, fileName, "/");
        }

        /// <summary>
        /// Save target named texture to choosen path
        /// </summary>
        /// <param name="texture">A texture2D</param>
        /// <param name="fileName">Name of the file, exclude file ending</param>
        /// <param name="subpath">Target subfolder within assets</param>
        public static void SaveTextureToFile(Texture2D texture, string fileName, string subpath)
        {
            if (texture != null && fileName != "")
            {
                var path = Application.dataPath + subpath + fileName + ".png";

                System.IO.File.WriteAllBytes(path, texture.EncodeToPNG());

                AssetDatabase.Refresh();
                Debug.Log("Saved the texture: " + fileName + " at " + path);
            }
            else
                Debug.LogWarning("The texture was not saved because the filename was not set");

        }

        public static Texture2D GetCutFromTexture(Color[] textureBytes, int xPosition, int yPosition, int width, int height)
        {
            Texture2D croppedTexture = new Texture2D(width, height);

            if (xPosition >= 0 && yPosition >= 0 && width > 0 && height > 0)
            {
                Debug.Log("lyckades!");
                //Overwrite pixels in texture according to current pixel array

                Debug.Log("xPosition: " + xPosition.ToString());
                Debug.Log("yPosition: " + yPosition.ToString());
                Debug.Log("width: " + width.ToString());
                Debug.Log("height: " + height.ToString());

                for (int i = xPosition; i < xPosition + width; i++)
                {
                    Debug.Log("x: " + i.ToString());
                    for (int j = yPosition; j < yPosition + height; j++)
                    {
                        Debug.Log("y: " + j.ToString());
                        croppedTexture.SetPixel(i, height - 1 - j, textureBytes[j + i * height]);
                        Debug.Log(textureBytes[j + i * height].ToString());
                    }

                }
            }


            return croppedTexture;
        }

        /// <summary>
        /// Return a random color
        /// </summary>
        /// <returns>Color</returns>
        public static Color RandomColor()
        {
            return new Color(Random.value, Random.value, Random.value, 1.0f);
        }

        /// <summary>
        /// Slice target Texture2D in 16 by 16 pixel subparts
        /// </summary>
        /// <param name="targetAsset">Target Texture2D that is going to be sliced</param>
        public static void SliceSprite(string targetAsset)
        {
            SliceSprite(targetAsset, 16, 16);
        }

        /// <summary>
        /// Slice target Texture2D in subparts
        /// </summary>
        /// <param name="targetAsset">Target Texture2D that is going to be sliced</param>
        /// <param name="tileWidth">The width of each slice</param>
        /// <param name="tileHeight">The height of each slice</param>
        public static void SliceSprite(string targetAsset, int tileWidth, int tileHeight)
        {
            Debug.Log("Asset Path: " + targetAsset);
            Texture2D myTexture = (Texture2D)AssetDatabase.LoadAssetAtPath<Texture2D>(targetAsset);

            if (myTexture != null)
            {
                string path = AssetDatabase.GetAssetPath(myTexture);
                Debug.Log("Path: " + path);

                TextureImporter ti = AssetImporter.GetAtPath(path) as TextureImporter;
                ti.isReadable = true;

                List<SpriteMetaData> newData = new List<SpriteMetaData>();


                for (int i = 0; i < myTexture.width; i += tileWidth)
                {
                    Debug.Log("x: " + i);

                    for (int j = myTexture.height; j > 0; j -= tileHeight)
                    {
                        Debug.Log("y: " + j);

                        SpriteMetaData smd = new SpriteMetaData()
                        {
                            pivot = new Vector2(0.5f, 0.5f),
                            alignment = 9,
                            name = (myTexture.height - j) / tileHeight + ", " + i / tileWidth,
                            rect = new Rect(i, j - tileHeight, tileWidth, tileHeight)
                        };

                        Debug.Log("Name: " + smd.name + ", Rect: " + smd.rect.ToString());

                        newData.Add(smd);
                    }
                }

                //ti.spriteImportMode = SpriteImportMode.Multiple;
                ti.spritesheet = newData.ToArray();
                AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);

                Debug.Log("Done!");

            }
            else
            {
                Debug.LogError("Unable to load Asset, from " + targetAsset);
            }
        }

        public static void SetTextureImporterFormat(Texture2D texture, bool isReadable)
        {
            if (null == texture) return;

            string assetPath = AssetDatabase.GetAssetPath(texture);
            var tImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;
            if (tImporter != null)
            {
                tImporter.textureType = TextureImporterType.Advanced;
                tImporter.filterMode = FilterMode.Point;
                tImporter.isReadable = isReadable;

                AssetDatabase.ImportAsset(assetPath);
                AssetDatabase.Refresh();
            }
        }

        public static Color[] RotateColorArray(Color[] originalColors, int size, int degree)
        {
            Color[] rotColors = new Color[originalColors.Length];

            for (int x = 0; x < size; x++) 
            {
                for (var y = 0; y < size; y++) 
                {
                    switch (degree)
                    { 
                        case 270:
                        case -90:
                            rotColors[x + y * size] = originalColors[y + (size - 1 - x) * size];
                        break;

                        case 90:
                        rotColors[x + y * size] = originalColors[(size - 1 - y) + x * size];
                        break;
                    
                        default:
                            Debug.LogError("RotateColorArrayN90: Incorrect rotation interval");
                        break;
                    }

                        
                }
            }
            return rotColors;
        }
    }
}