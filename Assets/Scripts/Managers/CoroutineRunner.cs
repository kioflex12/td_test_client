using System.Collections;
using UnityEngine;

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
        
        private void OnDestroy() {
            StopAllCoroutines();
        }
    }
}