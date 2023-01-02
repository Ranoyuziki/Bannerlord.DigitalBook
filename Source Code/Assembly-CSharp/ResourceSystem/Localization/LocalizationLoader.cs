using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace TaleWorlds.CompanionBook.ResourceSystem.Localization
{
	// Token: 0x02000030 RID: 48
	public class LocalizationLoader : IResource
	{
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00006797 File Offset: 0x00004997
		public bool IsLoaded
		{
			get
			{
				return this._isLoaded;
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000679F File Offset: 0x0000499F
		public LocalizationLoader()
		{
			this._tables = new Dictionary<string, StringTable>();
			this._tableHandles = new Dictionary<string, AsyncOperationHandle<StringTable>>();
			this.Initialize();
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x000067C3 File Offset: 0x000049C3
		public float GetLoadingProgress()
		{
			if (!this._isLoaded)
			{
				return 0f;
			}
			return 1f;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x000067D8 File Offset: 0x000049D8
		private void Initialize()
		{
			LocalizationLoader.<Initialize>d__7 <Initialize>d__;
			<Initialize>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<Initialize>d__.<>4__this = this;
			<Initialize>d__.<>1__state = -1;
			<Initialize>d__.<>t__builder.Start<LocalizationLoader.<Initialize>d__7>(ref <Initialize>d__);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00006810 File Offset: 0x00004A10
		private void AddEntry(StringTable table, string entryID, string entryValue)
		{
			StringTableEntry stringTableEntry = table.GetEntry(entryID);
			if (stringTableEntry == null)
			{
				stringTableEntry = table.AddEntry(entryID, entryValue);
			}
			else
			{
				stringTableEntry.Value = entryValue;
			}
			stringTableEntry.IsSmart = true;
		}

		// Token: 0x0400013C RID: 316
		private Dictionary<string, StringTable> _tables;

		// Token: 0x0400013D RID: 317
		private Dictionary<string, AsyncOperationHandle<StringTable>> _tableHandles;

		// Token: 0x0400013E RID: 318
		private bool _isLoaded;
	}
}
