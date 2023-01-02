using System;
using TaleWorlds.CompanionBook.ResourceSystem;
using UnityEngine;

namespace TaleWorlds.CompanionBook
{
	// Token: 0x0200000B RID: 11
	public class CursorManager : MonoBehaviour
	{
		// Token: 0x06000040 RID: 64 RVA: 0x0000291C File Offset: 0x00000B1C
		private void Start()
		{
			Game instance = Game.Instance;
			instance.OnControlSchemeChanged = (Action)Delegate.Combine(instance.OnControlSchemeChanged, new Action(this.RefreshVisibility));
			if (!Game.Instance.ResourceProvider.IsLoading)
			{
				this.RefreshVisibility();
				return;
			}
			ResourceProvider resourceProvider = Game.Instance.ResourceProvider;
			resourceProvider.OnPersistentResourcesLoadingFinished = (Action)Delegate.Combine(resourceProvider.OnPersistentResourcesLoadingFinished, new Action(this.RefreshVisibility));
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002992 File Offset: 0x00000B92
		private void RefreshVisibility()
		{
			if (SystemInfo.deviceType == 3)
			{
				Cursor.visible = !Game.Instance.IsCurrentSchemeGamepad;
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000029AE File Offset: 0x00000BAE
		[RuntimeInitializeOnLoadMethod(3)]
		private static void OnBeforeSceneLoadRuntimeMethod()
		{
			if (SystemInfo.deviceType == 3)
			{
				Cursor.visible = false;
			}
		}
	}
}
