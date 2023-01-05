using Microsoft.AspNetCore.Razor.TagHelpers;

namespace AppMVCBasic.UI.Extensions
{
    [HtmlTargetElement("*", Attributes = "supress-by-action")]
    public class HideElementByActionTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public HideElementByActionTagHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        [HtmlAttributeName("supress-by-action")]
        public string ActionName { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            var action = _contextAccessor.HttpContext.GetRouteData().Values["action"].ToString();

            if (ActionName.Contains(action)) return;

            output.SuppressOutput();
        }
    }
}
