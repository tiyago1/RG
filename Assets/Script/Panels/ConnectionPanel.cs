using RiotGamesService;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Linq;

public class ConnectionPanel : Panel 
{
    #region Fields

    public InputField SummnonerName;
    public Dropdown RegionDropDown;
    public Dropdown LaungaugeDrowDown;

    public FireBaseUserData UserData;

    private string Region;
    

    #endregion // Fields

    #region Public Methods
 
    public void Start()
    {
        UserData = new FireBaseUserData();
        FireBaseManager.Instance.GetUserCallBack += Instance_GetUserCallBack;
        //ResponseResult<string> deserialized = new ResponseResult<string>();
        //deserialized.Data = JsonConvert.DeserializeObject<string>(JSON);
        //
        //Debug.Log(deserialized.Data);
    }

    public void OnCreateButtonClicked()
    {
        if (!string.IsNullOrEmpty(SummnonerName.text) && RegionDropDown.value != 0 && LaungaugeDrowDown.value != 0)
        {
            Region = RegionDropDown.captionText.text;
            FireBaseManager.Instance.GetUser(SummnonerName.text);
                //SummonerRequest(SummnonerName.text);
           
        }
    }
    //"status_code": 404
    #endregion // Public Methods

    #region Private Nethods


        /// <summary>
        /// Summoner var mı yok mu diye atılıyor.
        /// </summary>
        /// <param name="summonerName"></param>
    private void SummonerRequest(string summonerName)
    {
        string url = String.Format(RiotGamesConstants.SUMMONER_API, summonerName);
        RiotGamesManager.Instance.SetHostRegion(GetSpecialRegionShortcut(Region));
        RiotGamesManager.Instance.PostData<string, SummonerData>(url, String.Empty, String.Empty, SummonerResponse, false);
    }
    private void Instance_GetUserCallBack(FireBaseUserData data)
    {
        if (data != null)
        {
            UserData = data;
            RiotGamesManager.Region = UserData.Region;
            RiotGamesManager.SpecialRegion = UserData.SpecialRegion;
            mManager.ShowPanel(Panels.ChampionsPanel);
        }
        else
        {
            SummonerRequest(SummnonerName.text);
        }
    }

    private string GetRegionShortcut(string value)
    {
        string returnValue = String.Empty;

        switch (value)
        {
            case "EU Nordic & East":
                returnValue = "eune";
                break;
            case "EU West":
                returnValue = "euw";
                break;
            case "Russia":
                returnValue = "ru";
                break;
            case "Turkey":
                returnValue = "tr";
                break;
            case "North America":
                returnValue = "na";
                break;
            case "Brazil":
                returnValue = "br";
                break;
            case "Latin America North":
                returnValue = "lan";
                break;
            case "Latin America South":
                returnValue = "las";
                break;
            case "Japan":
                returnValue = "jp";
                break;
            case "Korea":
                returnValue = "kr";
                break;
            case "Oceania":
                returnValue = "oce";
                break;
        }

        return returnValue;
    }

    private string GetSpecialRegionShortcut(string value)
    {
        string returnValue = String.Empty;

        switch (value)
        {
            case "EU Nordic & East":
                returnValue = "eun1";
                break;
            case "EU West":
                returnValue = "euw1";
                break;
            case "Russia":
                returnValue = "ru";
                break;
            case "Turkey":
                returnValue = "tr1";
                break;
            case "North America":
                returnValue = "na1";
                break;
            case "Brazil":
                returnValue = "br1";
                break;
            case "Latin America North":
                returnValue = "la2";
                break;
            case "Latin America South":
                returnValue = "la1";
                break;
            case "Japan":
                returnValue = "jp1";
                break;
            case "Korea":
                returnValue = "kr";
                break;
            case "Oceania":
                returnValue = "oc1";
                break;
        }

        return returnValue;
    }

    public string JSON;

    private void SummonerResponse(ResponseResult<SummonerData> response)
    {
        if (response.Status == ResponseCode.Success)
        {
            UserData.UserID = response.Data.id;
            UserData.SummanerName = response.Data.name;
            UserData.SummonerLevel = response.Data.summonerLevel;
            UserData.ProfileIconId = response.Data.profileIconId;
            UserData.Region = RiotGamesManager.Region;
            UserData.SpecialRegion = RiotGamesManager.SpecialRegion;
            UserData.Laungauge = LaungaugeDrowDown.captionText.text;
            LeagueRequest(response.Data.id.ToString());
        }
        else if(response.Status == ResponseCode.MatchNotFound)
        {
            Debug.Log("Böyle Bir Summoner Yok");
        }
        else
        {
            Debug.Log(response.Status);
        }
    }

    private void LeagueRequest(string id)
    {
        string api = "api/lol/{0}/v2.5/league/by-summoner/{1}/entry?";
        RiotGamesManager.Instance.SetHostRegion(GetRegionShortcut(Region));
        string url = String.Format(api, RiotGamesManager.Region, id);
        RiotGamesManager.Instance.PostData<string, List<LeagueEntryData>>(url, String.Empty, id, LeagueResponse, false);
    }

    private void LeagueResponse(ResponseResult<List<LeagueEntryData>> response)
    {
        Debug.Log(response.Status);

        if (response.Status == ResponseCode.Success)
        {
            UserData.League = response.Data.FirstOrDefault().tier;
            UserData.Division = response.Data.FirstOrDefault().entries.FirstOrDefault().division;
            FireBaseManager.Instance.CreateUser(UserData);
            mManager.ShowPanel(Panels.ChampionsPanel);
        }
        else if(response.Status == ResponseCode.MatchNotFound)
        {
            Debug.Log("Ligi Yok adamın");
            UserData.League = "";
            UserData.Division = "";
            FireBaseManager.Instance.CreateUser(UserData);
            mManager.ShowPanel(Panels.ChampionsPanel);

        }
        else
        {
            Debug.Log("LeagueResponse fail");
        }
    }
    #endregion // Private Methods
}