using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook;
using Facebook.Unity;
using UnityEngine.UI;


public class FacebookConnection : MonoBehaviour 
{
    #region Fields

    public RawImage ProfilePhato;

    #endregion // Fields

    #region Unity Methods

    public void Start()
    {
        FB.Init(InitCallback, OnHideUnity);
    }

    #endregion // Unity Methods
    
    #region Public Methods
    
    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            // Signal an app activation App Event
            FB.ActivateApp();
            // Continue with Facebook SDK
            FB.LogInWithReadPermissions(perms, AuthCallback);
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    public void ShareLink()
    {
        System.Uri uri = new System.Uri("https://youtu.be/adh1JDl2mDk");
        FB.ShareLink(uri, "İğne deliğinden Hindistan’ı seyretmek", "https://i.ytimg.com/vi/adh1JDl2mDk/hqdefault.jpg?custom=true&w=246&h=138&stc=true&jpg444=true&jpgq=90&sp=68&sigh=YkiuUz2QD2E5ZORugckg9GkN5Xw"); 
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else
        {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }


    List<string> perms = new List<string>() { "public_profile", "email", "user_friends" ,"user_photos" , "user_posts" };

    private void AuthCallback(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            // AccessToken class will have session details
            var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
            // Print current access token's User ID
            Debug.Log(aToken.UserId);
            // Print current access token's granted permissions
            foreach (string perm in aToken.Permissions)
            {
                Debug.Log(perm);
            }


            v2();
            Debug.Log("App ıd : " + FB.AppId + " isLoggedIn : " + FB.IsLoggedIn + " ");
        }
        else
        {
            Debug.Log("User cancelled login");
        }
    }
    
    public void v2()
    {
        FB.API("/me/picture?type=square&height=128&width=128", HttpMethod.GET, (response) => ADS(response));
      // this.GetComponent<TestFirebaseAuthentication>().ConnectFirebaseAuth(FB.ClientToken);
    }

    public void Clear()
    {
        ProfilePhato.texture = null;
    }

    private void ADS(IGraphResult response)
    {
        if (response.Error == null)
        {
            Debug.Log("RESPONSE CODE  : " + response.ToString());
            Debug.Log("V1 : " + response.Cancelled);
            Debug.Log("V2 : " + response.RawResult);
            ProfilePhato.texture = response.Texture;
            // Debug.Log("V1 : " + response);
        }
        else
        {
            Debug.Log("else" + response.ToString() + " " + response.Error);
           
        }
    }

    private IEnumerator GetFacebookProfilePhato(string imageUrl)
    {
        WWW request = new WWW(imageUrl);

        yield return request;
        SetTexture(request.texture);
    }

    private void SetTexture(Texture2D texture)
    {
        ProfilePhato.texture = texture;
    }

    #endregion // Public Methods
}