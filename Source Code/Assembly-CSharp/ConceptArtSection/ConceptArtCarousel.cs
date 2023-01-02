using System;
using System.Collections.Generic;
using TaleWorlds.CompanionBook.ResourceSystem.ConceptArtResources;
using TaleWorlds.CompanionBook.ScreenSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace TaleWorlds.CompanionBook.ConceptArtSection
{
	// Token: 0x02000066 RID: 102
	public class ConceptArtCarousel : MonoBehaviour
	{
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000328 RID: 808 RVA: 0x0000BAE1 File Offset: 0x00009CE1
		public bool IsControlsHidden
		{
			get
			{
				AutoEndingAnimationHandler controlsAnimationHandler = this._controlsAnimationHandler;
				return controlsAnimationHandler != null && controlsAnimationHandler.IsIdle;
			}
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000BAF4 File Offset: 0x00009CF4
		private void Awake()
		{
			this._controlsAnimationHandler = new AutoEndingAnimationHandler(6f, 0.2f, new Action<float>(this.OnControlsFadeOutProgress));
			ConceptArts conceptArtsResources = Game.Instance.ResourceProvider.ConceptArtsResources;
			List<string> list = new List<string>();
			Dictionary<ConceptArtFactionType, int> dictionary = new Dictionary<ConceptArtFactionType, int>();
			foreach (ConceptArtFactionType conceptArtFactionType in (ConceptArtFactionType[])Enum.GetValues(typeof(ConceptArtFactionType)))
			{
				dictionary.Add(conceptArtFactionType, list.Count);
				ConceptArtFaction conceptArtFaction;
				if (conceptArtsResources.Factions.TryGetValue(conceptArtFactionType, ref conceptArtFaction))
				{
					foreach (string text in conceptArtFaction.ImageNames)
					{
						list.Add(text);
					}
				}
			}
			this._allImageNames = list;
			this._factionIndices = dictionary;
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000BBE0 File Offset: 0x00009DE0
		private void OnEnable()
		{
			this._previousButton.onClick.AddListener(new UnityAction(this.ExecuteShowPrevious));
			this._nextButton.onClick.AddListener(new UnityAction(this.ExecuteShowNext));
			this._backButton.onClick.AddListener(new UnityAction(this.ExecuteBack));
			ScreenManager screenManager = Game.Instance.ScreenManager;
			screenManager.OnCurrentResolutionChanged = (Action)Delegate.Combine(screenManager.OnCurrentResolutionChanged, new Action(this.RefreshImageSize));
			this.RefreshImageSize();
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000BC74 File Offset: 0x00009E74
		private void OnDisable()
		{
			this._previousButton.onClick.RemoveListener(new UnityAction(this.ExecuteShowPrevious));
			this._nextButton.onClick.RemoveListener(new UnityAction(this.ExecuteShowNext));
			this._backButton.onClick.RemoveListener(new UnityAction(this.ExecuteBack));
			ScreenManager screenManager = Game.Instance.ScreenManager;
			screenManager.OnCurrentResolutionChanged = (Action)Delegate.Remove(screenManager.OnCurrentResolutionChanged, new Action(this.RefreshImageSize));
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000BD00 File Offset: 0x00009F00
		private void Update()
		{
			if (!Game.Instance.IsCurrentSchemeGamepad)
			{
				Vector2 vector = Mouse.current.position.ReadValue();
				bool flag = Vector2.Distance(this._lastCheckedMousePosition, vector) > Mathf.Epsilon;
				this._lastCheckedMousePosition = vector;
				if (flag)
				{
					this.DisplayControls();
				}
			}
			this._controlsAnimationHandler.Update(Time.deltaTime);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000BD5B File Offset: 0x00009F5B
		public void OnShow(ConceptArtFactionType faction)
		{
			this.TryChangeIndex(this._factionIndices[faction], true);
			this.DisplayControls();
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000BD78 File Offset: 0x00009F78
		public void ExecuteShowNext()
		{
			if (this._currentItem != null)
			{
				this.TryChangeIndex((this._currentItem.Index == this._allImageNames.Count - 1) ? 0 : (this._currentItem.Index + 1), false);
			}
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000BDC8 File Offset: 0x00009FC8
		public void ExecuteShowPrevious()
		{
			if (this._currentItem != null)
			{
				this.TryChangeIndex((this._currentItem.Index == 0) ? (this._allImageNames.Count - 1) : (this._currentItem.Index - 1), false);
			}
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000BE14 File Offset: 0x0000A014
		public void DisplayControls()
		{
			this._controlsCanvasGroup.alpha = 1f;
			this._controlsAnimationHandler.WakeUp();
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000BE31 File Offset: 0x0000A031
		private void ExecuteBack()
		{
			Action onBack = this.OnBack;
			if (onBack == null)
			{
				return;
			}
			onBack.Invoke();
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000BE44 File Offset: 0x0000A044
		private bool TryChangeIndex(int index, bool endCurrentImmediately = false)
		{
			bool flag = index >= 0 && index < this._allImageNames.Count;
			if (flag)
			{
				if (this._currentItem != null)
				{
					if (endCurrentImmediately)
					{
						Object.Destroy(this._currentItem.gameObject);
						this._currentItem = null;
					}
					else
					{
						this._currentItem.OnFinalize();
					}
				}
				this._currentItem = Object.Instantiate<GameObject>(this._conceptArtItemPrefab, this._conceptArtItemParent.transform).GetComponent<ConceptArtItem>();
				this._currentItem.Initialize(this._allImageNames[index], index);
				this.RefreshDescription();
				Game.Instance.AudioManager.PlayAudioClip(this._changeArtworkAudioClip);
			}
			return flag;
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000BEF8 File Offset: 0x0000A0F8
		private void RefreshDescription()
		{
			bool flag = false;
			if (this._currentItem != null)
			{
				foreach (KeyValuePair<ConceptArtFactionType, int> keyValuePair in this._factionIndices)
				{
					if (keyValuePair.Value == this._currentItem.Index)
					{
						this._factionDescription.gameObject.SetActive(true);
						this._factionDescription.OnFactionChanged(keyValuePair.Key);
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				this._factionDescription.gameObject.SetActive(false);
			}
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000BFA0 File Offset: 0x0000A1A0
		private void OnControlsFadeOutProgress(float progress)
		{
			this._controlsCanvasGroup.alpha = 1f - progress;
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000BFB4 File Offset: 0x0000A1B4
		private void RefreshImageSize()
		{
			ScreenManager screenManager = Game.Instance.ScreenManager;
			if (Vector2.Distance(this._lastCheckedResolution, screenManager.CurrentResolution) > Mathf.Epsilon)
			{
				this._lastCheckedResolution = screenManager.CurrentResolution;
				float num = screenManager.ReferenceResolution.x / screenManager.ReferenceResolution.y;
				if (screenManager.CurrentResolution.x / screenManager.CurrentResolution.y < num - Mathf.Epsilon)
				{
					float num2 = screenManager.CurrentResolution.x / screenManager.ResolutionScale;
					float num3 = num2 / num;
					this._conceptArtItemParent.SetSizeWithCurrentAnchors(0, num2);
					this._conceptArtItemParent.SetSizeWithCurrentAnchors(1, num3);
					return;
				}
				this._conceptArtItemParent.SetSizeWithCurrentAnchors(0, screenManager.ReferenceResolution.x);
				this._conceptArtItemParent.SetSizeWithCurrentAnchors(1, screenManager.ReferenceResolution.y);
			}
		}

		// Token: 0x0400028A RID: 650
		public Action OnBack;

		// Token: 0x0400028B RID: 651
		[SerializeField]
		private RectTransform _conceptArtItemParent;

		// Token: 0x0400028C RID: 652
		[SerializeField]
		private GameObject _conceptArtItemPrefab;

		// Token: 0x0400028D RID: 653
		[SerializeField]
		private CanvasGroup _controlsCanvasGroup;

		// Token: 0x0400028E RID: 654
		[SerializeField]
		private Button _previousButton;

		// Token: 0x0400028F RID: 655
		[SerializeField]
		private Button _nextButton;

		// Token: 0x04000290 RID: 656
		[SerializeField]
		private Button _backButton;

		// Token: 0x04000291 RID: 657
		[SerializeField]
		private AudioClip _changeArtworkAudioClip;

		// Token: 0x04000292 RID: 658
		[SerializeField]
		private FactionDescription _factionDescription;

		// Token: 0x04000293 RID: 659
		private ConceptArtItem _currentItem;

		// Token: 0x04000294 RID: 660
		private IReadOnlyList<string> _allImageNames = new List<string>();

		// Token: 0x04000295 RID: 661
		private IReadOnlyDictionary<ConceptArtFactionType, int> _factionIndices = new Dictionary<ConceptArtFactionType, int>();

		// Token: 0x04000296 RID: 662
		private Vector2 _lastCheckedResolution = Vector2.zero;

		// Token: 0x04000297 RID: 663
		private Vector2 _lastCheckedMousePosition = Vector2.zero;

		// Token: 0x04000298 RID: 664
		private AutoEndingAnimationHandler _controlsAnimationHandler;

		// Token: 0x04000299 RID: 665
		private const float _controlsStayDuration = 6f;

		// Token: 0x0400029A RID: 666
		private const float _controlsFadeOutTime = 0.2f;
	}
}
