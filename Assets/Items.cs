using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SimpleJSON;
using TMPro;
using UnityEngine.UI;

public class Items : MonoBehaviour
{
    public string ID;       
    public string ItemID;

    Action<string> _createItemsCallback;
    // Start is called before the first frame update
    void Start()
    {
        _createItemsCallback = (JsonArrayString) => {
            StartCoroutine(CreateItemsRoutine(JsonArrayString));
        };

        CreateItems();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateItems()
    {
        //string userId = Main.Instance.UserInfo.UserID;
        StartCoroutine(Main.Instance.Web.GetItemIDs());
    }   

    IEnumerator CreateItemsRoutine(string JsonArrayString)
    {
        JSONArray jsonArray = JSON.Parse(JsonArrayString) as JSONArray;

        for (int i = 0; i < jsonArray.Count; i++) 
        {
            bool isDone = false;
            string itemid = jsonArray[i].AsObject["itemID"];
            string id = jsonArray[i].AsObject["ID"];
            JSONObject itemInfoJson = new JSONObject();

            Action<string> getItemInfoCallback = (itemInfo) =>
            {
                isDone = true;
                JSONArray tempArray = JSON.Parse(itemInfo) as JSONArray;
                itemInfoJson = tempArray[0].AsObject;
            };

            StartCoroutine(Main.Instance.Web.GetItem(itemid));

            yield return new WaitUntil(() => isDone == true);

            GameObject itemGo = Instantiate(Resources.Load("Prefabs/Item") as GameObject);
            Items item = itemGo.AddComponent<Items>();
            item.ID = id;
            item.ItemID = itemid;
            itemGo.transform.SetParent(this.transform);
            itemGo.transform.localScale = Vector3.one;
            itemGo.transform.localPosition = Vector3.zero;

            itemGo.transform.Find("Name").GetComponent<Text>().text = itemInfoJson["name"];
            itemGo.transform.Find("Price").GetComponent<Text>().text = itemInfoJson["price"];
            itemGo.transform.Find("Description").GetComponent<Text>().text = itemInfoJson["description"];

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
}
