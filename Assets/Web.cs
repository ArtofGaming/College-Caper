using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Web : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    /*public void ShowUserItems()
    {
        StartCoroutine(GetItemIDs(Main.Instance.UserInfo.UserID));
    }*/
    public void Filter(Button button)
    {
        StartCoroutine(FilterItems(button.name));
    }

    public IEnumerator GetItemIDs(System.Action<string> callback)
    {
        WWWForm form = new WWWForm();

        using (UnityWebRequest www = UnityWebRequest.Post("https://colourful-squares.000webhostapp.com/GetItemIDs.php", form))
        {

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                string jsonArray = www.downloadHandler.text;
                callback(jsonArray);
            }
        }
    }

    public IEnumerator GetItem(string itemID, System.Action<string> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("itemID", itemID);

        using (UnityWebRequest www = UnityWebRequest.Post("https://colourful-squares.000webhostapp.com/GetItem.php", form))
        {

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(www.downloadHandler.text);
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                string jsonArray = www.downloadHandler.text;
                callback(jsonArray);
            }
        }
    }

    public IEnumerator GetItemIcon(string itemID, System.Action<byte[]> callback)
    {
        
        WWWForm form = new WWWForm();
        form.AddField("itemID", itemID);
        

        using (UnityWebRequest www = UnityWebRequest.Post("https://colourful-squares.000webhostapp.com/GetItemIcons.php", form))
        {

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("DOWNLOADING ICON: " + itemID);
                // results as byte array
                byte[] bytes = www.downloadHandler.data;
                callback(bytes);
                

            }
        }

        
    }

    public IEnumerator FilterItems(string filter)
    {
        WWWForm form = new WWWForm();
        form.AddField("filter", filter);

        using (UnityWebRequest www = UnityWebRequest.Post("https://colourful-squares.000webhostapp.com/FilterItems.php", form))
        {

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                string jsonArray = www.downloadHandler.text;

            }
        }
    }
}
