using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraViewer : MonoBehaviour
{
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // // Read the camera's view into a texture.
        // int width = 500;
        // int height = 500;
        // Rect rectReadPicture = new Rect(0, 0, width, height);
        // Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
                
        // // Initialize and render.
        // RenderTexture rt = new RenderTexture(width, height, 24);
        // cam.targetTexture = rt;
        // cam.Render();
        // RenderTexture.active = rt;
        
        // // Read pixels.
        // tex.ReadPixels(rectReadPicture, 0, 0);
        
        // // Clean up.
        // cam.targetTexture = null;
        // RenderTexture.active = null; // added to avoid errors 
        // DestroyImmediate(rt);
    }
}
