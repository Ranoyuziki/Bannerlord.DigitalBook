using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TaleWorlds.CompanionBook.MapSection
{
	// Token: 0x02000059 RID: 89
	[RequireComponent(typeof(Button))]
	public class ToggleBordersButton : MonoBehaviour
	{
		// Token: 0x060002DE RID: 734 RVA: 0x0000AD1B File Offset: 0x00008F1B
		private void Awake()
		{
			this._button = base.GetComponent<Button>();
			this.OnBordersVisibilityChanged(true);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000AD30 File Offset: 0x00008F30
		private void OnEnable()
		{
			this._button.onClick.AddListener(new UnityAction(this.ExecuteToggle));
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000AD4E File Offset: 0x00008F4E
		private void OnDisable()
		{
			this._button.onClick.RemoveListener(new UnityAction(this.ExecuteToggle));
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000AD6C File Offset: 0x00008F6C
		public void OnBordersVisibilityChanged(bool isVisible)
		{
			this._button.image.sprite = (isVisible ? this._enabledSprite : this._disabledSprite);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000AD8F File Offset: 0x00008F8F
		private void ExecuteToggle()
		{
			Action onToggle = this.OnToggle;
			if (onToggle == null)
			{
				return;
			}
			onToggle.Invoke();
		}

		// Token: 0x04000245 RID: 581
		public Action OnToggle;

		// Token: 0x04000246 RID: 582
		[SerializeField]
		private Sprite _enabledSprite;

		// Token: 0x04000247 RID: 583
		[SerializeField]
		private Sprite _disabledSprite;

		// Token: 0x04000248 RID: 584
		private Button _button;
	}
}
