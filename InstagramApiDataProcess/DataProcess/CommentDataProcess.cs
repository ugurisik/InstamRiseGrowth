using InstagramApiDataProcess.Models;
using InstagramApiSharp;
using InstagramApiSharp.API;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstagramApiDataProcess.DataProcess
{
    public class CommentDataProcess
    {
        public static async Task<List<InstaProfile>> getCommentsUser(IInstaApi api, string URL)
        {
            try
            {
                List<InstaProfile> ınstaProfiles = new List<InstaProfile>();
                var mediaID = await api.MediaProcessor.GetMediaIdFromUrlAsync(new Uri(URL));
                var commentList = await api.CommentProcessor.GetMediaCommentsAsync(mediaID.Value, PaginationParameters.MaxPagesToLoad(9));
                foreach (var comments in commentList.Value.Comments)
                {
                    InstaProfile ınstaProfile = new InstaProfile();
                    int count = 0;
                    ınstaProfile.UserID = comments.User.Pk;
                    ınstaProfile.UserName = comments.User.UserName;
                    ınstaProfile.Priv = comments.User.IsPrivate;

                    ınstaProfiles.Add(ınstaProfile);
                    count++;
                    if (count > 5000)
                    {
                        break;
                    }
                }
                return ınstaProfiles;
            }
            catch
            {
                return null;
            }
        }
    }
}
