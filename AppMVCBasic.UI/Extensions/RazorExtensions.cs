using Microsoft.AspNetCore.Mvc.Razor;

namespace AppMVCBasic.UI.Extensions
{
    public static class RazorExtensions
    {
        public static string FormatBrazilianDocument(this RazorPage page, int typeSupplier, string document)
        {
            return typeSupplier == 1 ? Convert.ToUInt64(document).ToString(@"000\.000\.000\-00") : 
                Convert.ToUInt64(document).ToString(@"00\.000\.000\/0000\-00");
        }
    }
}
