using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AnimeCrunch.Models.DataModels;
using System.Web.Security;

namespace AnimeCrunch.security
{
    public class CustomRoleProvider : RoleProvider
    {
        TVShowDb db = new TVShowDb();
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            //l'objectif est de retourner un tableau de string, contenant
            //les rôles (groupes) de l'utilisateur reçu en paramètre.
            //username contient l’information placée dans le cookie à l’aide de
            //FormsAuthentication.SetAuthCookie
            User user = this.db.Users.Find(int.Parse(username));

            if (user.Type == User.UserType.Administrator)
            {
                return new[] { "Administrator", "Member", "Donator" };
            }
            else if ((user.Type == User.UserType.Donator))
            {
                return new[] { "Donator" };
            }
            else if ((user.Type == User.UserType.Member))
            {
                return new[] { "Member"};
            }
            return new[] { "Visiteur"};

        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
    }