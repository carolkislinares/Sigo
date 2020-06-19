using System;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Payments.SigoCreditos.Models;
using Nop.Services;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Payments.SigoCreditos.Controllers
{
    [AuthorizeAdmin]
    [Area(AreaNames.Admin)]
    public class SigoCreditosInfoController : BasePaymentController
    {
        #region Fields
        
        private readonly ILocalizationService _localizationService;
        private readonly IPermissionService _permissionService;
        private readonly ISettingService _settingService;
        private readonly IStoreContext _storeContext;

        #endregion

        #region Ctor

        public SigoCreditosInfoController(ILocalizationService localizationService,
            IPermissionService permissionService,
            ISettingService settingService,
            IStoreContext storeContext)
        {
            this._localizationService = localizationService;
            this._permissionService = permissionService;
            this._settingService = settingService;
            this._storeContext = storeContext;
        }

        #endregion

        #region Methods

        public IActionResult Configure()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManagePaymentMethods))
                return AccessDeniedView();

            //load settings for a chosen store scope
            var storeScope = _storeContext.ActiveStoreScopeConfiguration;
            var SigoCreditosPaymentSettings = _settingService.LoadSetting<SigoCreditosPaymentSettings>(storeScope);

            var model = new ConfigurationModel
            {
                TransactModeId = Convert.ToInt32(SigoCreditosPaymentSettings.TransactMode),
                AdditionalFee = SigoCreditosPaymentSettings.AdditionalFee,
                AdditionalFeePercentage = SigoCreditosPaymentSettings.AdditionalFeePercentage,
                TransactModeValues = SigoCreditosPaymentSettings.TransactMode.ToSelectList(),
                ActiveStoreScopeConfiguration = storeScope
            };
            if (storeScope > 0)
            {
                model.TransactModeId_OverrideForStore = _settingService.SettingExists(SigoCreditosPaymentSettings, x => x.TransactMode, storeScope);
                model.AdditionalFee_OverrideForStore = _settingService.SettingExists(SigoCreditosPaymentSettings, x => x.AdditionalFee, storeScope);
                model.AdditionalFeePercentage_OverrideForStore = _settingService.SettingExists(SigoCreditosPaymentSettings, x => x.AdditionalFeePercentage, storeScope);
            }

            return View("~/Plugins/Payments.SigoCreditos/Views/Configure.cshtml", model);
        }

        [HttpPost]
        [AdminAntiForgery]
        public IActionResult Configure(ConfigurationModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManagePaymentMethods))
                return AccessDeniedView();

            if (!ModelState.IsValid)
                return Configure();

            //load settings for a chosen store scope
            var storeScope = _storeContext.ActiveStoreScopeConfiguration;
            var SigoCreditosPaymentSettings = _settingService.LoadSetting<SigoCreditosPaymentSettings>(storeScope);

            //save settings
            SigoCreditosPaymentSettings.TransactMode = (TransactMode)model.TransactModeId;
            SigoCreditosPaymentSettings.AdditionalFee = model.AdditionalFee;
            SigoCreditosPaymentSettings.AdditionalFeePercentage = model.AdditionalFeePercentage;

            /* We do not clear cache after each setting update.
             * This behavior can increase performance because cached settings will not be cleared 
             * and loaded from database after each update */

            _settingService.SaveSettingOverridablePerStore(SigoCreditosPaymentSettings, x => x.TransactMode, model.TransactModeId_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(SigoCreditosPaymentSettings, x => x.AdditionalFee, model.AdditionalFee_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(SigoCreditosPaymentSettings, x => x.AdditionalFeePercentage, model.AdditionalFeePercentage_OverrideForStore, storeScope, false);
            
            //now clear settings cache
            _settingService.ClearCache();

            SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));

            return Configure();
        }

        #endregion
    }
}