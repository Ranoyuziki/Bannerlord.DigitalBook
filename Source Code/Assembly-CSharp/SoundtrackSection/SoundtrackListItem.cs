using System;
using TaleWorlds.CompanionBook.BrushSystem;
using TaleWorlds.CompanionBook.Soundtracks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TaleWorlds.CompanionBook.SoundtrackSection
{
	// Token: 0x0200001F RID: 31
	public class SoundtrackListItem : MonoBehaviour
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00005293 File Offset: 0x00003493
		public Brush Brush
		{
			get
			{
				return this._brush;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600010A RID: 266 RVA: 0x0000529B File Offset: 0x0000349B
		// (set) Token: 0x0600010B RID: 267 RVA: 0x000052A3 File Offset: 0x000034A3
		public ISoundtrack Soundtrack { get; private set; }

		// Token: 0x0600010C RID: 268 RVA: 0x000052AC File Offset: 0x000034AC
		private void Awake()
		{
			this._actionButton.onClick.AddListener(new UnityAction(this.ExecuteTogglePlay));
			this._tupleButton.onClick.AddListener(new UnityAction(this.ExecuteTogglePlay));
			ButtonHoverHandler buttonHoverHandler;
			if (this._tupleButton.TryGetComponent<ButtonHoverHandler>(ref buttonHoverHandler))
			{
				ButtonHoverHandler buttonHoverHandler2 = buttonHoverHandler;
				buttonHoverHandler2.OnIsHoveredChanged = (Action<bool>)Delegate.Combine(buttonHoverHandler2.OnIsHoveredChanged, new Action<bool>(this.OnIsHoveredChanged));
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00005324 File Offset: 0x00003524
		private void OnDestroy()
		{
			this._actionButton.onClick.RemoveListener(new UnityAction(this.ExecuteTogglePlay));
			this._tupleButton.onClick.RemoveListener(new UnityAction(this.ExecuteTogglePlay));
			ISoundtrack soundtrack = this.Soundtrack;
			soundtrack.OnIsPlayingChanged = (Action<bool>)Delegate.Remove(soundtrack.OnIsPlayingChanged, new Action<bool>(this.OnIsPlayingChanged));
			ISoundtrack soundtrack2 = this.Soundtrack;
			soundtrack2.OnIsSelectedChanged = (Action<bool>)Delegate.Remove(soundtrack2.OnIsSelectedChanged, new Action<bool>(this.OnIsSelectedChanged));
			this.Soundtrack = null;
			ButtonHoverHandler buttonHoverHandler;
			if (this._tupleButton.TryGetComponent<ButtonHoverHandler>(ref buttonHoverHandler))
			{
				ButtonHoverHandler buttonHoverHandler2 = buttonHoverHandler;
				buttonHoverHandler2.OnIsHoveredChanged = (Action<bool>)Delegate.Remove(buttonHoverHandler2.OnIsHoveredChanged, new Action<bool>(this.OnIsHoveredChanged));
			}
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000053F0 File Offset: 0x000035F0
		public void Initialize(ISoundtrack soundtrack)
		{
			this.Soundtrack = soundtrack;
			ISoundtrack soundtrack2 = this.Soundtrack;
			soundtrack2.OnIsPlayingChanged = (Action<bool>)Delegate.Combine(soundtrack2.OnIsPlayingChanged, new Action<bool>(this.OnIsPlayingChanged));
			ISoundtrack soundtrack3 = this.Soundtrack;
			soundtrack3.OnIsSelectedChanged = (Action<bool>)Delegate.Combine(soundtrack3.OnIsSelectedChanged, new Action<bool>(this.OnIsSelectedChanged));
			this._nameText.text = this.Soundtrack.Name;
			this._durationText.text = this.Soundtrack.DurationText;
			this.OnIsPlayingChanged(this.Soundtrack.IsPlaying);
			this.OnIsSelectedChanged(this.Soundtrack.IsSelected);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000054A0 File Offset: 0x000036A0
		public void ExecuteTogglePlay()
		{
			Game.Instance.SoundtrackPlayer.ToggleSoundtrack(this.Soundtrack.ID);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000054BC File Offset: 0x000036BC
		private void OnIsPlayingChanged(bool isPlaying)
		{
			this.UpdateMainActionSprite();
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000054C4 File Offset: 0x000036C4
		private void OnIsSelectedChanged(bool isSelected)
		{
			this._brush.IsSelected = isSelected;
			this._toggleBrush.IsSelected = isSelected;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000054DE File Offset: 0x000036DE
		private void OnIsHoveredChanged(bool isHovered)
		{
			Action<ISoundtrack, bool> onHoverChanged = this.OnHoverChanged;
			if (onHoverChanged == null)
			{
				return;
			}
			onHoverChanged.Invoke(this.Soundtrack, isHovered);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000054F8 File Offset: 0x000036F8
		private void UpdateMainActionSprite()
		{
			Image image;
			if (this._actionButton.TryGetComponent<Image>(ref image))
			{
				image.sprite = (this.Soundtrack.IsPlaying ? this._pauseSprite : this._playSprite);
			}
		}

		// Token: 0x040000D3 RID: 211
		public Action<ISoundtrack, bool> OnHoverChanged;

		// Token: 0x040000D4 RID: 212
		[SerializeField]
		private TMP_Text _nameText;

		// Token: 0x040000D5 RID: 213
		[SerializeField]
		private TMP_Text _durationText;

		// Token: 0x040000D6 RID: 214
		[SerializeField]
		private Sprite _playSprite;

		// Token: 0x040000D7 RID: 215
		[SerializeField]
		private Sprite _pauseSprite;

		// Token: 0x040000D8 RID: 216
		[SerializeField]
		private Button _actionButton;

		// Token: 0x040000D9 RID: 217
		[SerializeField]
		private Button _tupleButton;

		// Token: 0x040000DA RID: 218
		[SerializeField]
		private Brush _brush;

		// Token: 0x040000DB RID: 219
		[SerializeField]
		private Brush _toggleBrush;
	}
}
