using System;
using UnityEngine;

namespace TaleWorlds.CompanionBook
{
	// Token: 0x02000003 RID: 3
	public class AnimationProgress
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000021B3 File Offset: 0x000003B3
		// (set) Token: 0x06000009 RID: 9 RVA: 0x000021BB File Offset: 0x000003BB
		public bool IsFinished { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000021C4 File Offset: 0x000003C4
		// (set) Token: 0x0600000B RID: 11 RVA: 0x000021CC File Offset: 0x000003CC
		public float Progression { get; private set; }

		// Token: 0x0600000C RID: 12 RVA: 0x000021D5 File Offset: 0x000003D5
		public AnimationProgress(float duration) : this(duration, AnimationProgressType.Linear)
		{
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021E0 File Offset: 0x000003E0
		public AnimationProgress(float duration, AnimationProgressType type)
		{
			if (duration > Mathf.Epsilon)
			{
				this._inverseDuration = 1f / duration;
				this._rawProgression = 0f;
				this.Progression = 0f;
				this.IsFinished = false;
				this._easing = AnimationProgress.GetEasing(type);
				return;
			}
			this.Progression = 1f;
			this.IsFinished = true;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002248 File Offset: 0x00000448
		public void Progress(float dt)
		{
			if (!this.IsFinished)
			{
				this._rawProgression += dt * this._inverseDuration;
				if (this._rawProgression >= 1f)
				{
					this.IsFinished = true;
					this._rawProgression = 1f;
					this.Progression = 1f;
					return;
				}
				this.Progression = ((this._easing != null) ? this._easing.Invoke(this._rawProgression) : this._rawProgression);
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022C4 File Offset: 0x000004C4
		private static float GetExpoEaseOut(float t)
		{
			if (t < 1f)
			{
				return 1f - Mathf.Pow(2f, -10f * t);
			}
			return 1f;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022EC File Offset: 0x000004EC
		private static float GetEaseInOutBack(float t)
		{
			if (t >= 0.5f)
			{
				return (Mathf.Pow(2f * t - 2f, 2f) * (2.8f * (t * 2f - 2f) + 1.8f) + 2f) / 2f;
			}
			return Mathf.Pow(2f * t, 2f) * (5.6f * t - 1.8f) / 2f;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002364 File Offset: 0x00000564
		private static float GetEaseOutCirc(float t)
		{
			return Mathf.Sqrt(1f - Mathf.Pow(t - 1f, 2f));
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002382 File Offset: 0x00000582
		private static float GetEaseOutCubic(float t)
		{
			return 1f - Mathf.Pow(1f - t, 3f);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000239B File Offset: 0x0000059B
		private static float GetEaseInCubic(float t)
		{
			return t * t * t;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000023A2 File Offset: 0x000005A2
		private static float GetEaseOutQuint(float t)
		{
			return 1f - Mathf.Pow(1f - t, 5f);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000023BC File Offset: 0x000005BC
		private static Func<float, float> GetEasing(AnimationProgressType type)
		{
			switch (type)
			{
			case AnimationProgressType.ExpoEaseOut:
				return new Func<float, float>(AnimationProgress.GetExpoEaseOut);
			case AnimationProgressType.EaseOutCirc:
				return new Func<float, float>(AnimationProgress.GetEaseOutCirc);
			case AnimationProgressType.EaseInOutBack:
				return new Func<float, float>(AnimationProgress.GetEaseInOutBack);
			case AnimationProgressType.EaseOutCubic:
				return new Func<float, float>(AnimationProgress.GetEaseOutCubic);
			case AnimationProgressType.EaseInCubic:
				return new Func<float, float>(AnimationProgress.GetEaseInCubic);
			case AnimationProgressType.EaseOutQuint:
				return new Func<float, float>(AnimationProgress.GetEaseOutQuint);
			default:
				return null;
			}
		}

		// Token: 0x0400000A RID: 10
		private float _rawProgression;

		// Token: 0x0400000B RID: 11
		private readonly float _inverseDuration;

		// Token: 0x0400000C RID: 12
		private readonly Func<float, float> _easing;
	}
}
