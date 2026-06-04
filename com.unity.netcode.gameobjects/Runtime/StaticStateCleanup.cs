using UnityEngine;

namespace Unity.Netcode
{
    /// <summary>
    /// Centralized static state cleanup to support Enter Play Mode without Domain Reload.
    /// When domain reload is disabled, static fields retain their values between play mode sessions,
    /// causing stale state, duplicate event registrations, and other issues.
    /// This class resets all mutable static state before each play mode session begins.
    /// </summary>
    internal static class StaticStateCleanup
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
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
