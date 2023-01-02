using System;
using UnityEngine;

namespace TaleWorlds.CompanionBook
{
	// Token: 0x02000015 RID: 21
	[RequireComponent(typeof(RectTransform))]
	public class NavigationAutoScrollItem : MonoBehaviour
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00003D8C File Offset: 0x00001F8C
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x00003D94 File Offset: 0x00001F94
		public RectTransform RectTransform { get; private set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00003D9D File Offset: 0x00001F9D
		public GameObject NavigationTarget
		{
			get
			{
				return this._navigationTarget;
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003DA5 File Offset: 0x00001FA5
		private void Awake()
		{
			this.RectTransform = base.GetComponent<RectTransform>();
		}

		// Token: 0x04000069 RID: 105
		[SerializeField]
		private GameObject _navigationTarget;
	}
}
