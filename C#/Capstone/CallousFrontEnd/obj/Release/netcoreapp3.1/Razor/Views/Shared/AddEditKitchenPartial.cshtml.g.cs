#pragma checksum "C:\Users\kgsos\Documents\GitHub\CallousHippo\C#\Capstone\CallousFrontEnd\Views\Shared\AddEditKitchenPartial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "fc7184ed3860b6c50ca93a54d5c0c8251d3e99bc"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_AddEditKitchenPartial), @"mvc.1.0.view", @"/Views/Shared/AddEditKitchenPartial.cshtml")]
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
#line 1 "C:\Users\kgsos\Documents\GitHub\CallousHippo\C#\Capstone\CallousFrontEnd\Views\_ViewImports.cshtml"
using CallousFrontEnd;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\kgsos\Documents\GitHub\CallousHippo\C#\Capstone\CallousFrontEnd\Views\_ViewImports.cshtml"
using CallousFrontEnd.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fc7184ed3860b6c50ca93a54d5c0c8251d3e99bc", @"/Views/Shared/AddEditKitchenPartial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5cf196c9d4a9c560b9c0f8ac21c4a0baec03e187", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_AddEditKitchenPartial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<CallousFrontEnd.Models.KitchenUser>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\kgsos\Documents\GitHub\CallousHippo\C#\Capstone\CallousFrontEnd\Views\Shared\AddEditKitchenPartial.cshtml"
 using (Html.BeginForm("AddEditKitchen", "User", FormMethod.Post))
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"form-group\">\r\n        ");
#nullable restore
#line 6 "C:\Users\kgsos\Documents\GitHub\CallousHippo\C#\Capstone\CallousFrontEnd\Views\Shared\AddEditKitchenPartial.cshtml"
   Write(Html.LabelFor(x => x.kitchen.Name, htmlAttributes: new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        ");
#nullable restore
#line 7 "C:\Users\kgsos\Documents\GitHub\CallousHippo\C#\Capstone\CallousFrontEnd\Views\Shared\AddEditKitchenPartial.cshtml"
   Write(Html.TextBoxFor(x => x.kitchen.Name, htmlAttributes: new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n");
#nullable restore
#line 9 "C:\Users\kgsos\Documents\GitHub\CallousHippo\C#\Capstone\CallousFrontEnd\Views\Shared\AddEditKitchenPartial.cshtml"
 if (Model.kitchen.Id == 0)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <button type=\"submit\" class=\"btn btn-primary\">Add Kitchen</button>\r\n");
#nullable restore
#line 12 "C:\Users\kgsos\Documents\GitHub\CallousHippo\C#\Capstone\CallousFrontEnd\Views\Shared\AddEditKitchenPartial.cshtml"
}
else
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <button type=\"submit\" class=\"btn btn-primary\">Edit Kitchen</button>\r\n");
#nullable restore
#line 16 "C:\Users\kgsos\Documents\GitHub\CallousHippo\C#\Capstone\CallousFrontEnd\Views\Shared\AddEditKitchenPartial.cshtml"
}

#line default
#line hidden
#nullable disable
#nullable restore
#line 16 "C:\Users\kgsos\Documents\GitHub\CallousHippo\C#\Capstone\CallousFrontEnd\Views\Shared\AddEditKitchenPartial.cshtml"
 
}

#line default
#line hidden
#nullable disable
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<CallousFrontEnd.Models.KitchenUser> Html { get; private set; }
    }
}
#pragma warning restore 1591
