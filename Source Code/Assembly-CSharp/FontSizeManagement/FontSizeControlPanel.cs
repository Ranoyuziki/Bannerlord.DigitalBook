using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Localization;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

namespace TaleWorlds.CompanionBook.FontSizeManagement
{
	// Token: 0x0200005A RID: 90
	public class FontSizeControlPanel : MonoBehaviour
	{
		// Token: 0x060002E4 RID: 740 RVA: 0x0000ADAC File Offset: 0x00008FAC
		private void Awake()
		{
			if (this._items == null)
			{
				this._items = new List<FontSizeOptionItem>();
			}
			this._changeFontSizeAction = Game.Instance.PlayerActions.UI.ChangeFontSize;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000ADE9 File Offset: 0x00008FE9
		private void OnEnable()
		{
			this._items.ForEach(delegate(FontSizeOptionItem x)
			{
				x.OnSelect = (Action<FontSizeType>)Delegate.Combine(x.OnSelect, new Action<FontSizeType>(this.OnItemSelected));
			});
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000AE02 File Offset: 0x00009002
		private void OnDisable()
		{
			this._items.ForEach(delegate(FontSizeOptionItem x)
			{
				x.OnSelect = (Action<FontSizeType>)Delegate.Remove(x.OnSelect, new Action<FontSizeType>(this.OnItemSelected));
			});
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000AE1B File Offset: 0x0000901B
		public void CheckActions()
		{
			if (this._changeFontSizeAction.WasPressedThisFrame())
			{
				this.SelectNextFontSize();
			}
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000AE30 File Offset: 0x00009030
		private void SelectNextFontSize()
		{
			FontSizeType fontSizeType = FontSizeType.Default;
			switch (Game.Instance.FontSizeManager.SelectedFontSizeType)
			{
			case FontSizeType.Default:
				fontSizeType = FontSizeType.Large;
				break;
			case FontSizeType.Small:
				fontSizeType = FontSizeType.Default;
				break;
			case FontSizeType.Large:
				fontSizeType = FontSizeType.Small;
				break;
			}
			this.ChangeFontSize(fontSizeType);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000AE74 File Offset: 0x00009074
		private void OnItemSelected(FontSizeType fontSizeType)
		{
			this.ChangeFontSize(fontSizeType);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000AE80 File Offset: 0x00009080
		private void ChangeFontSize(FontSizeType fontSizeType)
		{
			Game.Instance.AudioManager.PlayRandomAudioClip(this._changeFontSizeAudioClip);
			Game.Instance.FontSizeManager.ChangeFontSize(fontSizeType);
			Notification notification = Game.Instance.Notification;
			LocalizedString localizedString = LocalizedText.CreateLocalizedString("ui_notification_text_size");
			localizedString.Add("COLOR", new StringVariable
			{
				Value = notification.OnColorCode
			});
			localizedString.Add("TEXT_SIZE", LocalizedText.CreateLocalizedString("ui_notification_text_size_" + fontSizeType.ToString().ToLower()));
			notification.Display(localizedString);
		}

		// Token: 0x04000249 RID: 585
		[SerializeField]
		private List<FontSizeOptionItem> _items;

		// Token: 0x0400024A RID: 586
		[SerializeField]
		private List<AudioClip> _changeFontSizeAudioClip;

		// Token: 0x0400024B RID: 587
		private InputAction _changeFontSizeAction;
	}
}
