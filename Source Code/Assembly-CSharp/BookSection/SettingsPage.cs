using System;
using TaleWorlds.CompanionBook.FontSizeManagement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace TaleWorlds.CompanionBook.BookSection
{
	// Token: 0x02000078 RID: 120
	[RequireComponent(typeof(CanvasGroup))]
	public class SettingsPage : MonoBehaviour
	{
		// Token: 0x060003D6 RID: 982 RVA: 0x0000EB04 File Offset: 0x0000CD04
		private void Awake()
		{
			this._canvasGroup = base.GetComponent<CanvasGroup>();
			PlayerActions.BookActionsActions bookActions = Game.Instance.PlayerActions.BookActions;
			this._toggleVoiceOverAction = bookActions.ToggleVoiceOverAction;
			this._toggleAutoTurnAction = bookActions.ToggleAutoTurnAction;
			this._toggleUIAction = bookActions.ToggleUIAction;
			this._submitAction = Game.Instance.PlayerActions.UI.Submit;
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000EB71 File Offset: 0x0000CD71
		private void Start()
		{
			this.ResetPage();
			this._isInitialized = true;
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000EB80 File Offset: 0x0000CD80
		private void OnEnable()
		{
			if (this._isInitialized)
			{
				this.ResetPage();
			}
			this._doneButton.onClick.AddListener(new UnityAction(this.ExecuteDone));
			VoiceOverControls voiceOverControls = this._voiceOverControls;
			voiceOverControls.OnToggleVoiceOver = (Action)Delegate.Combine(voiceOverControls.OnToggleVoiceOver, new Action(this.ExecuteToggleVoiceOver));
			VoiceOverControls voiceOverControls2 = this._voiceOverControls;
			voiceOverControls2.OnToggleAutoTurn = (Action)Delegate.Combine(voiceOverControls2.OnToggleAutoTurn, new Action(this.ExecuteToggleAutoTurn));
			ToggleUIButton toggleUIButton = this._toggleUIButton;
			toggleUIButton.OnToggle = (Action)Delegate.Combine(toggleUIButton.OnToggle, new Action(this.ExecuteToggleUI));
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000EC2C File Offset: 0x0000CE2C
		private void OnDisable()
		{
			this._doneButton.onClick.RemoveListener(new UnityAction(this.ExecuteDone));
			VoiceOverControls voiceOverControls = this._voiceOverControls;
			voiceOverControls.OnToggleVoiceOver = (Action)Delegate.Remove(voiceOverControls.OnToggleVoiceOver, new Action(this.ExecuteToggleVoiceOver));
			VoiceOverControls voiceOverControls2 = this._voiceOverControls;
			voiceOverControls2.OnToggleAutoTurn = (Action)Delegate.Remove(voiceOverControls2.OnToggleAutoTurn, new Action(this.ExecuteToggleAutoTurn));
			ToggleUIButton toggleUIButton = this._toggleUIButton;
			toggleUIButton.OnToggle = (Action)Delegate.Remove(toggleUIButton.OnToggle, new Action(this.ExecuteToggleUI));
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000ECCC File Offset: 0x0000CECC
		private void Update()
		{
			if (this._closingAnimation != null)
			{
				this._closingAnimation.Progress(Time.deltaTime);
				this._canvasGroup.alpha = 1f - this._closingAnimation.Progression;
				if (this._closingAnimation.IsFinished)
				{
					base.gameObject.SetActive(false);
					return;
				}
			}
			else
			{
				if (this._toggleVoiceOverAction.WasPressedThisFrame())
				{
					this.ExecuteToggleVoiceOver();
					return;
				}
				if (this._toggleAutoTurnAction.WasPressedThisFrame())
				{
					this.ExecuteToggleAutoTurn();
					return;
				}
				if (this._toggleUIAction.WasPressedThisFrame())
				{
					this.ExecuteToggleUI();
					return;
				}
				if (this._submitAction.WasPressedThisFrame())
				{
					this.ExecuteDone();
					return;
				}
				this._fontSizeControlPanel.CheckActions();
				this._backToMenuButton.CheckActions();
			}
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000ED90 File Offset: 0x0000CF90
		public void Initialize(Action<VoiceOverState> notifyVoiceOverState, Action<bool> notifyUIMode)
		{
			this._notifyVoiceOverState = notifyVoiceOverState;
			this._notifyUIMode = notifyUIMode;
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000EDA0 File Offset: 0x0000CFA0
		private void ResetPage()
		{
			this.SetVoiceOverSetting(VoiceOverState.EnabledAuto);
			this.SetUIModeSetting(false);
			this._closingAnimation = null;
			this._canvasGroup.interactable = true;
			this._canvasGroup.blocksRaycasts = true;
			this._canvasGroup.alpha = 1f;
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000EDDF File Offset: 0x0000CFDF
		private void SetVoiceOverSetting(VoiceOverState state)
		{
			this._voiceOverState = state;
			this._voiceOverControls.State = this._voiceOverState;
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000EDF9 File Offset: 0x0000CFF9
		private void SetUIModeSetting(bool isEnabled)
		{
			this._isUIModeEnabled = isEnabled;
			this._toggleUIButton.OnUIModeChanged(isEnabled);
			Action<bool> onUIModeChanged = this.OnUIModeChanged;
			if (onUIModeChanged == null)
			{
				return;
			}
			onUIModeChanged.Invoke(isEnabled);
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000EE20 File Offset: 0x0000D020
		private void ExecuteToggleVoiceOver()
		{
			this.SetVoiceOverSetting((this._voiceOverState == VoiceOverState.EnabledAuto || this._voiceOverState == VoiceOverState.EnabledManual) ? VoiceOverState.Disabled : VoiceOverState.EnabledManual);
			Action<VoiceOverState> notifyVoiceOverState = this._notifyVoiceOverState;
			if (notifyVoiceOverState == null)
			{
				return;
			}
			notifyVoiceOverState.Invoke(this._voiceOverState);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000EE68 File Offset: 0x0000D068
		private void ExecuteToggleAutoTurn()
		{
			if (this._voiceOverState == VoiceOverState.EnabledAuto || this._voiceOverState == VoiceOverState.EnabledManual)
			{
				this.SetVoiceOverSetting((this._voiceOverState == VoiceOverState.EnabledAuto) ? VoiceOverState.EnabledManual : VoiceOverState.EnabledAuto);
				Action<VoiceOverState> notifyVoiceOverState = this._notifyVoiceOverState;
				if (notifyVoiceOverState == null)
				{
					return;
				}
				notifyVoiceOverState.Invoke(this._voiceOverState);
			}
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000EEB5 File Offset: 0x0000D0B5
		private void ExecuteToggleUI()
		{
			this.SetUIModeSetting(!this._isUIModeEnabled);
			Action<bool> notifyUIMode = this._notifyUIMode;
			if (notifyUIMode == null)
			{
				return;
			}
			notifyUIMode.Invoke(this._isUIModeEnabled);
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0000EEDC File Offset: 0x0000D0DC
		private void ExecuteDone()
		{
			this._canvasGroup.interactable = false;
			this._canvasGroup.blocksRaycasts = false;
			this._closingAnimation = new AnimationProgress(0.25f, AnimationProgressType.EaseOutCirc);
			Action<VoiceOverState, bool> onDone = this.OnDone;
			if (onDone == null)
			{
				return;
			}
			onDone.Invoke(this._voiceOverState, this._isUIModeEnabled);
		}

		// Token: 0x04000326 RID: 806
		public Action<VoiceOverState, bool> OnDone;

		// Token: 0x04000327 RID: 807
		public Action<bool> OnUIModeChanged;

		// Token: 0x04000328 RID: 808
		[SerializeField]
		private Button _doneButton;

		// Token: 0x04000329 RID: 809
		[SerializeField]
		private VoiceOverControls _voiceOverControls;

		// Token: 0x0400032A RID: 810
		[SerializeField]
		private ToggleUIButton _toggleUIButton;

		// Token: 0x0400032B RID: 811
		[SerializeField]
		private BackToMenuButton _backToMenuButton;

		// Token: 0x0400032C RID: 812
		[SerializeField]
		private FontSizeControlPanel _fontSizeControlPanel;

		// Token: 0x0400032D RID: 813
		private Action<VoiceOverState> _notifyVoiceOverState;

		// Token: 0x0400032E RID: 814
		private Action<bool> _notifyUIMode;

		// Token: 0x0400032F RID: 815
		private VoiceOverState _voiceOverState;

		// Token: 0x04000330 RID: 816
		private bool _isUIModeEnabled;

		// Token: 0x04000331 RID: 817
		private InputAction _toggleVoiceOverAction;

		// Token: 0x04000332 RID: 818
		private InputAction _toggleAutoTurnAction;

		// Token: 0x04000333 RID: 819
		private InputAction _toggleUIAction;

		// Token: 0x04000334 RID: 820
		private InputAction _submitAction;

		// Token: 0x04000335 RID: 821
		private CanvasGroup _canvasGroup;

		// Token: 0x04000336 RID: 822
		private AnimationProgress _closingAnimation;

		// Token: 0x04000337 RID: 823
		private const float _animationDuration = 0.25f;

		// Token: 0x04000338 RID: 824
		private const AnimationProgressType _animationProgressType = AnimationProgressType.EaseOutCirc;

		// Token: 0x04000339 RID: 825
		private bool _isInitialized;
	}
}
