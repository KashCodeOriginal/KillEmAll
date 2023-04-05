using System;
using UnityEngine;

namespace Other.Game
{
    public class GameBootstrapper : MonoBehaviour
    {
        private GameEntryPoint _gameEntryPoint;
        
        private void Awake()
        {
            _gameEntryPoint = new GameEntryPoint();
            
            DontDestroyOnLoad(this);
        }
    }
}