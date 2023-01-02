using System;
using System.Collections.Generic;
using TaleWorlds.CompanionBook.Options;
using UnityEngine;
using UnityEngine.Audio;

namespace TaleWorlds.CompanionBook.AudioSystem
{
	// Token: 0x0200007D RID: 125
	public class AudioManager : MonoBehaviour
	{
		// Token: 0x060003FA RID: 1018 RVA: 0x0000F484 File Offset: 0x0000D684
		private void Awake()
		{
			if (this._defaultButtonClickClips == null)
			{
				this._defaultButtonClickClips = new List<AudioClip>();
			}
			if (this._defaultButtonHoverClips == null)
			{
				this._defaultButtonHoverClips = new List<AudioClip>();
			}
			this._audioSource = base.gameObject.AddComponent<AudioSource>();
			this._audioSource.outputAudioMixerGroup = this._audioMixerGroup;
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0000F4D9 File Offset: 0x0000D6D9
		private void Start()
		{
			OptionsManager optionsManager = Game.Instance.OptionsManager;
			optionsManager.OnOptionValueChangedEvent = (Action<OptionType>)Delegate.Combine(optionsManager.OnOptionValueChangedEvent, new Action<OptionType>(this.OnOptionValueChanged));
			this.UpdateVolume();
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000F50C File Offset: 0x0000D70C
		public void PlayAudioClip(AudioClip audioClip)
		{
			if (audioClip != null)
			{
				this._audioSource.PlayOneShot(audioClip);
			}
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000F524 File Offset: 0x0000D724
		public void PlayRandomAudioClip(List<AudioClip> clips)
		{
			if (clips != null && clips.Count > 0)
			{
				int num = Random.Range(0, clips.Count);
				AudioClip audioClip = clips[num];
				this.PlayAudioClip(audioClip);
			}
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000F55C File Offset: 0x0000D75C
		public void PlayButtonClip(List<AudioClip> clips, bool isHover)
		{
			List<AudioClip> clips2 = (clips != null && clips.Count > 0) ? clips : (isHover ? this._defaultButtonHoverClips : this._defaultButtonClickClips);
			this.PlayRandomAudioClip(clips2);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000F596 File Offset: 0x0000D796
		private void OnOptionValueChanged(OptionType optionType)
		{
			this.UpdateVolume();
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000F5A0 File Offset: 0x0000D7A0
		private void UpdateVolume()
		{
			IReadOnlyDictionary<OptionType, float> optionValues = Game.Instance.OptionsManager.OptionValues;
			float optionValue;
			if (optionValues.TryGetValue(OptionType.MasterVolume, ref optionValue))
			{
				this._audioMixer.SetFloat("MasterVolume", this.GetVolumeValueFromOptionValue(optionValue));
			}
			float optionValue2;
			if (optionValues.TryGetValue(OptionType.MusicVolume, ref optionValue2))
			{
				this._audioMixer.SetFloat("MusicVolume", this.GetVolumeValueFromOptionValue(optionValue2));
			}
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000F601 File Offset: 0x0000D801
		private float GetVolumeValueFromOptionValue(float optionValue)
		{
			return Mathf.Log10(Mathf.Clamp(optionValue, 0.0001f, 1f)) * 20f;
		}

		// Token: 0x04000358 RID: 856
		[SerializeField]
		private List<AudioClip> _defaultButtonClickClips;

		// Token: 0x04000359 RID: 857
		[SerializeField]
		private List<AudioClip> _defaultButtonHoverClips;

		// Token: 0x0400035A RID: 858
		[SerializeField]
		private AudioMixerGroup _audioMixerGroup;

		// Token: 0x0400035B RID: 859
		[SerializeField]
		private AudioMixer _audioMixer;

		// Token: 0x0400035C RID: 860
		private AudioSource _audioSource;
	}
}
