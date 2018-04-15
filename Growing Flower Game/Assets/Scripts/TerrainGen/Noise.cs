using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise {

    // Generating a noise map returning a grid of values between 0 and 1
    public static float [,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int octaves, float persistence, float lacunarity, Vector2 offset)
    {
        // Same seed = same map
        float[,] noiseMap = new float[mapWidth, mapHeight];

        System.Random prng = new System.Random(seed);

        if (scale <= 0)
        {
            scale = 0.0001f;
        }

        // each octave to be sampled from different location
        Vector2[] octaveOffsets = new Vector2[octaves];
        for (int i = 0; i < octaves; i++)
        {
            float offsetX = prng.Next(-100000, 100000) + offset.x;
            float offsetY = prng.Next(-100000, 100000) + offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }


        // In order to normalize at the end, keep track of min and max values
        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        // Offset changes should zoom to the middle, not corner
        float halfWidth = mapWidth / 2f;
        float halfHeight = mapHeight / 2f;

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                for (int i = 0; i < octaves; i++)
                {
                    // The higher apart the frequency, the further apart the sample values will be
                    // -> Height values will change more rapidly
                    float sampleX = (x - halfWidth) / scale * frequency + octaveOffsets[i].x;
                    float sampleY = (y - halfHeight) / scale * frequency + octaveOffsets[i].y;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1; // range -1 to 1

                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistence; // amplitude decreases bc persistence is 0 > p < 1
                    frequency *= lacunarity; // frequency increases each octave since lacunarity >1


                }
                if (noiseHeight > maxNoiseHeight)
                {
                    maxNoiseHeight = noiseHeight;
                }
                else if (noiseHeight < minNoiseHeight)
                {
                    minNoiseHeight = noiseHeight;
                }

                noiseMap[x, y] = noiseHeight;
            }
        }

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                // This normalises our noisemap
                // inverselerp returns value between 0 and 1
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
            }
        }
        return noiseMap;
    }

}
