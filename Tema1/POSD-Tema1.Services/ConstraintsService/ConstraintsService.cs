using POSD_Tema1.Data.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSD_Tema1.Services.ConstraintsService
{
    public class ConstraintsService
    {
        private readonly Entities _dbEntities;

        public ConstraintsService()
        {
            _dbEntities = new Entities();
        }

        public bool ExistConstraints(string username, string roleName)
        {
            Role role = _dbEntities.Roles.FirstOrDefault(f => f.Name == roleName);
            var user = _dbEntities.Users.FirstOrDefault(f => f.Username == username);
            List<int> rolesOfUser = _dbEntities.UserInRoles.Where(f => f.UserId == user.Id).Select(f => f.RoleId).ToList();

            for (var i = 0; i < rolesOfUser.Count; i++)
            {
                var userRoleId = rolesOfUser[i];
                List<int> childrenRoles = new List<int>();
                childrenRoles = _dbEntities.Roles.Where(f => f.ParentId == userRoleId).Select(f => f.Id).ToList();
                foreach (int childRole in childrenRoles)
                {
                    if (!rolesOfUser.Contains(childRole))
                    {
                        rolesOfUser.Add(childRole);
                    }
                }
            }
            List<Constraint> constraints = _dbEntities.Constraints.Where(f => f.RoleId1 == role.Id || f.RoleId2 == role.Id).ToList();

            if (IsConstraint(role, rolesOfUser, constraints)) return true;

            return false;
        }

        public bool IsConstraint(Role role, List<int> rolesOfUser, List<Constraint> constraints)
        {
            foreach (Constraint constraint in constraints)
            {
                var constraintRole = 0;
                if (constraint.RoleId1 == role.Id)
                    constraintRole = constraint.RoleId2;
                else
                    constraintRole = constraint.RoleId1;

                if (rolesOfUser.Contains(constraintRole))
                {
                    return true;
                }
            }

            return false;
        }

        public void CreateConstraint(string roleName1, string roleName2)
        {
            var role1 = _dbEntities.Roles.FirstOrDefault(f => f.Name == roleName1);
            var role2 = _dbEntities.Roles.FirstOrDefault(f => f.Name == roleName2);

            _dbEntities.Constraints.Add(new Constraint
            {
                RoleId1 = role1.Id,
                RoleId2 = role2.Id
            });
            _dbEntities.SaveChanges();
        }

        public bool ExistsConstraint(string roleName1, string roleName2)
        {
            var role1 = _dbEntities.Roles.FirstOrDefault(f => f.Name == roleName1);
            var role2 = _dbEntities.Roles.FirstOrDefault(f => f.Name == roleName2);

            var constraint = _dbEntities.Constraints.FirstOrDefault(f => f.RoleId1 == role1.Id && f.RoleId2 == role2.Id);

            if (constraint == null)
                return false;
            return true;
        }


    }
}
