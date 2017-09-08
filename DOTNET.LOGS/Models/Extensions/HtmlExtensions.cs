using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using DOTNET.LOGS.Shared;

namespace DOTNET.LOGS.Models.Extensions
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString SmartDropdownListFor<TModel, TEnvironmentFor>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TEnvironmentFor>> expression,
            Expression<Func<TModel, string>> expressionName, string optionLabel, object htmlAttributes)
            where TEnvironmentFor : CustomEnum<TEnvironmentFor>
        {
            var modelName = ExpressionHelper.GetExpressionText(expressionName);
            var fullHtmlFieldName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(modelName);
            var selectList = CustomEnum<TEnvironmentFor>.GetAll().GetSelectList();
            if (!string.IsNullOrEmpty(optionLabel) && selectList.Count != 0)
            {
                selectList[0].Text = optionLabel;
            }
            return htmlHelper.DropDownList(fullHtmlFieldName, selectList, htmlAttributes);
        }
    }
}