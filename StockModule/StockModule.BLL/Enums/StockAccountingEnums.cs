
namespace StockModule.BLL
{
    public enum StockAccountingEnums
    {
        MISMATCH = -2,
        ERROR = -1,
        NOT_STARTED =0,
        COMPLETED=1,      
        CANCELED=2,
        AUTO=3      
    }

    public enum EnumRivileMethod
    {
        EDIT_I09_FULL,
        EDIT_I09,
        GET_N17_LIST
    }

}
