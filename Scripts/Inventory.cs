using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Item[] inventory;
    public Image[] inventoryUI=new Image[9];
    [SerializeField] private Text selectedItemName;

    //These are kinda useless images but keep them just in case
    public Sprite rockImage;
    public Sprite branchImage;
    public Sprite axeImage;

    public Sprite nullImage;

    [SerializeField] private Transform cam;
    [SerializeField] private LayerMask item;
    [SerializeField] private Transform itemParent;
    [SerializeField] private float range = 4;
    [SerializeField] private float timeBetweenClicks;
    [SerializeField] private Animator animator;
    private Item current;
    private float timer;
    private int selectedItem = 0;
    private void Start() {
        inventory = new Item[9];
        
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if(Input.GetKey(KeyCode.Mouse1)){
            RaycastHit hit;
            if(Physics.Raycast(transform.position, cam.forward, out hit,  range, item)){
                AddItem(hit.transform.GetComponent<Item>());
            }
        }

        if(Input.GetKey(KeyCode.Mouse0) && timer>=timeBetweenClicks){
            timer = 0;
            RaycastHit hit;
            animator.SetTrigger("Swing");
            if(Physics.Raycast(transform.position, cam.forward, out hit,  range)){
                if(hit.transform.name == "Tree(Clone)"){
                    if(current!=null && current.isAxe) hit.transform.GetComponent<Health>().TakeDamage(5);
                    else hit.transform.GetComponent<Health>().TakeDamage(1);
                }
                else if(hit.transform.name == "Rock(Clone)"){
                    if(current!=null && current.isPickaxe) hit.transform.GetComponent<Health>().TakeDamage(5);
                    else hit.transform.GetComponent<Health>().TakeDamage(1);
                }
                else if(hit.transform.name == "Enemy(Clone)"){
                    if(current != null && current.isSword) hit.transform.GetComponent<Health>().TakeDamage(5);
                    else if(current != null && current.isAxe) hit.transform.GetComponent<Health>().TakeDamage(3);
                    else hit.transform.GetComponent<Health>().TakeDamage(1);
                }
                else if(hit.transform.tag == "Breakable"){
                    hit.transform.GetComponent<Health>().TakeDamage(1);
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.Q)){
            RemoveItem(current);
        }
        int prev = selectedItem;
        if(Input.GetAxis("Mouse ScrollWheel")>0)
        {
            if(selectedItem >= inventory.Length - 1)
            {
                selectedItem = 0;
            }
            else
            {
                selectedItem++;
            }
            
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (selectedItem <=0)
            {
                selectedItem = inventory.Length - 1;
            }
            else
            {
                selectedItem--;
            }

        }
        #region big dumb
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedItem = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedItem = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedItem = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            selectedItem = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            selectedItem = 4;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            selectedItem = 5;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            selectedItem = 6;
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            selectedItem = 7;
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            selectedItem = 8;
        }
        #endregion
        if(prev!=selectedItem){
            SelectItem();
        }
        DisplayUi();
    }

    private void SetInventoryNull()
    {
        for(int i = 0; i < inventory.Length; i++){
            inventory[i] = null;
        }
    }
    private void SelectItem(){
        for(int i = 0; i < inventory.Length; i++){
            if(inventory[i] !=null) inventory[i].gameObject.SetActive(false);
        }
        current = inventory[selectedItem];
        if(current != null) current.gameObject.SetActive(true);
    }
    private void AddItem(Item item)
    {
        for(int i = 0; i < inventory.Length; i++){
            if(inventory[i]==null){
                inventory[i] = item;
                break;
            }
        }
        item.transform.parent = itemParent;
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;
        item.GetComponent<Rigidbody>().detectCollisions = false;
        item.GetComponent<Rigidbody>().isKinematic = true;
        item.GetComponent<Collider>().enabled = false;
        item.gameObject.SetActive(false);
    }

    private void RemoveItem(Item item)
    {
        for(int i = 0; i < inventory.Length; i++){
            if(inventory[i]==item){
                inventory[i] = null;
                break;
            }
        }
        item.transform.parent = null;
        item.GetComponent<Rigidbody>().detectCollisions = true;
        item.GetComponent<Rigidbody>().isKinematic = false;
        item.GetComponent<Collider>().enabled = true;
        item.gameObject.SetActive(true);
    }

    void DisplayUi()
    {
        if(current != null){
            selectedItemName.text = current.name;
        }
        else{
            selectedItemName.text = "";
        }
        for (int i = 0; i < inventoryUI.Length; i++)
        {
            if (inventory[i] != null)
            {
                inventoryUI[i].sprite = inventory[i].icon;
            }
            else
            {
                inventoryUI[i].sprite = nullImage;


                if (i == selectedItem)
                {
                    inventoryUI[i].rectTransform.sizeDelta = new Vector2(100, 100);
                }
                else
                {
                    inventoryUI[i].rectTransform.sizeDelta = new Vector2(90, 90);
                }

            }
        }
    }
}
