using System.Collections.Generic;
using LFM.DataAccess.DB.Core.Types;
using Lfm.Domain.Common.Extensions;
using Lfm.Web.Mvc.App.UIRenderers.Models;
using Microsoft.AspNetCore.Http;

namespace Lfm.Web.Mvc.App.UIRenderers
{
    public static class UserCabinetUiRenderer
    {
        public static List<LinkItem> GetCabinetNavigationItems(HttpContext context)
        {
            if (context.User.GetRole() == LfmIdentityRolesEnum.Mentor)
            {
                return MentorNavItems;
            }
            if (context.User.GetRole() == LfmIdentityRolesEnum.Student)
            {
                return StudentNavItems;
            }

            return new List<LinkItem>();
        }

        private static List<LinkItem> MentorNavItems =>
            new List<LinkItem>
            {
                new LinkItem
                {
                    Name = "General Info",
                    ControllerName = "MentorUserCabinet",
                    ActionName = "MentorGeneralInfo"
                },
                new LinkItem
                {
                    Name = "My subjects",
                    ControllerName = "MentorUserCabinet",
                    ActionName = "MentorSubjectsInfo"
                },
                new LinkItem
                {
                    Name = "My personal orders requests",
                    ControllerName = "MentorUserCabinet",
                    ActionName = "MentorPersonalOrders"
                },
                new LinkItem
                {
                    Name = "My approved orders",
                    ControllerName = "MentorUserCabinet",
                    ActionName = "MentorApprovedOrders"
                }
            };
        
        private static List<LinkItem> StudentNavItems =>
            new List<LinkItem>
            {
                new LinkItem
                {
                    Name = "Find Mentors requests",
                    ControllerName = "StudentUserCabinet",
                    ActionName = "FindMentorsRequests"
                }
            };
    }
}