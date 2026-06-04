#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Unity.Netcode
{
    /// <summary>
    /// Centralized static state cleanup to support Enter Play Mode without Domain Reload.
    /// When domain reload is disabled, static fields retain their values between play mode sessions,
    /// causing stale state, duplicate event registrations, and other issues.
    /// This class resets all mutable static state when exiting play mode.
    /// </summary>
    internal static class StaticStateCleanup
    {
#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingEditMode
               || state = PlayModeStateChange.EnteredEditMode)
            {
                ResetAllStaticState();
            }
        }
#endif

        private static void ResetAllStaticState()
        {
            NetworkUpdateLoop.ResetStaticState();
            NetworkManager.ResetStaticState();
            NetworkObject.ResetStaticState();
            NetworkBehaviour.ResetStaticState();
            ComponentFactory.ResetStaticState();
            NetworkTransform.ResetStaticState();
            NetworkSceneManager.ResetStaticState();
            SceneEventData.ResetStaticState();
            NetworkLog.ResetStaticState();
            DeferredMessageManager.ResetStaticState();
            NetworkMessageManager.ResetStaticState();
            Transports.SinglePlayer.SinglePlayerTransport.ResetStaticState();
            Transports.UTP.UnityTransport.ResetStaticState();
#if COM_UNITY_MODULES_PHYSICS
            RigidbodyContactEventManager.ResetStaticState();
#endif
        }
    }
}
