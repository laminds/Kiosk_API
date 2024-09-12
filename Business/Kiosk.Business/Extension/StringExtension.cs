using Kiosk.Business.Helpers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Kiosk.Business.Extension
{
    public static class StringExtension
    {
        public static Logger _logger;
        public static string GetString(this string StringValue)
        {
            try
            {
                return Regex.Replace(Regex.Replace(Regex.Replace(StringValue, @"[^a-zA-Z0-9\s]+", ""), @"[\r|\n|\t]", "").Trim(), @"^[\d\s-]*", "");
            }
            catch (Exception e)
            {
                _logger.Log("GetString : " + e.Message);
                return e.Message;
            }
        }

        public static string GetDraftAccountType(this string StringValue)
        {
            try
            {
                string savingString = "";
                if (StringValue.Contains("Consumer -"))
                {
                    savingString = Regex.Replace(StringValue, @"(Consumer\s-)\s+", "").ToLower();
                }
                else if (StringValue.Contains("Corporate -"))
                {
                    savingString = Regex.Replace(StringValue, @"(Corporate\s-)\s+", "").ToLower();
                }
                else if(StringValue.Contains("Checking"))
                {
                    savingString = "checking";
                }
                else
                {
                    savingString = savingString == "saving" ? savingString + "s" : savingString;
                }
                if (savingString.Contains("saving"))
                {
                    savingString =  savingString + "s";
                }
                return savingString;
            }
            catch (Exception e)
            {
                _logger.Log("GetDraftAccountType : " + e.Message);
                return e.Message;
            }
        }

        public static string GetValidPhoneNumber(this string StringValue)
        {
            try
            {
                if (StringValue.StartsWith("+1"))
                {
                    StringValue = Regex.Replace(StringValue, @"[^\d]+1", "");
                }
                else
                {
                    StringValue = Regex.Replace(StringValue, @"[^\d]+", "");
                }
                return StringValue.Length > 10 ? StringValue.Substring(StringValue.Length - 10, 10) : StringValue;
            }
            catch (Exception e)
            {
                _logger.Log("GetValidPhoneNumber : " + e.Message);
                return e.Message;
            }
        }

        public static string GetMaskAccountNumber(this string StringValue)
        {
            try
            {
                return string.Format("{0}", new string('X', StringValue.Length - 4) + StringValue.Substring(StringValue.Length - 4, 4));
            }
            catch (Exception ex)
            {
                _logger.Log("GetMaskAccountNumber : " + ex.Message);
                return ex.Message;
            }
        }

        public static string Escape(this string StringValue)
        {
            if (StringValue == null)
            {
                return null;
            }
            return StringValue.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'", "&apos;");
        }
    }
}
