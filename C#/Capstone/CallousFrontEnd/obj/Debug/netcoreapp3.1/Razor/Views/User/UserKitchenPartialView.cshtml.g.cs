#pragma checksum "C:\Users\kgsos\Documents\GitHub\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d8a7823fa34a779e8a88005df20a0e5d2de96dba"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_UserKitchenPartialView), @"mvc.1.0.view", @"/Views/User/UserKitchenPartialView.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d8a7823fa34a779e8a88005df20a0e5d2de96dba", @"/Views/User/UserKitchenPartialView.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5cf196c9d4a9c560b9c0f8ac21c4a0baec03e187", @"/Views/_ViewImports.cshtml")]
    public class Views_User_UserKitchenPartialView : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<AccountService.SerializableKitchen>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("text/javascript"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/User/UserKitchen.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#nullable restore
#line 3 "C:\Users\kgsos\Documents\GitHub\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"
 if (Model.Count != 0)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <table>\r\n");
#nullable restore
#line 7 "C:\Users\kgsos\Documents\GitHub\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"
         if (Model[0].Inventory.Count() != 0)
        {
            

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\kgsos\Documents\GitHub\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"
             for (int i = 1; i < 5; i++)
            {
                

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "C:\Users\kgsos\Documents\GitHub\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"
                 if (Model[0].Inventory.Where(x => x.StorageId == i).Count() != 0)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\r\n                        <th><strong>");
#nullable restore
#line 15 "C:\Users\kgsos\Documents\GitHub\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"
                               Write(Model[0].Inventory.Where(x => x.StorageId == i).FirstOrDefault().Storage.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</strong></th>\r\n                    </tr>\r\n");
#nullable restore
#line 17 "C:\Users\kgsos\Documents\GitHub\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"
                }

#line default
#line hidden
#nullable disable
#nullable restore
#line 22 "C:\Users\kgsos\Documents\GitHub\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"
                     foreach (var food in Model[0].Inventory.Where(x=> x.StorageId == i))
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr>\r\n                            <td>");
#nullable restore
#line 25 "C:\Users\kgsos\Documents\GitHub\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"
                           Write(food.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 26 "C:\Users\kgsos\Documents\GitHub\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"
                           Write(food.Quantity);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>\r\n                                <button type=\"button\" class=\"btn btn-primary editFoodBtn\" data-toggle=\"modal\" data-target=\"#AddFood\" data-food-Id=\"");
#nullable restore
#line 28 "C:\Users\kgsos\Documents\GitHub\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"
                                                                                                                                              Write(food.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\" data-kitchen-Id=\"");
#nullable restore
#line 28 "C:\Users\kgsos\Documents\GitHub\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"
                                                                                                                                                                         Write(Model[0].Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\">Edit</button>\r\n                                <button type=\"button\" class=\"btn btn-primary eatFoodBtn\" data-toggle=\"modal\" data-target=\"#EatFood\" data-food-Id=\"");
#nullable restore
#line 29 "C:\Users\kgsos\Documents\GitHub\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"
                                                                                                                                             Write(food.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\">Eat</button>\r\n                                <button type=\"button\" class=\"btn btn-danger deleteFoodBtn\" data-food-Id=\"");
#nullable restore
#line 30 "C:\Users\kgsos\Documents\GitHub\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"
                                                                                                    Write(food.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\">Delete</button>\r\n                            </td>\r\n                        </tr>\r\n");
#nullable restore
#line 33 "C:\Users\kgsos\Documents\GitHub\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"
                    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 33 "C:\Users\kgsos\Documents\GitHub\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"
                     


                }

#line default
#line hidden
#nullable disable
#nullable restore
#line 36 "C:\Users\kgsos\Documents\GitHub\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"
                 
            
        }
        else
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr><td><em>You have no food, add some</em></td></tr>\r\n");
#nullable restore
#line 42 "C:\Users\kgsos\Documents\GitHub\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"

        }

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\r\n            <td>\r\n                <button type=\"button\" class=\"btn btn-primary addFoodBtn\" data-toggle=\"modal\" data-target=\"#AddFood\" data-kitchen-Id=\"");
#nullable restore
#line 46 "C:\Users\kgsos\Documents\GitHub\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"
                                                                                                                                Write(Model[0].Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\">Add Food</button>\r\n            </td>\r\n        </tr>\r\n\r\n\r\n    </table>\r\n");
#nullable restore
#line 52 "C:\Users\kgsos\Documents\GitHub\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"

}
else
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <em>You have no kitchens, add one</em>\r\n");
#nullable restore
#line 57 "C:\Users\kgsos\Documents\GitHub\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

<div class=""modal fade"" role=""dialog"" id=""AddFood"">
    <div class=""modal-dialog"" role=""document"">
        <div class=""modal-content"">
            <div class=""modal-body"">
                <div id=""AddEditFoodBody"">
                </div>
            </div>
        </div>
    </div>
</div>

<div class=""modal fade"" role=""dialog"" id=""EatFood"">
    <div class=""modal-dialog"" role=""document"">
        <div class=""modal-content"">
            <div class=""modal-body"">
                <div id=""EatFoodBody"">
                </div>
            </div>
        </div>
    </div>
</div>

<div class=""modal fade"" role=""dialog"" id=""KithenModel"">
    <div class=""modal-dialog"" role=""document"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h5 class=""modal-title"">Kitchen</h5>
            </div>
            <div class=""modal-body"">

");
#nullable restore
#line 93 "C:\Users\kgsos\Documents\GitHub\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"
                   Html.RenderPartial("AddEditKitchenPartial", new KitchenUser { UserId = ViewBag.UserId, kitchen = new Capstone.Classes.Kitchen() });

#line default
#line hidden
#nullable disable
            WriteLiteral("            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d8a7823fa34a779e8a88005df20a0e5d2de96dba12510", async() => {
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
            WriteLiteral("\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<AccountService.SerializableKitchen>> Html { get; private set; }
    }
}
#pragma warning restore 1591
