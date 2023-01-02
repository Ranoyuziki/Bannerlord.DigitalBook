using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CompanionBook.ResourceSystem.ConceptArtResources;
using TaleWorlds.CompanionBook.ScreenSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TaleWorlds.CompanionBook.ConceptArtSection
{
	// Token: 0x0200006B RID: 107
	public class FactionSelection : MonoBehaviour
	{
		// Token: 0x06000356 RID: 854 RVA: 0x0000C4EC File Offset: 0x0000A6EC
		private void Awake()
		{
			this._defaultCellSize = this._factionsContainer.cellSize;
			foreach (ConceptArtFactionType factionType in (ConceptArtFactionType[])Enum.GetValues(typeof(ConceptArtFactionType)))
			{
				FactionItem factionItem;
				if (Object.Instantiate<GameObject>(this._factionItemPrefab, this._factionsContainer.transform).TryGetComponent<FactionItem>(ref factionItem))
				{
					factionItem.Initialize(factionType);
					this._items.Add(factionItem);
				}
			}
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000C564 File Offset: 0x0000A764
		private void OnEnable()
		{
			this._items.ForEach(delegate(FactionItem x)
			{
				x.OnSelect = (Action<FactionItem>)Delegate.Combine(x.OnSelect, new Action<FactionItem>(this.OnItemSelected));
			});
			this._items.ForEach(delegate(FactionItem x)
			{
				ButtonHoverHandler buttonHoverHandler = x.ButtonHoverHandler;
				buttonHoverHandler.OnIsHoveredChanged = (Action<bool>)Delegate.Combine(buttonHoverHandler.OnIsHoveredChanged, new Action<bool>(this.OnItemHoverChanged));
			});
			this.RefreshTitle();
			ScreenManager screenManager = Game.Instance.ScreenManager;
			screenManager.OnCurrentResolutionChanged = (Action)Delegate.Combine(screenManager.OnCurrentResolutionChanged, new Action(this.RefreshItemsSize));
			this.RefreshItemsSize();
			Game instance = Game.Instance;
			instance.OnControlSchemeChanged = (Action)Delegate.Combine(instance.OnControlSchemeChanged, new Action(this.RefreshNavigation));
			this.RefreshNavigation();
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000C604 File Offset: 0x0000A804
		private void OnDisable()
		{
			this._items.ForEach(delegate(FactionItem x)
			{
				x.OnSelect = (Action<FactionItem>)Delegate.Remove(x.OnSelect, new Action<FactionItem>(this.OnItemSelected));
			});
			this._items.ForEach(delegate(FactionItem x)
			{
				ButtonHoverHandler buttonHoverHandler = x.ButtonHoverHandler;
				buttonHoverHandler.OnIsHoveredChanged = (Action<bool>)Delegate.Remove(buttonHoverHandler.OnIsHoveredChanged, new Action<bool>(this.OnItemHoverChanged));
			});
			ScreenManager screenManager = Game.Instance.ScreenManager;
			screenManager.OnCurrentResolutionChanged = (Action)Delegate.Remove(screenManager.OnCurrentResolutionChanged, new Action(this.RefreshItemsSize));
			Game instance = Game.Instance;
			instance.OnControlSchemeChanged = (Action)Delegate.Remove(instance.OnControlSchemeChanged, new Action(this.RefreshNavigation));
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000C690 File Offset: 0x0000A890
		private void OnItemSelected(FactionItem item)
		{
			Action<ConceptArtFactionType> onFactionSelected = this.OnFactionSelected;
			if (onFactionSelected != null)
			{
				onFactionSelected.Invoke(item.Faction);
			}
			this._lastSelectedItem = item;
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000C6B0 File Offset: 0x0000A8B0
		private void OnItemHoverChanged(bool isHovered)
		{
			this.RefreshTitle();
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000C6B8 File Offset: 0x0000A8B8
		private void RefreshTitle()
		{
			FactionItem factionItem = Enumerable.FirstOrDefault<FactionItem>(this._items, (FactionItem x) => x.ButtonHoverHandler.IsHovered);
			if (factionItem != null)
			{
				this._title.SetReference(LocalizedText.CreateLocalizedString("ui_generic_faction_" + factionItem.Faction.ToString().ToLower()));
				return;
			}
			this._title.SetReference("empty_entry");
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000C740 File Offset: 0x0000A940
		private void RefreshNavigation()
		{
			if (Game.Instance.IsCurrentSchemeGamepad)
			{
				FactionItem factionItem = (this._lastSelectedItem != null) ? this._lastSelectedItem : ((this._items.Count > 0) ? this._items[0] : null);
				if (factionItem != null && factionItem.Button != null)
				{
					EventSystem.current.SetSelectedGameObject(factionItem.Button.gameObject);
				}
			}
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000C7BC File Offset: 0x0000A9BC
		private void RefreshItemsSize()
		{
			ScreenManager screenManager = Game.Instance.ScreenManager;
			if (Vector2.Distance(this._lastCheckedResolution, screenManager.CurrentResolution) > Mathf.Epsilon)
			{
				this._lastCheckedResolution = screenManager.CurrentResolution;
				float resolutionScale = screenManager.ResolutionScale;
				int constraintCount = this._factionsContainer.constraintCount;
				float num = (this._defaultCellSize.x * (float)constraintCount + this._factionsContainer.spacing.x * (float)(constraintCount - 1)) * resolutionScale;
				if (screenManager.CurrentResolution.x < num)
				{
					float num2 = (float)(constraintCount - 1) * this._factionsContainer.spacing.x * resolutionScale;
					float num3 = (screenManager.CurrentResolution.x - num2) / (float)constraintCount / resolutionScale;
					float num4 = num3 * (this._defaultCellSize.y / this._defaultCellSize.x);
					this._factionsContainer.cellSize = new Vector2(num3, num4);
					return;
				}
				this._factionsContainer.cellSize = this._defaultCellSize;
			}
		}

		// Token: 0x040002B0 RID: 688
		public Action<ConceptArtFactionType> OnFactionSelected;

		// Token: 0x040002B1 RID: 689
		[SerializeField]
		private GridLayoutGroup _factionsContainer;

		// Token: 0x040002B2 RID: 690
		[SerializeField]
		private GameObject _factionItemPrefab;

		// Token: 0x040002B3 RID: 691
		[SerializeField]
		private LocalizedText _title;

		// Token: 0x040002B4 RID: 692
		private List<FactionItem> _items = new List<FactionItem>();

		// Token: 0x040002B5 RID: 693
		private FactionItem _lastSelectedItem;

		// Token: 0x040002B6 RID: 694
		private Vector2 _defaultCellSize;

		// Token: 0x040002B7 RID: 695
		private Vector2 _lastCheckedResolution = Vector2.zero;
	}
}
