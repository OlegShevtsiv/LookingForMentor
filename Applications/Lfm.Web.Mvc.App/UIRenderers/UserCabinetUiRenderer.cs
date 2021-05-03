using System.Collections.Generic;
using LFM.DataAccess.DB.Core.Types;
using Lfm.Domain.Common.Extensions;
using Lfm.Web.Mvc.App.UIRenderers.Models;
using Microsoft.AspNetCore.Http;

namespace Lfm.Web.Mvc.App.UIRenderers
{
    public static class UserCabinetUiRenderer
    {
        public static List<CabinetNavItem> GetCabinetNavigationItems(HttpContext context)
        {
            if (context.User.GetRole() == LfmIdentityRolesEnum.Mentor)
            {
                return MentorNavItems;
            }

            return new List<CabinetNavItem>();
        }

        public static string StudyingPlaceToString(StudyingPlaces place)
        {
            return place switch
            {
                StudyingPlaces.ONLINE_ONLY => "Only online",
                StudyingPlaces.OFFLINE_ONLY => "Only offline",
                StudyingPlaces.ONLINE_AND_OFFLINE => "Online and offline",
                _ => string.Empty
            };
        }
        
        public static List<StudyingPlaceCheckBoxInfo> StudyingPlacesCheckboxesInfo(StudyingPlaces? place)
        {
            string checkedAttr = "checked";
            List<StudyingPlaceCheckBoxInfo> checkboxInfo = new List<StudyingPlaceCheckBoxInfo>
            {
                new StudyingPlaceCheckBoxInfo
                {
                    Name = StudyingPlaceToString(StudyingPlaces.ONLINE_ONLY),
                    Value = StudyingPlaces.ONLINE_ONLY,
                    Checked = place == StudyingPlaces.ONLINE_ONLY ? checkedAttr : string.Empty
                },
                new StudyingPlaceCheckBoxInfo
                {
                    Name = StudyingPlaceToString(StudyingPlaces.OFFLINE_ONLY),
                    Value = StudyingPlaces.OFFLINE_ONLY,
                    Checked = place == StudyingPlaces.OFFLINE_ONLY ? checkedAttr : string.Empty
                },
                new StudyingPlaceCheckBoxInfo
                {
                    Name = StudyingPlaceToString(StudyingPlaces.ONLINE_AND_OFFLINE),
                    Value = StudyingPlaces.ONLINE_AND_OFFLINE,
                    Checked = place == StudyingPlaces.ONLINE_AND_OFFLINE ? checkedAttr : string.Empty
                }
            };

            return checkboxInfo;
        }

        private static List<CabinetNavItem> MentorNavItems =>
            new List<CabinetNavItem>
            {
                new CabinetNavItem
                {
                    Name = "General Info",
                    Link = "/userCabinet/generalInfo/"
                },
                new CabinetNavItem
                {
                    Name = "My subjects",
                    Link = "/userCabinet/subjectsInfo/"
                }
            };
    }
}