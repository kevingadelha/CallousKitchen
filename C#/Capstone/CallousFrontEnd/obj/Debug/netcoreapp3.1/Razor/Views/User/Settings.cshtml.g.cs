#pragma checksum "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\Settings.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "59c82575b9a45b596829fd014aa4bb29852d2db1"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_Settings), @"mvc.1.0.view", @"/Views/User/Settings.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"59c82575b9a45b596829fd014aa4bb29852d2db1", @"/Views/User/Settings.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5cf196c9d4a9c560b9c0f8ac21c4a0baec03e187", @"/Views/_ViewImports.cshtml")]
    public class Views_User_Settings : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<AccountService.SerializableUser>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\Settings.cshtml"
  
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            WriteLiteral("<div class=\"row\">\r\n    <h3>\r\n        Account Settings\r\n    </h3>\r\n</div>\r\n<div class=\"form-group\">\r\n    <div class=\"row\">\r\n        ");
#nullable restore
#line 14 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\Settings.cshtml"
   Write(Html.Label("Diets:"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n    <div class=\"row\">\r\n        <div class=\"col-2\">\r\n            <input class=\"form-input\" type=\"radio\" name=\"Diet\" id=\"rbOmnivore\" value=\"Omnivore\" ");
#nullable restore
#line 18 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\Settings.cshtml"
                                                                                           Write(ViewBag.Selected[0]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@">
        </div>
        <div class=""col-10"">
            <label class=""form-label"" for=""rdOmnivore"">
                Omnivore
            </label>
        </div>
    </div>
    <div class=""row"">
        <div class=""col-2"">
            <input class=""form-input"" type=""radio"" name=""Diet"" id=""rbVegetarian"" value=""Vegetarian"" ");
#nullable restore
#line 28 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\Settings.cshtml"
                                                                                               Write(ViewBag.Selected[1]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@">
        </div>
        <div class=""col-10"">
            <label class=""form-label"" for=""rdVegetarian"">
                Vegetarian
            </label>
        </div>
    </div>
    <div class=""row"">
        <div class=""col-2"">
            <input class=""form-input"" type=""radio"" name=""Diet"" id=""rbVegan"" value=""Vegan"" ");
#nullable restore
#line 38 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\Settings.cshtml"
                                                                                     Write(ViewBag.Selected[2]);

#line default
#line hidden
#nullable disable
            WriteLiteral(">\r\n        </div>\r\n        <div class=\"col-10\">\r\n            <label class=\"form-label\" for=\"rdVegan\">\r\n                Vegan\r\n            </label>\r\n        </div>\r\n    </div>\r\n</div>\r\n<div class=\"form-group\">\r\n    ");
#nullable restore
#line 48 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\Settings.cshtml"
Write(Html.LabelFor(x => x.Allergies, "Allergies:"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 49 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\Settings.cshtml"
     for (int i = 0; i < CallousFrontEnd.Models.Allergies.GetAllergies().Length; i++)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div class=\"row\">\r\n            <div class=\"col-2\">\r\n                <input type=\"checkbox\" class=\"form-input\"");
            BeginWriteAttribute("id", " id=\"", 1607, "\"", 1663, 1);
#nullable restore
#line 53 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\Settings.cshtml"
WriteAttributeValue("", 1612, CallousFrontEnd.Models.Allergies.GetAllergies()[i], 1612, 51, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("name", " name=\"", 1664, "\"", 1722, 1);
#nullable restore
#line 53 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\Settings.cshtml"
WriteAttributeValue("", 1671, CallousFrontEnd.Models.Allergies.GetAllergies()[i], 1671, 51, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" ");
#nullable restore
#line 53 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\Settings.cshtml"
                                                                                                                                                                         Write(ViewBag.Checked[i]);

#line default
#line hidden
#nullable disable
            WriteLiteral(">\r\n            </div>\r\n            <div class=\"col-10\">\r\n                <label class=\"form-label\">");
#nullable restore
#line 56 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\Settings.cshtml"
                                     Write(CallousFrontEnd.Models.Allergies.GetAllergiesLabels()[i]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\r\n            </div>\r\n        </div>\r\n");
#nullable restore
#line 59 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\Settings.cshtml"

    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n<div class=\"row\">\r\n    <button class=\"btn btn-sm btn-primary\">Save Changes</button>\r\n</div>\r\n<div class=\"row\">\r\n    <span id=\"result\"></span>\r\n</div>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<AccountService.SerializableUser> Html { get; private set; }
    }
}
#pragma warning restore 1591
