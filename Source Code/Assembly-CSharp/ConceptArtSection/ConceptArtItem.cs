using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace TaleWorlds.CompanionBook.ConceptArtSection
{
	// Token: 0x02000067 RID: 103
	[RequireComponent(typeof(Image))]
	public class ConceptArtItem : AnimatedCarouselItem
	{
		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000337 RID: 823 RVA: 0x0000C0C1 File Offset: 0x0000A2C1
		// (set) Token: 0x06000338 RID: 824 RVA: 0x0000C0C9 File Offset: 0x0000A2C9
		public int Index { get; private set; }

		// Token: 0x06000339 RID: 825 RVA: 0x0000C0D2 File Offset: 0x0000A2D2
		private void Awake()
		{
			this._image = base.GetComponent<Image>();
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000C0E0 File Offset: 0x0000A2E0
		private void Update()
		{
			base.Update(Time.deltaTime);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000C0F0 File Offset: 0x0000A2F0
		public void Initialize(string imageName, int index)
		{
			this.Index = index;
			this._spriteHandle = Addressables.LoadAssetAsync<Sprite>("Assets/Sprites/ConceptArt/" + imageName);
			this._spriteHandle.Completed += new Action<AsyncOperationHandle<Sprite>>(this.OnSpriteLoaded);
			base.InitializeAnimationProperties(0.5f, AnimationProgressType.ExpoEaseOut);
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000C13D File Offset: 0x0000A33D
		protected override void OnBeforeDestory()
		{
			base.OnBeforeDestory();
			Addressables.Release<Sprite>(this._spriteHandle);
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000C150 File Offset: 0x0000A350
		protected override void OnAlphaChange(float newAlpha)
		{
			if (this._state != AnimatedCarouselItem.AnimatedCarouselItemState.FadingOut)
			{
				this._canvasGroup.alpha = newAlpha;
			}
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000C167 File Offset: 0x0000A367
		private void OnSpriteLoaded(AsyncOperationHandle<Sprite> handle)
		{
			if (handle.IsValid() && handle.Status == 1)
			{
				this._image.sprite = handle.Result;
				base.FadeIn();
			}
		}

		// Token: 0x0400029C RID: 668
		private Image _image;

		// Token: 0x0400029D RID: 669
		private AsyncOperationHandle<Sprite> _spriteHandle;

		// Token: 0x0400029E RID: 670
		private const float _transitionDuration = 0.5f;
	}
}
