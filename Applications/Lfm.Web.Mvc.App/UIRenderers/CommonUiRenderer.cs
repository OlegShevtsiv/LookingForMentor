using System;
using System.Collections.Generic;
using LFM.DataAccess.DB.Core.Types;
using Lfm.Web.Mvc.App.UIRenderers.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Lfm.Web.Mvc.App.UIRenderers
{
    public static class CommonUiRenderer
    {
        public static string ActiveLink(HttpContext context, string controllerName, string actionName)
        {
            if (context.GetRouteValue("controller").ToString() == controllerName &&
                context.GetRouteValue("action").ToString() == actionName)
            {
                return "active";
            }
            return string.Empty;
        }
        
        public static string LessonDurationToString(LessonDuration? duration)
        {
            return duration switch
            {
                LessonDuration.ONE_HOUR => "1 hour",
                LessonDuration.ONE_HALF_HOUR => "1.5 hour",
                LessonDuration.TWO_HOURS => "2 hours",
                _ => "More than 2 hours"
            };
        }
        
        public static List<CheckBox<LessonDuration>> LessonDurationCheckboxes(LessonDuration? currentDuration)
        {
            string checkedAttr = "checked";
            List<CheckBox<LessonDuration>> checkboxInfo = new List<CheckBox<LessonDuration>>
            {
                new CheckBox<LessonDuration>
                {
                    Name = LessonDurationToString(LessonDuration.ONE_HOUR),
                    Value = LessonDuration.ONE_HOUR,
                    Checked = currentDuration == LessonDuration.ONE_HOUR ? checkedAttr : string.Empty
                },
                new CheckBox<LessonDuration>
                {
                    Name = LessonDurationToString(LessonDuration.ONE_HALF_HOUR),
                    Value = LessonDuration.ONE_HALF_HOUR,
                    Checked = currentDuration == LessonDuration.ONE_HALF_HOUR ? checkedAttr : string.Empty
                },
                new CheckBox<LessonDuration>
                {
                    Name = LessonDurationToString(LessonDuration.TWO_HOURS),
                    Value = LessonDuration.TWO_HOURS,
                    Checked = currentDuration == LessonDuration.TWO_HOURS ? checkedAttr : string.Empty
                }
                ,
                new CheckBox<LessonDuration>
                {
                    Name = LessonDurationToString(LessonDuration.MORE),
                    Value = LessonDuration.MORE,
                    Checked = currentDuration == LessonDuration.MORE ? checkedAttr : string.Empty
                }
            };

            return checkboxInfo;
        }
        
        public static string StudyingPlaceToString(StudyingPlaces? place)
        {
            return place switch
            {
                StudyingPlaces.ONLINE_ONLY => "Only online",
                StudyingPlaces.OFFLINE_ONLY => "Only offline",
                StudyingPlaces.ONLINE_AND_OFFLINE => "Online or offline",
                _ => string.Empty
            };
        }
        
        public static List<CheckBox<StudyingPlaces>> StudyingPlacesCheckboxes(StudyingPlaces? currentPlace)
        {
            string checkedAttr = "checked";
            List<CheckBox<StudyingPlaces>> checkboxInfo = new List<CheckBox<StudyingPlaces>>
            {
                new CheckBox<StudyingPlaces>
                {
                    Name = StudyingPlaceToString(StudyingPlaces.ONLINE_ONLY),
                    Value = StudyingPlaces.ONLINE_ONLY,
                    Checked = currentPlace == StudyingPlaces.ONLINE_ONLY ? checkedAttr : string.Empty
                },
                new CheckBox<StudyingPlaces>
                {
                    Name = StudyingPlaceToString(StudyingPlaces.OFFLINE_ONLY),
                    Value = StudyingPlaces.OFFLINE_ONLY,
                    Checked = currentPlace == StudyingPlaces.OFFLINE_ONLY ? checkedAttr : string.Empty
                },
                new CheckBox<StudyingPlaces>
                {
                    Name = StudyingPlaceToString(StudyingPlaces.ONLINE_AND_OFFLINE),
                    Value = StudyingPlaces.ONLINE_AND_OFFLINE,
                    Checked = currentPlace == StudyingPlaces.ONLINE_AND_OFFLINE ? checkedAttr : string.Empty
                }
            };

            return checkboxInfo;
        }
        
        public static List<SelectItem<StudyingPlaces>> StudyingPlacesSelectItems(StudyingPlaces? currentPlace)
        {
            string selectedAttr = "selected";
            List<SelectItem<StudyingPlaces>> checkboxInfo = new List<SelectItem<StudyingPlaces>>
            {
                new SelectItem<StudyingPlaces>
                {
                    Name = StudyingPlaceToString(StudyingPlaces.ONLINE_ONLY),
                    Value = StudyingPlaces.ONLINE_ONLY,
                    Selected = currentPlace == StudyingPlaces.ONLINE_ONLY ? selectedAttr : string.Empty
                },
                new SelectItem<StudyingPlaces>
                {
                    Name = StudyingPlaceToString(StudyingPlaces.OFFLINE_ONLY),
                    Value = StudyingPlaces.OFFLINE_ONLY,
                    Selected = currentPlace == StudyingPlaces.OFFLINE_ONLY ? selectedAttr : string.Empty
                },
                new SelectItem<StudyingPlaces>
                {
                    Name = StudyingPlaceToString(StudyingPlaces.ONLINE_AND_OFFLINE),
                    Value = StudyingPlaces.ONLINE_AND_OFFLINE,
                    Selected = currentPlace == StudyingPlaces.ONLINE_AND_OFFLINE ? selectedAttr : string.Empty
                }
            };

            return checkboxInfo;
        }

        public static string GetDaysPassed(DateTime dateTime)
        {
            if (DateTime.UtcNow < dateTime)
                return string.Empty;

            TimeSpan subtractedDate = DateTime.UtcNow.Subtract(dateTime);
            if (subtractedDate.Days >= 1)
                return $"{subtractedDate.Days} days ago.";

            if (subtractedDate.Hours >= 1)
            {
                return $"{subtractedDate.Hours} hours ago.";
            }

            return $"Less than hour ago.";
        }
    }
}