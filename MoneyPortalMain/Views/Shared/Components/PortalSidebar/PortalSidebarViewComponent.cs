using Microsoft.AspNetCore.Mvc;

namespace MoneyPortalMain.Views.Shared.Components.PortalSidebar
{
    public class PortalSidebarViewComponent : ViewComponent
    {
        List<string> Accounts = new();
        public PortalSidebarViewComponent()
        {
            Accounts = new List<string>()
            {
                "Test Account 1",
                "Test Account 2",
                "Test Account 3",
            };
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = Accounts;
            return await Task.FromResult((IViewComponentResult)View("PortalSidebar", model));
        }
    }
}
