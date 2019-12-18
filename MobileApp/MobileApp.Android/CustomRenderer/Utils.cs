using Android.Content;

namespace MobileApp.Droid.CustomRenderer
{
    public static class Utils
    {
        public static float ConvertPixelToDp(Context context, float px)
        {
            return px / context.Resources.DisplayMetrics.Density;
        }

        public static float ConvertDpToPixel(Context context, float dp)
        {
            return dp * context.Resources.DisplayMetrics.Density;
        }
    }
}