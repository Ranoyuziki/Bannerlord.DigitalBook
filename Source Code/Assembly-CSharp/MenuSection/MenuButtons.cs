using System;
using System.Collections.Generic;
using TaleWorlds.CompanionBook.BrushSystem;
using TaleWorlds.CompanionBook.ScreenSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TaleWorlds.CompanionBook.MenuSection
{
	// Token: 0x02000047 RID: 71
	[RequireComponent(typeof(RectTransform))]
	[RequireComponent(typeof(CanvasGroup))]
	public class MenuButtons : MonoBehaviour
	{
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00007A10 File Offset: 0x00005C10
		// (set) Token: 0x0600022D RID: 557 RVA: 0x00007A18 File Offset: 0x00005C18
		public CanvasGroup CanvasGroup { get; private set; }

		// Token: 0x0600022E RID: 558 RVA: 0x00007A24 File Offset: 0x00005C24
		private void Awake()
		{
			List<Button> list = new List<Button>();
			list.Add(this._openBookScreenButton);
			list.Add(this._openMapScreenButton);
			list.Add(this._openConceptArtsScreenButton);
			list.Add(this._openSoundtracksScreenButton);
			list.Add(this._openCreditsScreenButton);
			this._buttons = list;
			this._rectTransform = base.GetComponent<RectTransform>();
			this.CanvasGroup = base.GetComponent<CanvasGroup>();
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00007A90 File Offset: 0x00005C90
		private void OnEnable()
		{
			this._openMapScreenButton.onClick.AddListener(new UnityAction(this.ExecuteOpenMapScreen));
			this._openSoundtracksScreenButton.onClick.AddListener(new UnityAction(this.ExecuteOpenSoundtracksScreen));
			this._openBookScreenButton.onClick.AddListener(new UnityAction(this.ExecuteOpenBookScreen));
			this._openConceptArtsScreenButton.onClick.AddListener(new UnityAction(this.ExecuteOpenConceptArtsScreen));
			this._openCreditsScreenButton.onClick.AddListener(new UnityAction(this.ExecuteOpenCreditsScreen));
			ScreenManager screenManager = Game.Instance.ScreenManager;
			screenManager.OnCurrentResolutionChanged = (Action)Delegate.Combine(screenManager.OnCurrentResolutionChanged, new Action(this.RefreshContainerSize));
			this.RefreshContainerSize();
			Game instance = Game.Instance;
			instance.OnControlSchemeChanged = (Action)Delegate.Combine(instance.OnControlSchemeChanged, new Action(this.LoadNavigationItem));
			this.LoadNavigationItem();
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00007B88 File Offset: 0x00005D88
		private void OnDisable()
		{
			this.SaveNavigationItem();
			this._openMapScreenButton.onClick.RemoveListener(new UnityAction(this.ExecuteOpenMapScreen));
			this._openSoundtracksScreenButton.onClick.RemoveListener(new UnityAction(this.ExecuteOpenSoundtracksScreen));
			this._openBookScreenButton.onClick.RemoveListener(new UnityAction(this.ExecuteOpenBookScreen));
			this._openConceptArtsScreenButton.onClick.RemoveListener(new UnityAction(this.ExecuteOpenConceptArtsScreen));
			this._openCreditsScreenButton.onClick.RemoveListener(new UnityAction(this.ExecuteOpenCreditsScreen));
			ScreenManager screenManager = Game.Instance.ScreenManager;
			screenManager.OnCurrentResolutionChanged = (Action)Delegate.Combine(screenManager.OnCurrentResolutionChanged, new Action(this.RefreshContainerSize));
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00007C54 File Offset: 0x00005E54
		public void SetButtonsEnabled(bool isEnabled)
		{
			this.CanvasGroup.interactable = isEnabled;
			this.CanvasGroup.blocksRaycasts = isEnabled;
			using (List<Button>.Enumerator enumerator = this._buttons.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Brush brush;
					if (enumerator.Current.TryGetComponent<Brush>(ref brush))
					{
						brush.IsDisabled = !isEnabled;
					}
				}
			}
			if (isEnabled)
			{
				this.LoadNavigationItem();
				return;
			}
			this.SaveNavigationItem();
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00007CDC File Offset: 0x00005EDC
		private void ExecuteOpenMapScreen()
		{
			Action<ScreenType> onOpenScreen = this.OnOpenScreen;
			if (onOpenScreen == null)
			{
				return;
			}
			onOpenScreen.Invoke(ScreenType.Map);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00007CEF File Offset: 0x00005EEF
		private void ExecuteOpenSoundtracksScreen()
		{
			Action<ScreenType> onOpenScreen = this.OnOpenScreen;
			if (onOpenScreen == null)
			{
				return;
			}
			onOpenScreen.Invoke(ScreenType.Soundtracks);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00007D02 File Offset: 0x00005F02
		private void ExecuteOpenBookScreen()
		{
			Action<ScreenType> onOpenScreen = this.OnOpenScreen;
			if (onOpenScreen == null)
			{
				return;
			}
			onOpenScreen.Invoke(ScreenType.Book);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00007D15 File Offset: 0x00005F15
		private void ExecuteOpenConceptArtsScreen()
		{
			Action<ScreenType> onOpenScreen = this.OnOpenScreen;
			if (onOpenScreen == null)
			{
				return;
			}
			onOpenScreen.Invoke(ScreenType.ConceptArts);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00007D28 File Offset: 0x00005F28
		private void ExecuteOpenCreditsScreen()
		{
			Action<ScreenType> onOpenScreen = this.OnOpenScreen;
			if (onOpenScreen == null)
			{
				return;
			}
			onOpenScreen.Invoke(ScreenType.Credits);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00007D3C File Offset: 0x00005F3C
		private void SaveNavigationItem()
		{
			if (EventSystem.current != null)
			{
				GameObject currentSelectedGameObject = EventSystem.current.currentSelectedGameObject;
				using (List<Button>.Enumerator enumerator = this._buttons.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.gameObject == currentSelectedGameObject)
						{
							this._savedNavigationItem = currentSelectedGameObject;
							break;
						}
					}
				}
			}
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00007DB8 File Offset: 0x00005FB8
		private void LoadNavigationItem()
		{
			bool flag = false;
			foreach (Button button in this._buttons)
			{
				if (button.gameObject == this._savedNavigationItem)
				{
					flag = true;
					EventSystem.current.SetSelectedGameObject(button.gameObject);
					break;
				}
			}
			if (!flag && this._buttons.Count > 0)
			{
				EventSystem.current.SetSelectedGameObject(this._buttons[0].gameObject);
			}
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00007E5C File Offset: 0x0000605C
		private void RefreshContainerSize()
		{
			ScreenManager screenManager = Game.Instance.ScreenManager;
			if (Vector2.Distance(this._lastCheckedResolution, screenManager.CurrentResolution) > Mathf.Epsilon)
			{
				this._lastCheckedResolution = screenManager.CurrentResolution;
				if (screenManager.CurrentResolution.x < this._defaultContainerSize.x * screenManager.ResolutionScale)
				{
					float num = screenManager.CurrentResolution.x / screenManager.ResolutionScale;
					float num2 = num * (this._defaultContainerSize.y / this._defaultContainerSize.x);
					this._rectTransform.SetSizeWithCurrentAnchors(0, num);
					this._rectTransform.SetSizeWithCurrentAnchors(1, num2);
					return;
				}
				this._rectTransform.SetSizeWithCurrentAnchors(0, this._defaultContainerSize.x);
				this._rectTransform.SetSizeWithCurrentAnchors(1, this._defaultContainerSize.y);
			}
		}

		// Token: 0x040001AC RID: 428
		public Action<ScreenType> OnOpenScreen;

		// Token: 0x040001AD RID: 429
		[SerializeField]
		private Button _openMapScreenButton;

		// Token: 0x040001AE RID: 430
		[SerializeField]
		private Button _openSoundtracksScreenButton;

		// Token: 0x040001AF RID: 431
		[SerializeField]
		private Button _openBookScreenButton;

		// Token: 0x040001B0 RID: 432
		[SerializeField]
		private Button _openConceptArtsScreenButton;

		// Token: 0x040001B1 RID: 433
		[SerializeField]
		private Button _openCreditsScreenButton;

		// Token: 0x040001B2 RID: 434
		[SerializeField]
		private Vector2 _defaultContainerSize;

		// Token: 0x040001B3 RID: 435
		private Vector2 _lastCheckedResolution = Vector2.zero;

		// Token: 0x040001B4 RID: 436
		private RectTransform _rectTransform;

		// Token: 0x040001B5 RID: 437
		private GameObject _savedNavigationItem;

		// Token: 0x040001B6 RID: 438
		private List<Button> _buttons = new List<Button>();
	}
}
