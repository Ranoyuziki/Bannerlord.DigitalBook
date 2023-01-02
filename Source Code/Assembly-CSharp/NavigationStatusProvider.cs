using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TaleWorlds.CompanionBook
{
	// Token: 0x02000016 RID: 22
	public class NavigationStatusProvider
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00003DBB File Offset: 0x00001FBB
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x00003DC3 File Offset: 0x00001FC3
		public bool IsOnDropdown { get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00003DCC File Offset: 0x00001FCC
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x00003DD4 File Offset: 0x00001FD4
		public bool IsOnDropdownItem { get; private set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00003DDD File Offset: 0x00001FDD
		// (set) Token: 0x060000AA RID: 170 RVA: 0x00003DE5 File Offset: 0x00001FE5
		public bool WasOnDropdownPrevFrame { get; private set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00003DEE File Offset: 0x00001FEE
		// (set) Token: 0x060000AC RID: 172 RVA: 0x00003DF6 File Offset: 0x00001FF6
		public bool WasOnDropdownItemPrevFrame { get; private set; }

		// Token: 0x060000AD RID: 173 RVA: 0x00003E00 File Offset: 0x00002000
		public void Update()
		{
			this.WasOnDropdownPrevFrame = this.IsOnDropdown;
			this.WasOnDropdownItemPrevFrame = this.IsOnDropdownItem;
			if (this._lastNavigatedItem != EventSystem.current.currentSelectedGameObject)
			{
				this._lastNavigatedItem = EventSystem.current.currentSelectedGameObject;
				this.OnNavigatedItemChanged();
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003E54 File Offset: 0x00002054
		private void OnNavigatedItemChanged()
		{
			this.IsOnDropdown = (this._lastNavigatedItem != null && (this._lastNavigatedItem.GetComponent<TMP_Dropdown>() != null || this._lastNavigatedItem.GetComponent<DropdownItemHandler>() != null));
			this.IsOnDropdownItem = (this._lastNavigatedItem != null && this._lastNavigatedItem.GetComponent<DropdownItemHandler>() != null);
		}

		// Token: 0x0400006E RID: 110
		private GameObject _lastNavigatedItem;
	}
}
