#pragma checksum "D:\3 курс\МДК 05.02\6 семестр\30\ПР30\shop\Views\Categorys\List.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "741bbc975c2ca731e3b7b8d1621b440649b74312"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Categorys_List), @"mvc.1.0.view", @"/Views/Categorys/List.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Categorys/List.cshtml", typeof(AspNetCore.Views_Categorys_List))]
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
#line 1 "D:\3 курс\МДК 05.02\6 семестр\30\ПР30\shop\Views\Categorys\List.cshtml"
using shop.Data.ViewModell;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"741bbc975c2ca731e3b7b8d1621b440649b74312", @"/Views/Categorys/List.cshtml")]
    public class Views_Categorys_List : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<VMCategorys>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(29, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(51, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 5 "D:\3 курс\МДК 05.02\6 семестр\30\ПР30\shop\Views\Categorys\List.cshtml"
  
    Layout = "_Layout";


#line default
#line hidden
            BeginContext(87, 385, true);
            WriteLiteral(@"<h2>Все категории продуктов</h2>
<script>
        function SearchItems(sender) {
            window.location = ""/Categorys/List?searchTerm="" + sender.value;
        }
        function Add() {
            window.location = ""/Categorys/Add"";
        }
        function Update(sender) {
            window.location = ""/Categorys/Update?id="" + sender.name;
        }
</script>
");
            EndContext();
#line 21 "D:\3 курс\МДК 05.02\6 семестр\30\ПР30\shop\Views\Categorys\List.cshtml"
  

    foreach (var categorys in Model.SearchTerm)
    {

#line default
#line hidden
            BeginContext(532, 111, true);
            WriteLiteral(" <div class=\"categorys\">\r\n            <img />\r\n            <div class=\"datac\">\r\n                <h3>Категория: ");
            EndContext();
            BeginContext(644, 14, false);
#line 27 "D:\3 курс\МДК 05.02\6 семестр\30\ПР30\shop\Views\Categorys\List.cshtml"
                          Write(categorys.Name);

#line default
#line hidden
            EndContext();
            BeginContext(658, 35, true);
            WriteLiteral("</h3>\r\n                <p>Описание:");
            EndContext();
            BeginContext(694, 21, false);
#line 28 "D:\3 курс\МДК 05.02\6 семестр\30\ПР30\shop\Views\Categorys\List.cshtml"
                       Write(categorys.Description);

#line default
#line hidden
            EndContext();
            BeginContext(715, 75, true);
            WriteLiteral("</p>\r\n            </div>\r\n            <input type=\"submit\" value=\"Изменить\"");
            EndContext();
            BeginWriteAttribute("name", " name=\"", 790, "\"", 810, 1);
#line 30 "D:\3 курс\МДК 05.02\6 семестр\30\ПР30\shop\Views\Categorys\List.cshtml"
WriteAttributeValue("", 797, categorys.Id, 797, 13, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(811, 135, true);
            WriteLiteral(" onclick=\"Update(this)\" />\r\n            <form method=\"post\" action=\"/Categorys/Delete\">\r\n                <input type=\"hidden\" name=\"id\"");
            EndContext();
            BeginWriteAttribute("value", " value=\"", 946, "\"", 967, 1);
#line 32 "D:\3 курс\МДК 05.02\6 семестр\30\ПР30\shop\Views\Categorys\List.cshtml"
WriteAttributeValue("", 954, categorys.Id, 954, 13, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(968, 97, true);
            WriteLiteral("/>\r\n                <input type=\"submit\" value=\"Удалить\"/>\r\n            </form>\r\n        </div>\r\n");
            EndContext();
#line 36 "D:\3 курс\МДК 05.02\6 семестр\30\ПР30\shop\Views\Categorys\List.cshtml"
    }

#line default
#line hidden
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<VMCategorys> Html { get; private set; }
    }
}
#pragma warning restore 1591
