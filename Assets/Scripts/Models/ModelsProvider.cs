using UnityEngine;

namespace Models
{
    public static class ModelsProvider
    {
        private static GameSettings _gameSettings;

        public static GameSettings GameSettings
        {
            get
            {
                if (_gameSettings == null)
                {
                    _gameSettings = Resources.Load<GameSettings>("Models/GameSettings");
                }
                return _gameSettings;
            }
        }
    }
}