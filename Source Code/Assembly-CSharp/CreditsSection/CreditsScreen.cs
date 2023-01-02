using System;
using TaleWorlds.CompanionBook.ResourceSystem.CreditsResources;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TaleWorlds.CompanionBook.CreditsSection
{
	// Token: 0x02000065 RID: 101
	public class CreditsScreen : MonoBehaviour
	{
		// Token: 0x0600031D RID: 797 RVA: 0x0000B7C0 File Offset: 0x000099C0
		private void Awake()
		{
			PlayerActions.CreditsActionsActions creditsActions = Game.Instance.PlayerActions.CreditsActions;
			this._previousStoryItemAction = creditsActions.PreviousStoryItemAction;
			this._nextStoryItemAction = creditsActions.NextStoryItemAction;
			this._credits = Game.Instance.ResourceProvider.CreditsResources;
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000B80C File Offset: 0x00009A0C
		private void OnEnable()
		{
			CompanyStoryNavigationControls storyNavigationControls = this._storyNavigationControls;
			storyNavigationControls.OnShowPrevious = (Action)Delegate.Combine(storyNavigationControls.OnShowPrevious, new Action(this.ExecuteOpenPreviousStoryItem));
			CompanyStoryNavigationControls storyNavigationControls2 = this._storyNavigationControls;
			storyNavigationControls2.OnShowNext = (Action)Delegate.Combine(storyNavigationControls2.OnShowNext, new Action(this.ExecuteOpenNextStoryItem));
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000B868 File Offset: 0x00009A68
		private void OnDisable()
		{
			CompanyStoryNavigationControls storyNavigationControls = this._storyNavigationControls;
			storyNavigationControls.OnShowPrevious = (Action)Delegate.Remove(storyNavigationControls.OnShowPrevious, new Action(this.ExecuteOpenPreviousStoryItem));
			CompanyStoryNavigationControls storyNavigationControls2 = this._storyNavigationControls;
			storyNavigationControls2.OnShowNext = (Action)Delegate.Remove(storyNavigationControls2.OnShowNext, new Action(this.ExecuteOpenNextStoryItem));
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000B8C3 File Offset: 0x00009AC3
		private void Start()
		{
			this.OpenStoryItem(0);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000B8CC File Offset: 0x00009ACC
		private void Update()
		{
			this.HandleThankYouTextAnimation(Time.deltaTime);
			if (this._previousStoryItemAction.WasPressedThisFrame())
			{
				this.ExecuteOpenPreviousStoryItem();
			}
			else if (this._nextStoryItemAction.WasPressedThisFrame())
			{
				this.ExecuteOpenNextStoryItem();
			}
			this._backToMenuButton.CheckActions();
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000B90C File Offset: 0x00009B0C
		private void ExecuteOpenPreviousStoryItem()
		{
			if (this._currentStoryItem != null)
			{
				int index = (this._currentStoryItem.Index > 0) ? (this._currentStoryItem.Index - 1) : (this._credits.CompanyStoryItems.Count - 1);
				this.OpenStoryItem(index);
			}
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000B960 File Offset: 0x00009B60
		private void ExecuteOpenNextStoryItem()
		{
			if (this._currentStoryItem != null)
			{
				int index = (this._currentStoryItem.Index < this._credits.CompanyStoryItems.Count - 1) ? (this._currentStoryItem.Index + 1) : 0;
				this.OpenStoryItem(index);
			}
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000B9B4 File Offset: 0x00009BB4
		private void OpenStoryItem(int index)
		{
			if (this._currentStoryItem != null)
			{
				this._currentStoryItem.OnFinalize();
			}
			if (index >= 0 && index < this._credits.CompanyStoryItems.Count)
			{
				this._currentStoryItem = Object.Instantiate<GameObject>(this._storyItemPrefab, this._storyItemParent.transform).GetComponent<CompanyStoryItemPanel>();
				this._currentStoryItem.Initialize(this._credits.CompanyStoryItems[index], index, new Action(this.ExecuteOpenNextStoryItem));
				if (index == this._credits.CompanyStoryItems.Count - 1)
				{
					this.ShowThankYouText();
				}
			}
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000BA58 File Offset: 0x00009C58
		private void HandleThankYouTextAnimation(float dt)
		{
			if (this._thankYouAnimation != null)
			{
				this._thankYouAnimation.Progress(dt);
				Color color = this._thankYouText.color;
				color.a = this._thankYouAnimation.Progression;
				this._thankYouText.color = color;
				if (this._thankYouAnimation.IsFinished)
				{
					this._thankYouAnimation = null;
				}
			}
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000BAB7 File Offset: 0x00009CB7
		private void ShowThankYouText()
		{
			if (!this._isThankYouShown)
			{
				this._isThankYouShown = true;
				this._thankYouAnimation = new AnimationProgress(1f, AnimationProgressType.EaseInCubic);
			}
		}

		// Token: 0x0400027E RID: 638
		[SerializeField]
		private GameObject _storyItemParent;

		// Token: 0x0400027F RID: 639
		[SerializeField]
		private GameObject _storyItemPrefab;

		// Token: 0x04000280 RID: 640
		[SerializeField]
		private BackToMenuButton _backToMenuButton;

		// Token: 0x04000281 RID: 641
		[SerializeField]
		private CompanyStoryNavigationControls _storyNavigationControls;

		// Token: 0x04000282 RID: 642
		[SerializeField]
		private TMP_Text _thankYouText;

		// Token: 0x04000283 RID: 643
		private InputAction _previousStoryItemAction;

		// Token: 0x04000284 RID: 644
		private InputAction _nextStoryItemAction;

		// Token: 0x04000285 RID: 645
		private Credits _credits;

		// Token: 0x04000286 RID: 646
		private CompanyStoryItemPanel _currentStoryItem;

		// Token: 0x04000287 RID: 647
		private bool _isThankYouShown;

		// Token: 0x04000288 RID: 648
		private AnimationProgress _thankYouAnimation;

		// Token: 0x04000289 RID: 649
		private const float _thankYouAnimationDuration = 1f;
	}
}
