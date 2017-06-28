using System;
using System.Globalization;
using System.Web.Mvc;

public class DecimalNullableModelBinder : IModelBinder
{
    public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
    {
        ValueProviderResult valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
        ModelState modelState = new ModelState { Value = valueResult };
        object actualValue = null;

        if (string.IsNullOrEmpty(valueResult.AttemptedValue))       // Fir, 21062017
            return actualValue;

        try
        {
            actualValue = Convert.ToDecimal(valueResult.AttemptedValue, CultureInfo.CurrentCulture);
        }
        catch (FormatException e)
        {
            modelState.Errors.Add(e);
        }

        if (bindingContext.ModelState.ContainsKey(bindingContext.ModelName))
            bindingContext.ModelState[bindingContext.ModelName] = modelState;
        else
            bindingContext.ModelState.Add(bindingContext.ModelName, modelState);

        return actualValue;
    }
}