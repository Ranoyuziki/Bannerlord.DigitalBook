using System;

namespace TaleWorlds.CompanionBook
{
	// Token: 0x02000005 RID: 5
	public class AutoEndingAnimationHandler
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000016 RID: 22 RVA: 0x0000243A File Offset: 0x0000063A
		public bool IsIdle
		{
			get
			{
				return this._state == AutoEndingAnimationHandler.AnimationState.Idle;
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002445 File Offset: 0x00000645
		public AutoEndingAnimationHandler(float awakeDuration, float fadeOutDuration, Action<float> onFadeOutProgress)
		{
			this._awakeDuration = awakeDuration;
			this._fadeOutDuration = fadeOutDuration;
			this._onFadeOutProgress = onFadeOutProgress;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002464 File Offset: 0x00000664
		public void Update(float dt)
		{
			if (this._state == AutoEndingAnimationHandler.AnimationState.Awake)
			{
				this._awakeTimer += dt;
				if (this._awakeTimer > this._awakeDuration)
				{
					this._state = AutoEndingAnimationHandler.AnimationState.FadingOut;
					this._animation = new AnimationProgress(this._fadeOutDuration, AnimationProgressType.EaseOutCubic);
					return;
				}
			}
			else if (this._state == AutoEndingAnimationHandler.AnimationState.FadingOut)
			{
				this._animation.Progress(dt);
				Action<float> onFadeOutProgress = this._onFadeOutProgress;
				if (onFadeOutProgress != null)
				{
					onFadeOutProgress.Invoke(this._animation.Progression);
				}
				if (this._animation.IsFinished)
				{
					this._state = AutoEndingAnimationHandler.AnimationState.Idle;
				}
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000024F5 File Offset: 0x000006F5
		public void WakeUp()
		{
			this._state = AutoEndingAnimationHandler.AnimationState.Awake;
			this._awakeTimer = 0f;
		}

		// Token: 0x04000015 RID: 21
		private Action<float> _onFadeOutProgress;

		// Token: 0x04000016 RID: 22
		private AnimationProgress _animation;

		// Token: 0x04000017 RID: 23
		private AutoEndingAnimationHandler.AnimationState _state;

		// Token: 0x04000018 RID: 24
		private float _awakeTimer;

		// Token: 0x04000019 RID: 25
		private readonly float _fadeOutDuration;

		// Token: 0x0400001A RID: 26
		private readonly float _awakeDuration;

		// Token: 0x02000081 RID: 129
		private enum AnimationState
		{
			// Token: 0x04000368 RID: 872
			Idle,
			// Token: 0x04000369 RID: 873
			FadingOut,
			// Token: 0x0400036A RID: 874
			Awake
		}
	}
}
