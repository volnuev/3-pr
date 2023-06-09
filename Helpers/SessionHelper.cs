using Microsoft.AspNetCore.Http;

namespace shop.Helpers
{
    public static class SessionHelper
    {
        public static int GetItemCount(this ISession session)
        {
            var itemCount = session.GetInt32("ItemCount");
            return itemCount ?? 0;
        }

        public static void SetItemCount(this ISession session, int itemCount)
        {
            session.SetInt32("ItemCount", itemCount);
        }
    }
}