using BepInEx;
using UnityEngine.XR;
using Utilla;

namespace AntMonkePC
{
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.6.7")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class AirBenderBlender : BaseUnityPlugin
    {
        bool inRoom;
        XRNode rNode = XRNode.RightHand;
        XRNode lNode = XRNode.LeftHand;
        private bool initialized;

        void OnEnable()
        {
            if (initialized)
            {
                GorillaLocomotion.Player.Instance.gameObject.GetComponent<SizeManager>().enabled = false;
            }
            HamburgerPickles.ExtraPickles();
        }

        void OnDisable()
        {
            if (initialized)
            {
                GorillaLocomotion.Player.Instance.gameObject.GetComponent<SizeManager>().enabled = true;
                GorillaLocomotion.Player.Instance.scale = 1f;
            }
            HamburgerPickles.NoPicklesPls();
        }

        void FixedUpdate()
        {
            if (!initialized) initialized = true;
            if (inRoom)
            {
                bool rSC;
                InputDevices.GetDeviceAtXRNode(rNode).TryGetFeatureValue(CommonUsages.primary2DAxisClick, out rSC);
                bool lSC;
                InputDevices.GetDeviceAtXRNode(lNode).TryGetFeatureValue(CommonUsages.primary2DAxisClick, out lSC);
                bool lG;
                InputDevices.GetDeviceAtXRNode(lNode).TryGetFeatureValue(CommonUsages.gripButton, out lG);

                if (rSC && !lG)
                {
                    GorillaLocomotion.Player.Instance.scale += GorillaLocomotion.Player.Instance.scale > 10f ? 0f : 0.01f;
                    GorillaLocomotion.Player.Instance.gameObject.GetComponent<SizeManager>().enabled = false;
                }
                if (lSC && !lG)
                {
                    GorillaLocomotion.Player.Instance.scale -= GorillaLocomotion.Player.Instance.scale < 0.06f ? 0f : 0.01f;
                    GorillaLocomotion.Player.Instance.gameObject.GetComponent<SizeManager>().enabled = false;
                }
                if (rSC && lG && !lSC)
                { 
                    GorillaLocomotion.Player.Instance.gameObject.GetComponent<SizeManager>().enabled = true;
                    GorillaLocomotion.Player.Instance.scale = 1f;
                }
            }
        }

        [ModdedGamemodeJoin]
        public void OnJoin(string gamemode)
        {
            inRoom = true;
            GorillaLocomotion.Player.Instance.gameObject.GetComponent<SizeManager>().enabled = false;
        }

        [ModdedGamemodeLeave]
        public void OnLeave(string gamemode)
        {
            inRoom = false;
            GorillaLocomotion.Player.Instance.gameObject.GetComponent<SizeManager>().enabled = true;
            GorillaLocomotion.Player.Instance.scale = 1f;
        }
    }
}