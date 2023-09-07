using UnityEngine;
using UnityEngine.AddressableAssets;

namespace DeaLoux.CoreSystems.ScriptableObjects
{
    /// <summary>
    /// Event on which <c>AudioCue</c> components send a message to play SFX and music. <c>AudioManager</c> listens on these events, and actually plays the sound.
    /// </summary>
    [CreateAssetMenu(menuName = "Scene Data/New Scene")]
	public class GameSceneSO : DescriptionBaseSO
	{
		public GameSceneType sceneType;
		public AssetReference sceneReference; //Used at runtime to load the scene from the right AssetBundle
		public Audio.AudioCueSO soundtrack;

		/// <summary>
		/// Used by the SceneSelector tool to discern what type of scene it needs to load
		/// </summary>
		public enum GameSceneType
		{
			//Playable scenes
			Stage, //SceneSelector tool will also load PersistentManagers and Gameplay
			Menu, //SceneSelector tool will also load Gameplay

			//Special scenes
			Initialisation,
			Managers,
			Gameplay,
		}
	}
}

