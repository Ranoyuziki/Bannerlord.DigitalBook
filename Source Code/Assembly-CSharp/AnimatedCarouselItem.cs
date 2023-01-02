using System;
using UnityEngine;

namespace TaleWorlds.CompanionBook
{
	// Token: 0x02000002 RID: 2
	[RequireComponent(typeof(CanvasGroup))]
	public abstract class AnimatedCarouselItem : MonoBehaviour
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public void OnFinalize()
		{
			this._state = AnimatedCarouselItem.AnimatedCarouselItemState.FadingOut;
			this._animationProgress = new AnimationProgress(this._transitionDuration, this._animationProgressType);
			this._transitionBeginAlpha = this._canvasGroup.alpha;
			this._transitionEndAlpha = 0f;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000208C File Offset: 0x0000028C
		protected void FadeIn()
		{
			if (this._state != AnimatedCarouselItem.AnimatedCarouselItemState.FadingOut)
			{
				this._state = AnimatedCarouselItem.AnimatedCarouselItemState.FadingIn;
				this._animationProgress = new AnimationProgress(this._transitionDuration, this._animationProgressType);
				this._transitionBeginAlpha = 0f;
				this._transitionEndAlpha = 1f;
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020CB File Offset: 0x000002CB
		protected void InitializeAnimationProperties(float transitionDuration, AnimationProgressType animationProgressType = AnimationProgressType.ExpoEaseOut)
		{
			this._canvasGroup = base.GetComponent<CanvasGroup>();
			this._transitionDuration = transitionDuration;
			this._animationProgressType = animationProgressType;
			this.OnAlphaChange(0f);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020F4 File Offset: 0x000002F4
		protected void Update(float dt)
		{
			if (this._state == AnimatedCarouselItem.AnimatedCarouselItemState.FadingIn || this._state == AnimatedCarouselItem.AnimatedCarouselItemState.FadingOut)
			{
				this._animationProgress.Progress(dt);
				bool flag = this._state == AnimatedCarouselItem.AnimatedCarouselItemState.FadingOut;
				if (this._animationProgress.IsFinished)
				{
					this.OnAlphaChange(this._transitionEndAlpha);
					if (flag)
					{
						this.OnBeforeDestory();
						Object.Destroy(base.gameObject);
						return;
					}
					this._state = AnimatedCarouselItem.AnimatedCarouselItemState.None;
					return;
				}
				else
				{
					float newAlpha = Mathf.Lerp(this._transitionBeginAlpha, this._transitionEndAlpha, this._animationProgress.Progression);
					this.OnAlphaChange(newAlpha);
				}
			}
		}

		// Token: 0x06000005 RID: 5
		protected abstract void OnAlphaChange(float newAlpha);

		// Token: 0x06000006 RID: 6 RVA: 0x00002183 File Offset: 0x00000383
		protected virtual void OnBeforeDestory()
		{
		}

		// Token: 0x04000001 RID: 1
		protected CanvasGroup _canvasGroup;

		// Token: 0x04000002 RID: 2
		protected AnimatedCarouselItem.AnimatedCarouselItemState _state;

		// Token: 0x04000003 RID: 3
		private float _transitionBeginAlpha;

		// Token: 0x04000004 RID: 4
		private float _transitionEndAlpha = 1f;

		// Token: 0x04000005 RID: 5
		private float _transitionDuration = 0.5f;

		// Token: 0x04000006 RID: 6
		private AnimationProgressType _animationProgressType;

		// Token: 0x04000007 RID: 7
		private AnimationProgress _animationProgress = new AnimationProgress(0f);

		// Token: 0x02000080 RID: 128
		protected enum AnimatedCarouselItemState
		{
			// Token: 0x04000364 RID: 868
			None,
			// Token: 0x04000365 RID: 869
			FadingIn,
			// Token: 0x04000366 RID: 870
			FadingOut
		}
	}
}
