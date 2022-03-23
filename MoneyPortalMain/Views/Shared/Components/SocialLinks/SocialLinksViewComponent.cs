using Microsoft.AspNetCore.Mvc;
using MoneyPortalMain.Models;

namespace MoneyPortalMain
{
    public class SocialLinksViewComponent : ViewComponent
    {
        List<SocialIcon> socialIcons = new List<SocialIcon>();
        public SocialLinksViewComponent()
        {
            socialIcons = SocialIcon.AppSocialIcons();
        }

        public async Task<IViewComponentResult> InvokeAsync(int IconsToShow)
        {
            var model = socialIcons.Take(IconsToShow).ToList();
            return await Task.FromResult((IViewComponentResult)View("SocialLinks", model));
        }
    }
}
