using System;
using TaleWorlds.CompanionBook.BrushSystem;
using TaleWorlds.CompanionBook.Soundtracks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Localization;
using UnityEngine.Localization.SmartFormat.PersistentVariables;
using UnityEngine.UI;

namespace TaleWorlds.CompanionBook
{
	// Token: 0x02000019 RID: 25
	public class SoundtrackControls : MonoBehaviour
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x000044F9 File Offset: 0x000026F9
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x00004501 File Offset: 0x00002701
		public bool IsBlockedByScreen
		{
			get
			{
				return this._isBlockedByScreen;
			}
			set
			{
				if (value != this._isBlockedByScreen)
				{
					this._isBlockedByScreen = value;
					this.RefreshEnabled();
				}
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004519 File Offset: 0x00002719
		private void Awake()
		{
			this._hasPrevNextButtons = !this._isToggleOnly;
			this._soundtrackPlayer = Game.Instance.SoundtrackPlayer;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000453C File Offset: 0x0000273C
		private void Start()
		{
			PlayerActions.UIActions ui = Game.Instance.PlayerActions.UI;
			this._previousSoundtrackAction = ui.PreviousSoundtrackAction;
			this._nextSoundtrackAction = ui.NextSoundtrackAction;
			this._toggleSoundtrackAction = ui.ToggleSoundtrackAction;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004580 File Offset: 0x00002780
		private void OnEnable()
		{
			if (this._hasPrevNextButtons)
			{
				this._previousSoundtrackButton.onClick.AddListener(new UnityAction(this.ExecutePreviousSoundtrack));
				this._nextSoundtrackButton.onClick.AddListener(new UnityAction(this.ExecuteNextSoundtrack));
			}
			this._toggleSoundtrackButton.onClick.AddListener(new UnityAction(this.ExecuteToggleSoundtrack));
			SoundtrackPlayer soundtrackPlayer = this._soundtrackPlayer;
			soundtrackPlayer.OnSoundtrackChanged = (Action<ISoundtrack>)Delegate.Combine(soundtrackPlayer.OnSoundtrackChanged, new Action<ISoundtrack>(this.OnSoundtrackChanged));
			this.OnSoundtrackChanged(this._soundtrackPlayer.CurrentSoundtrack);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004624 File Offset: 0x00002824
		private void OnDisable()
		{
			if (this._hasPrevNextButtons)
			{
				this._previousSoundtrackButton.onClick.RemoveListener(new UnityAction(this.ExecutePreviousSoundtrack));
				this._nextSoundtrackButton.onClick.RemoveListener(new UnityAction(this.ExecuteNextSoundtrack));
			}
			this._toggleSoundtrackButton.onClick.RemoveListener(new UnityAction(this.ExecuteToggleSoundtrack));
			SoundtrackPlayer soundtrackPlayer = this._soundtrackPlayer;
			soundtrackPlayer.OnSoundtrackChanged = (Action<ISoundtrack>)Delegate.Remove(soundtrackPlayer.OnSoundtrackChanged, new Action<ISoundtrack>(this.OnSoundtrackChanged));
			this.OnSoundtrackChanged(null);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000046BC File Offset: 0x000028BC
		private void Update()
		{
			if (this._toggleSoundtrackAction.WasPressedThisFrame())
			{
				this.ExecuteToggleSoundtrack();
				return;
			}
			if (this._hasPrevNextButtons)
			{
				if (this._previousSoundtrackAction.WasPressedThisFrame())
				{
					this.ExecutePreviousSoundtrack();
					return;
				}
				if (this._nextSoundtrackAction.WasPressedThisFrame())
				{
					this.ExecuteNextSoundtrack();
				}
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000470C File Offset: 0x0000290C
		private void OnSoundtrackChanged(ISoundtrack soundtrack)
		{
			if (this._soundtrack != null)
			{
				ISoundtrack soundtrack2 = this._soundtrack;
				soundtrack2.OnIsPlayingChanged = (Action<bool>)Delegate.Remove(soundtrack2.OnIsPlayingChanged, new Action<bool>(this.OnSoundtrackIsPlayingChanged));
			}
			this._soundtrack = soundtrack;
			if (this._soundtrack != null)
			{
				ISoundtrack soundtrack3 = this._soundtrack;
				soundtrack3.OnIsPlayingChanged = (Action<bool>)Delegate.Combine(soundtrack3.OnIsPlayingChanged, new Action<bool>(this.OnSoundtrackIsPlayingChanged));
			}
			this.OnSoundtrackIsPlayingChanged(this._soundtrack != null && this._soundtrack.IsPlaying);
			this.RefreshEnabled();
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000047A0 File Offset: 0x000029A0
		private void OnSoundtrackIsPlayingChanged(bool isPlaying)
		{
			Image image;
			if (this._toggleSoundtrackButton.TryGetComponent<Image>(ref image))
			{
				image.sprite = (isPlaying ? this._pauseSprite : this._playSprite);
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000047D4 File Offset: 0x000029D4
		private void RefreshEnabled()
		{
			this._isEnabled = (!this.IsBlockedByScreen && this._soundtrack != null);
			Brush.SetButtonEnabled(this._toggleSoundtrackButton, this._isEnabled);
			if (this._hasPrevNextButtons)
			{
				Brush.SetButtonEnabled(this._previousSoundtrackButton, this._isEnabled);
				Brush.SetButtonEnabled(this._nextSoundtrackButton, this._isEnabled);
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004836 File Offset: 0x00002A36
		private void ExecutePreviousSoundtrack()
		{
			if (this._isEnabled)
			{
				Game.Instance.SoundtrackPlayer.PlayPrevious();
				this.NotifyChangeSoundtrack(this._soundtrack.Name);
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004860 File Offset: 0x00002A60
		private void ExecuteNextSoundtrack()
		{
			if (this._isEnabled)
			{
				Game.Instance.SoundtrackPlayer.PlayNext();
				this.NotifyChangeSoundtrack(this._soundtrack.Name);
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000488C File Offset: 0x00002A8C
		private void ExecuteToggleSoundtrack()
		{
			if (this._isEnabled && this._soundtrack != null)
			{
				Game.Instance.AudioManager.PlayAudioClip(this._toggleSoundtrackAudioClip);
				Game.Instance.SoundtrackPlayer.ToggleSoundtrack(this._soundtrack.ID);
				this.NotifyToggleSoundtrack(this._soundtrack.Name, this._soundtrack.IsPlaying);
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000048F4 File Offset: 0x00002AF4
		private void NotifyChangeSoundtrack(string name)
		{
			Notification notification = Game.Instance.Notification;
			LocalizedString localizedString = LocalizedText.CreateLocalizedString("ui_notification_change_soundtrack");
			localizedString.Add("COLOR", new StringVariable
			{
				Value = notification.OnColorCode
			});
			localizedString.Add("SOUNDTRACK_NAME", new StringVariable
			{
				Value = name
			});
			notification.Display(localizedString);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004954 File Offset: 0x00002B54
		private void NotifyToggleSoundtrack(string name, bool isPlaying)
		{
			Notification notification = Game.Instance.Notification;
			if (isPlaying)
			{
				LocalizedString localizedString = LocalizedText.CreateLocalizedString("ui_notification_play_music");
				localizedString.Add("COLOR", new StringVariable
				{
					Value = notification.OnColorCode
				});
				localizedString.Add("SOUNDTRACK_NAME", new StringVariable
				{
					Value = name
				});
				notification.Display(localizedString);
				return;
			}
			notification.Display(LocalizedText.CreateLocalizedString("ui_notification_pause_music"));
		}

		// Token: 0x040000A7 RID: 167
		[SerializeField]
		private bool _isToggleOnly;

		// Token: 0x040000A8 RID: 168
		[SerializeField]
		private Button _previousSoundtrackButton;

		// Token: 0x040000A9 RID: 169
		[SerializeField]
		private Button _nextSoundtrackButton;

		// Token: 0x040000AA RID: 170
		[SerializeField]
		private Button _toggleSoundtrackButton;

		// Token: 0x040000AB RID: 171
		[SerializeField]
		private Sprite _playSprite;

		// Token: 0x040000AC RID: 172
		[SerializeField]
		private Sprite _pauseSprite;

		// Token: 0x040000AD RID: 173
		[SerializeField]
		private AudioClip _toggleSoundtrackAudioClip;

		// Token: 0x040000AE RID: 174
		private InputAction _previousSoundtrackAction;

		// Token: 0x040000AF RID: 175
		private InputAction _nextSoundtrackAction;

		// Token: 0x040000B0 RID: 176
		private InputAction _toggleSoundtrackAction;

		// Token: 0x040000B1 RID: 177
		private SoundtrackPlayer _soundtrackPlayer;

		// Token: 0x040000B2 RID: 178
		private ISoundtrack _soundtrack;

		// Token: 0x040000B3 RID: 179
		private bool _isBlockedByScreen;

		// Token: 0x040000B4 RID: 180
		private bool _isEnabled;

		// Token: 0x040000B5 RID: 181
		private bool _hasPrevNextButtons;
	}
}
