using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

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

    public IEnumerator GetItemIDs()
    {
        WWWForm form = new WWWForm();

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/FashionGame/GetItemIDs.php", form))
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

    public IEnumerator GetItem(string itemID)
    {
        WWWForm form = new WWWForm();
        form.AddField("itemID", itemID);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/FashionGame/GetItem.php", form))
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