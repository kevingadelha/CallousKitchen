#pragma checksum "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\CreateUser.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "20cff962b5ddb2a58931513228cb57089f987634"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_CreateUser), @"mvc.1.0.view", @"/Views/User/CreateUser.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\_ViewImports.cshtml"
using CallousFrontEnd;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\_ViewImports.cshtml"
using CallousFrontEnd.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"20cff962b5ddb2a58931513228cb57089f987634", @"/Views/User/CreateUser.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5cf196c9d4a9c560b9c0f8ac21c4a0baec03e187", @"/Views/_ViewImports.cshtml")]
    public class Views_User_CreateUser : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<AccountService.User>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("text/javascript"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/User/Settings.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n\r\n");
#nullable restore
#line 8 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\CreateUser.cshtml"
 using (Html.BeginForm("CreateUser", "User", FormMethod.Post))
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"form-group\">\r\n        ");
#nullable restore
#line 11 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\CreateUser.cshtml"
   Write(Html.LabelFor(x => x.Email, "Email:"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        ");
#nullable restore
#line 12 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\CreateUser.cshtml"
   Write(Html.TextBoxFor(x => x.Email, htmlAttributes: new { @class = "form-control", required="required" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n    <div class=\"form-group\">\r\n        ");
#nullable restore
#line 15 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\CreateUser.cshtml"
   Write(Html.LabelFor(x => x.Password, "Password:"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        ");
#nullable restore
#line 16 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\CreateUser.cshtml"
   Write(Html.PasswordFor(x => x.Password, htmlAttributes: new { @class = "form-control", required = "required" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n    <div class=\"form-group\">\r\n        ");
#nullable restore
#line 19 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\CreateUser.cshtml"
   Write(Html.Label("Diet: "));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
        <div class=""row"">
            <div class=""col-2"">
                <input class=""form-input"" type=""radio"" name=""Diet"" id=""rbOmnivore"" value=""Omnivore"" checked>
            </div>
            <div class=""col-10"">
                <label class=""form-label"" for=""rdOmnivore"">
                    Omnivore
                </label>
            </div>
        </div>
        <div class=""row"">
            <div class=""col-2"">
                <input class=""form-input"" type=""radio"" name=""Diet"" id=""rbVegetarian"" value=""Vegetarian"">
            </div>
            <div class=""col-10"">
                <label class=""form-label"" for=""rdVegetarian"">
                    Vegetarian
                </label>
            </div>
        </div>
        <div class=""row"">
            <div class=""col-2"">
                <input class=""form-input"" type=""radio"" name=""Diet"" id=""rbVegan"" value=""Vegan"">
            </div>
            <div class=""col-10"">
                <label class=""form-label"" for=""rdVegan"">
");
            WriteLiteral("                    Vegan\r\n                </label>\r\n            </div>\r\n        </div>\r\n    </div>\r\n    <div class=\"form-group\">\r\n        ");
#nullable restore
#line 52 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\CreateUser.cshtml"
   Write(Html.LabelFor(x => x.Allergies, "Allergies:"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 53 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\CreateUser.cshtml"
         for (int i = 0; i < CallousFrontEnd.Models.Allergies.GetAllergies().Length; i++)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"row\">\r\n                <div class=\"col-2\">\r\n                    <input type=\"checkbox\" class=\"form-input\"");
            BeginWriteAttribute("id", " id=\"", 2042, "\"", 2098, 1);
#nullable restore
#line 57 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\CreateUser.cshtml"
WriteAttributeValue("", 2047, CallousFrontEnd.Models.Allergies.GetAllergies()[i], 2047, 51, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("name", " name=\"", 2099, "\"", 2157, 1);
#nullable restore
#line 57 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\CreateUser.cshtml"
WriteAttributeValue("", 2106, CallousFrontEnd.Models.Allergies.GetAllergies()[i], 2106, 51, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                </div>\r\n                <div class=\"col-10\">\r\n                    <label class=\"form-label\">");
#nullable restore
#line 60 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\CreateUser.cshtml"
                                         Write(CallousFrontEnd.Models.Allergies.GetAllergiesLabels()[i]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\r\n                </div>\r\n            </div>\r\n");
#nullable restore
#line 63 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\CreateUser.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div class=\"row\">\r\n            <input type=\"text\" placeholder=\"Other\" class=\"form-control\" name=\"Other\" id=\"tbOther\" />\r\n        </div>\r\n    </div>\r\n    <button type=\"submit\" class=\"btn btn-primary\" id=\"btnSubmit\">Submit</button>\r\n");
#nullable restore
#line 69 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\CreateUser.cshtml"

}

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "20cff962b5ddb2a58931513228cb57089f9876349494", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<AccountService.User> Html { get; private set; }
    }
}
#pragma warning restore 1591
