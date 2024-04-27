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
    public GameObject[] items;
    // Start is called before the first frame update
    void Start()
    {

        _createItemsCallback = (JsonArrayString) => {
            StartCoroutine(CreateItemsRoutine(JsonArrayString));
        };
        Debug.Log("Testing");
        CreateItems();
        GetClothes();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetClothes() 
    {

    }
    public void CreateItems()
    {
        //string userId = Main.Instance.UserInfo.UserID;
        StartCoroutine(Main.Instance.Web.GetItemIDs(_createItemsCallback));
    }   

    IEnumerator CreateItemsRoutine(string JsonArrayString)
    {
        Debug.Log("Test");
        JSONArray jsonArray = JSON.Parse(JsonArrayString) as JSONArray;

        for (int i = 0; i < jsonArray.Count; i++) 
        {
            bool isDone = false;
            string itemid = jsonArray[i].AsObject["id"];
            string id = jsonArray[i].AsObject["ID"];
            JSONObject itemInfoJson = new JSONObject();
            string name = jsonArray[i].AsObject["name"];
            forbutton = name;
          
            Action<string> getItemInfoCallback = (itemInfo) =>
            {
                isDone = true;
                JSONArray tempArray = JSON.Parse(itemInfo) as JSONArray;
                itemInfoJson = tempArray[0].AsObject;
            };

            StartCoroutine(Main.Instance.Web.GetItem(itemid, getItemInfoCallback));
            

            yield return new WaitUntil(() => isDone == true);
            
            GameObject itemGo = Instantiate(Resources.Load("Prefabs/ItemButton") as GameObject);
            
            Item item = itemGo.AddComponent<Item>();
            item.ID = id;
            item.ItemID = itemid;
            GameObject parent = GameObject.Find("Canvas/Main Closet Interface/Scroll View/Viewport/Content");
            itemGo.transform.SetParent(parent.transform);
            itemGo.transform.localScale = Vector3.one;
            itemGo.transform.localPosition = Vector3.zero;
            //itemGo.GetComponent<Button>().onClick.AddListener(Dress());

            //itemGo.transform.Find("Name").GetComponent<Text>().text = itemInfoJson["name"];
            //itemGo.transform.Find("Price").GetComponent<Text>().text = itemInfoJson["price"];
            //itemGo.transform.Find("Description").GetComponent<Text>().text = itemInfoJson["description"];

            byte[] bytes = ImageManager.Instance.LoadImage(itemid);
            Debug.Log("Got here");
            //Download from web
            if (bytes.Length == 0)
            {
                Action<byte[]> getItemIconCallback = (downloadedBytes) =>
                {
                    Sprite sprite = ImageManager.Instance.BytesToSprite(downloadedBytes);
                    itemGo.transform.Find("Image").GetComponent<Image>().sprite = sprite;
                    ImageManager.Instance.SaveImage(itemid, downloadedBytes);
                    ImageManager.Instance.SaveVersionJson();
                };
                StartCoroutine(Main.Instance.Web.GetItemIcon(itemid, getItemIconCallback));
            }
            //Load from device
            else
            {
                Debug.Log("LOADING ICON FROM DEVICE: " + itemid);
                Sprite sprite = ImageManager.Instance.BytesToSprite(bytes);
                itemGo.transform.Find("Image").GetComponent<Image>().sprite = sprite;
                itemGo.GetComponent<Button>().onClick.AddListener(Dress);
            }

            /*itemGo.transform.Find("SellButton").GetComponent<Button>().onClick.AddListener(() =>
            {
                string iId = itemid;
                string uId = Main.Instance.UserInfo.UserID;
                StartCoroutine(Main.Instance.Web.SellItem(iId,uId));
            }
                );
            */


        }

    }

    public void Dress()
    {
        Debug.Log(forbutton);
        GameObject.Find(forbutton).GetComponent<MeshRenderer>().enabled = true;
        //GameObject clothes = mine.GameObject();
        //Debug.Log(mine);
        //Debug.Log(clothes);
        //clothes.SetActive(true);
        

    }
}
