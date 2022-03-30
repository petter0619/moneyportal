using Microsoft.AspNetCore.Mvc;
using MoneyPortalMain.Models;

namespace MoneyPortalMain.Views.Shared.Components.TransactionsTable
{
    public class TransactionsTableViewComponent : ViewComponent
    {

        public TransactionsTableViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync(List<Transaction> transactionsList)
        {
            return await Task.FromResult((IViewComponentResult)View("TransactionsTable", transactionsList));
        }
    }
}
