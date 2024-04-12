using UnityEngine;

public class UserInfo : MonoBehaviour
{
    public string UserID { get; private set;}
    public string UserName;
    public string UserPassword;
    public string Level;
    public string Coins;

    public void SetCredentials(string username, string userpassword)
    {
        UserName = username;
        UserPassword = userpassword;
    }

    public void SetID(string id)
    {
        UserID = id;
        Debug.Log(UserID);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
