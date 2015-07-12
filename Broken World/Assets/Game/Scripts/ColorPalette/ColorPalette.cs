using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BrokenWorld.ColorPalette
{
    [Serializable]
    public class Palette : ScriptableObject
    {
        public Texture2D Source;
        public Texture2D CachedTexture;
        public List<Color> CurrentPalette = new List<Color>();
        public List<Color> NewPalette = new List<Color>();

        public void ResetPalette()
        {
            BuildPalette();
            NewPalette = new List<Color>(CurrentPalette);
        }

        public Color GetColor(Color color)
        {

            for (var i = 0; i < CurrentPalette.Count; i++)
            {
                var tmpColor = CurrentPalette[i];

                if (Mathf.Approximately(color.r, tmpColor.r) &&
                   Mathf.Approximately(color.g, tmpColor.g) &&
                   Mathf.Approximately(color.b, tmpColor.b) &&
                   Mathf.Approximately(color.a, tmpColor.a))
                {
                    return NewPalette[i];
                }
            }

            return color;
        }

        private void BuildPalette()
        {

            List<Color> newPalette = new List<Color>();

            Color[] colors = Source.GetPixels();

            foreach (var color in colors)
                if (!newPalette.Contains(color) && color.a == 1)
                    newPalette.Add(color);

            CurrentPalette = newPalette;

        }

    }
}