using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    public Renderer textureRenderer;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    public void DrawNoiseMap(float[,] noiseMap){
        int width=noiseMap.GetLength(0);
        int height=noiseMap.GetLength(1);
        Texture2D texture=new Texture2D(width,height);
        Color[] colorMap=new Color[width*height];
        for(int y=0; y<height; y++){
            for(int x=0; x<width; x++){
                colorMap[y*width+x]=Color.Lerp(Color.black,Color.white, noiseMap[x,y]);
            }
        }
        texture.SetPixels(colorMap);
        texture.Apply();
        textureRenderer.sharedMaterial.mainTexture=texture;
        textureRenderer.transform.localScale=new Vector3(width,1,height);
    }
    public void DrawColorMap(Color[] colorMap,int width,int height){
        Texture2D texture=new Texture2D (width,height);
        texture.filterMode=FilterMode.Trilinear;
        texture.wrapMode=TextureWrapMode.Clamp;
        texture.SetPixels(colorMap);
        texture.Apply();
        textureRenderer.sharedMaterial.mainTexture=texture;
        textureRenderer.transform.localScale=new Vector3(width,1,height);
    }
    public void DrawMesh(MeshData meshData, Texture2D texture){
        meshFilter.sharedMesh=meshData.CreateMesh();
        meshRenderer.sharedMaterial.mainTexture=texture;
    }
}
