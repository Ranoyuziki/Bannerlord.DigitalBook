using System;
using TaleWorlds.CompanionBook.MenuSection.Options;
using TaleWorlds.CompanionBook.ScreenSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace TaleWorlds.CompanionBook.MenuSection
{
	// Token: 0x02000049 RID: 73
	public class MenuScreen : MonoBehaviour
	{
		// Token: 0x06000240 RID: 576 RVA: 0x00007FE0 File Offset: 0x000061E0
		private void Awake()
		{
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00007FE4 File Offset: 0x000061E4
		private void Start()
		{
			PlayerActions playerActions = Game.Instance.PlayerActions;
			this._cancelAction = playerActions.UI.Cancel;
			this._openOptionsAction = playerActions.MenuActions.OpenOptionsAction;
			this._anyButtonAction = playerActions.UI.CustomAnyButtonAction;
			this._globalBackground.SetIsMoveEnabled(true);
			this._menuButtons.SetButtonsEnabled(false);
			this._bottomRightButtonsCanvas.alpha = 0f;
			this._bottomRightButtonsCanvas.interactable = false;
			this._bottomRightButtonsCanvas.blocksRaycasts = false;
			if (SystemInfo.deviceType == 2)
			{
				this._exitButton.gameObject.SetActive(false);
				this._exitButton = null;
			}
			Game.Instance.SoundtrackPlayer.PlayCurrentPlaylistFromBeginning();
			this._pressAnyButtonText.SetReference("ui_menu_press_any_button_" + Helper.GetTextEntryPlatformExtension());
		}

		// Token: 0x06000242 RID: 578 RVA: 0x000080C4 File Offset: 0x000062C4
		private void OnEnable()
		{
			ExitConfirmationPopup exitConfirmationPopup = this._exitConfirmationPopup;
			exitConfirmationPopup.OnConfirm = (Action)Delegate.Combine(exitConfirmationPopup.OnConfirm, new Action(this.OnConfirmExitConfirmation));
			ExitConfirmationPopup exitConfirmationPopup2 = this._exitConfirmationPopup;
			exitConfirmationPopup2.OnCancel = (Action)Delegate.Combine(exitConfirmationPopup2.OnCancel, new Action(this.OnCancelExitConfirmation));
			BookQueryPopup bookQueryPopup = this._bookQueryPopup;
			bookQueryPopup.OnContinue = (Action)Delegate.Combine(bookQueryPopup.OnContinue, new Action(this.OnBookQueryContinue));
			BookQueryPopup bookQueryPopup2 = this._bookQueryPopup;
			bookQueryPopup2.OnRestart = (Action)Delegate.Combine(bookQueryPopup2.OnRestart, new Action(this.OnBookQueryRestart));
			BookQueryPopup bookQueryPopup3 = this._bookQueryPopup;
			bookQueryPopup3.OnCancel = (Action)Delegate.Combine(bookQueryPopup3.OnCancel, new Action(this.OnBookQueryCancel));
			MenuButtons menuButtons = this._menuButtons;
			menuButtons.OnOpenScreen = (Action<ScreenType>)Delegate.Combine(menuButtons.OnOpenScreen, new Action<ScreenType>(this.OnOpenScreen));
			this._optionsButton.onClick.AddListener(new UnityAction(this.ExecuteOpenOptions));
			OptionsPanel optionsPopup = this._optionsPopup;
			optionsPopup.OnClose = (Action)Delegate.Combine(optionsPopup.OnClose, new Action(this.OnCloseOptions));
			if (this._exitButton != null)
			{
				this._exitButton.onClick.AddListener(new UnityAction(this.ExecuteOpenExitConfirmation));
			}
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00008228 File Offset: 0x00006428
		private void OnDisable()
		{
			ExitConfirmationPopup exitConfirmationPopup = this._exitConfirmationPopup;
			exitConfirmationPopup.OnConfirm = (Action)Delegate.Remove(exitConfirmationPopup.OnConfirm, new Action(this.OnConfirmExitConfirmation));
			ExitConfirmationPopup exitConfirmationPopup2 = this._exitConfirmationPopup;
			exitConfirmationPopup2.OnCancel = (Action)Delegate.Remove(exitConfirmationPopup2.OnCancel, new Action(this.OnCancelExitConfirmation));
			BookQueryPopup bookQueryPopup = this._bookQueryPopup;
			bookQueryPopup.OnContinue = (Action)Delegate.Remove(bookQueryPopup.OnContinue, new Action(this.OnBookQueryContinue));
			BookQueryPopup bookQueryPopup2 = this._bookQueryPopup;
			bookQueryPopup2.OnRestart = (Action)Delegate.Remove(bookQueryPopup2.OnRestart, new Action(this.OnBookQueryRestart));
			BookQueryPopup bookQueryPopup3 = this._bookQueryPopup;
			bookQueryPopup3.OnCancel = (Action)Delegate.Remove(bookQueryPopup3.OnCancel, new Action(this.OnBookQueryCancel));
			MenuButtons menuButtons = this._menuButtons;
			menuButtons.OnOpenScreen = (Action<ScreenType>)Delegate.Remove(menuButtons.OnOpenScreen, new Action<ScreenType>(this.OnOpenScreen));
			this._optionsButton.onClick.RemoveListener(new UnityAction(this.ExecuteOpenOptions));
			OptionsPanel optionsPopup = this._optionsPopup;
			optionsPopup.OnClose = (Action)Delegate.Remove(optionsPopup.OnClose, new Action(this.OnCloseOptions));
			if (this._exitButton != null)
			{
				this._exitButton.onClick.RemoveListener(new UnityAction(this.ExecuteOpenExitConfirmation));
			}
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000838C File Offset: 0x0000658C
		private void Update()
		{
			if (!this._hasPressedAnyButton && this._anyButtonAction.WasPressedThisFrame())
			{
				this._hasPressedAnyButton = true;
			}
			if (this._state == MenuScreen.MenuScreenTransitionState.TransitionToWaitForKey)
			{
				this.ProcessTransitionToWaitForKeyState();
				return;
			}
			if (this._state == MenuScreen.MenuScreenTransitionState.WaitingForKey && this._hasPressedAnyButton)
			{
				this._globalBackground.SetIsMoveEnabled(false);
				this._state = MenuScreen.MenuScreenTransitionState.TransitionToFinished;
				Game.Instance.AudioManager.PlayAudioClip(this._startAudioClip);
				return;
			}
			if (this._state == MenuScreen.MenuScreenTransitionState.TransitionToFinished)
			{
				this.ProcessTransitionToFinishedState();
				return;
			}
			if (this._state == MenuScreen.MenuScreenTransitionState.Finished && !this._exitConfirmationPopup.gameObject.activeSelf && !this._bookQueryPopup.gameObject.activeSelf && !this._optionsPopup.gameObject.activeSelf)
			{
				if (SystemInfo.deviceType != 2 && this._cancelAction.WasPressedThisFrame())
				{
					this.ExecuteOpenExitConfirmation();
					return;
				}
				if (this._openOptionsAction.WasPressedThisFrame())
				{
					this.ExecuteOpenOptions();
				}
			}
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000847C File Offset: 0x0000667C
		private void ProcessTransitionToWaitForKeyState()
		{
			if (this._animationProgress == null)
			{
				this._animationProgress = new AnimationProgress(3f, AnimationProgressType.EaseOutCirc);
			}
			this._animationProgress.Progress(Time.deltaTime);
			this._pressAnyButtonTextCanvasGroup.alpha = this._animationProgress.Progression;
			this._bannerlordLogo.CanvasGroup.alpha = this._animationProgress.Progression;
			this._companionAppLogo.CanvasGroup.alpha = this._animationProgress.Progression;
			if (this._animationProgress.IsFinished || this._hasPressedAnyButton)
			{
				this._pressAnyButtonTextCanvasGroup.alpha = 1f;
				this._bannerlordLogo.CanvasGroup.alpha = 1f;
				this._companionAppLogo.CanvasGroup.alpha = 1f;
				this._animationProgress = null;
				this._state = MenuScreen.MenuScreenTransitionState.WaitingForKey;
			}
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000855C File Offset: 0x0000675C
		private void ProcessTransitionToFinishedState()
		{
			if (this._animationProgress == null)
			{
				this._animationProgress = new AnimationProgress(0.7f, AnimationProgressType.EaseInOutBack);
			}
			this._animationProgress.Progress(Time.deltaTime);
			float progression = this._animationProgress.Progression;
			this._bannerlordLogo.OnPositionProgressChanged(progression);
			this._companionAppLogo.OnPositionProgressChanged(progression);
			this._pressAnyButtonTextCanvasGroup.alpha = Mathf.Clamp(1f - progression, 0f, 1f);
			this._menuButtons.CanvasGroup.alpha = Mathf.Clamp(progression, 0f, 1f);
			this._bottomRightButtonsCanvas.alpha = Mathf.Clamp(progression, 0f, 1f);
			if (this._animationProgress.IsFinished)
			{
				this._bottomRightButtonsCanvas.interactable = true;
				this._bottomRightButtonsCanvas.blocksRaycasts = true;
				this._menuButtons.SetButtonsEnabled(true);
				this._state = MenuScreen.MenuScreenTransitionState.Finished;
			}
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000864C File Offset: 0x0000684C
		private void OnOpenScreen(ScreenType screenType)
		{
			if (screenType != ScreenType.Book)
			{
				Game.Instance.ScreenManager.SwitchToScreen(screenType, Array.Empty<object>());
				return;
			}
			if (Game.Instance.ActiveBookSessionChapter >= 0)
			{
				this.OpenBookQuery();
				return;
			}
			Game.Instance.ScreenManager.SwitchToScreen(ScreenType.Book, new object[]
			{
				true
			});
		}

		// Token: 0x06000248 RID: 584 RVA: 0x000086A6 File Offset: 0x000068A6
		private void OnBookQueryContinue()
		{
			this.CloseBookQuery();
			Game.Instance.ScreenManager.SwitchToScreen(ScreenType.Book, new object[]
			{
				false
			});
		}

		// Token: 0x06000249 RID: 585 RVA: 0x000086CD File Offset: 0x000068CD
		private void OnBookQueryRestart()
		{
			this.CloseBookQuery();
			Game.Instance.ScreenManager.SwitchToScreen(ScreenType.Book, new object[]
			{
				true
			});
		}

		// Token: 0x0600024A RID: 586 RVA: 0x000086F4 File Offset: 0x000068F4
		private void OnBookQueryCancel()
		{
			this.CloseBookQuery();
		}

		// Token: 0x0600024B RID: 587 RVA: 0x000086FC File Offset: 0x000068FC
		private void OpenBookQuery()
		{
			this._menuButtons.SetButtonsEnabled(false);
			this._bookQueryPopup.gameObject.SetActive(true);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000871B File Offset: 0x0000691B
		private void CloseBookQuery()
		{
			this._menuButtons.SetButtonsEnabled(true);
			this._bookQueryPopup.gameObject.SetActive(false);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000873A File Offset: 0x0000693A
		private void ExecuteOpenExitConfirmation()
		{
			this._menuButtons.SetButtonsEnabled(false);
			this._exitConfirmationPopup.gameObject.SetActive(true);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00008759 File Offset: 0x00006959
		private void OnCancelExitConfirmation()
		{
			this._exitConfirmationPopup.gameObject.SetActive(false);
			this._menuButtons.SetButtonsEnabled(true);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00008778 File Offset: 0x00006978
		private void ExecuteOpenOptions()
		{
			this._menuButtons.SetButtonsEnabled(false);
			this._optionsPopup.gameObject.SetActive(true);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00008797 File Offset: 0x00006997
		private void OnCloseOptions()
		{
			this._menuButtons.SetButtonsEnabled(true);
			this._optionsPopup.gameObject.SetActive(false);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x000087B6 File Offset: 0x000069B6
		private void OnConfirmExitConfirmation()
		{
			Application.Quit();
		}

		// Token: 0x040001BB RID: 443
		[SerializeField]
		private GlobalBackground _globalBackground;

		// Token: 0x040001BC RID: 444
		[SerializeField]
		private MenuButtons _menuButtons;

		// Token: 0x040001BD RID: 445
		[SerializeField]
		private CanvasGroup _bottomRightButtonsCanvas;

		// Token: 0x040001BE RID: 446
		[SerializeField]
		private Button _exitButton;

		// Token: 0x040001BF RID: 447
		[SerializeField]
		private Button _optionsButton;

		// Token: 0x040001C0 RID: 448
		[SerializeField]
		private ExitConfirmationPopup _exitConfirmationPopup;

		// Token: 0x040001C1 RID: 449
		[SerializeField]
		private OptionsPanel _optionsPopup;

		// Token: 0x040001C2 RID: 450
		[SerializeField]
		private BookQueryPopup _bookQueryPopup;

		// Token: 0x040001C3 RID: 451
		[SerializeField]
		private CanvasGroup _pressAnyButtonTextCanvasGroup;

		// Token: 0x040001C4 RID: 452
		[SerializeField]
		private LocalizedText _pressAnyButtonText;

		// Token: 0x040001C5 RID: 453
		[SerializeField]
		private MenuLogo _bannerlordLogo;

		// Token: 0x040001C6 RID: 454
		[SerializeField]
		private MenuLogo _companionAppLogo;

		// Token: 0x040001C7 RID: 455
		[SerializeField]
		private AudioClip _startAudioClip;

		// Token: 0x040001C8 RID: 456
		private InputAction _cancelAction;

		// Token: 0x040001C9 RID: 457
		private InputAction _openOptionsAction;

		// Token: 0x040001CA RID: 458
		private InputAction _anyButtonAction;

		// Token: 0x040001CB RID: 459
		private bool _hasPressedAnyButton;

		// Token: 0x040001CC RID: 460
		private const float _transitionToWaitForKeyDuration = 3f;

		// Token: 0x040001CD RID: 461
		private const float _transitionToFinishedDuration = 0.7f;

		// Token: 0x040001CE RID: 462
		private AnimationProgress _animationProgress;

		// Token: 0x040001CF RID: 463
		private MenuScreen.MenuScreenTransitionState _state;

		// Token: 0x020000B2 RID: 178
		private enum MenuScreenTransitionState
		{
			// Token: 0x040003ED RID: 1005
			TransitionToWaitForKey,
			// Token: 0x040003EE RID: 1006
			WaitingForKey,
			// Token: 0x040003EF RID: 1007
			TransitionToFinished,
			// Token: 0x040003F0 RID: 1008
			Finished
		}
	}
}
