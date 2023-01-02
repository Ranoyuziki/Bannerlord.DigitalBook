using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

namespace TaleWorlds.CompanionBook
{
	// Token: 0x02000013 RID: 19
	[RequireComponent(typeof(LocalizeStringEvent))]
	[RequireComponent(typeof(TMP_Text))]
	public class LocalizedText : MonoBehaviour
	{
		// Token: 0x06000092 RID: 146 RVA: 0x00003A5C File Offset: 0x00001C5C
		private void Awake()
		{
			this._text = base.GetComponent<TMP_Text>();
			this._localizeStringEvent = base.GetComponent<LocalizeStringEvent>();
			this._localizeStringEvent.StringReference = ((this._initialString != null) ? this._initialString : new LocalizedString("SingleTable", this._entryName));
			this._isInitialized = true;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003ABD File Offset: 0x00001CBD
		private void Start()
		{
			this._localizeStringEvent.RefreshString();
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003ACA File Offset: 0x00001CCA
		private void OnEnable()
		{
			this._localizeStringEvent.OnUpdateString.AddListener(new UnityAction<string>(this.OnUpdateString));
			this._localizeStringEvent.RefreshString();
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003AF3 File Offset: 0x00001CF3
		private void OnDisable()
		{
			this._localizeStringEvent.OnUpdateString.RemoveListener(new UnityAction<string>(this.OnUpdateString));
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003B11 File Offset: 0x00001D11
		public TMP_Text GetText()
		{
			if (this._text == null)
			{
				this._text = base.GetComponent<TMP_Text>();
			}
			return this._text;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003B34 File Offset: 0x00001D34
		public void SetReference(string entry)
		{
			LocalizedString localizedString = new LocalizedString("SingleTable", entry);
			if (this._isInitialized)
			{
				this._localizeStringEvent.StringReference = localizedString;
				this._localizeStringEvent.RefreshString();
				return;
			}
			this._initialString = localizedString;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003B7E File Offset: 0x00001D7E
		public void SetReference(LocalizedString stringReference)
		{
			if (this._isInitialized)
			{
				this._localizeStringEvent.StringReference = stringReference;
				this._localizeStringEvent.RefreshString();
				return;
			}
			this._initialString = stringReference;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003BA7 File Offset: 0x00001DA7
		public static LocalizedString CreateLocalizedString(string entry)
		{
			return new LocalizedString("SingleTable", entry);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003BBE File Offset: 0x00001DBE
		private void OnUpdateString(string updatedString)
		{
			this._text.text = updatedString;
		}

		// Token: 0x0400005F RID: 95
		[SerializeField]
		private string _entryName;

		// Token: 0x04000060 RID: 96
		private LocalizeStringEvent _localizeStringEvent;

		// Token: 0x04000061 RID: 97
		private TMP_Text _text;

		// Token: 0x04000062 RID: 98
		private bool _isInitialized;

		// Token: 0x04000063 RID: 99
		private LocalizedString _initialString;

		// Token: 0x04000064 RID: 100
		private const string _tableName = "SingleTable";
	}
}
