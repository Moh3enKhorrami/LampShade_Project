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
                    new PermissionDto(InventoryPermission.ListInventory, "ListInventory"),
                    new PermissionDto(InventoryPermission.SearchInventory,"SearchInventory"),
                    new PermissionDto(InventoryPermission.CreateInventory,"CreateInventory"),
                    new PermissionDto(InventoryPermission.EditInventory,"EditInventory"),
                    new PermissionDto(InventoryPermission.IncreaseInventory,"IncreaseInventory"),
                    new PermissionDto(InventoryPermission.ReduceInventory,"ReduceInventory"),
                    new PermissionDto(InventoryPermission.OperationLog,"GetOperationLog"),


                }
            }
        };
    }
}