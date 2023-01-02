using System;
using TMPro;
using UnityEngine;

namespace TaleWorlds.CompanionBook.CreditsSection
{
	// Token: 0x02000063 RID: 99
	public class CreditsNodeTextItem : CreditsNodeItem
	{
		// Token: 0x06000314 RID: 788 RVA: 0x0000B461 File Offset: 0x00009661
		public override void Initialize(string value)
		{
			this._text.text = value;
		}

		// Token: 0x0400026A RID: 618
		[SerializeField]
		private TMP_Text _text;
	}
}
