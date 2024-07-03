using Application.Repositories;

namespace Infrastructures.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _context;

        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public string GetRoleName(int roleid)
        {
            var role = _context.Roles.Where(r=>r.Id == roleid).FirstOrDefault();
            return role.Name;
        }
    }
}
