using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Text;
using RiotGamesService;

public class RiotGamesManager : MonoBehaviour 
{
    #region Constants

    public const string API_KEY = "RGAPI-62f9e85c-e64d-4c63-928f-01dca1bf4019";
    
    #endregion // Constants

    #region Singelaton

    public static RiotGamesManager mRiotGamesManager;

    public static RiotGamesManager Instance
    {
        get
        {
            if (GameObject.FindWithTag("RiotGames").GetComponent<RiotGamesManager>() == null)
            {
                mRiotGamesManager = GameObject.FindWithTag("RiotGames").AddComponent<RiotGamesManager>();
            }
            mRiotGamesManager = GameObject.FindWithTag("RiotGames").GetComponent<RiotGamesManager>();
            return mRiotGamesManager;
        }
    }

    #endregion // Singelaton

    #region Fields

    public static string HOST = "https://{0}.api.riotgames.com/";
    public static string Region = String.Empty;
    public static string SpecialRegion = String.Empty;

    #endregion // Fields

    #region Tables

    public FireBaseUserData PlayerData;

    #endregion // Tables

    #region Public Methods

    public void PostData<T, TResult>(string url, T data, string token, Action<ResponseResult<TResult>> responseAction, bool cache)
    {
        string jsonString = JsonConvert.SerializeObject(data);
        token = token.Replace(" ", "");
        
        url = HOST + url + "api_key=" + API_KEY;
        url = url.Replace(" ", "");
        Debug.LogError(url);
        StartCoroutine(Post<TResult>(url, jsonString, token, responseAction, cache));
    }

    public void DownloadTexture(string url, Action<ResponseResult<Texture2D>> response,string a)
    {
        StartCoroutine(TextureDownloader(url, response,a));
    }

    public void SetHostRegion(string region)
    {
        Region = region;
        HOST = String.Format(HOST, region);
    }

    public void SetHostSpeacialRegion(string region)
    {
        SpecialRegion = region;
        HOST = String.Format(HOST, region);
    }

    #endregion // Public Methods

    #region Private Nethods

    private IEnumerator Post<T>(string url, string jsonString, string token, Action<ResponseResult<T>> responseAction, bool cache)
    {
        PlayerPrefs.DeleteAll(); // TO_DO:Test code remove it
        //Debug.Log(url);
        string data = PlayerPrefs.GetString(url);

        if (String.IsNullOrEmpty(data) || !cache)
        {
            Debug.Log("REQUEST ATIYOR");

            using (WWW request = new WWW(url))
            {
                yield return request;

                if (String.IsNullOrEmpty(request.error))
                {
                    string response = request.text;

                    try
                    {
                        if (responseAction != null)
                        {
                            Debug.Log("1");
                            if(cache)
                                PlayerPrefs.SetString(url, response);

                            Debug.Log("2");
                            if (!String.IsNullOrEmpty(token))
                            {
                                Debug.Log("3");
                                JObject obje = JObject.Parse(response);
                                //Debug.Log(token.ToLower());
                                response = obje.SelectToken(token.ToLower()).ToString();
                                //Debug.Log(response);
                            }
                            Debug.Log("4");
                            Debug.Log(response);
                            ResponseResult<T> deserialized = new ResponseResult<T>();
                            deserialized.Data = JsonConvert.DeserializeObject<T>(response);
                            responseAction(deserialized);
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.Log(request.error);
                        Debug.Log(e.InnerException);
                        ResponseResult<T> des = new ResponseResult<T>(ResponseCode.Fail);

                        if (responseAction != null)
                            responseAction(des);
                    }
                }
                else
                {
                    if (responseAction != null)
                    {
                        Debug.Log(request.error);
                        ResponseCode code = ResponseCode.Fail;

                        if (request.error.Contains("404 Not Found"))
                        {
                            code = ResponseCode.MatchNotFound;
                        }

                        ResponseResult<T> des = new ResponseResult<T>(code);
                        responseAction(des);
                    }
                }
            }
        }
        else
        {
            if (responseAction != null)
            {
                ResponseResult<T> deserialized = new ResponseResult<T>();
                deserialized.Data = JsonConvert.DeserializeObject<T>(data);
                responseAction(deserialized);
            }
        }
    }

    private void SaveImage(Texture2D texture, string fileName)
    {
        var bytes = texture.EncodeToPNG();
        Debug.Log(" Application.persistentDataPath " + Application.persistentDataPath);
        var path = System.IO.Path.Combine(Application.persistentDataPath, fileName + ".png");

        System.IO.File.WriteAllBytes(path, bytes);
    }

    private IEnumerator TextureDownloader(string url, Action<ResponseResult<Texture2D>> response,string c)
    {
        using (WWW request = new WWW(url))
        {
            yield return request;
            ResponseResult<Texture2D> result;
            try
            {
                if (String.IsNullOrEmpty(request.error))
                {
                    if (request.texture != null)
                    {
                        result = new ResponseResult<Texture2D>(request.texture, ResponseCode.Success);
                        SaveImage(request.texture, c);
                        response(result);

                    }
                }
                else
                {
                    result = new ResponseResult<Texture2D>(ResponseCode.Fail);
                    response(result);
                    Debug.Log(url);
                    Debug.LogError("Error : " + request.error);
                }
            }
            catch
            {
                result = new ResponseResult<Texture2D>(ResponseCode.Fail);
                response(result);
                Debug.LogError("Error : " + request.error);
            }

        }
    }


    #endregion // Private Methods
}
