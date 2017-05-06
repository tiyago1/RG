using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour 
{
    #region Fields

    protected UIManager mManager;

    #endregion // Fields

    #region Public Methods
    
    public void Initialize(UIManager manager)
    {
        mManager = manager;
    }

    #endregion // Public Methods
}