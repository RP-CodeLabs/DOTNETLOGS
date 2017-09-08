using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DOTNET.LOGS.Shared;

namespace DOTNET.LOGS.Models.Extensions
{
    public static class CustomEnumHelper
    {
        public static IList<SelectListItem> GetSelectList<T>(this IEnumerable<T> customEnums) where T : CustomEnum<T>
        {
            var selectListItems = new List<SelectListItem>();
            selectListItems.AddRange(customEnums.Select(x => new SelectListItem()
            {
                Text = x.Description,
                Value = x.Key
            }));
            return selectListItems;
        }
    }
}