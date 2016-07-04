using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Northwind.Store.UI.Web.Models;
using System.Collections.Generic;
public class IdentityManager
{
    public bool RoleExists(string name)
    {
        var rm = new RoleManager<IdentityRole>(
            new RoleStore<IdentityRole>(new ApplicationDbContext()));
        return rm.RoleExists(name);
    }

    public bool CreateRole(string name)
    {
        var rm = new RoleManager<IdentityRole>(
            new RoleStore<IdentityRole>(new ApplicationDbContext()));
        var idResult = rm.Create(new IdentityRole(name));

        return idResult.Succeeded;
    }

    public bool CreateUser(ApplicationUser user, string password)
    {
        var um = new UserManager<ApplicationUser>(
            new UserStore<ApplicationUser>(new ApplicationDbContext()));
        var idResult = um.Create(user, password);
        return idResult.Succeeded;
    }

    public static bool UserInRole(string userId, string roleName)
    {
        var um = new UserManager<ApplicationUser>(
            new UserStore<ApplicationUser>(new ApplicationDbContext()));
        return um.IsInRole(userId, roleName);
    }

    public bool AddUserToRole(string userId, string roleName)
    {
        var um = new UserManager<ApplicationUser>(
            new UserStore<ApplicationUser>(new ApplicationDbContext()));

        if (!RoleExists(roleName))
        {
            CreateRole(roleName);
        }

        var idResult = um.AddToRole(userId, roleName);
        return idResult.Succeeded;
    }

    public void ClearUserRoles(string userId)
    {
        var um = new UserManager<ApplicationUser>(
            new UserStore<ApplicationUser>(new ApplicationDbContext()));

        var user = um.FindById(userId);
        var currentRoles = new List<IdentityUserRole>();
        currentRoles.AddRange(user.Roles);

        foreach (var role in currentRoles)
        {
            um.RemoveFromRole(userId, role.RoleId);
        }
    }

    public static ApplicationUser GetUser(string userId)
    {
        var um = new UserManager<ApplicationUser>(
            new UserStore<ApplicationUser>(new ApplicationDbContext()));

        return um.FindById(userId);
    }
}