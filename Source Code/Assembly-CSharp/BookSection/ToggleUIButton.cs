using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TaleWorlds.CompanionBook.BookSection
{
	// Token: 0x02000079 RID: 121
	public class ToggleUIButton : MonoBehaviour
	{
		// Token: 0x060003E4 RID: 996 RVA: 0x0000EF36 File Offset: 0x0000D136
		private void Awake()
		{
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0000EF38 File Offset: 0x0000D138
		private void OnEnable()
		{
			this._button.onClick.AddListener(new UnityAction(this.ExecuteToggle));
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000EF56 File Offset: 0x0000D156
		private void OnDisable()
		{
			this._button.onClick.RemoveListener(new UnityAction(this.ExecuteToggle));
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000EF74 File Offset: 0x0000D174
		public void OnUIModeChanged(bool isUIEnabled)
		{
			this._button.image.sprite = (isUIEnabled ? this._UIEnabledSprite : this._UIDisabledSprite);
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000EF97 File Offset: 0x0000D197
		private void ExecuteToggle()
		{
			Action onToggle = this.OnToggle;
			if (onToggle == null)
			{
				return;
			}
			onToggle.Invoke();
		}

		// Token: 0x0400033A RID: 826
		public Action OnToggle;

		// Token: 0x0400033B RID: 827
		[SerializeField]
		private Sprite _UIEnabledSprite;

		// Token: 0x0400033C RID: 828
		[SerializeField]
		private Sprite _UIDisabledSprite;

		// Token: 0x0400033D RID: 829
		[SerializeField]
		private Button _button;
	}
}
