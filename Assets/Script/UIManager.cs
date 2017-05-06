using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Panels
{
    ConnectionPanel,
    ChampionsPanel
}

public class UIManager : MonoBehaviour 
{
    #region Fields

    public List<Panel> Panels;

    #endregion // Fields

    #region Unity Methods

    public void Start()
    {
        Initialize();
    }

    public void Update()
    {

    }

    #endregion // Unity Methods
    
    #region Public Methods
    
    public void Initialize()
    {
        Panels.ForEach(it => it.Initialize(this));
    }

    public void ShowPanel(Panels panel)
    {
    
        Panels.ForEach(it => it.gameObject.SetActive(false));
        Panels[(int)panel].gameObject.SetActive(true);
    }

    #endregion // Public Methods
    
    #region Private Nethods
    
    private void TestPrivateMethods()
    {
        // to stuff
    }

    #endregion // Private Methods
}