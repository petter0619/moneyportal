using Microsoft.AspNetCore.Mvc;
using MoneyPortalMain.DTOs;
using MoneyPortalMain.Models;

namespace MoneyPortalMain.Views.Shared.Components.PortalSidebar
{
    public class PortalSidebarViewComponent : ViewComponent
    {
        List<AccountDto> AccountsList = new List<AccountDto>();

        public PortalSidebarViewComponent()
        {
            AccountsList = new List<AccountDto>()
            {
                new AccountDto() { Id = 1, Name = "Skandia", Type = "Checking Account" },
                new AccountDto() { Id = 2, Name = "ICA", Type = "Savings Account" },
                new AccountDto() { Id = 4, Name = "Cash", Type = "Cash" },
                new AccountDto() { Id = 5, Name = "Coop Kreditkort", Type = "Credit Card" },
                new AccountDto() { Id = 3, Name = "Revolut", Type = "Savings Account" }
            };
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = AccountsList;
            return await Task.FromResult((IViewComponentResult)View("PortalSidebar", model));
        }
    }
}
