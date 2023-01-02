using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TaleWorlds.CompanionBook.AudioSystem
{
	// Token: 0x0200007E RID: 126
	[RequireComponent(typeof(Button))]
	[RequireComponent(typeof(ButtonHoverHandler))]
	public class ButtonAudioPlayer : MonoBehaviour
	{
		// Token: 0x06000403 RID: 1027 RVA: 0x0000F626 File Offset: 0x0000D826
		private void Awake()
		{
			this._button = base.GetComponent<Button>();
			this._buttonHoverHandler = base.GetComponent<ButtonHoverHandler>();
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000F640 File Offset: 0x0000D840
		private void OnEnable()
		{
			ButtonHoverHandler buttonHoverHandler = this._buttonHoverHandler;
			buttonHoverHandler.OnIsHoveredChanged = (Action<bool>)Delegate.Combine(buttonHoverHandler.OnIsHoveredChanged, new Action<bool>(this.OnIsHoveredChanged));
			this._button.onClick.AddListener(new UnityAction(this.PlayButtonClickAudio));
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000F690 File Offset: 0x0000D890
		private void OnDisable()
		{
			ButtonHoverHandler buttonHoverHandler = this._buttonHoverHandler;
			buttonHoverHandler.OnIsHoveredChanged = (Action<bool>)Delegate.Remove(buttonHoverHandler.OnIsHoveredChanged, new Action<bool>(this.OnIsHoveredChanged));
			this._button.onClick.RemoveListener(new UnityAction(this.PlayButtonClickAudio));
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000F6E0 File Offset: 0x0000D8E0
		private void OnIsHoveredChanged(bool isHovered)
		{
			if (isHovered)
			{
				this.PlayButtonHoverAudio();
			}
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000F6EB File Offset: 0x0000D8EB
		private void PlayButtonClickAudio()
		{
			if (!this._disableClickClips)
			{
				Game.Instance.AudioManager.PlayButtonClip(this._clickClips, false);
			}
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000F70B File Offset: 0x0000D90B
		private void PlayButtonHoverAudio()
		{
			if (!this._disableHoverClips)
			{
				Game.Instance.AudioManager.PlayButtonClip(this._hoverClips, true);
			}
		}

		// Token: 0x0400035D RID: 861
		[SerializeField]
		private bool _disableClickClips;

		// Token: 0x0400035E RID: 862
		[SerializeField]
		private List<AudioClip> _clickClips;

		// Token: 0x0400035F RID: 863
		[SerializeField]
		private bool _disableHoverClips;

		// Token: 0x04000360 RID: 864
		[SerializeField]
		private List<AudioClip> _hoverClips;

		// Token: 0x04000361 RID: 865
		private Button _button;

		// Token: 0x04000362 RID: 866
		private ButtonHoverHandler _buttonHoverHandler;
	}
}
