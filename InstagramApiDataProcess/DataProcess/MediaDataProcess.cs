using InstagramApiDataProcess.Models;
using InstagramApiSharp;
using InstagramApiSharp.API;
using InstagramApiSharp.Classes.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstagramApiDataProcess.DataProcess
{
    public class MediaDataProcess
    {
        public static async Task<List<InstaProfile>> getMediaLikers(IInstaApi api, string URL)
        {
            try
            {
                List<InstaProfile> ınstaProfiles = new List<InstaProfile>();
                int count = 0;
                var mediaID = await api.MediaProcessor.GetMediaIdFromUrlAsync(new Uri(URL));
                var likerList = await api.MediaProcessor.GetMediaLikersAsync(mediaID.Value);
                foreach (var likers in likerList.Value)
                {
                    InstaProfile ınstaProfile = new InstaProfile();
                    ınstaProfile.UserID = likers.Pk;
                    ınstaProfile.UserName = likers.UserName;
                    ınstaProfile.Priv = likers.IsPrivate;

                    count++;
                    if (count > 5000)
                    {
                        break;
                    }
                }
                return ınstaProfiles;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static async Task<List<InstaMedia>> Media(IInstaApi api, string getMediaTypesText, DownloadSettings downloadSettings, int DownloadPageCount)
        {
            try
            {
                switch (downloadSettings)
                {
                    case DownloadSettings.Tags:
                        var Tags = await api.FeedProcessor.GetTagFeedAsync(getMediaTypesText, PaginationParameters.MaxPagesToLoad(DownloadPageCount));
                        if (Tags.Succeeded)
                        {
                            return Tags.Value.Medias;
                        }
                        break;
                    case DownloadSettings.Profile:
                        var getMediaInfo = await api.UserProcessor.GetUserMediaAsync(getMediaTypesText, PaginationParameters.Empty);
                        if (getMediaInfo.Succeeded)
                        {
                            return getMediaInfo.Value;
                        }
                        break;
                    case DownloadSettings.Explorer:
                        var Explorer = await api.FeedProcessor.GetExploreFeedAsync(PaginationParameters.MaxPagesToLoad(DownloadPageCount));
                        if (Explorer.Succeeded)
                        {
                            return Explorer.Value.Medias;
                        }
                        break;
                    case DownloadSettings.HomePage:
                        break;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public static async Task<bool> LikeMediaInProfile(IInstaApi api, int LikeCount, int delay, long userID)
        {
            try
            {
                List<string> MediaID = new List<string>();
                var media = await api.UserProcessor.GetUserMediaByIdAsync(userID, PaginationParameters.MaxPagesToLoad(1));
                if (media.Succeeded)
                {
                    foreach (var media_info in media.Value)
                    {
                        var mediaID = media_info.Pk;
                        MediaID.Add(mediaID);
                    }
                }
                for (int i = 0; i < MediaID.Count; i++)
                {
                    var LikeMedia = await api.MediaProcessor.LikeMediaAsync(MediaID[i]);
                    await Task.Delay(delay * 897);
                    if (i >= LikeCount)
                    {
                        break;
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
