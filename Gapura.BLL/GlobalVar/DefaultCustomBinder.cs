using System.ComponentModel;
using System.Web.Mvc;


public class DefaultCustomBinder : DefaultModelBinder
{
    protected override void SetProperty(ControllerContext controllerContext, ModelBindingContext bindingContext, PropertyDescriptor propertyDescriptor, object value)
    {
        if (value != null && propertyDescriptor.PropertyType == typeof(string))
        {
            value = ((string)value).Trim();
            if ((string)value == string.Empty)
            {
                value = null;
            }
        }
        base.SetProperty(controllerContext, bindingContext, propertyDescriptor, value);
    }
}