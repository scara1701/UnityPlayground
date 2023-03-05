using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region boilerplate
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Game manager is null!");

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    #endregion
}
