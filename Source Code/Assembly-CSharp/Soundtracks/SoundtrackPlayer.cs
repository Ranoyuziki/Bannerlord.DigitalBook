using System;
using System.Collections.Generic;
using TaleWorlds.CompanionBook.ResourceSystem;
using TaleWorlds.CompanionBook.ResourceSystem.SoundtrackResources;
using UnityEngine;
using UnityEngine.Audio;

namespace TaleWorlds.CompanionBook.Soundtracks
{
	// Token: 0x02000025 RID: 37
	public class SoundtrackPlayer : MonoBehaviour
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600013D RID: 317 RVA: 0x000058D7 File Offset: 0x00003AD7
		public float SoundtrackPlayProgress
		{
			get
			{
				if (!(this._audioSource.clip != null))
				{
					return 0f;
				}
				return this._audioSource.time / this._audioSource.clip.length;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600013E RID: 318 RVA: 0x0000590E File Offset: 0x00003B0E
		public ISoundtrack CurrentSoundtrack
		{
			get
			{
				return this._currentSoundtrack;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00005916 File Offset: 0x00003B16
		public IPlaylist Playlist
		{
			get
			{
				return this._playlist;
			}
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000591E File Offset: 0x00003B1E
		private void Awake()
		{
			this._audioSource = base.gameObject.AddComponent<AudioSource>();
			this._audioSource.outputAudioMixerGroup = this._audioMixerGroup;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00005944 File Offset: 0x00003B44
		private void Start()
		{
			if (Game.Instance.ResourceProvider.IsLoading)
			{
				ResourceProvider resourceProvider = Game.Instance.ResourceProvider;
				resourceProvider.OnPersistentResourcesLoadingFinished = (Action)Delegate.Combine(resourceProvider.OnPersistentResourcesLoadingFinished, new Action(this.InitializeSoundtracks));
				return;
			}
			this.InitializeSoundtracks();
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00005994 File Offset: 0x00003B94
		private void Update()
		{
			Soundtrack currentSoundtrack = this._currentSoundtrack;
			if (currentSoundtrack != null && currentSoundtrack.IsPlaying && !this._audioSource.isPlaying)
			{
				this.OnAudioSourcePlayEnd();
			}
		}

		// Token: 0x06000143 RID: 323 RVA: 0x000059BD File Offset: 0x00003BBD
		public void PlayCurrentPlaylistFromBeginning()
		{
			Playlist playlist = this._playlist;
			this.ToggleSoundtrack(((playlist != null) ? playlist.GetFirstSoundtrackID() : null) ?? string.Empty);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000059E0 File Offset: 0x00003BE0
		public void ToggleSoundtrack(string ID)
		{
			if (this._currentSoundtrack != null && this._currentSoundtrack.ID == ID)
			{
				this._currentSoundtrack.IsPlaying = !this._currentSoundtrack.IsPlaying;
				return;
			}
			Soundtrack soundtrack;
			if (this._soundtracks.TryGetValue(ID, ref soundtrack))
			{
				this.ChangeSoundtrack(soundtrack);
				this._currentSoundtrack.IsPlaying = true;
			}
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00005A48 File Offset: 0x00003C48
		public void PlayPrevious()
		{
			if (this._currentSoundtrack != null)
			{
				if (this._audioSource.time > 3f)
				{
					this._audioSource.time = 0f;
					return;
				}
				bool flag;
				string previousSoundtrackID = this._playlist.GetPreviousSoundtrackID(out flag);
				if (!string.IsNullOrEmpty(previousSoundtrackID))
				{
					if (!flag)
					{
						this.ToggleSoundtrack(previousSoundtrackID);
						return;
					}
					this._audioSource.time = 0f;
				}
			}
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00005AB1 File Offset: 0x00003CB1
		public void PlayNext()
		{
			if (this._currentSoundtrack != null)
			{
				this.OnAudioSourcePlayEnd();
			}
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00005AC4 File Offset: 0x00003CC4
		private void ChangeSoundtrack(Soundtrack soundtrack)
		{
			if (this._currentSoundtrack != null)
			{
				this._currentSoundtrack.IsPlaying = false;
				this._currentSoundtrack.IsSelected = false;
				Soundtrack currentSoundtrack = this._currentSoundtrack;
				currentSoundtrack.OnIsPlayingChanged = (Action<bool>)Delegate.Remove(currentSoundtrack.OnIsPlayingChanged, new Action<bool>(this.OnCurrentSoundtrackIsPlayingChanged));
			}
			this._currentSoundtrack = soundtrack;
			if (this._currentSoundtrack != null)
			{
				this._currentSoundtrack.IsSelected = true;
				Soundtrack currentSoundtrack2 = this._currentSoundtrack;
				currentSoundtrack2.OnIsPlayingChanged = (Action<bool>)Delegate.Combine(currentSoundtrack2.OnIsPlayingChanged, new Action<bool>(this.OnCurrentSoundtrackIsPlayingChanged));
			}
			this._audioSource.Stop();
			this._audioSource.time = 0f;
			this._audioSource.clip = ((soundtrack != null) ? soundtrack.Clip : null);
			Action<ISoundtrack> onSoundtrackChanged = this.OnSoundtrackChanged;
			if (onSoundtrackChanged == null)
			{
				return;
			}
			onSoundtrackChanged.Invoke(soundtrack);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00005B9D File Offset: 0x00003D9D
		private void OnCurrentSoundtrackIsPlayingChanged(bool isPlaying)
		{
			if (isPlaying)
			{
				this._audioSource.Play();
				return;
			}
			this._audioSource.Pause();
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00005BBC File Offset: 0x00003DBC
		private void OnAudioSourcePlayEnd()
		{
			bool flag;
			string nextSoundtrackID = this._playlist.GetNextSoundtrackID(out flag);
			if (!string.IsNullOrEmpty(nextSoundtrackID))
			{
				if (flag)
				{
					Soundtrack soundtrack;
					if (this._soundtracks.TryGetValue(nextSoundtrackID, ref soundtrack))
					{
						this.ChangeSoundtrack(soundtrack);
						return;
					}
				}
				else
				{
					this.ToggleSoundtrack(nextSoundtrackID);
				}
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00005C04 File Offset: 0x00003E04
		private void InitializeSoundtracks()
		{
			this._soundtracks = new Dictionary<string, Soundtrack>();
			List<ISoundtrack> list = new List<ISoundtrack>();
			foreach (SoundtrackClip soundtrackClip in Game.Instance.ResourceProvider.SoundtrackResources.Clips)
			{
				Soundtrack soundtrack = new Soundtrack(soundtrackClip);
				this._soundtracks.Add(soundtrackClip.ID, soundtrack);
				list.Add(soundtrack);
			}
			this._playlist = new Playlist(list);
		}

		// Token: 0x040000EC RID: 236
		public Action<ISoundtrack> OnSoundtrackChanged;

		// Token: 0x040000ED RID: 237
		[SerializeField]
		private AudioMixerGroup _audioMixerGroup;

		// Token: 0x040000EE RID: 238
		private Dictionary<string, Soundtrack> _soundtracks;

		// Token: 0x040000EF RID: 239
		private AudioSource _audioSource;

		// Token: 0x040000F0 RID: 240
		private Soundtrack _currentSoundtrack;

		// Token: 0x040000F1 RID: 241
		private Playlist _playlist;

		// Token: 0x040000F2 RID: 242
		private const float _executePreviousPlayFromBeginningThreshold = 3f;
	}
}
