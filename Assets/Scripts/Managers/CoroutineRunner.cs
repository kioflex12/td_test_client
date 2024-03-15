using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers {
    public class CoroutineRunner : MonoBehaviour {
        private static CoroutineRunner m_instance;

        private static CoroutineRunner Instance {
            get {
                if (m_instance != null) return m_instance;
                var go = new GameObject("[CoroutineRunner]");
                m_instance = go.AddComponent<CoroutineRunner>();
                DontDestroyOnLoad(go);
                return m_instance;
            }
        }
        
        public new static Coroutine StartCoroutine(IEnumerator coroutine) {
            return ((MonoBehaviour)Instance).StartCoroutine(coroutine);
        }
        
        public static void Stop(Coroutine coroutine) {
            if (coroutine != null) {
                Instance.StopCoroutine(coroutine);
            }
        }

        private void OnEnable() {
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        private void OnDisable() {
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
        }

        private void OnSceneUnloaded(Scene current) {
            if (this != null) { 
                Destroy(gameObject);
            }
        }
        private void OnDestroy() {
            StopAllCoroutines();
        }
    }
}