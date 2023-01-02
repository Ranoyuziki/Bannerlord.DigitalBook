using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CompanionBook.FontSizeManagement;
using TaleWorlds.CompanionBook.ResourceSystem.BookResources;
using TaleWorlds.CompanionBook.ScreenSystem;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.Localization;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

namespace TaleWorlds.CompanionBook.BookSection
{
	// Token: 0x02000072 RID: 114
	[RequireComponent(typeof(UIModeHandler))]
	public class BookScreen : MonoBehaviour
	{
		// Token: 0x06000395 RID: 917 RVA: 0x0000D3D8 File Offset: 0x0000B5D8
		private void Awake()
		{
			this._UIModeHandler = base.GetComponent<UIModeHandler>();
			this._book = Game.Instance.ResourceProvider.BookResources;
			this._audioSource = base.gameObject.AddComponent<AudioSource>();
			this._audioSource.outputAudioMixerGroup = this._audioMixerGroup;
			this._audioSource.playOnAwake = false;
			this._carouselControls = Object.Instantiate<CarouselControls>(this._carouselControlsPrefab, this._carouselControlsParent.transform);
			this._carouselControls.Initialize(this._book.Chapters.Count, new Action<int>(this.OnChaptersCarouselItemSelected), true);
			PlayerActions playerActions = Game.Instance.PlayerActions;
			PlayerActions.BookActionsActions bookActions = playerActions.BookActions;
			this._previousPageAction = bookActions.PreviousPage;
			this._nextPageAction = bookActions.NextPage;
			this._toggleVoiceOverAction = bookActions.ToggleVoiceOverAction;
			this._toggleAutoTurnAction = bookActions.ToggleAutoTurnAction;
			this._toggleChapterSelectionAction = bookActions.ToggleChapterSelection;
			this._toggleUIAction = bookActions.ToggleUIAction;
			this._cancelAction = playerActions.UI.Cancel;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000D4EC File Offset: 0x0000B6EC
		private void Start()
		{
			this._soundtrackControls.IsBlockedByScreen = true;
			this._settingsPage.Initialize(new Action<VoiceOverState>(this.NotifyVoiceOverState), new Action<bool>(this.NotifyUIMode));
			this.OpenPage(0, 0);
			this.SetVoiceOverState(VoiceOverState.Disabled);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000D52C File Offset: 0x0000B72C
		private void OnEnable()
		{
			SettingsPage settingsPage = this._settingsPage;
			settingsPage.OnDone = (Action<VoiceOverState, bool>)Delegate.Combine(settingsPage.OnDone, new Action<VoiceOverState, bool>(this.OnSettingsDone));
			SettingsPage settingsPage2 = this._settingsPage;
			settingsPage2.OnUIModeChanged = (Action<bool>)Delegate.Combine(settingsPage2.OnUIModeChanged, new Action<bool>(this.OnSettingsUIModeChanged));
			UIModeHandler uimodeHandler = this._UIModeHandler;
			uimodeHandler.OnIsUIEnabledChanged = (Action)Delegate.Combine(uimodeHandler.OnIsUIEnabledChanged, new Action(this.OnIsUIEnabledChanged));
			ToggleUIButton toggleUIButton = this._toggleUIButton;
			toggleUIButton.OnToggle = (Action)Delegate.Combine(toggleUIButton.OnToggle, new Action(this.ExecuteToggleUI));
			VoiceOverControls voiceOverControls = this._voiceOverControls;
			voiceOverControls.OnToggleVoiceOver = (Action)Delegate.Combine(voiceOverControls.OnToggleVoiceOver, new Action(this.ExecuteToggleVoiceOver));
			VoiceOverControls voiceOverControls2 = this._voiceOverControls;
			voiceOverControls2.OnToggleAutoTurn = (Action)Delegate.Combine(voiceOverControls2.OnToggleAutoTurn, new Action(this.ExecuteToggleAutoTurn));
			ChapterSelectionPopup chapterSelectionPopup = this._chapterSelectionPopup;
			chapterSelectionPopup.OnSelectChapter = (Action<Chapter>)Delegate.Combine(chapterSelectionPopup.OnSelectChapter, new Action<Chapter>(this.OnChapterSelected));
			ChapterSelectionPopup chapterSelectionPopup2 = this._chapterSelectionPopup;
			chapterSelectionPopup2.OnClose = (Action)Delegate.Combine(chapterSelectionPopup2.OnClose, new Action(this.ExecuteToggleChapterSelection));
			NavigationControls navigationControls = this._navigationControls;
			navigationControls.OnOpenNextPage = (Action)Delegate.Combine(navigationControls.OnOpenNextPage, new Action(this.ExecuteOpenNextPage));
			NavigationControls navigationControls2 = this._navigationControls;
			navigationControls2.OnOpenPreviousPage = (Action)Delegate.Combine(navigationControls2.OnOpenPreviousPage, new Action(this.ExecuteOpenPreviousPage));
			NavigationControls navigationControls3 = this._navigationControls;
			navigationControls3.OnToggleChapterSelection = (Action)Delegate.Combine(navigationControls3.OnToggleChapterSelection, new Action(this.ExecuteToggleChapterSelection));
			Game instance = Game.Instance;
			instance.OnGamePauseStateChanged = (Action<bool>)Delegate.Combine(instance.OnGamePauseStateChanged, new Action<bool>(this.OnGamePauseStateChanged));
			bool flag = false;
			ScreenComponent screenComponent;
			if (base.TryGetComponent<ScreenComponent>(ref screenComponent) && screenComponent.OpeningArgs.Length != 0)
			{
				flag = (bool)screenComponent.OpeningArgs[0];
			}
			if (flag)
			{
				this._settingsPage.gameObject.SetActive(true);
				this._contentPageCanvasGroup.alpha = 0f;
				this.OpenPage(0, 0);
				return;
			}
			this._settingsPage.gameObject.SetActive(false);
			this._contentPageCanvasGroup.alpha = 1f;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000D780 File Offset: 0x0000B980
		private void OnDisable()
		{
			bool activeSelf = this._settingsPage.gameObject.activeSelf;
			Game.Instance.ActiveBookSessionChapter = ((activeSelf || this._currentPage == null) ? -1 : this._currentPage.Chapter.Index);
			this._settingsPage.gameObject.SetActive(false);
			this.SetVoiceOverState(VoiceOverState.Disabled);
			SettingsPage settingsPage = this._settingsPage;
			settingsPage.OnDone = (Action<VoiceOverState, bool>)Delegate.Remove(settingsPage.OnDone, new Action<VoiceOverState, bool>(this.OnSettingsDone));
			SettingsPage settingsPage2 = this._settingsPage;
			settingsPage2.OnUIModeChanged = (Action<bool>)Delegate.Remove(settingsPage2.OnUIModeChanged, new Action<bool>(this.OnSettingsUIModeChanged));
			UIModeHandler uimodeHandler = this._UIModeHandler;
			uimodeHandler.OnIsUIEnabledChanged = (Action)Delegate.Remove(uimodeHandler.OnIsUIEnabledChanged, new Action(this.OnIsUIEnabledChanged));
			ToggleUIButton toggleUIButton = this._toggleUIButton;
			toggleUIButton.OnToggle = (Action)Delegate.Remove(toggleUIButton.OnToggle, new Action(this.ExecuteToggleUI));
			VoiceOverControls voiceOverControls = this._voiceOverControls;
			voiceOverControls.OnToggleVoiceOver = (Action)Delegate.Remove(voiceOverControls.OnToggleVoiceOver, new Action(this.ExecuteToggleVoiceOver));
			VoiceOverControls voiceOverControls2 = this._voiceOverControls;
			voiceOverControls2.OnToggleAutoTurn = (Action)Delegate.Remove(voiceOverControls2.OnToggleAutoTurn, new Action(this.ExecuteToggleAutoTurn));
			ChapterSelectionPopup chapterSelectionPopup = this._chapterSelectionPopup;
			chapterSelectionPopup.OnSelectChapter = (Action<Chapter>)Delegate.Remove(chapterSelectionPopup.OnSelectChapter, new Action<Chapter>(this.OnChapterSelected));
			ChapterSelectionPopup chapterSelectionPopup2 = this._chapterSelectionPopup;
			chapterSelectionPopup2.OnClose = (Action)Delegate.Remove(chapterSelectionPopup2.OnClose, new Action(this.ExecuteToggleChapterSelection));
			NavigationControls navigationControls = this._navigationControls;
			navigationControls.OnOpenNextPage = (Action)Delegate.Remove(navigationControls.OnOpenNextPage, new Action(this.ExecuteOpenNextPage));
			NavigationControls navigationControls2 = this._navigationControls;
			navigationControls2.OnOpenPreviousPage = (Action)Delegate.Remove(navigationControls2.OnOpenPreviousPage, new Action(this.ExecuteOpenPreviousPage));
			NavigationControls navigationControls3 = this._navigationControls;
			navigationControls3.OnToggleChapterSelection = (Action)Delegate.Remove(navigationControls3.OnToggleChapterSelection, new Action(this.ExecuteToggleChapterSelection));
			Game instance = Game.Instance;
			instance.OnGamePauseStateChanged = (Action<bool>)Delegate.Remove(instance.OnGamePauseStateChanged, new Action<bool>(this.OnGamePauseStateChanged));
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000D9B8 File Offset: 0x0000BBB8
		private void Update()
		{
			bool activeSelf = this._chapterSelectionPopup.gameObject.activeSelf;
			bool activeSelf2 = this._settingsPage.gameObject.activeSelf;
			this._soundtrackControls.IsBlockedByScreen = (activeSelf || activeSelf2);
			if (this._currentPage != null)
			{
				this._currentPage.IsDisabled = (activeSelf || activeSelf2);
			}
			if (!activeSelf2)
			{
				if (activeSelf)
				{
					if (this._toggleChapterSelectionAction.WasPressedThisFrame() || this._cancelAction.WasPressedThisFrame())
					{
						this.ExecuteToggleChapterSelection();
					}
				}
				else if (this._previousPageAction.WasPressedThisFrame())
				{
					this.ExecuteOpenPreviousPage();
				}
				else if (this._nextPageAction.WasPressedThisFrame())
				{
					this.ExecuteOpenNextPage();
				}
				else if (this._voiceOverControls.IsOn && this._toggleVoiceOverAction.WasPressedThisFrame())
				{
					this.ExecuteToggleVoiceOver();
				}
				else if (this._voiceOverControls.IsOn && this._toggleAutoTurnAction.WasPressedThisFrame())
				{
					this.ExecuteToggleAutoTurn();
				}
				else if (this._toggleChapterSelectionAction.WasPressedThisFrame())
				{
					this.ExecuteToggleChapterSelection();
				}
				else if (this._currentPage != null && this._currentPage.Chapter.Index == this._book.Chapters.Count - 1 && this._cancelAction.WasPressedThisFrame())
				{
					if (!this._UIModeHandler.IsUIEnabled)
					{
						this._UIModeHandler.SetUIEnabled(true, true);
					}
					this._backToMenuButton.CheckActions();
				}
				else if (this._toggleUIAction.WasPressedThisFrame() || (!this._UIModeHandler.IsUIEnabled && this._cancelAction.WasPressedThisFrame()))
				{
					this.ExecuteToggleUI();
				}
				else
				{
					this._fontSizeControlPanel.CheckActions();
					this._backToMenuButton.CheckActions();
				}
				this.HandleVoiceOverContinuity();
			}
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000DB85 File Offset: 0x0000BD85
		private void OnSettingsDone(VoiceOverState voiceOverState, bool isUIModeEnabled)
		{
			this._contentPageCanvasGroup.alpha = 1f;
			this.SetVoiceOverState(voiceOverState);
			this._UIModeHandler.SetUIEnabled(isUIModeEnabled, true);
			this._toggleUIButton.OnUIModeChanged(this._UIModeHandler.IsUIEnabled);
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000DBC1 File Offset: 0x0000BDC1
		private void OnSettingsUIModeChanged(bool isUIModeEnabled)
		{
			this._UIModeHandler.SetUIEnabled(isUIModeEnabled, true);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000DBD0 File Offset: 0x0000BDD0
		private void OnIsUIEnabledChanged()
		{
			if (this._currentPage != null)
			{
				this._toggleUIButton.OnUIModeChanged(this._UIModeHandler.IsUIEnabled);
				this._currentPage.OnIsUIEnabledChanged(this._UIModeHandler.IsUIEnabled);
			}
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000DC0C File Offset: 0x0000BE0C
		private void HandleVoiceOverContinuity()
		{
			if (this._voiceOverState != VoiceOverState.Disabled && this._currentPage != null && this._audioSource != null && this._audioSource.clip != null)
			{
				Chapter chapter = this._currentPage.Chapter;
				Page page = this._currentPage.Page;
				bool flag = chapter.Index == this._book.Chapters.Count - 1;
				bool flag2 = page.Index == chapter.Pages.Count - 1;
				bool flag3 = flag && flag2;
				if (this._audioSource.time > page.EndTime || !this._audioSource.isPlaying)
				{
					if (this._voiceOverState == VoiceOverState.EnabledManual || flag3)
					{
						this.SetVoiceOverState(VoiceOverState.Disabled);
						return;
					}
					if (flag2)
					{
						this.OpenPage(chapter.Index + 1, 0);
						return;
					}
					this.OpenPage(chapter.Index, page.Index + 1);
				}
			}
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000DD0C File Offset: 0x0000BF0C
		private void ExecuteToggleVoiceOver()
		{
			this.SetVoiceOverState((this._voiceOverState == VoiceOverState.EnabledAuto || this._voiceOverState == VoiceOverState.EnabledManual) ? VoiceOverState.Disabled : VoiceOverState.EnabledManual);
			this.NotifyVoiceOverState(this._voiceOverState);
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000DD48 File Offset: 0x0000BF48
		private void ExecuteToggleAutoTurn()
		{
			if (this._voiceOverState == VoiceOverState.EnabledAuto || this._voiceOverState == VoiceOverState.EnabledManual)
			{
				this.SetVoiceOverState((this._voiceOverState == VoiceOverState.EnabledAuto) ? VoiceOverState.EnabledManual : VoiceOverState.EnabledAuto);
				this.NotifyVoiceOverState(this._voiceOverState);
			}
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000DD80 File Offset: 0x0000BF80
		private void ExecuteToggleUI()
		{
			this._UIModeHandler.SetUIEnabled(!this._UIModeHandler.IsUIEnabled, false);
			this.NotifyUIMode(this._UIModeHandler.IsUIEnabled);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000DDAD File Offset: 0x0000BFAD
		private void ExecuteOpenPreviousPage()
		{
			this.OpenPreviousPage();
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000DDB5 File Offset: 0x0000BFB5
		private void ExecuteOpenNextPage()
		{
			this.OpenNextPage();
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000DDC0 File Offset: 0x0000BFC0
		private void ExecuteToggleChapterSelection()
		{
			if (this._chapterSelectionPopup.gameObject.activeSelf)
			{
				this._chapterSelectionPopup.OnHide();
				this._chapterSelectionPopup.gameObject.SetActive(false);
				return;
			}
			this._chapterSelectionPopup.gameObject.SetActive(true);
			this._chapterSelectionPopup.OnShow((this._currentPage != null) ? this._currentPage.Chapter : null);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000DE34 File Offset: 0x0000C034
		private void OnChapterSelected(Chapter chapter)
		{
			this.ExecuteToggleChapterSelection();
			this.OpenPage(chapter.Index, 0);
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000DE4C File Offset: 0x0000C04C
		private void OnChaptersCarouselItemSelected(int index)
		{
			Chapter chapter = Enumerable.FirstOrDefault<Chapter>(this._book.Chapters, (Chapter x) => x.Index == index);
			if (chapter != null)
			{
				this.OpenPage(chapter.Index, 0);
			}
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000DE94 File Offset: 0x0000C094
		private void SetVoiceOverState(VoiceOverState voiceOverState)
		{
			if (this._voiceOverState != voiceOverState)
			{
				VoiceOverState voiceOverState2 = this._voiceOverState;
				this._voiceOverState = voiceOverState;
				if (this._audioSource != null && this._audioSource.clip != null)
				{
					if (voiceOverState == VoiceOverState.Disabled)
					{
						this._audioSource.Pause();
					}
					else if (voiceOverState2 == VoiceOverState.Disabled)
					{
						this._audioSource.Play();
					}
				}
				this._voiceOverControls.State = voiceOverState;
			}
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000DF08 File Offset: 0x0000C108
		private void NotifyVoiceOverState(VoiceOverState state)
		{
			Notification notification = Game.Instance.Notification;
			if (state == VoiceOverState.Disabled)
			{
				LocalizedString localizedString = LocalizedText.CreateLocalizedString("ui_notification_voice_over_disabled");
				localizedString.Add("COLOR", new StringVariable
				{
					Value = notification.OffColorCode
				});
				localizedString.Add("STATUS", LocalizedText.CreateLocalizedString("ui_notification_off"));
				notification.Display(localizedString);
				return;
			}
			string value = (state == VoiceOverState.EnabledAuto) ? notification.OnColorCode : notification.OffColorCode;
			string entry = (state == VoiceOverState.EnabledAuto) ? "ui_notification_on" : "ui_notification_off";
			LocalizedString localizedString2 = LocalizedText.CreateLocalizedString("ui_notification_voice_over_enabled");
			localizedString2.Add("DEFAULT_COLOR", new StringVariable
			{
				Value = notification.DefaultColorCode
			});
			localizedString2.Add("VO_COLOR", new StringVariable
			{
				Value = notification.OnColorCode
			});
			localizedString2.Add("TURN_COLOR", new StringVariable
			{
				Value = value
			});
			localizedString2.Add("VO_STATUS", LocalizedText.CreateLocalizedString("ui_notification_on"));
			localizedString2.Add("TURN_STATUS", LocalizedText.CreateLocalizedString(entry));
			notification.Display(localizedString2);
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000E01C File Offset: 0x0000C21C
		private void NotifyUIMode(bool isEnabled)
		{
			Notification notification = Game.Instance.Notification;
			LocalizedString localizedString = LocalizedText.CreateLocalizedString("ui_notification_user_interface");
			localizedString.Add("COLOR", new StringVariable
			{
				Value = (isEnabled ? notification.OnColorCode : notification.OffColorCode)
			});
			localizedString.Add("STATUS", LocalizedText.CreateLocalizedString(isEnabled ? "ui_notification_on" : "ui_notification_off"));
			notification.Display(localizedString);
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000E08C File Offset: 0x0000C28C
		private void OpenPreviousPage()
		{
			if (this._currentPage != null)
			{
				Chapter chapter = this._currentPage.Chapter;
				Page page = this._currentPage.Page;
				bool flag = chapter.Index == 0;
				bool flag2 = page.Index == 0;
				if (!flag || !flag2)
				{
					if (flag2)
					{
						Chapter chapter2 = this._book.Chapters[chapter.Index - 1];
						this.OpenPage(chapter2.Index, chapter2.Pages.Count - 1);
						return;
					}
					this.OpenPage(chapter.Index, page.Index - 1);
				}
			}
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000E120 File Offset: 0x0000C320
		private void OpenNextPage()
		{
			if (this._currentPage != null)
			{
				Chapter chapter = this._currentPage.Chapter;
				Page page = this._currentPage.Page;
				bool flag = chapter.Index == this._book.Chapters.Count - 1;
				bool flag2 = page.Index == chapter.Pages.Count - 1;
				if (!flag || !flag2)
				{
					if (flag2)
					{
						this.OpenPage(chapter.Index + 1, 0);
						return;
					}
					this.OpenPage(chapter.Index, page.Index + 1);
				}
			}
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000E1B0 File Offset: 0x0000C3B0
		private void OpenPage(int chapterIndex, int pageIndex)
		{
			int num = -1;
			if (this._currentPage != null)
			{
				num = this._currentPage.Chapter.Index;
				BookPageController currentPage = this._currentPage;
				currentPage.OnReadyToPlayVoiceOver = (Action)Delegate.Remove(currentPage.OnReadyToPlayVoiceOver, new Action(this.OnCurrentPageReadyToPlayVoiceOver));
				this._currentPage.OnFinalize();
				this._audioSource.Stop();
				this._audioSource.clip = null;
			}
			if (chapterIndex >= 0 && chapterIndex < this._book.Chapters.Count)
			{
				Chapter chapter = this._book.Chapters[chapterIndex];
				if (pageIndex >= 0 && pageIndex < chapter.Pages.Count)
				{
					Page page = chapter.Pages[pageIndex];
					GameObject gameObject = Object.Instantiate<GameObject>(this.GetPagePrefab(page.PageType), this._pageParent.transform);
					this._currentPage = gameObject.GetComponent<BookPageController>();
					this._currentPage.Initialize(chapter, page, this._pageParent.transform.GetComponent<RectTransform>(), num == chapterIndex);
					BookPageController currentPage2 = this._currentPage;
					currentPage2.OnReadyToPlayVoiceOver = (Action)Delegate.Combine(currentPage2.OnReadyToPlayVoiceOver, new Action(this.OnCurrentPageReadyToPlayVoiceOver));
					this.OnPageChanged(this._currentPage);
				}
			}
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000E2F8 File Offset: 0x0000C4F8
		private void OnCurrentPageReadyToPlayVoiceOver()
		{
			if (this._currentPage != null)
			{
				this._audioSource.Stop();
				this._audioSource.clip = this._currentPage.Chapter.VoiceOverClip;
				this._audioSource.time = this._currentPage.Page.StartTime;
				if (this._voiceOverState != VoiceOverState.Disabled)
				{
					this._audioSource.Play();
				}
			}
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000E368 File Offset: 0x0000C568
		private void OnPageChanged(BookPageController currentPage)
		{
			if (this._chapterSelectionPopup.gameObject.activeSelf)
			{
				this._chapterSelectionPopup.OnChapterChanged(currentPage.Chapter);
			}
			currentPage.OnIsUIEnabledChanged(this._UIModeHandler.IsUIEnabled);
			bool isFirstPage = currentPage.Chapter.Index == 0 && currentPage.Page.Index == 0;
			bool isLastPage = currentPage.Chapter.Index == this._book.Chapters.Count - 1 && currentPage.Page.Index == currentPage.Chapter.Pages.Count - 1;
			this._navigationControls.OnPageChanged(isFirstPage, isLastPage);
			this._carouselControls.RefreshSelectedIndex(currentPage.Chapter.Index);
			if (currentPage.Chapter.VoiceOverClip == null)
			{
				this.SetVoiceOverState(VoiceOverState.Disabled);
				this._voiceOverControls.IsOn = false;
			}
			else
			{
				this._voiceOverControls.IsOn = true;
			}
			Game.Instance.AudioManager.PlayRandomAudioClip(this._changePageAudioClip);
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000E478 File Offset: 0x0000C678
		private GameObject GetPagePrefab(PageType pageType)
		{
			GameObject result = this._defaultPage;
			if (pageType != PageType.First)
			{
				if (pageType != PageType.Last)
				{
					result = this._defaultPage;
				}
				else
				{
					result = this._lastPage;
				}
			}
			else
			{
				result = this._firstPage;
			}
			return result;
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000E4B0 File Offset: 0x0000C6B0
		private void OnGamePauseStateChanged(bool isPaused)
		{
			if (isPaused)
			{
				this.SetVoiceOverState(VoiceOverState.Disabled);
			}
		}

		// Token: 0x040002E8 RID: 744
		[SerializeField]
		private GameObject _pageParent;

		// Token: 0x040002E9 RID: 745
		[SerializeField]
		private GameObject _defaultPage;

		// Token: 0x040002EA RID: 746
		[SerializeField]
		private GameObject _firstPage;

		// Token: 0x040002EB RID: 747
		[SerializeField]
		private GameObject _lastPage;

		// Token: 0x040002EC RID: 748
		[SerializeField]
		private ChapterSelectionPopup _chapterSelectionPopup;

		// Token: 0x040002ED RID: 749
		[SerializeField]
		private SoundtrackControls _soundtrackControls;

		// Token: 0x040002EE RID: 750
		[SerializeField]
		private VoiceOverControls _voiceOverControls;

		// Token: 0x040002EF RID: 751
		[SerializeField]
		private BackToMenuButton _backToMenuButton;

		// Token: 0x040002F0 RID: 752
		[SerializeField]
		private NavigationControls _navigationControls;

		// Token: 0x040002F1 RID: 753
		[SerializeField]
		private AudioMixerGroup _audioMixerGroup;

		// Token: 0x040002F2 RID: 754
		[SerializeField]
		private SettingsPage _settingsPage;

		// Token: 0x040002F3 RID: 755
		[SerializeField]
		private FontSizeControlPanel _fontSizeControlPanel;

		// Token: 0x040002F4 RID: 756
		[SerializeField]
		private ToggleUIButton _toggleUIButton;

		// Token: 0x040002F5 RID: 757
		[SerializeField]
		private CanvasGroup _contentPageCanvasGroup;

		// Token: 0x040002F6 RID: 758
		[SerializeField]
		private GameObject _carouselControlsParent;

		// Token: 0x040002F7 RID: 759
		[SerializeField]
		private CarouselControls _carouselControlsPrefab;

		// Token: 0x040002F8 RID: 760
		[SerializeField]
		private List<AudioClip> _changePageAudioClip;

		// Token: 0x040002F9 RID: 761
		private UIModeHandler _UIModeHandler;

		// Token: 0x040002FA RID: 762
		private InputAction _previousPageAction;

		// Token: 0x040002FB RID: 763
		private InputAction _nextPageAction;

		// Token: 0x040002FC RID: 764
		private InputAction _toggleVoiceOverAction;

		// Token: 0x040002FD RID: 765
		private InputAction _toggleAutoTurnAction;

		// Token: 0x040002FE RID: 766
		private InputAction _toggleChapterSelectionAction;

		// Token: 0x040002FF RID: 767
		private InputAction _toggleUIAction;

		// Token: 0x04000300 RID: 768
		private InputAction _cancelAction;

		// Token: 0x04000301 RID: 769
		private VoiceOverState _voiceOverState;

		// Token: 0x04000302 RID: 770
		private AudioSource _audioSource;

		// Token: 0x04000303 RID: 771
		private CarouselControls _carouselControls;

		// Token: 0x04000304 RID: 772
		private BookPageController _currentPage;

		// Token: 0x04000305 RID: 773
		private Book _book;
	}
}
