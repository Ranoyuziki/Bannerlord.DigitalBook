using System;
using TaleWorlds.CompanionBook.BrushSystem;
using TaleWorlds.CompanionBook.ResourceSystem.ConceptArtResources;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TaleWorlds.CompanionBook.ConceptArtSection
{
	// Token: 0x0200006A RID: 106
	public class FactionItem : MonoBehaviour
	{
		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600034B RID: 843 RVA: 0x0000C400 File Offset: 0x0000A600
		// (set) Token: 0x0600034C RID: 844 RVA: 0x0000C408 File Offset: 0x0000A608
		public ConceptArtFactionType Faction { get; private set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600034D RID: 845 RVA: 0x0000C411 File Offset: 0x0000A611
		// (set) Token: 0x0600034E RID: 846 RVA: 0x0000C419 File Offset: 0x0000A619
		public ButtonHoverHandler ButtonHoverHandler { get; private set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600034F RID: 847 RVA: 0x0000C422 File Offset: 0x0000A622
		public Button Button
		{
			get
			{
				return this._button;
			}
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000C42A File Offset: 0x0000A62A
		private void Awake()
		{
			this.ButtonHoverHandler = this._button.GetComponent<ButtonHoverHandler>();
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000C43D File Offset: 0x0000A63D
		private void OnEnable()
		{
			this._button.onClick.AddListener(new UnityAction(this.ExecuteSelect));
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000C45B File Offset: 0x0000A65B
		private void OnDisable()
		{
			this._button.onClick.RemoveListener(new UnityAction(this.ExecuteSelect));
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000C47C File Offset: 0x0000A67C
		public void Initialize(ConceptArtFactionType factionType)
		{
			this.Faction = factionType;
			ConceptArtFaction conceptArtFaction;
			if (Game.Instance.ResourceProvider.ConceptArtsResources.Factions.TryGetValue(factionType, ref conceptArtFaction))
			{
				this._defaultImage.sprite = conceptArtFaction.DefaultCard;
				this._hoverImage.sprite = conceptArtFaction.HoveredCard;
			}
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000C4D0 File Offset: 0x0000A6D0
		public void ExecuteSelect()
		{
			Action<FactionItem> onSelect = this.OnSelect;
			if (onSelect == null)
			{
				return;
			}
			onSelect.Invoke(this);
		}

		// Token: 0x040002AB RID: 683
		public Action<FactionItem> OnSelect;

		// Token: 0x040002AC RID: 684
		[SerializeField]
		private Button _button;

		// Token: 0x040002AD RID: 685
		[SerializeField]
		private Brush _brush;

		// Token: 0x040002AE RID: 686
		[SerializeField]
		private Image _defaultImage;

		// Token: 0x040002AF RID: 687
		[SerializeField]
		private Image _hoverImage;
	}
}
