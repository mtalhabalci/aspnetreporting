using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace SDIKit.Common.Identity
{
    public static class ClaimPrincipalExtensions
    {
        #region [DefaultClaimsExtensions]

        /// <summary>
        /// Gets the subject identifier.
        /// </summary>
        /// <param name="principal">The principal.</param>
        /// <returns></returns>

        public static string GetNameIdentifier(this IPrincipal principal)
        {
            return principal.Identity.GetNameIdentifier();
        }

        /// <summary>
        /// Gets the subject identifier.
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException">sub claim is missing</exception>

        public static string GetNameIdentifier(this IIdentity identity)
        {
            var id = identity as ClaimsIdentity;
            var claim = id.FindFirst(ClaimTypes.NameIdentifier);

            if (claim == null) throw new InvalidOperationException("sub claim is missing");
            return claim.Value;
        }

        /// <summary>
        /// Gets the authentication method.
        /// </summary>
        /// <param name="principal">The principal.</param>
        /// <returns></returns>

        public static string GetAuthenticationMethod(this IPrincipal principal)
        {
            return principal.Identity.GetAuthenticationMethod();
        }

        /// <summary>
        /// Gets the authentication method claims.
        /// </summary>
        /// <param name="principal">The principal.</param>
        /// <returns></returns>

        public static IEnumerable<Claim> GetAuthenticationMethods(this IPrincipal principal)
        {
            return principal.Identity.GetAuthenticationMethods();
        }

        /// <summary>
        /// Gets the authentication method.
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException">amr claim is missing</exception>

        public static string GetAuthenticationMethod(this IIdentity identity)
        {
            var id = identity as ClaimsIdentity;
            var claim = id.FindFirst(ClaimTypes.AuthenticationMethod);

            if (claim == null) throw new InvalidOperationException("amr claim is missing");
            return claim.Value;
        }

        /// <summary>
        /// Gets the authentication method claims.
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <returns></returns>

        public static IEnumerable<Claim> GetAuthenticationMethods(this IIdentity identity)
        {
            var id = identity as ClaimsIdentity;
            return id.FindAll(ClaimTypes.AuthenticationMethod);
        }

        /// <summary>
        /// Determines whether this instance is authenticated.
        /// </summary>
        /// <param name="principal">The principal.</param>
        /// <returns>
        ///   <c>true</c> if the specified principal is authenticated; otherwise, <c>false</c>.
        /// </returns>

        public static bool IsAuthenticated(this IPrincipal principal)
        {
            return principal != null && principal.Identity != null && principal.Identity.IsAuthenticated;
        }

        #endregion [DefaultClaimsExtensions]

        #region [CurrentUser Claims]

        /// <summary>
        /// ClaimTypes.NameIdentifier
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public static string GetUserId(this IIdentity identity)
        {
            return GetNameIdentifier(identity);
        }

        /// <summary>
        /// ClaimTypes.NameIdentifier
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static int GetUserId(this ClaimsPrincipal principal)
        {
            return principal.GetIntegerValue(ClaimTypes.NameIdentifier);
        }

        /// <summary>
        /// ClaimTypes.NameIdentifier
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static Guid GetUserGuid(this ClaimsPrincipal principal)
        {
            return principal.GetGuidValue(ClaimTypes.NameIdentifier);
        }

        /// <summary>
        /// SDIKitJwtClaimTypes.CurrentUserId
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static int GetCurrentUserId(this ClaimsPrincipal principal)
        {
            return principal.GetIntegerValue(SDIKitJwtClaimTypes.CurrentUserId);
        }

        /// <summary>
        /// SDIKitJwtClaimTypes.CurrentUserId
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static RequestHeaderInformation GetCurrentUserHeaderInformations(this HttpContext context)
        {
            var headerInformation = new RequestHeaderInformation();
            headerInformation.Language = context.Request.Headers["client-lang"].FirstOrDefault();
            if (headerInformation.Language == null)
                headerInformation.Language = "tr";
            headerInformation.TimeZone = context.Request.Headers["client-timezone"].FirstOrDefault();
            if (headerInformation.TimeZone == null)
                headerInformation.TimeZone = "+3";
            headerInformation.UserId = context.User.GetCurrentUserId();
            return headerInformation;
        }
        public static RequestHeaderInformation GetCurrentCurrentUserLang(this HttpContext context)
        {
            var headerInformation = new RequestHeaderInformation();
            headerInformation.Language = context.Request.Headers["client-lang"].FirstOrDefault();
            if (headerInformation.Language == null)
                headerInformation.Language = "tr";
            headerInformation.TimeZone = context.Request.Headers["client-timezone"].FirstOrDefault();
            if (headerInformation.TimeZone == null)
                headerInformation.TimeZone = "+3";
            return headerInformation;
        }
        /// <summary>
        /// SDIKitJwtClaimTypes.CurrentUserId
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static Guid GetCurrentUserGuid(this ClaimsPrincipal principal)
        {
            return principal.GetGuidValue(SDIKitJwtClaimTypes.CurrentUserId);
        }

        /// <summary>
        /// ClaimTypes.Email
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static string GetEmail(this ClaimsPrincipal principal)
        {
            return principal.GetStringValue(ClaimTypes.Email);
        }

        public static string GetFullDisplayName(this ClaimsPrincipal principal)
        {
            var name = principal.GetStringValue(ClaimTypes.Name);
            var surName = principal.GetStringValue(ClaimTypes.Surname);

            return String.Join(" ", name, surName);
        }

        /// <summary>
        /// ClaimTypes.Name
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static string GetName(this ClaimsPrincipal principal)
        {
            var name = principal.GetStringValue(ClaimTypes.Name);
            return name;
        }

        /// <summary>
        /// ClaimTypes.Surname
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static string GetSurname(this ClaimsPrincipal principal)
        {
            var name = principal.GetStringValue(ClaimTypes.Surname);
            return name;
        }

        /// <summary>
        /// SDIKitJwtClaimTypes.LoginDate
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static DateTime GetLoginDate(this ClaimsPrincipal principal)
        {
            var value = principal.GetDateTimeValue(SDIKitJwtClaimTypes.LoginDate);
            return value;
        }

        /// <summary>
        /// SDIKitJwtClaimTypes.MustChangePasswordNextLogon
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static bool GetMustChangePasswordNextLogon(this ClaimsPrincipal principal)
        {
            var value = principal.GetBooleanValue(SDIKitJwtClaimTypes.MustChangePasswordNextLogon);
            return value;
        }

        /// <summary>
        /// SDIKitJwtClaimTypes.IsTrustedUser
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static bool GetIsTrustedUser(this ClaimsPrincipal principal)
        {
            var value = principal.GetBooleanValue(SDIKitJwtClaimTypes.IsTrustedUser);
            return value;
        }

        /// <summary>
        /// SDIKitJwtClaimTypes.IsSystemAdmin
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static bool GetIsSystemAdmin(this ClaimsPrincipal principal)
        {
            var value = principal.GetBooleanValue(SDIKitJwtClaimTypes.IsSystemAdmin);
            return value;
        }

        /// <summary>
        /// SDIKitJwtClaimTypes.IsPublicUser
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static bool GetIsPublicUser(this ClaimsPrincipal principal)
        {
            var value = principal.GetBooleanValue(SDIKitJwtClaimTypes.IsPublicUser);
            return value;
        }

        /// <summary>
        /// SDIKitJwtClaimTypes.IsDashboardUser
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static bool GetIsDashboardUser(this ClaimsPrincipal principal)
        {
            var value = principal.GetBooleanValue(SDIKitJwtClaimTypes.IsDashboardUser);
            return value;
        }

        #endregion [CurrentUser Claims]

        public static string GetStringValue(this ClaimsPrincipal principal, string claimType)
        {
            var obj = principal.FindFirst(claimType);
            if (obj != null) return obj.Value;

            throw new InvalidOperationException($"{claimType} claim is missing");
        }

        public static int GetIntegerValue(this ClaimsPrincipal principal, string claimType)
        {
            var obj = principal.FindFirst(claimType);
            if (obj != null && int.TryParse(obj.Value, out int value))
            {
                return value;
            }

            throw new InvalidOperationException($"{claimType} claim is missing");
        }

        public static Guid GetGuidValue(this ClaimsPrincipal principal, string claimType)
        {
            var obj = principal.FindFirst(claimType);
            if (obj != null && Guid.TryParse(obj.Value, out Guid value))
            {
                return value;
            }

            throw new InvalidOperationException($"{claimType} claim is missing");
        }

        public static long GetLongValue(this ClaimsPrincipal principal, string claimType)
        {
            var obj = principal.FindFirst(claimType);
            if (obj != null && long.TryParse(obj.Value, out long value))
            {
                return value;
            }

            throw new InvalidOperationException($"{claimType} claim is missing");
        }

        public static bool GetBooleanValue(this ClaimsPrincipal principal, string claimType)
        {
            var obj = principal.FindFirst(claimType);
            if (obj != null) return Convert.ToBoolean(obj.Value);

            throw new InvalidOperationException($"{claimType} claim is missing");
        }

        public static DateTime GetDateTimeValue(this ClaimsPrincipal principal, string claimType)
        {
            var obj = principal.FindFirst(claimType);
            if (obj != null) return Convert.ToDateTime(obj.Value);

            throw new InvalidOperationException($"{claimType} claim is missing");
        }
    }
}