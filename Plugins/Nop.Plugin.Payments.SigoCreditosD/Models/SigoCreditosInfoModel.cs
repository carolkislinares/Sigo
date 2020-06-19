using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Models;

namespace Nop.Plugin.Payments.SigoCreditos.Models
{
    public class SigoCreditosInfoModel : BaseNopModel
    {
        public SigoCreditosInfoModel()
        {
            AccountsList = new List<SigoCreditosAccountsListModel>();
        }

        [NopResourceDisplayName("Account.Fields.FirstName")]
        public string FirstName { get; set; }

        [NopResourceDisplayName("Account.Fields.LastName")]
        public string LastName { get; set; }

        [NopResourceDisplayName("Account.SigoCreditos.BsAccountBalance")]
        public string BsAccountBalance { get; set; }

        [NopResourceDisplayName("Account.SigoCreditos.UsdAccountBalance")]
        public string UsdAccountBalance { get; set; }

        [NopResourceDisplayName("Account.SigoCreditos.AccountTypeList")]
        public IList<SigoCreditosAccountsListModel> AccountsList { get; set; }
    }
}