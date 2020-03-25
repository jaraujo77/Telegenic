using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace Telegenic.Web.Plumbing.MVCHelpers
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString YesNo(this HtmlHelper htmlHelper, bool YesNo)
        {
            var text = YesNo ? "Yes" : "No";
            return new MvcHtmlString(text);
        }

        public static MvcHtmlString YesNo<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);            
            var text = (bool)metadata.Model ? "Yes" : "No";
            return new MvcHtmlString(text);
        }
    }
}