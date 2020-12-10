#pragma checksum "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\Shared\AddEditFoodPartial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "eec4dc453bdf3a9902dfeb432a81bd7a57c9d092"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_AddEditFoodPartial), @"mvc.1.0.view", @"/Views/Shared/AddEditFoodPartial.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"eec4dc453bdf3a9902dfeb432a81bd7a57c9d092", @"/Views/Shared/AddEditFoodPartial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5cf196c9d4a9c560b9c0f8ac21c4a0baec03e187", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_AddEditFoodPartial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<CallousFrontEnd.Models.FoodKitchen>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 6 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\Shared\AddEditFoodPartial.cshtml"
 using (Html.BeginForm("AddEditFood", "User", FormMethod.Post))

{
    

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\Shared\AddEditFoodPartial.cshtml"
Write(Html.HiddenFor(x => x.KitchenId));

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\Shared\AddEditFoodPartial.cshtml"
Write(Html.HiddenFor(x => x.Food.Id));

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\Shared\AddEditFoodPartial.cshtml"
Write(Html.HiddenFor(x => x.Food.OnShoppingList));

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\Shared\AddEditFoodPartial.cshtml"
Write(Html.HiddenFor(x => x.Food.Traces));

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\Shared\AddEditFoodPartial.cshtml"
Write(Html.HiddenFor(x => x.Food.Ingredients));

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"form-group\">\r\n        ");
#nullable restore
#line 17 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\Shared\AddEditFoodPartial.cshtml"
   Write(Html.LabelFor(x => x.Food.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        ");
#nullable restore
#line 18 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\Shared\AddEditFoodPartial.cshtml"
   Write(Html.TextBoxFor(x => x.Food.Name, htmlAttributes: new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n    <div class=\"form-group\">\r\n        ");
#nullable restore
#line 21 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\Shared\AddEditFoodPartial.cshtml"
   Write(Html.LabelFor(x => x.Food.Quantity));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        ");
#nullable restore
#line 22 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\Shared\AddEditFoodPartial.cshtml"
   Write(Html.TextBoxFor(x => x.Food.Quantity, htmlAttributes: new { @class = "form-control", @type = "number" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n    <div class=\"form-group\">\r\n        ");
#nullable restore
#line 25 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\Shared\AddEditFoodPartial.cshtml"
   Write(Html.LabelFor(x => x.Food.ExpiryDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        ");
#nullable restore
#line 26 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\Shared\AddEditFoodPartial.cshtml"
   Write(Html.TextBoxFor(x => x.Food.ExpiryDate, "{0:yyyy-MM-dd}", htmlAttributes: new { @class = "form-control", @type = "date" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n    <div class=\"form-group\">\r\n        ");
#nullable restore
#line 29 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\Shared\AddEditFoodPartial.cshtml"
   Write(Html.LabelFor(x => x.Food.QuantityClassifier, "Quantity Classifier"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        ");
#nullable restore
#line 30 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\Shared\AddEditFoodPartial.cshtml"
   Write(Html.DropDownListFor(x => x.Food.QuantityClassifier, new SelectList(ViewBag.Classifier), htmlAttributes: new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n    <div class=\"form-group\">\r\n        ");
#nullable restore
#line 33 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\Shared\AddEditFoodPartial.cshtml"
   Write(Html.LabelFor(x => x.Food.Storage, "Storage"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        ");
#nullable restore
#line 34 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\Shared\AddEditFoodPartial.cshtml"
   Write(Html.DropDownListFor(x => x.Food.Storage, new SelectList(ViewBag.StorageTypesList), new { htmlAttributes = new { @class = "form-control" } }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n");
#nullable restore
#line 39 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\Shared\AddEditFoodPartial.cshtml"
     if (Model.Food.Id == 0)
    {


#line default
#line hidden
#nullable disable
            WriteLiteral("        <div class=\"form-group\">\r\n            ");
#nullable restore
#line 43 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\Shared\AddEditFoodPartial.cshtml"
       Write(Html.LabelFor(x => x.Food.Vegetarian, "Vegetarian"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            ");
#nullable restore
#line 44 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\Shared\AddEditFoodPartial.cshtml"
       Write(Html.DropDownListFor(x => x.Food.Vegetarian, new SelectList(ViewBag.VegVegan, "Id", "Name"), new { htmlAttributes = new { @class = "form-control" } }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n");
            WriteLiteral("        <div class=\"form-group\">\r\n            ");
#nullable restore
#line 48 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\Shared\AddEditFoodPartial.cshtml"
       Write(Html.LabelFor(x => x.Food.Vegan, "Vegan"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            ");
#nullable restore
#line 49 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\Shared\AddEditFoodPartial.cshtml"
       Write(Html.DropDownListFor(x => x.Food.Vegan, new SelectList(ViewBag.VegVegan, "Id", "Name"), new { htmlAttributes = new { @class = "form-control" } }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n");
#nullable restore
#line 51 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\Shared\AddEditFoodPartial.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"form-group\">\r\n        ");
#nullable restore
#line 54 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\Shared\AddEditFoodPartial.cshtml"
   Write(Html.LabelFor(x => x.Food.Favourite));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        ");
#nullable restore
#line 55 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\Shared\AddEditFoodPartial.cshtml"
   Write(Html.CheckBoxFor(x => x.Food.Favourite, new { htmlAttributes = new { @class = "form-control" } }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n");
#nullable restore
#line 57 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\Shared\AddEditFoodPartial.cshtml"
     if (Model.Food.Id == 0)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <button type=\"submit\" class=\"btn btn-primary\">Add Food</button>\r\n");
#nullable restore
#line 60 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\Shared\AddEditFoodPartial.cshtml"
    }
    else
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <button type=\"submit\" class=\"btn btn-primary\">Edit Food</button>\r\n");
#nullable restore
#line 64 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\Shared\AddEditFoodPartial.cshtml"
    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 64 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\Shared\AddEditFoodPartial.cshtml"
     
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<CallousFrontEnd.Models.FoodKitchen> Html { get; private set; }
    }
}
#pragma warning restore 1591
