using System;
using UnityEngine;

namespace TaleWorlds.CompanionBook
{
	// Token: 0x0200001D RID: 29
	public class VerticalScrollablePanelBindings : MonoBehaviour
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00004EAA File Offset: 0x000030AA
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x00004EB2 File Offset: 0x000030B2
		public bool IsEnabled
		{
			get
			{
				return this._isEnabled;
			}
			set
			{
				if (value != this._isEnabled)
				{
					this._isEnabled = value;
					this.Refresh();
				}
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00004ECA File Offset: 0x000030CA
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x00004ED2 File Offset: 0x000030D2
		public bool CanScroll
		{
			get
			{
				return this._canScroll;
			}
			set
			{
				if (value != this._canScroll)
				{
					this._canScroll = value;
					this.Refresh();
				}
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004EEA File Offset: 0x000030EA
		private void Awake()
		{
			this.Refresh();
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00004EF4 File Offset: 0x000030F4
		private void Refresh()
		{
			bool flag = this.CanScroll && this.IsEnabled;
			if (this._scrollUpBindingCanvasGroup != null && this._scrollDownBindingCanvasGroup != null)
			{
				float alpha = flag ? 1f : 0f;
				this._scrollUpBindingCanvasGroup.alpha = alpha;
				this._scrollDownBindingCanvasGroup.alpha = alpha;
			}
		}

		// Token: 0x040000CA RID: 202
		public bool IsGamepadScrollDisabled;

		// Token: 0x040000CB RID: 203
		[SerializeField]
		private CanvasGroup _scrollUpBindingCanvasGroup;

		// Token: 0x040000CC RID: 204
		[SerializeField]
		private CanvasGroup _scrollDownBindingCanvasGroup;

		// Token: 0x040000CD RID: 205
		private bool _isEnabled;

		// Token: 0x040000CE RID: 206
		private bool _canScroll;
	}
}
