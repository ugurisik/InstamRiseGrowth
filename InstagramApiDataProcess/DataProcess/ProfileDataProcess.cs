using InstagramApiDataProcess.Models;
using InstagramApiSharp;
using InstagramApiSharp.API;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace InstagramApiDataProcess.DataProcess
{
    public class ProfileDataProcess
    {
        public static async Task<InstaProfile> ProfileInfo(IInstaApi api, string UserName)
        {
            try
            {
                InstaProfile ınstaProfile = new InstaProfile();
                string PictureURL = "";
                var getuserID = await api.UserProcessor.GetUserAsync(UserName);
                if (getuserID.Succeeded)
                {
                    ınstaProfile.ExceptionStatus = null;
                    ınstaProfile.UserID = getuserID.Value.Pk;

                    var user = await api.UserProcessor.GetFullUserInfoAsync(ınstaProfile.UserID);
                    if (user.Succeeded)
                    {
                        ınstaProfile.Biographi = user.Value.UserDetail.Biography;
                        ınstaProfile.UserName = user.Value.UserDetail.Username;
                        ınstaProfile.Followers = user.Value.UserDetail.FollowerCount.ToString();
                        ınstaProfile.Following = user.Value.UserDetail.FollowingCount.ToString();
                        ınstaProfile.Postcount = user.Value.UserDetail.MediaCount.ToString();
                        ınstaProfile.Priv = user.Value.UserDetail.IsPrivate;
                        ınstaProfile.Verify = user.Value.UserDetail.IsVerified;
                        ınstaProfile.Fullname = user.Value.UserDetail.FullName;
                        PictureURL = user.Value.UserDetail.HdProfilePicUrlInfo.Uri;
                    }
                    else
                    {
                        var secondResult = await api.UserProcessor.GetUserInfoByIdAsync(ınstaProfile.UserID);
                        if (secondResult.Succeeded)
                        {
                            ınstaProfile.Biographi = secondResult.Value.Biography;
                            ınstaProfile.UserName = secondResult.Value.Username;
                            ınstaProfile.Followers = secondResult.Value.FollowerCount.ToString();
                            ınstaProfile.Following = secondResult.Value.FollowingCount.ToString();
                            ınstaProfile.Postcount = secondResult.Value.MediaCount.ToString();
                            ınstaProfile.Priv = secondResult.Value.IsPrivate;
                            ınstaProfile.Verify = secondResult.Value.IsVerified;
                            ınstaProfile.Fullname = secondResult.Value.FullName;
                            PictureURL = secondResult.Value.HdProfilePicUrlInfo.Uri;
                        }
                        ınstaProfile.ExceptionStatus = user.Info.Message;
                    }
                }
                else
                {
                    ınstaProfile.ExceptionStatus = getuserID.Info.Message;
                }

                if (!string.IsNullOrEmpty(PictureURL))
                {
                    WebClient wc = new WebClient();
                    byte[] bytes = wc.DownloadData(PictureURL);
                    MemoryStream ms = new MemoryStream(bytes);
                    ınstaProfile.Picture = Image.FromStream(ms);
                }

                return ınstaProfile;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static async Task<List<InstaProfile>> SuggestionUsers(IInstaApi api)
        {
            try
            {
                List<InstaProfile> ınstaProfiles = new List<InstaProfile>();
                var pending = await api.UserProcessor.GetPendingFriendRequestsAsync();
                if (pending.Succeeded && pending != null)
                {
                    foreach (var users in pending.Value.SuggestedUsers.Suggestions)
                    {
                        InstaProfile ınstaProfile = new InstaProfile();
                        ınstaProfile.UserID = users.User.Pk;
                        ınstaProfile.UserName = users.User.UserName;
                        ınstaProfile.Priv = users.User.IsPrivate;

                        ınstaProfiles.Add(ınstaProfile);
                    }
                }
                return ınstaProfiles;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static async Task<List<InstaProfile>> Followers(IInstaApi api, string UserName)
        {
            try
            {
                List<InstaProfile> ınstaProfiles = new List<InstaProfile>();
                var userID = await api.UserProcessor.GetUserInfoByUsernameAsync(UserName);
                var followerList = await api.UserProcessor.GetUserFollowersByIdAsync(userID.Value.Pk, PaginationParameters.MaxPagesToLoad(10));
                // var followerList = await api.UserProcessor.GetUserFollowersByIdAsync(Convert.ToInt64(userID), PaginationParameters.MaxPagesToLoad(9));
                if (followerList.Succeeded)
                {
                    foreach (var followers in followerList.Value)
                    {
                        InstaProfile ınstaProfile = new InstaProfile();
                        ınstaProfile.UserID = followers.Pk;
                        ınstaProfile.UserName = followers.UserName;
                        ınstaProfile.Priv = followers.IsPrivate;

                        ınstaProfiles.Add(ınstaProfile);
                    }
                }
                else
                {
                    return null;
                }
                return ınstaProfiles;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public static async Task<List<InstaProfile>> Following(IInstaApi api, string UserName)
        {
            try
            {
                List<InstaProfile> ınstaProfiles = new List<InstaProfile>();
                var followerList = await api.UserProcessor.GetUserFollowingAsync(UserName, PaginationParameters.MaxPagesToLoad(9));
                if (followerList.Succeeded)
                {
                    int count = 0;
                    foreach (var followers in followerList.Value)
                    {
                        InstaProfile ınstaProfile = new InstaProfile();
                        ınstaProfile.UserID = followers.Pk;
                        ınstaProfile.UserName = followers.UserName;
                        ınstaProfile.Priv = followers.IsPrivate;

                        ınstaProfiles.Add(ınstaProfile);
                        count++;
                        if (count > 5000)
                        {
                            break;
                        }
                    }
                    return ınstaProfiles;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }

        }

        public static async Task<List<InstaProfile>> NotFollowing(IInstaApi api, string UserName)
        {
            try
            {
                List<InstaProfile> following = await Following(api, UserName);
                List<InstaProfile> followers = await Followers(api, UserName);

                return followers.Except(following).ToList();
            }
            catch (Exception)
            {

                return null;
            }
        }

        public static async Task<List<InstaProfile>> NotFollower(IInstaApi api, string UserName)
        {
            try
            {
                List<InstaProfile> following = await Following(api, UserName);
                List<InstaProfile> followers = await Followers(api, UserName);

                return following.Except(followers).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
