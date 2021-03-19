using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : PushDownAutomata<GameState>
{
    public GameManager instance;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }
}

