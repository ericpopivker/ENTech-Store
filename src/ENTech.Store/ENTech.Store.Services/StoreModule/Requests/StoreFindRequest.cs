using ENTech.Store.Services.SharedModule.Requests;
using ENTech.Store.Services.StoreModule.Dtos;
using ENTech.Store.Services.StoreModule.Responses;

namespace ENTech.Store.Services.StoreModule.Requests
{
	public class StoreFindRequest : FindRequestBase<StoreFindResponse, StoreLoadOption, StoreSortField, StoreFindCriteriaDto>
	{
	}
}