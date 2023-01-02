using System;
using UnityEngine;
using UnityEngine.UI;

namespace TaleWorlds.CompanionBook.CreditsSection
{
	// Token: 0x02000061 RID: 97
	public class CreditsNodeImageItem : CreditsNodeItem
	{
		// Token: 0x06000310 RID: 784 RVA: 0x0000B42F File Offset: 0x0000962F
		public override void Initialize(string value)
		{
			this._image.sprite = Game.Instance.ResourceProvider.CreditsLogos.GetSprite(value);
		}

		// Token: 0x04000269 RID: 617
		[SerializeField]
		private Image _image;
	}
}
