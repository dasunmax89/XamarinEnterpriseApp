using Android.Content;
using Android.Text.Method;
using Android.Util;
using Android.Views;
using Android.Views.InputMethods;
using XamarinEnterpriseApp.Xamarin.Core.Controls;
using XamarinEnterpriseApp.Xamarin.Core.Ioc;
using XamarinEnterpriseApp.Xamarin.Core.Services;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace XamarinEnterpriseApp.Xamarin.Droid.Renderers
{
    public static class RenderUtil
    {
        public static float DpToPixels(Context context, float valueInDp)
        {
            DisplayMetrics metrics = context.Resources.DisplayMetrics;
            return TypedValue.ApplyDimension(ComplexUnitType.Dip, valueInDp, metrics);
        }

        public static void AddDoneAndNextButton(FormsEditText control, InputView entry, bool isMultiline = false)
        {
            var renderService = DependencyResolver.Resolve<IUIRenderService>();

            string nextString = renderService.Translate("Next");
            string doneString = renderService.Translate("Done");

            ImeAction imeAction = isMultiline ? ImeAction.Unspecified : ImeAction.Done;

            if (entry is INavigatableInput)
            {
                INavigatableInput navigatableInput = (INavigatableInput)entry;

                if (navigatableInput.NextTextField != null)
                {
                    control.ImeOptions = imeAction;
                    control.SetImeActionLabel(nextString, ImeAction.Next);
                }
                else
                {
                    control.ImeOptions = imeAction;
                    control.SetImeActionLabel(doneString, ImeAction.Done);
                }
            }
            else
            {
                control.ImeOptions = imeAction;
                control.SetImeActionLabel(doneString, ImeAction.Done);
            }
        }

        public static void SetScrollable(FormsEditText control)
        {
            var nativeEditText = (global::Android.Widget.EditText)control;

            //While scrolling inside Editor stop scrolling parent view.
            nativeEditText.OverScrollMode = OverScrollMode.Always;
            nativeEditText.ScrollBarStyle = ScrollbarStyles.InsideInset;
            nativeEditText.SetOnTouchListener(new ExtendedTouchListener());

            //For Scrolling in Editor innner area
            control.VerticalScrollBarEnabled = true;
            control.MovementMethod = ScrollingMovementMethod.Instance;
            control.ScrollBarStyle = Android.Views.ScrollbarStyles.InsideInset;

            nativeEditText.ScrollTo(0,0);
        }
    }
}
