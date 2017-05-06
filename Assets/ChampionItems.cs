using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampionItems : MonoBehaviour 
{
    #region Fields

    public List<ChampItem> ChampChildItems;
    public int Piority;
    private RectTransform mTransform;

    #endregion // Fields

    #region Unity Methods
    public void Awake()
    {
        mTransform = this.GetComponent<RectTransform>();
        ChampChildItems = new List<ChampItem>();

        for (int i = 0; i < this.transform.childCount; i++)
        {
            ChampChildItems.Add(this.transform.GetChild(i).GetComponent<ChampItem>());
        }
    }

   
    #endregion // Unity Methods
    
    #region Public Methods

    public void InitData(List<Texture2D> textures, int piority)
    {
        for (int i = 0; i < ChampChildItems.Count; i++)
        {
            ChampChildItems[i].SetData(textures[i]);
        }

        mTransform.anchoredPosition = new Vector2(0, (-1 * (mTransform.sizeDelta.y * (piority - 1))));
        Piority = piority;
    }

    #endregion // Public Methods
    
    #region Private Nethods
    
    private void TestPrivateMethods()
    {
        // to stuff
    }

    #endregion // Private Methods
}