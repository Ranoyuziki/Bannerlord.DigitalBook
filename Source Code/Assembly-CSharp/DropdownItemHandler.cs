using System;
using TaleWorlds.CompanionBook.BrushSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TaleWorlds.CompanionBook
{
	// Token: 0x0200000C RID: 12
	[RequireComponent(typeof(Brush))]
	[RequireComponent(typeof(Toggle))]
	public class DropdownItemHandler : MonoBehaviour
	{
		// Token: 0x06000044 RID: 68 RVA: 0x000029C6 File Offset: 0x00000BC6
		private void Awake()
		{
			this._toggle = base.GetComponent<Toggle>();
			this._brush = base.GetComponent<Brush>();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000029E0 File Offset: 0x00000BE0
		private void OnEnable()
		{
			this._toggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged));
			this.RefreshIsSelected();
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002A04 File Offset: 0x00000C04
		private void OnDisable()
		{
			this._toggle.onValueChanged.RemoveListener(new UnityAction<bool>(this.OnValueChanged));
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002A22 File Offset: 0x00000C22
		private void RefreshIsSelected()
		{
			this._brush.IsSelected = this._toggle.isOn;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002A3A File Offset: 0x00000C3A
		private void OnValueChanged(bool isOn)
		{
			this.RefreshIsSelected();
		}

		// Token: 0x0400002E RID: 46
		private Toggle _toggle;

		// Token: 0x0400002F RID: 47
		private Brush _brush;
	}
}
