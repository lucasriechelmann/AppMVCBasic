using AppMVCBasic.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AppMVCBasic.UI.Controllers
{
    public abstract class BaseController : Controller
    {
        readonly INotifier _notifier;

        protected BaseController(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected bool IsRequestValid() => !_notifier.IsThereNotification();
    }
}
