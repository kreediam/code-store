namespace SitefinityWebApp
{
    public class MyCoolController
    {
        private IEnumerable<string> GetUserRoleNames()
        {
            IPrincipal principal = this.ControllerContext?.HttpContext?.User;

            if (principal == null || principal.Identity == null || !principal.Identity.IsAuthenticated)
            {
                return new string[] { };
            }

            /*
             * Sitefinity role claim values look like this: b802cd51-93ac-4afb-bdf0-bc64dc6a7df1;Administrators;AppRoles
             * Parse the role name (ex: Administrators) out of the role claim values
             */

            var userRoleNames = ((SitefinityPrincipal)principal).Claims
                .Where(claim => claim.Type == "http://schemas.sitefinity.com/ws/2011/06/identity/claims/role")
                .Select(roleClaim => roleClaim.Value.Split(';'))
                .Select(roleClaimParts => roleClaimParts.Length == 3 ? roleClaimParts[1] : string.Join(";", roleClaimParts));

            return userRoleNames;
        }
    }
}
