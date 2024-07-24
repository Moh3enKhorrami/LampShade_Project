using _0_Framework.Infrastructure;

namespace InventoryMangement.Configuration.Permissions;

public class InventoryPermissionExposer : IPermissionExposer
{
    public Dictionary<string, List<PermissionDto>> Expose()
    {
        return new Dictionary<string, List<PermissionDto>>
        {
            {
                "Inventory", new List<PermissionDto>
                {
                    new(InventoryPermission.ListInventory, "ListInventory"),
                    new(InventoryPermission.SearchInventory,"SearchInventory"),
                    new(InventoryPermission.CreateInventory,"CreateInventory"),
                    new(InventoryPermission.EditInventory,"EditInventory"),
                }
            }
        };
    }
}