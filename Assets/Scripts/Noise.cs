using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    
    public static float[,] GenerateNoiseMap(int seed, int mapWidth, int mapHeight, float scale, int octaves, float persistance, float lacunarity, Vector2 offset)
    {
        /* A function which retrieves the perlin noise map of the data
         * Arguments:
         * seed: the seed number for the random algorithm
         * mapWidth: an intiger representing the number of pixels wide the map is
         * mapHeight: an intiger representing the number of pixels high the final map is
         * scale: The scaling of the noise used
         * octaves: how many 'layers of noise' are going to be added together to make the final mountain scape
         * persistance: a factor describing how much the amplitude if each octave is decreased with an increase in octave (higher octaves have lower amplitudes)
         *     persistance affects 'how much each feature affects the shape.' Higher persistances will mean that smaller features will have less effect on the shape
         * lacunarity: a factor describing how much the frequency of each octave is increased with an increase in octave.
         *     lacunarity affects 'how many features there are' Higher lacunarity values will yield more smaller features
         * offset: a vector allowing the user to 'pan' around the noise
         * 
         * Returns:
         * noiseMap = a two dimensional array of floats representing the noise values for each location in the requested map
         */

        //create a pesudo-random number generator
        System.Random prng = new System.Random(seed);

        //generate a set of x and y offsets for each octave based on the seed
        Vector2[] octaveOffsets = new Vector2[octaves];
        for (int o = 0; o < octaves; o++)
        {
            //pull offsets from the random number generator
            float offsetX = prng.Next(-100000, 100000)+offset.x;
            float offsetY = prng.Next(-100000, 100000)+offset.y;
            //build the offset vector from the random values
            octaveOffsets[o] = new Vector2(offsetX, offsetY);
        }


        //Create the empty array to hold the noise values
        float[,] noiseMap = new float[mapWidth,mapHeight];

        //prevents the scale from being zero as this causes a divide by zero error
        if (scale <= 0)
        {
            scale = 0.0001f;
        }

        //create variables to keep track of the maximum and minmum height values
        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        //defines varibles used for zooming into the center of the noise instead of the corner
        float halfWidth = mapWidth / 2f;
        float halfHeight = mapHeight / 2f;


        //loop through each value and update it to the noise value
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                //creates the variables which change with each octave
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                //iterate through the octaves
                for (int o = 0; o <octaves; o++)
                {
                    //find the sampeling cordinates in the perlin noise function accounting for the change in frequency and account to make scaling occur form the center of the map
                    float sampleX = (x-halfWidth) / scale * frequency + octaveOffsets[o].x;
                    float sampleY = (y-halfHeight) / scale * frequency + octaveOffsets[o].y;

                    //find the respecive noise value then shift into the range -1 to 1 so that the values can be negative and decrease the overall height
                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;

                    //increases the hight to account for the newest octave
                    noiseHeight += perlinValue * amplitude;

                    //update the frequency and amplitude for each octave
                    amplitude *= persistance;
                    frequency *= lacunarity;

                }

                if (noiseHeight > maxNoiseHeight)
                {
                    maxNoiseHeight = noiseHeight;
                }else if (noiseHeight < minNoiseHeight)
                {
                    minNoiseHeight = noiseHeight;
                }

                //apply the overall height to the map
                noiseMap[x, y] = noiseHeight;
            }
        }

        //loop through each value and update it to the noise value
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
            }
        }
                return noiseMap;
    }
}
