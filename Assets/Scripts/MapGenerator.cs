using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    //public variables which control how the editor displays the map
    public bool autoUpdate;

    //A bunch of public variables which define the noise generated
    public int seed;
    public int mapWidth;
    public int mapHeight;
    public float noiseScale;
    public int octaves;

    [Range(0,1)]
    public float persistance;

    public float lacunarity;

    //Public variables which allow the user to perform high level actions without changing the noise underneath
    public Vector2 offset;


    public void GenerateMap()
    {
        /*Generates your given noise map and dislayes it on a plane
         * Arguments:
         * none
         * Returns:
         * none
         */

        //retrieve the noise data from the noise generator
        float[,] noiseMap = Noise.GenerateNoiseMap(seed,mapWidth, mapHeight, noiseScale, octaves, persistance, lacunarity, offset);

        //gets a reference to the mapDisplay instance attached to the map generator object
        MapDisplay display = FindObjectOfType<MapDisplay>();
        display.DrawNoiseMap(noiseMap); 
    }

    //called whenever one of the scripts variables is changed from the editor
    private void OnValidate()
    {
        //put in stops so the program can't have improper values
        if (mapWidth < 1)
        {
            mapWidth = 1;
        }
        if (mapHeight < 1)
        {
            mapHeight = 1;
        }
        if (lacunarity < 1)
        {
            lacunarity = 1;
        }
        if (octaves < 0)
        {
            octaves = 0;
        }
    }

}
