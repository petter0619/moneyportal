using MoneyPortalMain.DTOs;

namespace MoneyPortalMain.Models
{
    public class SidebarViewComponentModel
    {
        public List<Account> AccountsList { get; set; }
        public UserInfoDto CurrentUser { get; set; }
    }
}
