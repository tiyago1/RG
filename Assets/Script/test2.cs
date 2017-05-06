using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using RiotGamesService;
using System.Text;

public class test2 : MonoBehaviour 
{
    #region Fields

    #endregion // Fields

    #region Unity Methods

    public void Start()
    {
        Initialize();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            Debug.Log("as");
            string url = "https://global.api.riotgames.com/api/lol/static-data/TR/v1.2/champion?api_key=RGAPI-62f9e85c-e64d-4c63-928f-01dca1bf4019";
            PostData<string, ChampionListDto>(url, String.Empty, ResponseCallBack);
        }
    }

    public void ResponseCallBack(ResponseResult<ChampionListDto> response)
    {
        Debug.Log(response.Status);

        if (response.Status == ResponseCode.Success)
        {
            Debug.Log(response.Data.version);
        }
    }

    public void PostData<T, TResult>(string url, T data, Action<ResponseResult<TResult>> responseAction)
    {
        string jsonString = JsonConvert.SerializeObject(data);

        StartCoroutine(Post<TResult>(url, jsonString, responseAction));
    }


    private IEnumerator Post<T>(string url, string jsonString, Action<ResponseResult<T>> responseAction)
    {
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
                        ResponseResult<T> deserialized = new ResponseResult<T>();
                        deserialized.Data = JsonConvert.DeserializeObject<T>(response);
                        responseAction(deserialized);
                    }
                }
                catch (Exception e)
                {
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

                    //if (request.error.Contains("java.net") || request.error.Contains("java.io")) //Holy mother of exceptions...
                    //    code = ResponseCode.GoogleFail;

                    ResponseResult<T> des = new ResponseResult<T>(code);
                    responseAction(des);
                }
            }
        }
    }

    #endregion // Unity Methods

    #region Public Methods

    public void Initialize()
    {

    }

    #endregion // Public Methods
    
    #region Private Nethods
    
    private void TestPrivateMethods()
    {
        // to stuff
    }

    #endregion // Private Methods
}

//https://tr1.api.riotgames.com/lol/static-data/v3/champion?champListData=partype,tags&api_key=RGAPI-62f9e85c-e64d-4c63-928f-01dca1bf4019
//https://tr1.api.riotgames.com/lol/static-data/v3/champions?champListData=partype&api_key=RGAPI-62f9e85c-e64d-4c63-928f-01dca1bf4019