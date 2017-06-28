using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Security;

namespace Gapura.Models.RBAC
{
    public class GapuraRoleProvider : RoleProvider
    {
        private int _cacheTimeOutInMinute = 20;
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            if (roleName == null || roleName == "")
                throw new ProviderException("Role name cannot be empty or null.");
            if (roleName.Contains(","))
                throw new ArgumentException("Role names cannot contain commas.");
            if (RoleExists(roleName))
                throw new ProviderException("Role name already exists.");
            if (roleName.Length > 255)
                throw new ProviderException("Role name cannot exceed 255 characters.");

            SqlConnection conn = new SqlConnection("Data source=(local); Database=YSIDGA;User Id=firman;Password=123");
            SqlCommand cmd = new SqlCommand("INSERT INTO RBAC_Roles " +
                                              //" (Rolename, ApplicationName) " +
                                              //" Values(?, ?)", conn);
                                              " (RoleName) " +
                                              " Values(?)", conn);

            cmd.Parameters.Add("@RoleName", SqlDbType.VarChar, 255).Value = roleName;
            //cmd.Parameters.Add("@ApplicationName", SqlDbType.VarChar, 255).Value = ApplicationName;
            try
            {
                conn.Open();

                cmd.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                // Handle exception.
            }
            finally
            {
                conn.Close();
            }
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            if (!RoleExists(roleName))
            {
                throw new ProviderException("Role does not exist.");
            }

            if (throwOnPopulatedRole && GetUsersInRole(roleName).Length > 0)
            {
                throw new ProviderException("Cannot delete a populated role.");
            }

            SqlConnection conn = new SqlConnection("Data source=(local); Database=YSIDGA;User Id=firman;Password=123");
            SqlCommand cmd = new SqlCommand("DELETE FROM RBAC_Roles " +
                                                                    //" WHERE Rolename = ? AND ApplicationName = ?", conn);
                                                                    " WHERE RoleName = ?", conn);

            cmd.Parameters.Add("@RoleName", SqlDbType.VarChar, 255).Value = roleName;
            //cmd.Parameters.Add("@ApplicationName", SqlDbType.VarChar, 255).Value = ApplicationName;

            SqlCommand cmd2 = new SqlCommand("DELETE FROM UsersInRoles " +
                                               //" WHERE Rolename = ? AND ApplicationName = ?", conn);
                                               " WHERE Rolename = ?", conn);

            cmd2.Parameters.Add("@RoleName", SqlDbType.VarChar, 255).Value = roleName;
            //cmd2.Parameters.Add("@ApplicationName", SqlDbType.VarChar, 255).Value = ApplicationName;
            try
            {
                conn.Open();

                cmd2.ExecuteNonQuery();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                // Handle exception.

                return false;
            }
            finally
            {
                conn.Close();
            }

            return true;
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            //throw new NotImplementedException();
            // Fir 23032017

            string[] sRoles = new string[] { };
            using (YSIDGAEntitiesConn dbConn = new YSIDGAEntitiesConn())
            {
                //sRoles = (from r in dbConn.RBAC_Roles select r.RoleName).ToArray<string>();
                sRoles = dbConn.RBAC_Roles.Select(r => r.RoleName).ToArray();
            }
            return sRoles;
        }

        public override string[] GetRolesForUser(string username)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return null;
            }

            //check cache
            var cacheKey = string.Format("{0}_role", username);
            if (HttpRuntime.Cache[cacheKey] != null)
            {
                return (string[])HttpRuntime.Cache[cacheKey];
            }
            string[] roles = new string[] { };
            using (YSIDGAEntitiesConn dbConn = new YSIDGAEntitiesConn())
            {
                roles = (from a in dbConn.RBAC_Roles
                         join b in dbConn.RBAC_UsersInRoles on a.RoleID equals b.RoleID
                         join c in dbConn.RBAC_UserProfile on b.UserID equals c.UserID
                         where c.UserName.Equals(username)
                         select a.RoleName).ToArray<string>();
                if (roles.Count() > 0)
                {
                    HttpRuntime.Cache.Insert(cacheKey, roles, null, DateTime.Now.AddMinutes(_cacheTimeOutInMinute), Cache.NoSlidingExpiration);
                }
            }
            return roles;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string UserName, string RoleName)
        {
            var userRoles = GetRolesForUser(UserName);
            return userRoles.Contains(RoleName);
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