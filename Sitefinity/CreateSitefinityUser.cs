        private void Make_Default_Login()
        {
            string email = "email";
            var userManager = Telerik.Sitefinity.Security.UserManager.GetManager("Default");

            if (userManager.GetUserByEmail(email) == null)
            {
                userManager.Provider.SuppressSecurityChecks = true;
                var user = userManager.CreateUser("username", "password", email, "Question", "Answer", true, null, out System.Web.Security.MembershipCreateStatus status);

                if (status != System.Web.Security.MembershipCreateStatus.Success)
                {
                    throw new Exception(status.ToString());
                }

                user.IsBackendUser = true;
                userManager.SaveChanges();
            }

            var roleManager = Telerik.Sitefinity.Security.RoleManager.GetManager("AppRoles");
            var adminUser = userManager.GetUserByEmail(email);

            if (adminUser != null && !roleManager.GetRolesForUser(adminUser.Id).Any())
            {
                roleManager.Provider.SuppressSecurityChecks = true;

                var roleA = roleManager.GetRole("Administrators");
                roleManager.AddUserToRole(adminUser, roleA);

                var roleB = roleManager.GetRole("BackendUsers");
                roleManager.AddUserToRole(adminUser, roleB);

                var roleC = roleManager.GetRole("Users");
                roleManager.AddUserToRole(adminUser, roleC);
                roleManager.SaveChanges();
            }
        }
