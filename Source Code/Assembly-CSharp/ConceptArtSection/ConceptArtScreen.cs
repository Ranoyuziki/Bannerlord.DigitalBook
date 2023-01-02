using System;
using TaleWorlds.CompanionBook.ResourceSystem.ConceptArtResources;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TaleWorlds.CompanionBook.ConceptArtSection
{
	// Token: 0x02000068 RID: 104
	public class ConceptArtScreen : MonoBehaviour
	{
		// Token: 0x06000340 RID: 832 RVA: 0x0000C19C File Offset: 0x0000A39C
		private void Awake()
		{
			PlayerActions playerActions = Game.Instance.PlayerActions;
			PlayerActions.ConceptArtActionsActions conceptArtActions = playerActions.ConceptArtActions;
			this._previousArtworkAction = conceptArtActions.PreviousArtwork;
			this._nextArtworkAction = conceptArtActions.NextArtwork;
			this._cancelAction = playerActions.UI.Cancel;
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000C1EC File Offset: 0x0000A3EC
		private void OnEnable()
		{
			this._globalBackground.SetIsBlurryEnabled(true);
			FactionSelection factionSelection = this._factionSelection;
			factionSelection.OnFactionSelected = (Action<ConceptArtFactionType>)Delegate.Combine(factionSelection.OnFactionSelected, new Action<ConceptArtFactionType>(this.OnFactionSelected));
			ConceptArtCarousel carousel = this._carousel;
			carousel.OnBack = (Action)Delegate.Combine(carousel.OnBack, new Action(this.SwitchToFactionSelection));
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000C254 File Offset: 0x0000A454
		private void OnDisable()
		{
			this._globalBackground.SetIsBlurryEnabled(false);
			FactionSelection factionSelection = this._factionSelection;
			factionSelection.OnFactionSelected = (Action<ConceptArtFactionType>)Delegate.Remove(factionSelection.OnFactionSelected, new Action<ConceptArtFactionType>(this.OnFactionSelected));
			ConceptArtCarousel carousel = this._carousel;
			carousel.OnBack = (Action)Delegate.Remove(carousel.OnBack, new Action(this.SwitchToFactionSelection));
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000C2BC File Offset: 0x0000A4BC
		private void Update()
		{
			if (this._carousel.gameObject.activeSelf)
			{
				if (this._previousArtworkAction.WasPressedThisFrame())
				{
					this._carousel.ExecuteShowPrevious();
					return;
				}
				if (this._nextArtworkAction.WasPressedThisFrame())
				{
					this._carousel.ExecuteShowNext();
					return;
				}
				if (this._cancelAction.WasPressedThisFrame())
				{
					if (this._carousel.IsControlsHidden)
					{
						this._carousel.DisplayControls();
						return;
					}
					this.SwitchToFactionSelection();
					return;
				}
			}
			else
			{
				this._backToMenuButton.CheckActions();
			}
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000C345 File Offset: 0x0000A545
		private void OnFactionSelected(ConceptArtFactionType faction)
		{
			this.SwitchToCarousel(faction);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000C34E File Offset: 0x0000A54E
		private void SwitchToCarousel(ConceptArtFactionType faction)
		{
			this._factionSelection.gameObject.SetActive(false);
			this._carousel.gameObject.SetActive(true);
			this._carousel.OnShow(faction);
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000C37E File Offset: 0x0000A57E
		private void SwitchToFactionSelection()
		{
			this._carousel.gameObject.SetActive(false);
			this._factionSelection.gameObject.SetActive(true);
		}

		// Token: 0x0400029F RID: 671
		[SerializeField]
		private GlobalBackground _globalBackground;

		// Token: 0x040002A0 RID: 672
		[SerializeField]
		private FactionSelection _factionSelection;

		// Token: 0x040002A1 RID: 673
		[SerializeField]
		private FactionDescription _factionDescription;

		// Token: 0x040002A2 RID: 674
		[SerializeField]
		private ConceptArtCarousel _carousel;

		// Token: 0x040002A3 RID: 675
		[SerializeField]
		private BackToMenuButton _backToMenuButton;

		// Token: 0x040002A4 RID: 676
		private InputAction _cancelAction;

		// Token: 0x040002A5 RID: 677
		private InputAction _previousArtworkAction;

		// Token: 0x040002A6 RID: 678
		private InputAction _nextArtworkAction;
	}
}
