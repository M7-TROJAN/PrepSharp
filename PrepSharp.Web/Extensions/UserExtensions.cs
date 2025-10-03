using PrepSharp.Consts;
using System.Security.Claims;

namespace PrepSharp.Web.Extensions
{
    public static class UserExtensions
    {
        /// <summary>
        /// Get the user ID from claims.
        /// </summary>
        public static string GetUserId(this ClaimsPrincipal user) =>
            user.FindFirstValue(ClaimTypes.NameIdentifier)!;

        // <summary>
        /// Get the userName from claims.
        /// </summary>
        public static string GetUserName(this ClaimsPrincipal user) =>
            user.FindFirstValue(ClaimTypes.Name)!;

        /// <summary>
        /// Get the full name
        /// </summary>
        public static string? GetFullName(this ClaimsPrincipal user) =>
            user.FindFirstValue(ClaimTypes.GivenName);

        /// <summary>
        /// Get the user's email.
        /// </summary>
        public static string GetEmail(this ClaimsPrincipal user) =>
            user.FindFirstValue(ClaimTypes.Email)!;

        /// <summary>
        /// Get the user's Image thumbnail url.
        /// </summary>
        public static string? GetImageThumbnail(this ClaimsPrincipal user) =>
            user.FindFirstValue(CustomClaimTypes.ImageThumbnailUrl);

        /// <summary>
        /// check if the user is an admin.
        /// </summary>
        public static bool IsAdmin(this ClaimsPrincipal user) =>
            user.IsInRole(AppRoles.Admin);

        /// <summary>
        /// check if the user is authenticated.
        /// </summary>
        public static bool IsAuthenticated(this ClaimsPrincipal user) =>
            user.Identity != null && user.Identity.IsAuthenticated;
    }
}