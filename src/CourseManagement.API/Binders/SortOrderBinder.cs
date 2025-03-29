using CourseManagement.Application.Base;
using CourseManagement.Application.Base.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CourseManagement.API.Binders;

public class SortOrderBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var valueProviderResult = bindingContext.ValueProvider
            .GetValue(bindingContext.ModelName);

        if (valueProviderResult == ValueProviderResult.None)
        {
            return Task.CompletedTask;
        }

        var value = valueProviderResult.FirstValue?.ToLowerCase();
        if (string.IsNullOrEmpty(value))
        {
            bindingContext.Result = ModelBindingResult.Success(SortOrder.Asc);
            return Task.CompletedTask;
        }

        var result = value switch
        {
            "ascending" => SortOrder.Asc,
            "asc" => SortOrder.Asc,
            "0" => SortOrder.Asc,
            "descending" => SortOrder.Desc,
            "desc" => SortOrder.Desc,
            "1" => SortOrder.Desc,
            _ => SortOrder.Asc
        };

        bindingContext.Result = ModelBindingResult.Success(result);
        
        return Task.CompletedTask;
    }
}
