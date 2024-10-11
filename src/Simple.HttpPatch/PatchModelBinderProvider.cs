using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace Simple.HttpPatch;

public class PatchModelBinderProvider:IModelBinderProvider
{
    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        if (context.Metadata is { IsComplexType: true, ModelType.IsGenericType: true } &&
            context.Metadata.ModelType.GetGenericTypeDefinition() == typeof(Patch<>))
        {
            var modelType = context.Metadata.ModelType.GenericTypeArguments[0];
            var binderType = typeof(PatchObjectModelBinder<>).MakeGenericType(modelType);
            return (IModelBinder)Activator.CreateInstance(binderType);
        }

        return null;
    }
}