using AppMVCBasic.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AppMVCBasic.UI.Extensions
{
    public class SummaryViewComponent : ViewComponent
    {
        readonly INotifier _notifier;

        public SummaryViewComponent(INotifier notifier)
        {
            _notifier = notifier;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var notification = await Task.FromResult(_notifier.GetNotifications());

            notification.ForEach(n =>
            {
                ViewData.ModelState.AddModelError(string.Empty, n.Message);
            });
            return View();
        }
    }
}
