using MoneyPortalMain.DTOs;

namespace MoneyPortalMain.Models
{
    public class SidebarViewComponentModel
    {
        public List<AccountDto> AccountsList { get; set; }
        public UserInfoDto CurrentUser { get; set; }
    }
}
