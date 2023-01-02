using System;
using TaleWorlds.CompanionBook.FontSizeManagement;
using TaleWorlds.CompanionBook.ResourceSystem.BookResources;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.SmartFormat.PersistentVariables;
using UnityEngine.UI;

namespace TaleWorlds.CompanionBook.BookSection
{
	// Token: 0x02000071 RID: 113
	public class BookPageController : AnimatedCarouselItem
	{
		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000386 RID: 902 RVA: 0x0000D0EA File Offset: 0x0000B2EA
		// (set) Token: 0x06000387 RID: 903 RVA: 0x0000D0F2 File Offset: 0x0000B2F2
		public Chapter Chapter { get; private set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000388 RID: 904 RVA: 0x0000D0FB File Offset: 0x0000B2FB
		// (set) Token: 0x06000389 RID: 905 RVA: 0x0000D103 File Offset: 0x0000B303
		public Page Page { get; private set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600038A RID: 906 RVA: 0x0000D10C File Offset: 0x0000B30C
		// (set) Token: 0x0600038B RID: 907 RVA: 0x0000D114 File Offset: 0x0000B314
		public bool IsDisabled
		{
			get
			{
				return this._isDisabled;
			}
			set
			{
				if (value != this._isDisabled)
				{
					this._isDisabled = value;
					this.OnIsDisabledChanged(value);
				}
			}
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000D12D File Offset: 0x0000B32D
		private void Awake()
		{
			FontSizeManager fontSizeManager = Game.Instance.FontSizeManager;
			fontSizeManager.OnSelectedFontSizeChanged = (Action)Delegate.Combine(fontSizeManager.OnSelectedFontSizeChanged, new Action(this.OnFontSizeChanged));
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000D15C File Offset: 0x0000B35C
		private void Update()
		{
			base.Update(Time.deltaTime);
			if (this._state != AnimatedCarouselItem.AnimatedCarouselItemState.FadingOut && !this._voiceOverPlayTimerDone)
			{
				this._voiceOverPlayTimer += Time.deltaTime;
				if (this._voiceOverPlayTimer > 1.7f)
				{
					this._voiceOverPlayTimerDone = true;
					Action onReadyToPlayVoiceOver = this.OnReadyToPlayVoiceOver;
					if (onReadyToPlayVoiceOver == null)
					{
						return;
					}
					onReadyToPlayVoiceOver.Invoke();
				}
			}
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000D1BB File Offset: 0x0000B3BB
		private void OnDestroy()
		{
			FontSizeManager fontSizeManager = Game.Instance.FontSizeManager;
			fontSizeManager.OnSelectedFontSizeChanged = (Action)Delegate.Remove(fontSizeManager.OnSelectedFontSizeChanged, new Action(this.OnFontSizeChanged));
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000D1E8 File Offset: 0x0000B3E8
		public void Initialize(Chapter chapter, Page page, RectTransform parentRectTransform, bool onlyTransitionText)
		{
			this._illustrationSizeController.Initialize(parentRectTransform);
			this.Chapter = chapter;
			this.Page = page;
			this._image.sprite = chapter.Image;
			LocalizedString localizedString = LocalizedText.CreateLocalizedString(string.Format("travels_in_calradia_chapter_{0}_page_{1}", chapter.Index, page.Index));
			localizedString.Add("newline", new StringVariable
			{
				Value = "<br>"
			});
			this._text.SetReference(localizedString);
			if (this._title != null)
			{
				LocalizedString localizedString2 = LocalizedText.CreateLocalizedString(string.Format("travels_in_calradia_chapter_{0}_title", chapter.Index));
				localizedString2.Add("newline", new StringVariable
				{
					Value = "<br>"
				});
				this._title.SetReference(localizedString2);
			}
			this._onlyTransitionText = onlyTransitionText;
			base.InitializeAnimationProperties(0.25f, AnimationProgressType.EaseOutCirc);
			base.FadeIn();
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000D2D8 File Offset: 0x0000B4D8
		public void OnIsUIEnabledChanged(bool isEnabled)
		{
			VerticalScrollablePanelBindings verticalScrollablePanelBindings;
			if (this._scroll != null && this._scroll.TryGetComponent<VerticalScrollablePanelBindings>(ref verticalScrollablePanelBindings))
			{
				verticalScrollablePanelBindings.IsEnabled = isEnabled;
			}
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000D309 File Offset: 0x0000B509
		protected override void OnAlphaChange(float newAlpha)
		{
			if (this._state != AnimatedCarouselItem.AnimatedCarouselItemState.FadingOut && this._onlyTransitionText)
			{
				this._text.GetText().alpha = newAlpha;
				return;
			}
			this._canvasGroup.alpha = newAlpha;
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000D340 File Offset: 0x0000B540
		private void OnIsDisabledChanged(bool isDisabled)
		{
			VerticalScrollablePanelBindings verticalScrollablePanelBindings;
			if (this._scroll != null && this._scroll.TryGetComponent<VerticalScrollablePanelBindings>(ref verticalScrollablePanelBindings))
			{
				verticalScrollablePanelBindings.IsGamepadScrollDisabled = isDisabled;
			}
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000D374 File Offset: 0x0000B574
		private void OnFontSizeChanged()
		{
			ScrollRect scrollRect;
			if (this._scroll != null && this._scroll.TryGetComponent<ScrollRect>(ref scrollRect))
			{
				Vector3 localPosition = scrollRect.content.localPosition;
				localPosition.y = 0f;
				scrollRect.content.localPosition = localPosition;
				this._scroll.ResetSpeed();
			}
		}

		// Token: 0x040002DC RID: 732
		public Action OnReadyToPlayVoiceOver;

		// Token: 0x040002DD RID: 733
		[SerializeField]
		private Image _image;

		// Token: 0x040002DE RID: 734
		[SerializeField]
		private LocalizedText _title;

		// Token: 0x040002DF RID: 735
		[SerializeField]
		private LocalizedText _text;

		// Token: 0x040002E0 RID: 736
		[SerializeField]
		private VerticalScrollablePanel _scroll;

		// Token: 0x040002E1 RID: 737
		[SerializeField]
		private IllustrationSizeController _illustrationSizeController;

		// Token: 0x040002E2 RID: 738
		private const float _voiceOverPlayDelay = 1.7f;

		// Token: 0x040002E3 RID: 739
		private const float _animationDuration = 0.25f;

		// Token: 0x040002E4 RID: 740
		private float _voiceOverPlayTimer;

		// Token: 0x040002E5 RID: 741
		private bool _voiceOverPlayTimerDone;

		// Token: 0x040002E6 RID: 742
		private bool _onlyTransitionText;

		// Token: 0x040002E7 RID: 743
		private bool _isDisabled;
	}
}
