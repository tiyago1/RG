using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChampItem : MonoBehaviour 
{
    #region Fields

    private RawImage ChampImage;
    private Text ChampName;

    #endregion // Fields

    #region Unity Methods

    public void Awake()
    {
        ChampImage = this.transform.GetChild(0).GetChild(0).GetComponent<RawImage>();
        ChampName = this.transform.GetChild(3).GetComponent<Text>();
    }

    public void Update()
    {

    }

    #endregion // Unity Methods
    
    #region Public Methods
    
    public void SetData(Texture2D texture)
    {
        Debug.LogError("ASASDAD");
        ChampImage.texture = texture;
        ChampName.text = texture.name;
    }

    #endregion // Public Methods
    
    #region Private Nethods
    
    private void TestPrivateMethods()
    {
        // to stuff
    }

    #endregion // Private Methods
}