using server.Models;
using server.Responses.Transfer;

namespace server.Extensions;

public static class TransferExtensions
{
    public static TransferResponse MapToResponse(this Transfer transfer)
    {
        return new TransferResponse
        {
            AccountFromId = transfer.AccountFromId,
            AccountToId = transfer.AccountToId,
            Date = transfer.Date,
            Description = transfer.Description,
            CreatedDate = transfer.CreatedDate,
            UpdatedDate = transfer.UpdatedDate,
        };
    }
}