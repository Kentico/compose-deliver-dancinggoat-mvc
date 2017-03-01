using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace DeliverDancingGoatMVC.Helpers
{
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Returns HTML input element with a label and validation fields for each property in the object that is represented by the System.Linq.Expressions.Expression expression.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="html">The HTML helper instance that this method extends</param>
        /// <param name="expression">An expression that identifies the object that contains the properties to display</param>
        public static MvcHtmlString ValidatedEditorFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var label = html.LabelFor(expression).ToString();
            var editor = html.EditorFor(expression).ToString();
            var message = html.ValidationMessageFor(expression).ToString();

            var generatedHtml = string.Format(@"
<div class=""form-group"">
    <div class=""form-group-label"">{0}</div>
    <div class=""form-group-input"">{1}</div>
    <div class=""message message-error"">{2}</div>
</div>", label, editor, message);

            return MvcHtmlString.Create(generatedHtml);
        }
    }
}