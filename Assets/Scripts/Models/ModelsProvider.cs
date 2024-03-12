using UnityEngine;

namespace Models
{
    public static class ModelsProvider
    {
        private static GameSettings m_gameSettings;
        public static GameSettings GameSettings
        {
            get
            {
                if (m_gameSettings == null)
                {
                    m_gameSettings = Resources.Load<GameSettings>("Models/GameSettings");
                }

                return m_gameSettings;
            }
        }
    }
}