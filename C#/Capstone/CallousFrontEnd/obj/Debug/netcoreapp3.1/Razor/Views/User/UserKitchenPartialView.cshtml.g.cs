#pragma checksum "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a36dc27947609fd4b3dc03b6b5934787334198e4"
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a36dc27947609fd4b3dc03b6b5934787334198e4", @"/Views/User/UserKitchenPartialView.cshtml")]
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
#line 2 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"
  
    string[] StorageTypes = { "Fridge", "Freezer", "Pantry", "Cupboard", "Cellar", "Other" };


#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"row\">\r\n    <input type=\"text\" class=\"form-control\" placeholder=\"Search\" id=\"tbSearchbar\" />\r\n</div>\r\n<input type=\"hidden\" id=\"isVegan\"");
            BeginWriteAttribute("value", " value=\"", 299, "\"", 323, 1);
#nullable restore
#line 9 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"
WriteAttributeValue("", 307, ViewBag.isVegan, 307, 16, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n<input type=\"hidden\" id=\"isVeg\"");
            BeginWriteAttribute("value", " value=\"", 360, "\"", 382, 1);
#nullable restore
#line 10 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"
WriteAttributeValue("", 368, ViewBag.isVeg, 368, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n");
#nullable restore
#line 11 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"
 if (Model.Count != 0)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"container\">\r\n");
#nullable restore
#line 15 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"
         if (Model[0].Inventory.Count() != 0)
        {
            Model[0].Inventory = Model[0].Inventory.OrderByDescending(x => x.Favourite).ToArray();
            

#line default
#line hidden
#nullable disable
#nullable restore
#line 19 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"
             for (int i = 0; i < StorageTypes.Length; i++)
            

#line default
#line hidden
#nullable disable
#nullable restore
#line 20 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"
                              
            {
                

#line default
#line hidden
#nullable disable
#nullable restore
#line 22 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"
                 if (Model[0].Inventory.Where(x => x.Storage == StorageTypes[i]).Count() != 0)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <div class=\"row\">\r\n                        <h4><em>");
#nullable restore
#line 25 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"
                           Write(Model[0].Inventory.Where(x => x.Storage == StorageTypes[i]).FirstOrDefault().Storage);

#line default
#line hidden
#nullable disable
            WriteLiteral("</em></h4>\r\n                    </div>\r\n");
#nullable restore
#line 27 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"
                }

#line default
#line hidden
#nullable disable
#nullable restore
#line 32 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"
                 foreach (var food in Model[0].Inventory.Where(x => x.Storage == StorageTypes[i]))
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <div class=\"foodRow row\" data-foodName=\"");
#nullable restore
#line 34 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"
                                                       Write(food.Name.ToLower());

#line default
#line hidden
#nullable disable
            WriteLiteral("\">\r\n                        <div class=\"col-8\">\r\n                            <div class=\"row\"><h5>");
#nullable restore
#line 36 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"
                                            Write(food.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5> </div>\r\n                            <div class=\"row\">");
#nullable restore
#line 37 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"
                                        Write(food.Quantity);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 37 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"
                                                       Write(food.QuantityClassifier);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n");
#nullable restore
#line 38 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"
                             if (food.ExpiryDate != null)
                            {
                                string expiryString = "";
                                DateTime date2 = DateTime.Now.AddDays(3);
                                TimeSpan time = date2 - food.ExpiryDate.Value;

                                if (time.Days == 0)
                                {
                                    expiryString = "expiringSoon";
                                }
                                else if (time.Days >= 0)
                                {
                                    expiryString = "expriringCritical";

                                }
                                else
                                {

                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <div");
            BeginWriteAttribute("class", " class=\"", 2414, "\"", 2439, 2);
            WriteAttributeValue("", 2422, "row", 2422, 3, true);
#nullable restore
#line 57 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"
WriteAttributeValue(" ", 2425, expiryString, 2426, 13, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Expires: ");
#nullable restore
#line 57 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"
                                                                   Write(food.ExpiryDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n");
#nullable restore
#line 58 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                        </div>\r\n                        <div class=\"col-4\">\r\n                            <button type=\"button\" class=\"btn btn-primary editFoodBtn\" data-toggle=\"modal\" data-target=\"#AddFood\" data-food-Id=\"");
#nullable restore
#line 61 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"
                                                                                                                                          Write(food.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\" data-kitchen-Id=\"");
#nullable restore
#line 61 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"
                                                                                                                                                                     Write(Model[0].Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\">Edit</button>\r\n                            <button type=\"button\" class=\"btn btn-primary eatFoodBtn\" data-food-Id=\"");
#nullable restore
#line 62 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"
                                                                                              Write(food.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\" data-vegan=\"");
#nullable restore
#line 62 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"
                                                                                                                    Write(food.Vegan);

#line default
#line hidden
#nullable disable
            WriteLiteral("\" data-veg=\"");
#nullable restore
#line 62 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"
                                                                                                                                           Write(food.Vegetarian);

#line default
#line hidden
#nullable disable
            WriteLiteral("\">Eat</button>\r\n                            <button type=\"button\" class=\"btn btn-danger deleteFoodBtn\" data-food-Id=\"");
#nullable restore
#line 63 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"
                                                                                                Write(food.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\">Delete</button>\r\n                        </div>\r\n                    </div>\r\n");
#nullable restore
#line 66 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"
                }

#line default
#line hidden
#nullable disable
#nullable restore
#line 66 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"
                 


            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 69 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"
             

        }
        else
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"row\"><em>You have no food, add some</em></div>\r\n");
#nullable restore
#line 75 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"

        }

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div class=\"row\">\r\n\r\n            <button type=\"button\" class=\"btn btn-primary addFoodBtn\" data-toggle=\"modal\" data-target=\"#AddFood\" data-kitchen-Id=\"");
#nullable restore
#line 79 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"
                                                                                                                            Write(Model[0].Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\">Add Food</button>\r\n\r\n        </div>\r\n\r\n\r\n    </div>\r\n");
#nullable restore
#line 85 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"

}
else
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <em>You have no kitchens, add one</em>\r\n");
#nullable restore
#line 90 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"
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
#line 126 "F:\Capstone\CallousHippo\C#\Capstone\CallousFrontEnd\Views\User\UserKitchenPartialView.cshtml"
                   Html.RenderPartial("AddEditKitchenPartial", new KitchenUser { UserId = ViewBag.UserId, kitchen = new Capstone.Classes.Kitchen() });

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            </div>
        </div>
    </div>
</div>

<div class=""modal fade"" role=""dialog"" id=""RecipeModal"">
    <div class=""modal-dialog modal-xl"" role=""document"">

        <div class=""modal-content"">
            <div class=""modal-header"">Search Recipe</div>
            <div class=""modal-body"" id=""recipeSearchBody"">

            </div>
            <div class=""modal-footer"">
                <div id=""edamam-badge"" data-color=""transparent""></div>

            </div>
        </div>

    </div>
</div>

<div class=""modal fade"" role=""dialog"" id=""ShoppingListModal"">
    <div class=""modal-dialog"" role=""document"">

        <div class=""modal-content"">
            <div class=""modal-header"">Shopping List</div>
            <div class=""modal-body"" id=""shoppingListBody"">

            </div>
        </div>

    </div>
</div>

");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a36dc27947609fd4b3dc03b6b5934787334198e417937", async() => {
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
