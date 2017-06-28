using System;
using System.Globalization;
using System.Threading;
using System.Web.Mvc;

public class DecimalBinder : DefaultModelBinder
{
    public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
    {
        if (bindingContext.ModelType == typeof(decimal) || bindingContext.ModelType == typeof(decimal?))
        {
            return BindDecimal(bindingContext);
        }
        else
        {
            return base.BindModel(controllerContext, bindingContext);
        }
    }

    private object BindDecimal(ModelBindingContext bindingContext)
    {
        ValueProviderResult valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
        if (valueProviderResult == null)
        {
            return null;
        }
        bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);
        decimal value;
        string valueAsString = valueProviderResult.AttemptedValue == null ? null : valueProviderResult.AttemptedValue.Trim();
        if (string.IsNullOrEmpty(valueAsString))
        {
            return null;
        }
        if (!decimal.TryParse(valueAsString, NumberStyles.Any, Thread.CurrentThread.CurrentCulture, out value))
        {
            var ex = new InvalidOperationException("Invalid Value", new Exception("Invalid Value", new FormatException("Invalid Value")));
            bindingContext.ModelState.AddModelError(bindingContext.ModelName, ex);
            return null;
        }
        return bindingContext.ModelType == typeof(decimal)? value : new decimal?(value);
    }
}