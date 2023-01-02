using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TaleWorlds.CompanionBook.CreditsSection
{
	// Token: 0x02000060 RID: 96
	public class CompanyStoryNavigationControls : MonoBehaviour
	{
		// Token: 0x0600030A RID: 778 RVA: 0x0000B38D File Offset: 0x0000958D
		private void Awake()
		{
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000B38F File Offset: 0x0000958F
		private void OnEnable()
		{
			this._showPreviousButton.onClick.AddListener(new UnityAction(this.ExecuteShowPrevious));
			this._showNextButton.onClick.AddListener(new UnityAction(this.ExecuteShowNext));
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000B3C9 File Offset: 0x000095C9
		private void OnDisable()
		{
			this._showPreviousButton.onClick.RemoveListener(new UnityAction(this.ExecuteShowPrevious));
			this._showNextButton.onClick.RemoveListener(new UnityAction(this.ExecuteShowNext));
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000B403 File Offset: 0x00009603
		private void ExecuteShowPrevious()
		{
			Action onShowPrevious = this.OnShowPrevious;
			if (onShowPrevious == null)
			{
				return;
			}
			onShowPrevious.Invoke();
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000B415 File Offset: 0x00009615
		private void ExecuteShowNext()
		{
			Action onShowNext = this.OnShowNext;
			if (onShowNext == null)
			{
				return;
			}
			onShowNext.Invoke();
		}

		// Token: 0x04000265 RID: 613
		public Action OnShowPrevious;

		// Token: 0x04000266 RID: 614
		public Action OnShowNext;

		// Token: 0x04000267 RID: 615
		[SerializeField]
		private Button _showPreviousButton;

		// Token: 0x04000268 RID: 616
		[SerializeField]
		private Button _showNextButton;
	}
}
