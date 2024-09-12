using Kiosk.Business.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Business.Extension
{
    public static class RemoveState_ClubPrefixExtension
    {
        public static string GetPlainPlan(this string recurringServicePlanName)
        {
            var statePrefixesList = ABC.StatePrefixes.ToString().Split(',');
            var clubPrefixesList = ABC.ClubPrefixes.ToString().Split(',');
            var RemoveWordsPTPlan = ABC.RemoveWordsPTPlan;

            var stateMatch = statePrefixesList.FirstOrDefault(stringToCheck => recurringServicePlanName.Contains(" " + stringToCheck + "") || recurringServicePlanName.StartsWith(stringToCheck));

            if (!string.IsNullOrEmpty(stateMatch))
            {
                recurringServicePlanName = recurringServicePlanName.Replace(stateMatch, "");
                recurringServicePlanName = recurringServicePlanName.Replace(RemoveWordsPTPlan, "");
            }

            var clubMatch = clubPrefixesList.FirstOrDefault(stringToCheck => recurringServicePlanName.Contains(" " + stringToCheck + "") || recurringServicePlanName.StartsWith(stringToCheck));

            if (!string.IsNullOrEmpty(clubMatch))
            {
                recurringServicePlanName = recurringServicePlanName.Replace(clubMatch, "");
                recurringServicePlanName = recurringServicePlanName.Replace(RemoveWordsPTPlan, "");
            }

            return recurringServicePlanName.Trim();
        }


    }
}
