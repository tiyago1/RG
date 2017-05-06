using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using RiotGamesService;

public enum RoleType 
{
    Fighter,
    Support,
    Mage,
    Assassin,
    Marksman
}

public class ChampionProfilePanel : MonoBehaviour 
{
    #region Fields

    public RawImage ChampionImage;
    public string Name;

    public List<Texture2D> RoleTextures;

    private Dictionary<RoleType,Texture2D> mRoleTexture;

    #endregion // Fields

    #region Unity Methods

    public void Start()
    {
        Initialize();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            DownloadChampionProfileTexture();
        }
    }

    #endregion // Unity Methods
    
    #region Public Methods
    
    public void Initialize()
    {
        mRoleTexture = new Dictionary<RoleType, Texture2D>();

        for (int i = 0; i < RoleTextures.Count; i++)
        {
            mRoleTexture.Add((RoleType)i, RoleTextures[i]);
        }

        DownloadChampionProfileTexture();
    }

    public void DownloadChampionProfileTexture()
    {
        string url = String.Format("http://ddragon.leagueoflegends.com/cdn/img/champion/splash/{0}.jpg",Name);
        RiotGamesManager.Instance.DownloadTexture(url, ResponseChampionProfileTexture,"");
    }

    public void ResponseChampionProfileTexture(ResponseResult<Texture2D> response)
    {
        if (response.Status == ResponseCode.Success)
        {
            ChampionImage.texture = response.Data;
        }
    }

    #endregion // Public Methods
    
    #region Private Nethods
    
    private void TestPrivateMethods()
    {
        // to stuff
    }

    #endregion // Private Methods
}