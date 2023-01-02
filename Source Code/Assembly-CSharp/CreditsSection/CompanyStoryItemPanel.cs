using System;
using TaleWorlds.CompanionBook.ResourceSystem.CreditsResources;
using UnityEngine;
using UnityEngine.UI;

namespace TaleWorlds.CompanionBook.CreditsSection
{
	// Token: 0x0200005F RID: 95
	public class CompanyStoryItemPanel : AnimatedCarouselItem
	{
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000302 RID: 770 RVA: 0x0000B1AB File Offset: 0x000093AB
		// (set) Token: 0x06000303 RID: 771 RVA: 0x0000B1B3 File Offset: 0x000093B3
		public int Index { get; private set; }

		// Token: 0x06000304 RID: 772 RVA: 0x0000B1BC File Offset: 0x000093BC
		private void Update()
		{
			float deltaTime = Time.deltaTime;
			base.Update(deltaTime);
			this.EnsureImageRatio();
			this._parallaxTimer += deltaTime;
			float num = Mathf.Clamp(this._parallaxTimer / 10f, 0f, 1f) * 0.05f;
			this._mainImage.material.SetFloat("_MainTexOffsetX", -num);
			if (this._parallaxTimer > 9f && this._state != AnimatedCarouselItem.AnimatedCarouselItemState.FadingOut)
			{
				Action onAutoEnd = this._onAutoEnd;
				if (onAutoEnd == null)
				{
					return;
				}
				onAutoEnd.Invoke();
			}
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000B24C File Offset: 0x0000944C
		public void Initialize(CompanyStoryItem storyItem, int index, Action onAutoEnd)
		{
			this.Index = index;
			this._onAutoEnd = onAutoEnd;
			this._mainImage.sprite = storyItem.MainImage;
			Material material = new Material(this._shader);
			material.SetTexture("_MaskTex", this.GetMask(storyItem.Size).texture);
			this._mainImage.material = material;
			base.InitializeAnimationProperties(1f, AnimationProgressType.ExpoEaseOut);
			base.FadeIn();
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000B2BE File Offset: 0x000094BE
		protected override void OnAlphaChange(float newAlpha)
		{
			this._canvasGroup.alpha = newAlpha;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000B2CC File Offset: 0x000094CC
		private Sprite GetMask(CompanyStoryItemSize size)
		{
			Credits creditsResources = Game.Instance.ResourceProvider.CreditsResources;
			switch (size)
			{
			case CompanyStoryItemSize.Small:
				return creditsResources.SmallMask;
			case CompanyStoryItemSize.Medium:
				return creditsResources.MediumMask;
			case CompanyStoryItemSize.Large:
				return creditsResources.LargeMask;
			default:
				return creditsResources.SmallMask;
			}
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000B318 File Offset: 0x00009518
		private void EnsureImageRatio()
		{
			if (Mathf.Abs(this._mainImageParent.rect.width - this._lastCheckedImageWidth) > Mathf.Epsilon)
			{
				this._lastCheckedImageWidth = this._mainImageParent.rect.width;
				LayoutElement layoutElement;
				if (this._mainImageParent.TryGetComponent<LayoutElement>(ref layoutElement))
				{
					layoutElement.preferredHeight = this._lastCheckedImageWidth / 1.7777778f;
				}
			}
		}

		// Token: 0x0400025B RID: 603
		[SerializeField]
		private RectTransform _mainImageParent;

		// Token: 0x0400025C RID: 604
		[SerializeField]
		private Image _mainImage;

		// Token: 0x0400025D RID: 605
		[SerializeField]
		private Shader _shader;

		// Token: 0x0400025E RID: 606
		private Action _onAutoEnd;

		// Token: 0x0400025F RID: 607
		private const float _transitionDuration = 1f;

		// Token: 0x04000260 RID: 608
		private float _parallaxTimer;

		// Token: 0x04000261 RID: 609
		private const float _parallaxDuration = 10f;

		// Token: 0x04000262 RID: 610
		private const float _parallaxMaxOffset = 0.05f;

		// Token: 0x04000263 RID: 611
		private float _lastCheckedImageWidth;

		// Token: 0x04000264 RID: 612
		private const float _imageWidthHeightRatio = 1.7777778f;
	}
}
