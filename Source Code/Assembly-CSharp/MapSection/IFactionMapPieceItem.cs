using System;
using TaleWorlds.CompanionBook.ResourceSystem.MapResources;
using UnityEngine;

namespace TaleWorlds.CompanionBook.MapSection
{
	// Token: 0x02000054 RID: 84
	public interface IFactionMapPieceItem
	{
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002A6 RID: 678
		GameObject GameObject { get; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002A7 RID: 679
		Vector2 Position { get; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002A8 RID: 680
		Vector2 PopupOffset { get; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060002A9 RID: 681
		MapFactionType Faction { get; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002AA RID: 682
		FactionMapPieceItemPopupLayout Layout { get; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002AB RID: 683
		Sprite Banner { get; }
	}
}
