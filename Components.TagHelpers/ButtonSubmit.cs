using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

//http://netcoders.com.br/tag-helpers-customizadas-asp-net-5/
//https://www.davepaquette.com/archive/2015/06/22/creating-custom-mvc-6-tag-helpers.aspx
//http://coremvc.com.br/listas-dropdown-usando-o-select-tag-helper-no-core-mvc/

namespace Components.TagHelpers
{
    [HtmlTargetElement("submit", Attributes = Button_Disabled)]
    [HtmlTargetElement("submit", Attributes = Button_Label)]
    [HtmlTargetElement("submit", Attributes = Button_ClassCss)]
    [HtmlTargetElement("submit", Attributes = Asp_For)]
    public class ButtonSubmit : TagHelper
    {
        protected const string Button_Disabled = "button-disabled";
        protected const string Button_Label = "button-label";
        protected const string Button_ClassCss = "button-class-css";
        protected const string Asp_For = "asp-for";

        [HtmlAttributeName(Asp_For)]
        public ModelExpression AspFor { get; set; }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        [HtmlAttributeName(Button_Disabled)]
        public bool Disabled { get; set; } = false;

        [HtmlAttributeName(Button_Label)]
        public string Label { get; set; } = "Submeter";

        [HtmlAttributeName(Button_ClassCss)]
        public string ClassCss { get; set; } = "";

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
            output.TagName = "button";
            output.Attributes.Add("type", "submit");
            if (Disabled) output.Attributes.Add("disabled", "disabled");
            if (string.IsNullOrEmpty(Label)) Label = "Submeter";            
            if (!string.IsNullOrEmpty(ClassCss))
            {
                output.Attributes.Add("class", ClassCss);
            }
            output.Content.SetContent(Label);
            output.TagMode = TagMode.StartTagAndEndTag;            
        }

    }
}
