using UnityEngine;
using System.Collections;

public class PaletteSwapper : MonoBehaviour {

    public SpriteRenderer spriteRenderer;
    public Color[] palettes;

    private Texture2D texture;
    private Texture2D cloneTexture;
    private MaterialPropertyBlock block;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }



	// Use this for initialization
	void Start () {
        texture = spriteRenderer.sprite.texture;

        var w = texture.width;
        var h = texture.height;

        cloneTexture = new Texture2D(w, h);
        cloneTexture.wrapMode = TextureWrapMode.Clamp;
        cloneTexture.filterMode = FilterMode.Point;

        Color[] pixels = texture.GetPixels();
        cloneTexture.SetPixels(pixels);
        cloneTexture.Apply();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
