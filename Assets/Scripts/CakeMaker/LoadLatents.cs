using UnityEngine;
using System;
using Newtonsoft.Json;

public static class LoadLatents
{
    public static double[][] LoadLatentsFromJson()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("Latents");

        if (jsonFile != null)
        {
            string json = jsonFile.text;

            LatentsData latentsData = JsonConvert.DeserializeObject<LatentsData>(json);
            if (latentsData != null)
            {
                return latentsData.vectors;
            }
            else
            {
                throw new Exception("Failed to deserialize Latents.json");
            }
        }
        else
        {
            throw new Exception("Failed to load Latents.json");
        }
    }

    [Serializable]
    private class LatentsData
    {
        public double[][] vectors;
    }
}