using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlpineSkiHouse.TagHelpers
{
    [HtmlTargetElement("button", Attributes = "ash-login-provider")]
    public class LoginProviderButtonTagHelper : TagHelper
    {
        [HtmlAttributeName("ash-login-provider")]
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
