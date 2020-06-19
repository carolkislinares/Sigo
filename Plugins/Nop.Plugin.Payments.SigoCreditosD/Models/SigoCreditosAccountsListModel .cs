﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Models;

namespace Nop.Plugin.Payments.SigoCreditos.Models
{
    public class SigoCreditosAccountsListModel : BaseNopModel
    {
        public SigoCreditosAccountsListModel() { }

        [NopResourceDisplayName("Account.SigoCreditos.AccountType.Name")]
        public string AccountTypeName { get; set; }

        [NopResourceDisplayName("Account.SigoCreditos.AccountType.Quantity")]
        public string AccountTypeQuantity { get; set; }

        [NopResourceDisplayName("Account.SigoCreditos.AccountType.Currency")]
        public string AccountTypeCurrency { get; set; }
    }
}