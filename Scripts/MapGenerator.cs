using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public enum DrawMode{NoiseMap,ColorMap,Mesh};
    public DrawMode drawMode;
    public int mapWidth;
    public int mapHeight;
    public float noiseScale;

    public int octaves;
    public float persistence;
    public float lacunarity;

    public float meshHeightMultiplier;
    public AnimationCurve meshHeightCurve;
    public int seed;
    public Vector2 offset;
    public TerrainType[] regions;


    public bool autoUpdate;
    public void GenerateMap(){
        float[,] noiseMap=Noise.GenerateNoiseMap(mapWidth,mapHeight,seed,noiseScale,octaves,persistence,lacunarity,offset);
        Color[] colorMap=new Color[mapWidth*mapHeight];
        for(int y=0; y<mapHeight; y++){
            for(int x=0; x<mapWidth; x++){
                float currentHeight=noiseMap[x,y];
                for(int i=0; i<regions.Length;i++){
                    if(currentHeight <= regions[i].height){
                        colorMap[y*mapWidth+x]=regions[i].color;
                        break;
                    }
                }
            }
        }

        MapDisplay display=FindObjectOfType<MapDisplay>();
        if(drawMode==DrawMode.NoiseMap)display.DrawNoiseMap(noiseMap);
        else if(drawMode==DrawMode.ColorMap)display.DrawColorMap(colorMap,mapWidth,mapHeight);
        else if(drawMode==DrawMode.Mesh){
        Texture2D texture=new Texture2D (mapWidth,mapHeight);
        texture.filterMode=FilterMode.Point;
        texture.wrapMode=TextureWrapMode.Clamp;
        texture.SetPixels(colorMap);
        texture.Apply();
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap,meshHeightMultiplier,meshHeightCurve),texture);
        }
    }
    private void OnValidate() {
        if(mapWidth<1){
            mapWidth=1;
        }    
        if(mapHeight<1){
            mapHeight=1;
        }
    }
    [System.Serializable]
    public struct TerrainType{
        public string name;
        public float height;
        public Color color;
    }
}
