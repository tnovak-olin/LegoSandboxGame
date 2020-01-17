using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{

    public Renderer textureRenderer;
    
    public void DrawNoiseMap(float[,] noiseMap)
    {
        /*Draws the noisemap onto the plane which the texture renderer references
         * Arguments:
         * noisemap: the 2d array describing the noise values
         * 
         * Returns:
         * none
         */
        
        //finds the dimensions of the array passed in
        int textureWidth = noiseMap.GetLength(0);
        int textureHeight = noiseMap.GetLength(1);

        //Creates an empty texture of the right size
        Texture2D noiseTexture = new Texture2D(textureWidth, textureHeight);

        //creates an array of the colors in the texture for each pixel
        Color[] colorMap = new Color[textureWidth * textureHeight];
        for (int y = 0; y < textureHeight; y++)
        {
            for (int x = 0; x < textureWidth; x++)
            {
                //gets the color for the pixel by linearly interpreting the noiseMap float value between black and white on a scale of 0 (black) to 1(white) 
                colorMap[y * textureWidth + x] = Color.Lerp(Color.black, Color.white, noiseMap[x, y]);
            }
        }

        //set the colors of the pixels to the generated colors
        noiseTexture.SetPixels(colorMap);
        //Apply the changes to the texture
        noiseTexture.Apply();

        //apply the texture to the plane
        textureRenderer.sharedMaterial.mainTexture = noiseTexture;
        //Scale the plane to be the same size as the texture
        textureRenderer.transform.localScale = new Vector3(textureWidth,1, textureHeight);
    }

}
