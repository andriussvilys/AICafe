using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CafeGame;
using System;

public static class UTILS
{
    public static float[] GetRandomStyleVector(int size){
        float[] randomValues = new float[size];
        for (int i = 0; i < randomValues.Length; i++)
        {
            randomValues[i] = UnityEngine.Random.Range(-1.0f, 1.0f);
        }
        return randomValues;
    }

    public static IEnumerator DelayedDestroy(GameObject obj, float delay){
        yield return new WaitForSeconds(delay);
        MonoBehaviour.Destroy(obj);
    }

    public static void SetTexture(MeshRenderer mesh, Texture2D texture){
        Shader shader = Shader.Find("Universal Render Pipeline/Unlit");
        Material material = new Material(shader);
        material.mainTexture = texture;
        mesh.material = material;
    }

    public static float CalcSimilarity(float[] v1, float[] v2){
        float dotProd = calcDotProduct(v1, v2);
        float magV1 = calcMagnitude(v1);
        float magV2 = calcMagnitude(v2);
        return dotProd / (magV1 * magV2);
    }

    public static float calcDotProduct(float[] v1, float[] v2){
        
        float dotProd = 0.0f;
        if(v1.Length == v2.Length){
            for (int i = 0; i < v1.Length; i++)
            {
                dotProd += v1[i] * v2[i];
            }
        }

        return dotProd;
    }

    public static float calcMagnitude(float[] v){
        
        float sqrtSum = 0.0f;
        for (int i = 0; i < v.Length; i++)
        {
            sqrtSum += v[i] * v[i];
        }

        return (float)Math.Sqrt(sqrtSum);
    }

    public static void PrintArray(float[] arr){
        string str = "";
        for (int i = 0; i < arr.Length; i++)
        {
            str += arr[i] + ", ";
            if(i > 9) break;
        }
    }

    public static bool IsArrayNull(float[] arr){
    bool empty = true;
    for (int i = 0; i < arr.Length; i++)
    {
        if(arr[i] != 0.0){
            empty = false;
            break;
        }
    }
    return empty;
}
}

public interface ITexturable{
    public void SetTexture(GANTexture ganTexture);
    public GANTexture GetTexture();
}

public enum MODEL_NAMES {CAKES, FACES}