using UnityEngine;
using Unity.Sentis;
using System.Threading.Tasks;
using System;
using CafeGame;

public class GanLoader : MonoBehaviour
{
    [SerializeField] BoolEvent GANLoadChannel;
    [SerializeField] MODEL_NAMES modelName;
    Model model;
    IWorker worker;
    TensorFloat inputTensor;
    TensorFloat outputTensor;
    
    private void Start() {
        LoadModel(modelName);
    }

    public Model LoadModel(MODEL_NAMES modelName){
        string name;

        switch (modelName)
        {
            case MODEL_NAMES.CAKES: {name = "cakes"; break;}
            case MODEL_NAMES.FACES: {name = "faces"; break;}
            default: throw new Exception("Model name not recognised");
        }

        ModelAsset modelAsset = Resources.Load(name) as ModelAsset;
        if(modelAsset != null){
            model = ModelLoader.Load(modelAsset);
        }
        else{
            model = null;
        }
        GANLoadChannel?.RaiseEvent(true);
        return model;
    }

    public bool IsLoaded(){
        return model == null ? false : true;
    }

    public async Task<GANTexture> GetTextureAsync(float[] input)
    {
        if(model == null){
            throw new Exception("Model not loaded");
        }

        inputTensor = CreateInput(512, input);

        var outputTask = Task.Run(() => RunModel(inputTensor, model));

        float[] output = await outputTask;

        output = NormalizeArray(output);
        float[,,,] unflattened = Unflatten(output, outputTensor);
        Texture2D tensorTexture = TensorToTexture2D(outputTensor, unflattened);
        inputTensor.Dispose();
        DisposeAll();
        return new GANTexture(tensorTexture, input);
    }

    static float[] NormalizeArray(float[] arr)
    {
        float min = arr[0];
        float max = arr[0];
        for (int i = 1; i < arr.Length; i++)
        {
            if (arr[i] < min)
                min = arr[i];
            if (arr[i] > max)
                max = arr[i];
        }

        float[] normalizedArray = new float[arr.Length];
        for (int i = 0; i < arr.Length; i++)
        {
            normalizedArray[i] = (arr[i] - min) / (max - min);
        }

        return normalizedArray;
    }

    TensorFloat CreateInput(int size, float[] values){

        TensorShape shape = new TensorShape(1, size);
        TensorFloat tensor = new TensorFloat(shape, values);

        return tensor;

    }

    float[] RunModel(TensorFloat input, Model runtimeModel){
        if(runtimeModel == null){
            throw new Exception("Model not loaded in RunModel");
        }
        worker = WorkerFactory.CreateWorker(BackendType.CPU, runtimeModel);
        worker.Execute(input);
        outputTensor = worker.PeekOutput() as TensorFloat;

        outputTensor.CompleteOperationsAndDownload();

        float[] readonlyResult = outputTensor.ToReadOnlyArray();
        float[] result = new float[readonlyResult.Length];
        outputTensor.ToReadOnlyArray().AsMemory().CopyTo(result);

        // DisposeAll();

        return result;
    }

    private void OnDestroy() {
        DisposeAll();
    }

    private void DisposeAll(){
        outputTensor?.Dispose();
        inputTensor?.Dispose();
        worker?.Dispose();
    }

    Texture2D TensorToTexture2D(TensorFloat outputTensor, float[,,,] unflattened)
    {
        int channels = outputTensor.shape[1];
        int width = outputTensor.shape[2];
        int height = outputTensor.shape[3];

        Texture2D texture = new Texture2D(width, height, TextureFormat.RGB24, false);

        Color[] pixels = new Color[width*height];
        for (int hh = 0; hh < height; hh++)
        {
            for(int ww = 0; ww < width; ww++){
                Color color = new Color(0,0,0,1);

                for(int k = 0; k < channels; k++){
                    float value = unflattened[0, k, hh, ww];
                    color[k] = value; 
                }

                pixels[hh*width + ww] = color;
            }
        }

        texture.SetPixels(pixels);
        texture.Apply();

        return texture;
    }

    float[,,,] Unflatten(float[] flattenedTensor, Tensor tensor){
        TensorShape shape = tensor.shape;
        int batchCount = shape[0];
        int channels = shape[1];
        int width = shape[2];
        int height = shape[3];

        float[,,,] unflattened = new float[batchCount, channels, width, height];

        for(int c = 0; c < channels; c++){
            for(int h = 0; h < height; h++){
                for(int w = 0; w < width; w++){
                    float pixelValue = flattenedTensor[(c*width*height)+h*width + w];
                    unflattened[0,c,h,w] = pixelValue;
                }
            }
        }

        return unflattened;
    }
}
