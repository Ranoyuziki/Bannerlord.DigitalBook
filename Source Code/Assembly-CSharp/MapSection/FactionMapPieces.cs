using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CompanionBook.ResourceSystem.MapResources;
using UnityEngine;
using UnityEngine.UI;

namespace TaleWorlds.CompanionBook.MapSection
{
	// Token: 0x02000051 RID: 81
	public class FactionMapPieces : MonoBehaviour
	{
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000286 RID: 646 RVA: 0x00009185 File Offset: 0x00007385
		public IReadOnlyList<IFactionMapPieceItem> Pieces
		{
			get
			{
				return this._pieces;
			}
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00009190 File Offset: 0x00007390
		private void Awake()
		{
			HashSet<MapFactionType> hashSet = new HashSet<MapFactionType>();
			for (int i = 0; i < base.gameObject.transform.childCount; i++)
			{
				FactionMapPieceItem factionMapPieceItem;
				if (base.gameObject.transform.GetChild(i).TryGetComponent<FactionMapPieceItem>(ref factionMapPieceItem))
				{
					this._pieces.Add(factionMapPieceItem);
					if (!hashSet.Contains(factionMapPieceItem.Faction))
					{
						hashSet.Add(factionMapPieceItem.Faction);
					}
				}
			}
			foreach (MapFactionType mapFactionType in (MapFactionType[])Enum.GetValues(typeof(MapFactionType)))
			{
			}
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000922B File Offset: 0x0000742B
		private void OnEnable()
		{
			this._pieces.ForEach(delegate(FactionMapPieceItem x)
			{
				x.OnToggleSelected = (Action<MapFactionType>)Delegate.Combine(x.OnToggleSelected, new Action<MapFactionType>(this.OnPieceItemToggleSelect));
			});
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00009244 File Offset: 0x00007444
		private void OnDisable()
		{
			this._pieces.ForEach(delegate(FactionMapPieceItem x)
			{
				x.OnToggleSelected = (Action<MapFactionType>)Delegate.Remove(x.OnToggleSelected, new Action<MapFactionType>(this.OnPieceItemToggleSelect));
			});
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00009260 File Offset: 0x00007460
		public void OnSelectedFactionChanged(MapFactionType faction, Image glowImage)
		{
			FactionMapPieceItem factionMapPieceItem = Enumerable.FirstOrDefault<FactionMapPieceItem>(this._pieces, (FactionMapPieceItem x) => x.IsSelected);
			FactionMapPieceItem factionMapPieceItem2 = Enumerable.FirstOrDefault<FactionMapPieceItem>(this._pieces, (FactionMapPieceItem x) => x.Faction == faction);
			if (factionMapPieceItem != null)
			{
				factionMapPieceItem.IsSelected = false;
			}
			if (factionMapPieceItem2 != null)
			{
				factionMapPieceItem2.IsSelected = true;
				factionMapPieceItem2.RefreshGlow(glowImage);
			}
		}

		// Token: 0x0600028B RID: 651 RVA: 0x000092E4 File Offset: 0x000074E4
		private void OnPieceItemToggleSelect(MapFactionType faction)
		{
			Action<MapFactionType, bool> onToggleMapFaction = this.OnToggleMapFaction;
			if (onToggleMapFaction == null)
			{
				return;
			}
			onToggleMapFaction.Invoke(faction, false);
		}

		// Token: 0x040001F8 RID: 504
		public Action<MapFactionType, bool> OnToggleMapFaction;

		// Token: 0x040001F9 RID: 505
		private List<FactionMapPieceItem> _pieces = new List<FactionMapPieceItem>();
	}
}
