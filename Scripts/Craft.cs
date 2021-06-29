using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Craft : MonoBehaviour
{
    [SerializeField] private LayerMask items;
    [SerializeField] private Recipe[] craftingRecipes; //wth doesn't serialize
    private void Update() {
        if(Input.GetKeyDown(KeyCode.F)){
            Collider[] hits = Physics.OverlapSphere(transform.position, 5, items);
            Item[] craft = new Item[hits.Length];
            for(int i = 0; i < hits.Length; i++){
                craft[i] = hits[i].gameObject.GetComponent<Item>();
            }
            for(int i = 0; i < craftingRecipes.Length; i++){    
                if(isEqual(craftingRecipes[i].input, craft)){
                    for(int f = 0; f < craft.Length; f++){
                        Destroy(craft[f].gameObject);
                    }
                    Instantiate(craftingRecipes[i].output, transform.position + transform.forward * 1, Quaternion.identity);
                }
            }
        }
    }
    private bool isEqual(Item[] arrayOne, Item[] arrayTwo){
        Debug.Log("Worked");
        bool isSame = true;
        QuickSort(arrayOne, 0, arrayOne.Length-1);
        QuickSort(arrayTwo, 0, arrayTwo.Length-1);
        if(arrayOne.Length == arrayTwo.Length)
        {
            for (int i = 0; i < arrayOne.Length; i++)
            {
                if(arrayOne[i].GetComponent<Item>().id != arrayTwo[i].GetComponent<Item>().id)
                {
                    isSame = false;
                }
            }
        }
        else
        {
            isSame = false;
        }
        return isSame;
    }
    private void QuickSort(Item[] arr, int start, int end)
    {
        int i;
        if (start < end)
        {
            i = Partition(arr, start, end);
 
            QuickSort(arr, start, i - 1);
            QuickSort(arr, i + 1, end);
        }
    }
 
    private int Partition(Item[] arr, int start, int end)
    {
        Item temp;
        Item p = arr[end];
        int i = start - 1;
 
        for (int j = start; j <= end - 1; j++)
        {
            if (arr[j].id <= p.id)
            {
                i++;
                temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
        }
 
        temp = arr[i + 1];
        arr[i + 1] = arr[end];
        arr[end] = temp;
        return i + 1;
    }


}   

    [System.Serializable]
    struct Recipe{
        public Item[] input;
        public Item output;
    }
    
    

