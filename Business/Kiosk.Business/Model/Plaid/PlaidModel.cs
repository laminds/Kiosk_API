using Kiosk.Business.Helpers;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Business.Model.Plaid
{

    public class PlaidParams
    {
        public string client_id { get; set; }
        public string secret { get; set; }
        public string client_name { get; set; }
        public List<string> country_codes { get; set; }
        public string language { get; set; }
        public PlaidUser user { get; set; }
        public List<string> products { get; set; }
        public LinkAuth auth { get; set; }
    }

    public class PlaidUser
    {
        public string client_user_id { get; set; }
    }

    public class LinkAuth
    {
        public bool same_day_microdeposits_enabled { get; set; }
        public bool auth_type_select_enabled { get; set; }
    }

    public class AuthToken
    {
        public string publicToken { get; set; }
        public List<string> account_ids { get; set; }
        public string LinkToken_requestid { get; set; }
        public string Linktoken_expired { get; set; }
        public string link_session_id { get; set; }
        public int ClubNumber { get; set; }
        public string LinkToken { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PlanId { get; set; }
        public string PlanName { get; set; }
        public string PlanType { get; set; }
        public long PlaidID { get; set; }
        public string institution_id { get; set; }
    }

    public class AccessTokenParams
    {
        public string client_id { get; set; }
        public string secret { get; set; }
        public string public_token { get; set; }
    }

    public class AccessTokenModel
    {
        public string publicToken { get; set; }
        public string account_ids { get; set; }
        public string LinkToken_requestid { get; set; }
        public string Linktoken_expired { get; set; }
        public string link_session_id { get; set; }
        public int ClubNumber { get; set; }
        public string LinkToken { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PlanId { get; set; }
        public string PlanName { get; set; }
        public string PlanType { get; set; }
        public long PlaidID { get; set; }
        public string institution_id { get; set; }
    }

    public class AccessTokenResponse
    {
        public string access_token { get; set; }
        public string item_id { get; set; }
        public string request_id { get; set; }
        public string link_token { get; set; }
        public AccountDetails accountDetails { get; set; }
        public Root root { get; set; }
    }

    public class Root
    {
        public List<Account> accounts { get; set; }
        public Numbers numbers { get; set; }
        public Item item { get; set; }
        public string request_id { get; set; }
    }

    public class Numbers
    {
        public List<Ach> ach { get; set; }
        public List<Eft> eft { get; set; }
        public List<International> international { get; set; }
        public List<Bac> bacs { get; set; }
    }

    public class Ach
    {
        public string account { get; set; }
        public string account_id { get; set; }
        public string routing { get; set; }
        public string wire_routing { get; set; }
    }

    public class Bac
    {
        public string account { get; set; }
        public string account_id { get; set; }
        public string sort_code { get; set; }
    }


    public class Eft
    {
        public string account { get; set; }
        public string account_id { get; set; }
        public string institution { get; set; }
        public string branch { get; set; }
    }

    public class International
    {
        public string account_id { get; set; }
        public string bic { get; set; }
        public string iban { get; set; }
    }
    public class PlaidLinkResponse
    {
        public string expiration { get; set; }
        public string link_token { get; set; }
        public string request_id { get; set; }
        public string access_token { get; set; }
        public AccountDetails accountDetails { get; set; }
    }
    public class AccountDetails
    {
        public List<Account> accounts { get; set; }
        public Item item { get; set; }
        public string request_id { get; set; }
    }
    public class Account
    {
        public string account_id { get; set; }
        public Balances balances { get; set; }
        public string mask { get; set; }
        public string name { get; set; }
        public string official_name { get; set; }
        public string persistent_account_id { get; set; }
        public string subtype { get; set; }
        public string type { get; set; }
    }
    public class Balances
    {
        public double? available { get; set; }
        public double current { get; set; }
        public string iso_currency_code { get; set; }
        public object limit { get; set; }
        public object unofficial_currency_code { get; set; }
    }
    public class Item
    {
        public List<string> available_products { get; set; }
        public List<string> billed_products { get; set; }
        public object consent_expiration_time { get; set; }
        public object error { get; set; }
        public string institution_id { get; set; }
        public string item_id { get; set; }
        public string update_type { get; set; }
        public string webhook { get; set; }
    }
    public class AccountParams
    {
        public string client_id { get; set; }
        public string secret { get; set; }
        public string access_token { get; set; }
        public Options options { get; set; }
    }
    public class Options
    {
        public List<string> account_ids { get; set; }
    }

    public class PlaidLogTable
    {
        public long PlaidID { get; set; }
        public int? ClubNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PlanId { get; set; }
        public string PlanName { get; set; }
        public string PlanType { get; set; }
        public string link_token_expiration { get; set; }
        public string link_token { get; set; }
        public string link_token_request_id { get; set; }
        public string public_token { get; set; }
        public string link_session_id { get; set; }
        public string access_token { get; set; }
        public string access_token_request_id { get; set; }
        public string item_id { get; set; }
        public string plaid_response { get; set; }
        public string status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedOn { get; set; }

    }

    public class PlaidLogModel
    {
        public long LogID { get; set; }
        public long? PlaidID { get; set; }
        public string RequestURL { get; set; }
        public string RequestBody { get; set; }
        public string ResponseBody { get; set; }
        public string InnerException { get; set; }
        public string ExceptionMsg { get; set; }
        public string Status { get; set; }
        public string StatusCode { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
