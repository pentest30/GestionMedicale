using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace Gm.UI.HtmlExtenssions
{
    public static class HtmlExtensions
    {

        public static MvcHtmlString DatePickerDropDowns(this HtmlHelper html,
            string dayName, string monthName, string yearName,
            int? beginYear = null, int? endYear = null,
            int? selectedDay = null, int? selectedMonth = null, int? selectedYear = null, bool localizeLabels = true, bool disabled = false)
        {
            var daysList = new TagBuilder("select");
            daysList.MergeAttribute("style", "width: 70px;height: 35px");
            var monthsList = new TagBuilder("select");
            monthsList.MergeAttribute("style", "width: 120px;height: 35px");
            var yearsList = new TagBuilder("select");
            yearsList.MergeAttribute("style", "width: 90px;height: 35px");

            daysList.Attributes.Add("name", dayName);
            monthsList.Attributes.Add("name", monthName);
            yearsList.Attributes.Add("name", yearName);
            daysList.Attributes.Add("id", dayName);
            monthsList.Attributes.Add("id", monthName);
            yearsList.Attributes.Add("id", yearName);

            daysList.Attributes.Add("class", "date-part");
            monthsList.Attributes.Add("class", "date-part");
            yearsList.Attributes.Add("class", "date-part");

            if (disabled)
            {
                daysList.Attributes.Add("disabled", "disabled");
                monthsList.Attributes.Add("disabled", "disabled");
                yearsList.Attributes.Add("disabled", "disabled");
            }

            var days = new StringBuilder();
            var months = new StringBuilder();
            var years = new StringBuilder();

            string dayLocale = "Jour";
            string monthLocale = "Mois";
            string yearLocale = "Année";

            days.AppendFormat("<option>{0}</option>", dayLocale);
            for (int i = 1; i <= 31; i++)
                days.AppendFormat("<option value='{0}'{1}>{0}</option>", i,
                    (selectedDay.HasValue && selectedDay.Value == i) ? " selected=\"selected\"" : null);


            months.AppendFormat("<option>{0}</option>", monthLocale);
            for (int i = 1; i <= 12; i++)
            {
                months.AppendFormat("<option value='{0}'{1}>{2}</option>",
                                    i,
                                    (selectedMonth.HasValue && selectedMonth.Value == i) ? " selected=\"selected\"" : null,
                                    CultureInfo.CurrentUICulture.DateTimeFormat.GetMonthName(i));
            }


            years.AppendFormat("<option>{0}</option>", yearLocale);

            if (beginYear == null)
                beginYear = DateTime.UtcNow.Year - 90;
            if (endYear == null)
                endYear = DateTime.UtcNow.Year + 10;

            for (int i = beginYear.Value; i <= endYear.Value; i++)
                years.AppendFormat("<option value='{0}'{1}>{0}</option>", i,
                    (selectedYear.HasValue && selectedYear.Value == i) ? " selected=\"selected\"" : null);

            daysList.InnerHtml = days.ToString();
            monthsList.InnerHtml = months.ToString();
            yearsList.InnerHtml = years.ToString();

            return MvcHtmlString.Create(string.Concat(daysList, monthsList, yearsList));
        }
        public static string FieldNameFor<T, TResult>(this HtmlHelper<T> html, Expression<Func<T, TResult>> expression)
        {
            return html.ViewData.TemplateInfo.GetFullHtmlFieldName(ExpressionHelper.GetExpressionText(expression));
        }
        public static MvcHtmlString Custom_DropdownList(this HtmlHelper helper, string name, IEnumerable<SelectListItem> list)
        {
            //This method in turns calls below overload.
            return Custom_DropdownList(helper, name, list, null);
        }

        //This overload is extension method accepts name, list and htmlAttributes as parameters.
        public static MvcHtmlString Custom_DropdownList(this HtmlHelper helper, string name, IEnumerable<SelectListItem> list, object htmlAttributes)
        {
            //Creating a select element using TagBuilder class which will create a dropdown.
            TagBuilder dropdown = new TagBuilder("select");
            //Setting the name and id attribute with name parameter passed to this method.
            dropdown.Attributes.Add("name", name);
            dropdown.Attributes.Add("id", name);

            //Created StringBuilder object to store option data fetched oen by one from list.
            StringBuilder options = new StringBuilder();
            //Iterated over the IEnumerable list.
            options = list.Aggregate(options, (current, item) => current.AppendFormat(!item.Selected ? "<option value='{0}' >{1}</option>" : "<option value='{0}' selected='{0}' >{1}</option>", item.Value, item.Text));
            //assigned all the options to the dropdown using innerHTML property.
            dropdown.InnerHtml = options.ToString();
            //Assigning the attributes passed as a htmlAttributes object.
            dropdown.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            //Returning the entire select or dropdown control in HTMLString format.
            return MvcHtmlString.Create(dropdown.ToString(TagRenderMode.Normal));
        }

    }
}