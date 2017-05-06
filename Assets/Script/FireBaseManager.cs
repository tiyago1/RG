using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using Firebase.Unity;
using Newtonsoft.Json;
using Firebase.Unity.Editor;
using System.Linq;
using Newtonsoft.Json.Linq;
using System;

public class FireBaseManager : MonoBehaviour 
{
    #region Singelaton

    public static FireBaseManager mRiotGamesManager;

    public static FireBaseManager Instance
    {
        get
        {
            if (GameObject.FindWithTag("RiotGames").GetComponent<FireBaseManager>() == null)
            {
                mRiotGamesManager = GameObject.FindWithTag("RiotGames").AddComponent<FireBaseManager>();
            }
            mRiotGamesManager = GameObject.FindWithTag("RiotGames").GetComponent<FireBaseManager>();
            return mRiotGamesManager;
        }
    }

    #endregion // Singelaton
    #region Fields

    public FirebaseApp app;
    public DatabaseReference RootReference;
    public DatabaseReference UsersReference;

    public event Action<FireBaseUserData> GetUserCallBack;

    #endregion // Fields

    #region Unity Methods

    public void Start()
    {
        Initialize();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            System.Guid guid = System.Guid.NewGuid();
            //FireBaseUserData userData = new FireBaseUserData()
            //{
            //    SummanerName = "DarkCravlet",
            //    UserID = guid.ToString(),
            //    Region = "Turkey",
            //    Laungauge = "tr_TR",
            //    SummonerLevel = 123,
            //    League = ""
            //};

            //Debug.Log(JsonConvert.SerializeObject(userData));
            //UsersReference.Child(guid.ToString()).Push().SetRawJsonValueAsync(JsonConvert.SerializeObject(userData));
        }   
    }

    #endregion // Unity Methods
    
    #region Public Methods
    
    public void Initialize()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://testportal-c8299.firebaseio.com/");
        RootReference = FirebaseDatabase.DefaultInstance.RootReference;
        UsersReference = RootReference.Child("Users");
    }

    public void CreateUser(FireBaseUserData summaner)
    {
        string tempCreateUserName = summaner.SummanerName.Replace(" ", "").ToLower();
        Debug.Log("tempCreateUserName : " + tempCreateUserName);
        UsersReference.Child(tempCreateUserName).Push().SetRawJsonValueAsync(JsonConvert.SerializeObject(summaner));
    }

    private JToken tk;

    public void GetUser(string summanerName)
    {
        string temp = summanerName.Replace(" ", "").ToLower();
        //Debug.Log("TEMP" + temp);
        FireBaseUserData data = null;

        UsersReference.Child(temp).GetValueAsync().ContinueWith(it =>
        {
            Debug.Log("1");
            if (it.IsCompleted)
            {
                Debug.Log("2");
                DataSnapshot cx = it.Result;
                string response = cx.GetRawJsonValue();

                if (String.IsNullOrEmpty(response))
                {
                    if (GetUserCallBack != null)
                    {
                        GetUserCallBack(data);
                        return;
                    }
                }

                JObject obje = JObject.Parse(response);
                Debug.Log("3");
                foreach (JToken item in obje.Children())
                {
                    tk = item;
                    break;
                }
                Debug.Log("4");
                data = JsonConvert.DeserializeObject<FireBaseUserData>(obje.Children().First().Last.ToString());
                if (GetUserCallBack != null)
                {
                    GetUserCallBack(data);
                }
                Debug.Log("5");
                Debug.Log(data.Division);
            }
        });

        Debug.Log("8");
    }

    #endregion // Public Methods
    
    #region Private Methods
    
    private void TestPrivateMethods()
    {
        // to stuff
    }

    #endregion // Private Methods
}



