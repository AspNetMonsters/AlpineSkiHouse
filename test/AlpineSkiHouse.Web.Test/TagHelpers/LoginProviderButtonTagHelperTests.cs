using AlpineSkiHouse.TagHelpers;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AlpineSkiHouse.Web.Tests.TagHelpers
{
    public class LoginProviderButtonTagHelperTests
    {
        public class WhenTargettingAnEmptyButtonTag
        {
            TagHelperContext _context;
            TagHelperOutput _output;
            AuthenticationDescription _loginProvider;
            LoginProviderButtonTagHelper _tagHelper;

            public WhenTargettingAnEmptyButtonTag()
            {
                _loginProvider = new AuthenticationDescription
                {
                    DisplayName = "This is the display name",
                    AuthenticationScheme = "This is the scheme"
                };

                _tagHelper = new LoginProviderButtonTagHelper()
                {
                    LoginProvider = _loginProvider
                };

                _context = GetTagHelperContext();
                _output = GetTagHelperOutput();
            }

            [Fact]
            public void TheTypeAttributeShouldBeSetToSubmit()
            {
                _tagHelper.Process(_context, _output);

                Assert.True(_output.Attributes.ContainsName("type"));
                Assert.Equal("submit", _output.Attributes["type"].Value);
            }

            [Fact]
            public void TheNameAttributeShouldBeSetToProvider()
            {
                _tagHelper.Process(_context, _output);

                Assert.True(_output.Attributes.ContainsName("name"));
                Assert.Equal("provider", _output.Attributes["name"].Value);
            }

            [Fact]
            public void TheValueAttributeShouldBeTheAuthenticationScheme()
            {
                _tagHelper.Process(_context, _output);

                Assert.True(_output.Attributes.ContainsName("value"));
                Assert.Equal(_loginProvider.AuthenticationScheme, _output.Attributes["value"].Value);
            }

            [Fact]
            public void TheTitleAttributeShouldContainTheDisplayName()
            {
                _tagHelper.Process(_context, _output);

                Assert.True(_output.Attributes.ContainsName("title"));
                Assert.Contains(_loginProvider.DisplayName, _output.Attributes["title"].Value.ToString());
            }

            [Fact]
            public void TheContentsShouldBeSetToTheAuthenticationScheme()
            {
                _tagHelper.Process(_context, _output);

                Assert.Equal(_loginProvider.AuthenticationScheme, _output.Content.GetContent());
            }
        }


        private static TagHelperContext GetTagHelperContext(string id = "testid")
        {
            return new TagHelperContext(
                allAttributes: new TagHelperAttributeList(),
                items: new Dictionary<object, object>(),
                uniqueId: id);
        }

        private static TagHelperOutput GetTagHelperOutput(
            string tagName = "button",
            TagHelperAttributeList attributes = null,
            string childContent = "")
        {
            attributes = attributes ?? new TagHelperAttributeList();
            return new TagHelperOutput(
                tagName,
                attributes,
                getChildContentAsync: (useCachedResult, encoder) =>
                {
                    var tagHelperContent = new DefaultTagHelperContent();
                    tagHelperContent.SetHtmlContent(childContent);
                    return Task.FromResult<TagHelperContent>(tagHelperContent);
                });
        }
    }
}
