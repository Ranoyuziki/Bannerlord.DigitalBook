using System;
using UnityEngine;

namespace TaleWorlds.CompanionBook.BookSection
{
	// Token: 0x0200007A RID: 122
	public class UIModeHandler : MonoBehaviour
	{
		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x0000EFB1 File Offset: 0x0000D1B1
		public bool IsUIEnabled
		{
			get
			{
				return this._isUIEnabled;
			}
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0000EFBC File Offset: 0x0000D1BC
		private void Update()
		{
			if (this._transitionState == UIModeHandler.UIModeTransitionState.UITransition)
			{
				if (this._animation == null)
				{
					this._animationBeginAlpha = this._UIControlsCanvasGroup.alpha;
					this._animation = new AnimationProgress(0.25f, this._isUIEnabled ? AnimationProgressType.EaseInCubic : AnimationProgressType.EaseOutCubic);
				}
				this._animation.Progress(Time.deltaTime);
				float num = this._isUIEnabled ? 1f : 0f;
				float alpha = Mathf.Lerp(this._animationBeginAlpha, num, this._animation.Progression);
				this._pageCanvasGroup.alpha = alpha;
				this._UIControlsCanvasGroup.alpha = alpha;
				if (this._animation.IsFinished)
				{
					this._animation = null;
					this._transitionState = (this._isUIEnabled ? UIModeHandler.UIModeTransitionState.None : UIModeHandler.UIModeTransitionState.ResizingPage);
					return;
				}
			}
			else if (this._transitionState == UIModeHandler.UIModeTransitionState.ResizingPage)
			{
				if (this._animation == null)
				{
					this.UpdateMargins(default(Vector4), this._isUIEnabled ? this._UIEnabledMargins : this._UIDisabledMargins, 1f);
					this._animation = new AnimationProgress(0f);
				}
				this._animation.Progress(Time.deltaTime);
				if (this._animation.IsFinished)
				{
					this._animation = null;
					this._transitionState = (this._isUIEnabled ? UIModeHandler.UIModeTransitionState.UITransition : UIModeHandler.UIModeTransitionState.NoUITransition);
					return;
				}
			}
			else if (this._transitionState == UIModeHandler.UIModeTransitionState.NoUITransition)
			{
				if (this._animation == null)
				{
					this._animationBeginAlpha = this._pageCanvasGroup.alpha;
					this._animation = new AnimationProgress(0.25f, this._isUIEnabled ? AnimationProgressType.EaseOutCubic : AnimationProgressType.EaseInCubic);
				}
				this._animation.Progress(Time.deltaTime);
				float num2 = this._isUIEnabled ? 0f : 1f;
				float alpha2 = Mathf.Lerp(this._animationBeginAlpha, num2, this._animation.Progression);
				this._pageCanvasGroup.alpha = alpha2;
				if (this._animation.IsFinished)
				{
					this._animation = null;
					this._transitionState = (this._isUIEnabled ? UIModeHandler.UIModeTransitionState.ResizingPage : UIModeHandler.UIModeTransitionState.None);
				}
			}
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0000F1C0 File Offset: 0x0000D3C0
		public void SetUIEnabled(bool isUIEnabled, bool skipTransition = false)
		{
			if (this._isUIEnabled != isUIEnabled)
			{
				this._animation = null;
				this._isUIEnabled = isUIEnabled;
				this._UIControlsCanvasGroup.interactable = isUIEnabled;
				this._UIControlsCanvasGroup.blocksRaycasts = isUIEnabled;
				if (skipTransition)
				{
					this._transitionState = UIModeHandler.UIModeTransitionState.None;
					this._UIControlsCanvasGroup.alpha = (isUIEnabled ? 1f : 0f);
					this._pageCanvasGroup.alpha = 1f;
					this.UpdateMargins(default(Vector4), isUIEnabled ? this._UIEnabledMargins : this._UIDisabledMargins, 1f);
				}
				else if (this._transitionState == UIModeHandler.UIModeTransitionState.None)
				{
					this._transitionState = (this._isUIEnabled ? UIModeHandler.UIModeTransitionState.NoUITransition : UIModeHandler.UIModeTransitionState.UITransition);
				}
				Action onIsUIEnabledChanged = this.OnIsUIEnabledChanged;
				if (onIsUIEnabledChanged == null)
				{
					return;
				}
				onIsUIEnabledChanged.Invoke();
			}
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0000F284 File Offset: 0x0000D484
		private void UpdateMargins(Vector4 fromMargins, Vector4 toMargins, float transitionProgress)
		{
			Vector4 vector = Vector4.Lerp(fromMargins, toMargins, transitionProgress);
			this._pageParentRectTransform.offsetMin = new Vector2(vector.y, vector.x);
			this._pageParentRectTransform.offsetMax = new Vector2(-vector.w, -vector.z);
		}

		// Token: 0x0400033E RID: 830
		public Action OnIsUIEnabledChanged;

		// Token: 0x0400033F RID: 831
		[SerializeField]
		private Vector4 _UIEnabledMargins;

		// Token: 0x04000340 RID: 832
		[SerializeField]
		private Vector4 _UIDisabledMargins;

		// Token: 0x04000341 RID: 833
		[SerializeField]
		private RectTransform _pageParentRectTransform;

		// Token: 0x04000342 RID: 834
		[SerializeField]
		private CanvasGroup _pageCanvasGroup;

		// Token: 0x04000343 RID: 835
		[SerializeField]
		private CanvasGroup _UIControlsCanvasGroup;

		// Token: 0x04000344 RID: 836
		private UIModeHandler.UIModeTransitionState _transitionState;

		// Token: 0x04000345 RID: 837
		private float _animationBeginAlpha;

		// Token: 0x04000346 RID: 838
		private AnimationProgress _animation;

		// Token: 0x04000347 RID: 839
		private const float _transitionAnimationDuration = 0.25f;

		// Token: 0x04000348 RID: 840
		private const float _resizeAnimationDuration = 0f;

		// Token: 0x04000349 RID: 841
		private bool _isUIEnabled = true;

		// Token: 0x020000BF RID: 191
		private enum UIModeTransitionState
		{
			// Token: 0x0400040A RID: 1034
			None,
			// Token: 0x0400040B RID: 1035
			UITransition,
			// Token: 0x0400040C RID: 1036
			NoUITransition,
			// Token: 0x0400040D RID: 1037
			ResizingPage
		}
	}
}
