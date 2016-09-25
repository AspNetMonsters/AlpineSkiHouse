using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace AlpineSkiHouse.TagHelpers
{
    [HtmlTargetElement("button", Attributes = "ski-login-provider")]
    public class LoginProviderButtonTagHelper : TagHelper
    {
        [HtmlAttributeName("ski-login-provider")]
        public AuthenticationDescription LoginProvider { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.SetAttribute("type", "submit");
            output.Attributes.SetAttribute("name", "provider");
            output.Attributes.SetAttribute("value", LoginProvider.AuthenticationScheme);
            output.Attributes.SetAttribute("title", $"Log in using your {LoginProvider.DisplayName} account");

            output.Attributes.MergeClassAttributeValue("btn btn-default");            

            output.Content.SetContent(LoginProvider.AuthenticationScheme);
        }
    }
}
