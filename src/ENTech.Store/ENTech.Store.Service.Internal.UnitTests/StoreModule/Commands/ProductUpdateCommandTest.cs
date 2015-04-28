namespace ENTech.Store.Services.Internal.UnitTests.StoreModule.Commands
{
	class ProductUpdateCommandTest
	{
		//Validation 

		//Id is specified

		//Sku is unique

		//Execute:
		//		First call SaveChanges()
		//				Existing product and children has new values
		//						if( child.Id is not NewId - update)
		//				New children added to dbproduct
		//						if(child id is NewId - add
		//
		//				Variants remain old
		//		Second call SaveChanges()
		//				Existing productvariants and children has new values
		//				New productvariants and children added to dbproduct
	}
}
