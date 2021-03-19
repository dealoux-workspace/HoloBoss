using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;


namespace DeaLoux.CoreSystems.ScriptableObjects
{
	[CreateAssetMenu(menuName = "Events/Playable Director Channel")]
	public class PlayableDirectorChannelSO : ScriptableObject
	{
		public UnityAction<PlayableDirector> OnEventRaised;
		public void RaiseEvent(PlayableDirector playable)
		{
			if (OnEventRaised != null)
				OnEventRaised.Invoke(playable);
		}
	}
}
