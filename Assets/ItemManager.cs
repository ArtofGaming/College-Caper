using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SimpleJSON;
using TMPro;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public string ID;       
    public string ItemID;
    public string forbutton;
    Action<string> _createItemsCallback;
    Action<string> _destroyItemsCallback;
    public GameObject[] items;
    // Start is called before the first frame update
    void Start()
    {

        _createItemsCallback = (JsonArrayString) => {
            StartCoroutine(CreateItemsRoutine(JsonArrayString));
        };
        _destroyItemsCallback = (JsonArrayString) => {
            StartCoroutine(DestroyItemsRoutine(JsonArrayString));
        };
        //Debug.Log("Testing");
        CreateItems();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Filter(Button button)
    {
        StartCoroutine(Main.Instance.Web.FilterItems(button.name, _createItemsCallback));
        StartCoroutine(Main.Instance.Web.FilterOutItems(button.name, _destroyItemsCallback));
        Debug.Log("Filter is called");
    }
    public void CreateItems()
    {
        //string userId = Main.Instance.UserInfo.UserID;
        StartCoroutine(Main.Instance.Web.GetItemIDs(_createItemsCallback));
    }   

    IEnumerator CreateItemsRoutine(string JsonArrayString)
    {
        JSONArray jsonArray = JSON.Parse(JsonArrayString) as JSONArray;

        for (int i = 0; i < jsonArray.Count; i++) 
        {
            bool isDone = false;
            string itemid = jsonArray[i].AsObject["id"];
            string id = jsonArray[i].AsObject["ID"];
            JSONObject itemInfoJson = new JSONObject();
            string name = jsonArray[i].AsObject["name"];
            
          
            Action<string> getItemInfoCallback = (itemInfo) =>
            {
                isDone = true;
                JSONArray tempArray = JSON.Parse(itemInfo) as JSONArray;
                itemInfoJson = tempArray[0].AsObject;
            };

            StartCoroutine(Main.Instance.Web.GetItem(itemid, getItemInfoCallback));
            

            yield return new WaitUntil(() => isDone == true);
            
            GameObject itemGo = Instantiate(Resources.Load("Prefabs/ItemButton") as GameObject);
            itemGo.GetComponent<Button>().onClick.AddListener(delegate { Dress(itemGo.GetComponent<Button>());});
            
            Item item = itemGo.AddComponent<Item>();
            item.ID = id;
            item.ItemID = itemid;

            GameObject parent = GameObject.Find("Canvas/Main Closet Interface/Scroll View/Viewport/Content");
            itemGo.transform.SetParent(parent.transform);
            itemGo.transform.localScale = Vector3.one;
            itemGo.transform.localPosition = Vector3.zero;
            GameObject go = Instantiate(Resources.Load(name) as GameObject);
            GameObject goParent = itemGo;
            go.transform.SetParent(goParent.transform);
            go.transform.localScale = new Vector3(5000,5000,5000);
            go.transform.Rotate(0, 0, -90);
            go.transform.localPosition = new Vector3(0,0,-60);
        }

    }

    IEnumerator DestroyItemsRoutine(string JsonArrayString)
    {
        //Debug.Log("Test");
        JSONArray jsonArray = JSON.Parse(JsonArrayString) as JSONArray;

        for (int i = 0; i < jsonArray.Count; i++)
        {
            bool isDone = false;
            string itemid = jsonArray[i].AsObject["id"];
            string id = jsonArray[i].AsObject["ID"];
            JSONObject itemInfoJson = new JSONObject();
            string name = jsonArray[i].AsObject["name"];

            Debug.Log(name);

            Action<string> getItemInfoCallback = (itemInfo) =>
            {
                isDone = true;
                JSONArray tempArray = JSON.Parse(itemInfo) as JSONArray;
                itemInfoJson = tempArray[0].AsObject;
            };

            StartCoroutine(Main.Instance.Web.GetItem(itemid, getItemInfoCallback));


            yield return new WaitUntil(() => isDone == true);

            GameObject parent = GameObject.Find("Canvas/Main Closet Interface/Scroll View/Viewport/Content");
            Transform parentTransform = parent.transform;

            foreach(Transform child in parentTransform.transform)
            {
                foreach (Transform c in child.transform)
                {
                    if (c.name.Contains(name))
                    {
                        Destroy(c.parent.gameObject);
                        Debug.Log(name + " was destroyed");
                    }
                }
                
            }

        }

    }

    public void Dress(Button button)
    {
        string selectedItem = button.transform.GetChild(2).name;
        string[] nameList = selectedItem.Split("(");
        Debug.Log(nameList[0]);
        GameObject model = GameObject.Find("model/clothes");
        //use name of object that is child of button to find object that is child of dressupmodel
        model.transform.Find(nameList[0]).gameObject.GetComponent<MeshRenderer>().enabled = true;
            //(button.name)
        //GameObject clothes = mine.GameObject();
        //Debug.Log(mine);
        //Debug.Log(clothes);
        //clothes.SetActive(true);
        

    }

    public void Clear()
    {
        GameObject modelclothes = GameObject.Find("model/clothes");
        foreach(Transform child in modelclothes.transform)
        {
            child.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
