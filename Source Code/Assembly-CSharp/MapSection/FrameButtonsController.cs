using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CompanionBook.BrushSystem;
using TaleWorlds.CompanionBook.ResourceSystem.MapResources;
using UnityEngine;

namespace TaleWorlds.CompanionBook.MapSection
{
	// Token: 0x02000053 RID: 83
	public class FrameButtonsController : MonoBehaviour
	{
		// Token: 0x06000298 RID: 664 RVA: 0x000093FB File Offset: 0x000075FB
		private void Awake()
		{
			if (this._items == null)
			{
				this._items = new List<FrameButtonItem>();
			}
			if (this._empireSlots == null)
			{
				this._empireSlots = new List<Brush>();
			}
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00009424 File Offset: 0x00007624
		private void OnEnable()
		{
			foreach (FrameButtonItem frameButtonItem in this._items)
			{
				frameButtonItem.OnToggle = (Action<FrameButtonItem>)Delegate.Combine(frameButtonItem.OnToggle, new Action<FrameButtonItem>(this.OnToggleItem));
			}
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00009490 File Offset: 0x00007690
		private void OnDisable()
		{
			foreach (FrameButtonItem frameButtonItem in this._items)
			{
				frameButtonItem.OnToggle = (Action<FrameButtonItem>)Delegate.Remove(frameButtonItem.OnToggle, new Action<FrameButtonItem>(this.OnToggleItem));
			}
		}

		// Token: 0x0600029B RID: 667 RVA: 0x000094FC File Offset: 0x000076FC
		public void OnSelectedFactionChanged(MapFactionType faction)
		{
			this._currentEmpireFaction = MapFactionType.None;
			FrameButtonItem frameButtonItem = Enumerable.FirstOrDefault<FrameButtonItem>(this._items, (FrameButtonItem x) => x.IsSelected);
			FrameButtonItem frameButtonItem2 = Enumerable.FirstOrDefault<FrameButtonItem>(this._items, (FrameButtonItem x) => x.Faction == faction || (this.IsEmpireFaction(faction) && this.IsEmpireFaction(x.Faction)));
			if (frameButtonItem != null)
			{
				frameButtonItem.IsSelected = false;
			}
			if (frameButtonItem2 != null)
			{
				frameButtonItem2.IsSelected = true;
				if (this.IsEmpireFaction(faction))
				{
					this._currentEmpireFaction = faction;
				}
			}
			this.RefreshEmpireSlots();
		}

		// Token: 0x0600029C RID: 668 RVA: 0x000095A7 File Offset: 0x000077A7
		public void SelectNext()
		{
			this.SelectItem(true);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x000095B0 File Offset: 0x000077B0
		public void SelectPrevious()
		{
			this.SelectItem(false);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x000095BC File Offset: 0x000077BC
		private void SelectItem(bool isNext)
		{
			if (this._items.Count > 0)
			{
				int num = this._items.FindIndex((FrameButtonItem x) => x.IsSelected);
				if (num != -1)
				{
					FrameButtonItem frameButtonItem = this._items[num];
					int num2 = num + (isNext ? 1 : -1);
					int num3 = (num2 < 0) ? (this._items.Count - 1) : ((num2 >= this._items.Count) ? 0 : num2);
					FrameButtonItem frameButtonItem2 = this._items[num3];
					MapFactionType mapFactionType = this._empireFactionsOrder[isNext ? 0 : (this._empireFactionsOrder.Count - 1)];
					MapFactionType mapFactionType2 = this._empireFactionsOrder[isNext ? (this._empireFactionsOrder.Count - 1) : 0];
					if (this.IsEmpireFaction(frameButtonItem2.Faction))
					{
						this.InvokeOnToggleFactionEvent(mapFactionType);
						return;
					}
					if (this.IsEmpireFaction(frameButtonItem.Faction) && this._currentEmpireFaction != mapFactionType2)
					{
						this.InvokeOnToggleFactionEvent(this.GetCycledEmpireFaction(this._currentEmpireFaction, isNext));
						return;
					}
					this.InvokeOnToggleFactionEvent(frameButtonItem2.Faction);
					return;
				}
				else
				{
					this.InvokeOnToggleFactionEvent(this._items[0].Faction);
				}
			}
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00009700 File Offset: 0x00007900
		private void OnToggleItem(FrameButtonItem faction)
		{
			if (!this.IsEmpireFaction(faction.Faction))
			{
				this.InvokeOnToggleFactionEvent(faction.Faction);
				return;
			}
			if (faction.IsSelected)
			{
				this.InvokeOnToggleFactionEvent(this.GetCycledEmpireFaction(this._currentEmpireFaction, true));
				return;
			}
			this.InvokeOnToggleFactionEvent(this.GetCycledEmpireFaction(MapFactionType.None, true));
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00009752 File Offset: 0x00007952
		private void InvokeOnToggleFactionEvent(MapFactionType mapFactionType)
		{
			Action<MapFactionType, bool> onToggleFaction = this.OnToggleFaction;
			if (onToggleFaction == null)
			{
				return;
			}
			onToggleFaction.Invoke(mapFactionType, true);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00009768 File Offset: 0x00007968
		private MapFactionType GetCycledEmpireFaction(MapFactionType faction, bool isNext)
		{
			MapFactionType result = MapFactionType.None;
			if (this._empireFactionsOrder != null && this._empireFactionsOrder.Count > 0)
			{
				int num = this._empireFactionsOrder.FindIndex((MapFactionType x) => x == faction);
				if (num == -1)
				{
					result = this._empireFactionsOrder[0];
				}
				else
				{
					int num2 = num + (isNext ? 1 : -1);
					if (num2 < 0)
					{
						result = this._empireFactionsOrder[this._empireFactionsOrder.Count - 1];
					}
					else if (num2 >= this._empireFactionsOrder.Count)
					{
						result = this._empireFactionsOrder[0];
					}
					else
					{
						result = this._empireFactionsOrder[num2];
					}
				}
			}
			return result;
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000981C File Offset: 0x00007A1C
		private bool IsEmpireFaction(MapFactionType faction)
		{
			return faction == MapFactionType.EmpireWest || faction == MapFactionType.EmpireSouth || faction == MapFactionType.EmpireNorth;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000982C File Offset: 0x00007A2C
		private void RefreshEmpireSlots()
		{
			int num = (this._empireFactionsOrder != null) ? this._empireFactionsOrder.FindIndex((MapFactionType x) => x == this._currentEmpireFaction) : -1;
			this._empireSlots.ForEach(delegate(Brush x)
			{
				x.IsSelected = false;
			});
			if (num >= 0 && num < this._empireSlots.Count)
			{
				this._empireSlots[num].IsSelected = true;
			}
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x000098AA File Offset: 0x00007AAA
		public FrameButtonsController()
		{
			List<MapFactionType> list = new List<MapFactionType>();
			list.Add(MapFactionType.EmpireWest);
			list.Add(MapFactionType.EmpireSouth);
			list.Add(MapFactionType.EmpireNorth);
			this._empireFactionsOrder = list;
			this._currentEmpireFaction = MapFactionType.None;
			base..ctor();
		}

		// Token: 0x040001FF RID: 511
		public Action<MapFactionType, bool> OnToggleFaction;

		// Token: 0x04000200 RID: 512
		[SerializeField]
		private List<FrameButtonItem> _items;

		// Token: 0x04000201 RID: 513
		[SerializeField]
		private List<Brush> _empireSlots;

		// Token: 0x04000202 RID: 514
		private readonly IReadOnlyList<MapFactionType> _empireFactionsOrder;

		// Token: 0x04000203 RID: 515
		private MapFactionType _currentEmpireFaction;
	}
}
