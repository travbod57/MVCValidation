using System.Linq.Expressions;

namespace System.Web.Mvc.Html
{
    public static class HTMLHelpers
    {
        public static MvcHtmlString AJAXValidationFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, object htmlAttributes = null)
        {
            var data = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            string propertyName = data.PropertyName;
            TagBuilder span = new TagBuilder("span");
            span.Attributes.Add("id", string.Format("validation-message-{0}", propertyName));
            span.Attributes.Add("class", "field-validation-error");

            if (htmlAttributes != null)
            {
                var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                span.MergeAttributes(attributes);
            }

            return new MvcHtmlString(span.ToString());
        }

        public static MvcHtmlString ClearButtonAJAX(this HtmlHelper helper, object htmlAttributes = null)
        {
            TagBuilder input = new TagBuilder("input");
            input.Attributes.Add("type", "button");
            input.Attributes.Add("onClick", "ajaxFormValidation.Clear(this)");
            input.Attributes.Add("Value", "Clear");

            if (htmlAttributes != null)
            {
                var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                input.MergeAttributes(attributes);
            }

            return new MvcHtmlString(input.ToString());
        }

        public static MvcHtmlString ValidationSummaryAJAX(this HtmlHelper helper, object htmlAttributes = null)
        {
            TagBuilder ul = new TagBuilder("ul");
            ul.Attributes.Add("id", "validation");

            if (htmlAttributes != null)
            {
                var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                ul.MergeAttributes(attributes);
            }

            return new MvcHtmlString(ul.ToString());
        }
    }
}