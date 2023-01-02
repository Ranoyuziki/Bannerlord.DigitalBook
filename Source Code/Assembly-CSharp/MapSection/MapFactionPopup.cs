using System;
using TaleWorlds.CompanionBook.ResourceSystem.MapResources;
using UnityEngine;
using UnityEngine.UI;

namespace TaleWorlds.CompanionBook.MapSection
{
	// Token: 0x02000055 RID: 85
	[RequireComponent(typeof(CanvasGroup))]
	public class MapFactionPopup : MonoBehaviour
	{
		// Token: 0x060002AC RID: 684 RVA: 0x000098E4 File Offset: 0x00007AE4
		private void Awake()
		{
			this._canvasGroup = base.GetComponent<CanvasGroup>();
		}

		// Token: 0x060002AD RID: 685 RVA: 0x000098F4 File Offset: 0x00007AF4
		private void Update()
		{
			if (this._animation != null)
			{
				this._animation.Progress(Time.deltaTime);
				this._canvasGroup.alpha = this._animation.Progression;
				if (this._animation.IsFinished)
				{
					this._animation = null;
				}
			}
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00009944 File Offset: 0x00007B44
		public void Refresh(IFactionMapPieceItem mapPiece)
		{
			if (mapPiece != null)
			{
				this._scrollablePanel.ResetSpeed();
				Vector3 localPosition = this._scrollablePanel.content.localPosition;
				localPosition.y = 0f;
				this._scrollablePanel.content.localPosition = localPosition;
				this._banner.sprite = mapPiece.Banner;
				this._canvasGroup.alpha = 0f;
				this._animation = new AnimationProgress(0.6f, AnimationProgressType.ExpoEaseOut);
				MapFaction mapFaction;
				if (Game.Instance.ResourceProvider.MapResources.Factions.TryGetValue(mapPiece.Faction, ref mapFaction))
				{
					string text = mapPiece.Faction.ToString().ToLower();
					this._title.SetReference("ui_generic_faction_" + text);
					this._description.SetReference("calradia_map_description_" + text);
				}
			}
		}

		// Token: 0x04000204 RID: 516
		[SerializeField]
		private Image _banner;

		// Token: 0x04000205 RID: 517
		[SerializeField]
		private LocalizedText _title;

		// Token: 0x04000206 RID: 518
		[SerializeField]
		private LocalizedText _description;

		// Token: 0x04000207 RID: 519
		[SerializeField]
		private VerticalScrollablePanel _scrollablePanel;

		// Token: 0x04000208 RID: 520
		private CanvasGroup _canvasGroup;

		// Token: 0x04000209 RID: 521
		private AnimationProgress _animation;

		// Token: 0x0400020A RID: 522
		private const float _animationDuration = 0.6f;
	}
}
