using System;
using TaleWorlds.CompanionBook.BrushSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TaleWorlds.CompanionBook.BookSection
{
	// Token: 0x0200007B RID: 123
	[RequireComponent(typeof(Button))]
	public class VoiceOverControls : MonoBehaviour
	{
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x0000F2E3 File Offset: 0x0000D4E3
		// (set) Token: 0x060003F0 RID: 1008 RVA: 0x0000F2EB File Offset: 0x0000D4EB
		public bool IsOn
		{
			get
			{
				return this._isOn;
			}
			set
			{
				if (value != this._isOn)
				{
					this._isOn = value;
					this.Refresh();
				}
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x0000F303 File Offset: 0x0000D503
		// (set) Token: 0x060003F2 RID: 1010 RVA: 0x0000F30B File Offset: 0x0000D50B
		public VoiceOverState State
		{
			get
			{
				return this._state;
			}
			set
			{
				if (value != this._state)
				{
					this._state = value;
					this.Refresh();
				}
			}
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000F323 File Offset: 0x0000D523
		private void Awake()
		{
			this._button = base.GetComponent<Button>();
			this.Refresh();
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000F337 File Offset: 0x0000D537
		private void OnEnable()
		{
			this._button.onClick.AddListener(new UnityAction(this.ExecuteToggleVoiceOver));
			this._autoTurnButton.onClick.AddListener(new UnityAction(this.ExecuteToggleAutoTurn));
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000F371 File Offset: 0x0000D571
		private void OnDisable()
		{
			this._button.onClick.RemoveListener(new UnityAction(this.ExecuteToggleVoiceOver));
			this._autoTurnButton.onClick.RemoveListener(new UnityAction(this.ExecuteToggleAutoTurn));
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000F3AC File Offset: 0x0000D5AC
		private void Refresh()
		{
			Sprite sprite = (this._isOn && this._state != VoiceOverState.Disabled) ? this._voiceOverEnabledImage : this._voiceOverDisabledImage;
			Sprite sprite2 = (this._isOn && this._state == VoiceOverState.EnabledAuto) ? this._autoTurnEnabledImage : this._autoTurnDisabledImage;
			bool isOn = this._isOn;
			bool isEnabled = this._isOn && this._state > VoiceOverState.Disabled;
			Brush.SetButtonEnabled(this._button, isOn);
			Brush.SetButtonEnabled(this._autoTurnButton, isEnabled);
			this._button.image.sprite = sprite;
			this._autoTurnButton.image.sprite = sprite2;
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000F44E File Offset: 0x0000D64E
		private void ExecuteToggleVoiceOver()
		{
			Action onToggleVoiceOver = this.OnToggleVoiceOver;
			if (onToggleVoiceOver == null)
			{
				return;
			}
			onToggleVoiceOver.Invoke();
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000F460 File Offset: 0x0000D660
		private void ExecuteToggleAutoTurn()
		{
			Action onToggleAutoTurn = this.OnToggleAutoTurn;
			if (onToggleAutoTurn == null)
			{
				return;
			}
			onToggleAutoTurn.Invoke();
		}

		// Token: 0x0400034A RID: 842
		public Action OnToggleVoiceOver;

		// Token: 0x0400034B RID: 843
		public Action OnToggleAutoTurn;

		// Token: 0x0400034C RID: 844
		[SerializeField]
		private Sprite _voiceOverEnabledImage;

		// Token: 0x0400034D RID: 845
		[SerializeField]
		private Sprite _voiceOverDisabledImage;

		// Token: 0x0400034E RID: 846
		[SerializeField]
		private Sprite _autoTurnEnabledImage;

		// Token: 0x0400034F RID: 847
		[SerializeField]
		private Sprite _autoTurnDisabledImage;

		// Token: 0x04000350 RID: 848
		[SerializeField]
		private Button _autoTurnButton;

		// Token: 0x04000351 RID: 849
		private Button _button;

		// Token: 0x04000352 RID: 850
		private bool _isOn = true;

		// Token: 0x04000353 RID: 851
		private VoiceOverState _state;
	}
}
