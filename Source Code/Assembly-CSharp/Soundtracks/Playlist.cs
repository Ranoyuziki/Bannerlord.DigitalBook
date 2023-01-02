using System;
using System.Collections.Generic;
using System.Linq;

namespace TaleWorlds.CompanionBook.Soundtracks
{
	// Token: 0x02000023 RID: 35
	public class Playlist : IPlaylist
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00005694 File Offset: 0x00003894
		public IReadOnlyList<ISoundtrack> Soundtracks { get; }

		// Token: 0x06000129 RID: 297 RVA: 0x0000569C File Offset: 0x0000389C
		public Playlist(IEnumerable<ISoundtrack> soundtracks)
		{
			List<ISoundtrack> list = new List<ISoundtrack>();
			foreach (ISoundtrack soundtrack in soundtracks)
			{
				list.Add(soundtrack);
			}
			this.Soundtracks = list;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x000056F8 File Offset: 0x000038F8
		public string GetFirstSoundtrackID()
		{
			ISoundtrack soundtrack = Enumerable.FirstOrDefault<ISoundtrack>(this.Soundtracks);
			return ((soundtrack != null) ? soundtrack.ID : null) ?? string.Empty;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000571A File Offset: 0x0000391A
		public string GetNextSoundtrackID(out bool isOverflowed)
		{
			return this.GetSoundtrackID(false, out isOverflowed);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00005724 File Offset: 0x00003924
		public string GetPreviousSoundtrackID(out bool isOverflowed)
		{
			return this.GetSoundtrackID(true, out isOverflowed);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00005730 File Offset: 0x00003930
		private string GetSoundtrackID(bool isPrevious, out bool isOverflowed)
		{
			string result = string.Empty;
			isOverflowed = false;
			int num = this.Soundtracks.FindIndex((ISoundtrack x) => x.IsSelected);
			if (num != -1)
			{
				if (isPrevious ? (num == 0) : (num == this.Soundtracks.Count - 1))
				{
					result = this.Soundtracks[isPrevious ? (this.Soundtracks.Count - 1) : 0].ID;
					isOverflowed = true;
				}
				else
				{
					result = this.Soundtracks[num + (isPrevious ? -1 : 1)].ID;
				}
			}
			return result;
		}
	}
}
