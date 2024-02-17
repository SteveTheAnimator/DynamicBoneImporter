using System;
using BepInEx;
using UnityEngine;
using Utilla;
using DynamicBoneImporter.DynamicBone;

namespace DynamicBoneImporter
{
	[ModdedGamemode]
	[BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
	[BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
	public class Plugin : BaseUnityPlugin
	{
		bool inRoom;
		public GameObject LeftArmRig;
        public GameObject RightArmRig;

        void Start()
		{
			Utilla.Events.GameInitialized += OnGameInitialized;
		}

		void OnEnable()
		{
			HarmonyPatches.ApplyHarmonyPatches();
		}

		void OnDisable()
		{
			HarmonyPatches.RemoveHarmonyPatches();
		}

		void OnGameInitialized(object sender, EventArgs e)
		{
			LeftArmRig = GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/rig/body/shoulder.L/");
            RightArmRig = GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/rig/body/shoulder.R/");

			LeftArmRig.AddComponent<DynamicBone.DynamicBone>();
            RightArmRig.AddComponent<DynamicBone.DynamicBone>();

			LeftArmRig.GetComponent<DynamicBone.DynamicBone>().m_Root = LeftArmRig.transform;
            LeftArmRig.GetComponent<DynamicBone.DynamicBone>().m_Stiffness = 0.966f;
            RightArmRig.GetComponent<DynamicBone.DynamicBone>().m_Root = RightArmRig.transform;
            RightArmRig.GetComponent<DynamicBone.DynamicBone>().m_Stiffness = 0.966f;
        }

		void Update()
		{
			
		}

		
		[ModdedGamemodeJoin]
		public void OnJoin(string gamemode)
		{
			inRoom = true;
		}

		/* This attribute tells Utilla to call this method when a modded room is left */
		[ModdedGamemodeLeave]
		public void OnLeave(string gamemode)
		{
			inRoom = false;
		}
	}
}
