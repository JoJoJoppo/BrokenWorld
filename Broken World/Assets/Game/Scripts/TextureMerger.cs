using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TextureMerger: MonoBehaviour {

    public GameObject textureTestObject;
    public Texture2D[] TextureLayers;
    public Texture2D MergedTexture;
    public int TileWidth = 32;
    public int TileHeight = 32;

    public void OnStart()
    {
        MergedTexture = new Texture2D(1024,1024);
        if (TextureLayers.Length > 0)
        {
            Merge();
        }

    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 150, 100), "I am a button"))
            Merge();

    }



    public void Merge()
    { 
        

        //Get layer count
        int layerCount = TextureLayers.Length;
       
        if(layerCount >0)
        {
            //MergedTexture = new Texture2D(TextureLayers[0].width, TextureLayers[0].height);
            
            ////Get pixel count 
            //int pixelCount = TextureLayers[0].GetPixels().Length;
            //Color[] mergedTexturePixels = new Color[pixelCount];

            //Color[,] layersPixels = new Color[layerCount, pixelCount];

            ////Get pixels
            //for (int x = 0; x < layerCount; x++)
            //{ 
            //    //layersPixels.SetValue(TextureLayers[x].GetPixels(), x);
            //    for (int y = 0; y < pixelCount; y++)
            //    {

            //            mergedTexturePixels[y] = layersPixels[x, y];
                    
            //    }



            //}

            //MergedTexture.SetPixels(mergedTexturePixels);

            MergedTexture = CombineTextures(TextureLayers[1], TextureLayers[0]);


            MergedTexture.Apply();
            Texture2D texture = textureTestObject.GetComponent<Texture2D>();
            texture = MergedTexture;
        }
    }


    /// <summary>
    /// Slår ihop två texturer, funkar som det ser ut nu endast två texturer, vilket kan vara bristfälligt
    /// </summary>
    /// <param name="Background"></param>
    /// <param name="Overlay"></param>
    /// <returns></returns>
    public Texture2D CombineTextures (Texture2D Background, Texture2D Overlay) 
    {
        var newPixels = Background.GetPixels();
        var pixelsOverlay = Overlay.GetPixels();

        //Create a new Texture object
        var newTexture = new Texture2D(Background.width, Background.height);

        for (int x = 0; x < newPixels.Length; x++)
        {
            if (pixelsOverlay[x].a == 1)
                 newPixels[x] = pixelsOverlay[x];
        }

        newTexture.SetPixels(newPixels);
        newTexture.Apply();

        return newTexture;
     }
}