using Microsoft.AspNetCore.Mvc;
using MoneyPortalMain.Services;

namespace MoneyPortalMain.Views.Shared.Components.PortalSidebar
{
    public class PortalSidebarViewComponent : ViewComponent
    {
        private readonly IUserService _userService;
        private readonly IAccountsService _accountsService;

        public PortalSidebarViewComponent(IUserService userService, IAccountsService accountsService)
        {
            _userService = userService;
            _accountsService = accountsService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewData["DisplayName"] = _userService.GetDisplayName();
            ViewData["DisplayImage"] = _userService.GetDisplayImage();

            var model = _accountsService.GetAllAccounts();
            return await Task.FromResult((IViewComponentResult)View("PortalSidebar", model));
        }
    }
}
