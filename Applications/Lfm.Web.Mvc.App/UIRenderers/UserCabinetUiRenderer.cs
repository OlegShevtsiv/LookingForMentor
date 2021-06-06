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
                    Name = "My personal orders",
                    ControllerName = "MentorUserCabinet",
                    ActionName = "MentorPersonalOrders"
                }
            };
    }
}