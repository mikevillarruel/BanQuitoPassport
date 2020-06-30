using System.Web;
using System.Web.Mvc;

namespace BanQuitoPassport
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new Filters.VerificaSession());
        }
    }
}
