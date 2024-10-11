using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Simple.HttpPatch;

public class PatchObjectModelBinder<T> : IModelBinder where T : class
{
    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
        {
            throw new ArgumentNullException(nameof(bindingContext));
        }
        
        using (var reader = new StreamReader(bindingContext.HttpContext.Request.Body))
        {
            var json = await reader.ReadToEndAsync();
            
            dynamic dynamicObject = Activator.CreateInstance(typeof(Patch<T>));
            
            using (JsonDocument doc = JsonDocument.Parse(json))
            {
                foreach (var property in doc.RootElement.EnumerateObject())
                {
                    dynamicObject.TrySetMember(new DynamicMemberBinder(property.Name, true), property.Value.ToString());
                }
            }
            
            bindingContext.Result = ModelBindingResult.Success(dynamicObject);
        }
    }
}