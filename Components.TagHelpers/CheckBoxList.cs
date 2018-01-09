using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Components.TagHelpers
{
    [HtmlTargetElement("checkboxlist", Attributes = check_items)]
    [HtmlTargetElement("checkboxlist", Attributes = check_name)]
    [HtmlTargetElement("checkboxlist", Attributes = asp_for)]
    public class CheckBoxList: TagHelper
    {
        protected const string check_items = "asp-items";        
        protected const string check_name = "list";
        protected const string asp_for = "asp-for";

        [HtmlAttributeName(asp_for)]
        public ModelExpression AspFor { get; set; }

        [HtmlAttributeName(check_items)]
        public IEnumerable<SelectListItem> AspItems { get; set; }

        [HtmlAttributeName(check_name)]
        public string Name { get; set; } = "name";       

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            Render(context, output);
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var childContent = await output.GetChildContentAsync();
            if (childContent.IsEmptyOrWhiteSpace)
            {
                Render(context, output);
            }
        }

        protected void Render(TagHelperContext context, TagHelperOutput output)
        {
            
            output.TagMode = TagMode.StartTagAndEndTag;
            output.TagName = "p";
            if (AspItems != null)
            {
                foreach (SelectListItem item in AspItems)
                {
                    var _html = $"<input type=\"checkbox\" name=\"{Name}\" value=\"{item.Value}\"/> {item.Text}";
                    output.Content.AppendHtml("<div>");
                    output.Content.AppendHtml(_html);
                    output.Content.AppendHtml("</div>");
                }
            }
        }
    }
}
