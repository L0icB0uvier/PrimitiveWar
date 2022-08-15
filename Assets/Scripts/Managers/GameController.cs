using ScriptableObjects.Events;
using UnityEngine;

namespace Managers
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private VoidEventChannel quitGameEventChannel;

        private void Awake()
        {
            quitGameEventChannel.onEventRaised += QuitGame;
        }

        private void OnDestroy()
        {
            quitGameEventChannel.onEventRaised -= QuitGame;
        }

        private void QuitGame()
        {
#if UNITY_STANDALONE
            Application.Quit();
#endif
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }

    }
}