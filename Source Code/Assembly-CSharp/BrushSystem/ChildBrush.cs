using System;
using UnityEngine;

namespace TaleWorlds.CompanionBook.BrushSystem
{
	// Token: 0x02000070 RID: 112
	public class ChildBrush : BrushBase
	{
		// Token: 0x06000380 RID: 896 RVA: 0x0000D033 File Offset: 0x0000B233
		private void Awake()
		{
			base.Initialize();
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000D03B File Offset: 0x0000B23B
		private void Update()
		{
			base.UpdateTransition(Time.deltaTime);
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000D048 File Offset: 0x0000B248
		private void OnDisable()
		{
			if (this._parentBrush != null)
			{
				Brush parentBrush = this._parentBrush;
				parentBrush.OnStateChanged = (Action<BrushState>)Delegate.Remove(parentBrush.OnStateChanged, new Action<BrushState>(this.OnParentBrushStateChanged));
			}
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000D080 File Offset: 0x0000B280
		private void OnEnable()
		{
			if (this._parentBrush != null)
			{
				Brush parentBrush = this._parentBrush;
				parentBrush.OnStateChanged = (Action<BrushState>)Delegate.Combine(parentBrush.OnStateChanged, new Action<BrushState>(this.OnParentBrushStateChanged));
				this.OnParentBrushStateChanged(this._parentBrush.CurrentState);
				base.SkipTransition();
			}
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000D0D9 File Offset: 0x0000B2D9
		private void OnParentBrushStateChanged(BrushState state)
		{
			base.ChangeState(state);
		}

		// Token: 0x040002D9 RID: 729
		[SerializeField]
		private Brush _parentBrush;
	}
}
