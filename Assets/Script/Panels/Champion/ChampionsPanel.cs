using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RiotGamesService;
using System;
using Newtonsoft.Json;
using System.Linq;

public class ChampionsPanel : Panel 
{
    #region Fields

    public Dropdown ChampionDropDown;
    public RawImage sp;
    public string asdsad;

    public GameObject ChampionPrefab;
    public Transform Container;

    private List<ChampItem> ChampItems;
    private List<ChampionDto> ChampionData;

    public LayoutManager LayoutManager;

    #endregion // Fields

    #region Unity Methods

    public void Start()
    {
        Debug.Log(Application.dataPath);
        ChampItems = new List<ChampItem>();

        for (int i = 0; i < Container.childCount; i++)
        {
            ChampItems.Add(Container.GetChild(i).GetComponent<ChampItem>());
        }

        string api = RiotGamesConstants.STATIC_API + "/champions?champListData=partype,tags&";
        Debug.Log(api);
        RiotGamesManager.Instance.SetHostRegion(RiotGamesManager.Region);
        RiotGamesManager.Instance.PostData<string, ChampionListDto>(api, String.Empty, String.Empty, ChampionListResponse, true);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
           // StartCoroutine(ChampionTextureSet());
            // Debug.Log(RiotGamesManager.Instance.GetData<List<ChampionData>>()[0].Name);
        }
    }

    #endregion // Unity Methods

    #region Public Methods

    #endregion // Public Methods

    #region Private Nethods

    public List<Texture2D> textures;

    private void ChampionListResponse(ResponseResult<ChampionListDto> response)
    {
        if (response.Status == ResponseCode.Success)
        {
            List<Dropdown.OptionData> data = new List<Dropdown.OptionData>();
            ChampionData = new List<ChampionDto>();
            
            foreach (KeyValuePair<string, ChampionDto> item in response.Data.data)
            {
                data.Add(new Dropdown.OptionData(item.Value.name));
                ChampionData.Add(item.Value);
            }
            
            ChampionData.FirstOrDefault(it => it.key == "Fiddlesticks").key = "FiddleSticks";
            
            textures = GetImage();
            
            Debug.Log(textures.Count() +" - "+ ChampionData.Count());
            
            //if (textures.Count() == ChampionData.Count())
            //{
            //    StartCoroutine(ChampionTextureSet(textures));
            //}
            //else
            //{
            //    StartCoroutine(ChampionTextureSet(textures));
            //}

            ChampionDropDown.ClearOptions();
            ChampionDropDown.AddOptions(data);
            LayoutManager.mData = textures;
            LayoutManager.FirstLayoutSet();

            //StartCoroutine(ChampionTextureDownload());
        }
    }

    private IEnumerator ChampionTextureSet(List<Texture2D> textures)
    {
        Debug.Log("ZZZZZZZZZ : " + ChampItems.Count);
        for (int i = 0; i < ChampItems.Count; i++)
        {
            ChampItems[i].SetData(textures[i]);
            Debug.Log("z");
            yield return new WaitForSeconds(0.001f);
        }
    }

    private IEnumerator ChampionTextureDownload()
    {
        for (int i = 0; i < ChampionData.Count-1; i++)
        {
            string url = String.Format("{0}{1}",RiotGamesConstants.STATIC_IMAGE_API , String.Format("champion/{0}.png", ChampionData[i].key));
            RiotGamesManager.Instance.DownloadTexture(url, ChampionTextureResponse, ChampionData[i].key);
            yield return new WaitForSeconds(0.001f);
        }
    }

    private void ChampionTextureResponse(ResponseResult<Texture2D> response)
    {
        Debug.Log("ChampionTextureResponse.STATUS : " + response.Status);

        if (response.Status == ResponseCode.Success)
        {
            GameObject gm = Instantiate(ChampionPrefab) as GameObject;
            gm.transform.SetParent(Container, false);
            gm.GetComponent<ChampItem>().SetData(response.Data);
         }
    }

    private List<Texture2D> GetImage()
    {


        //string path = System.IO.Path.Combine(Application.dataPath + "/ChampionTexture/", fileName + ".png");
        //Debug.Log(path);
        //byte[] bytes = System.IO.File.ReadAllBytes(path);

        //Texture2D tx = new Texture2D(50, 50, TextureFormat.RGB565, true, true);
        //tx.LoadImage(bytes);

        Debug.Log("*"+Resources.LoadAll<ChampionsPanel>("/Script/").Length);

        return Resources.LoadAll<Texture2D>("ChampionTexture").ToList(); 
    }


    #endregion // Private Methods
}