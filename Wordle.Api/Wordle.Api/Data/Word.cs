namespace Wordle.Api.Data;

public class Word
{
    //Good practice to name ID primary keys as 'ClassName'Id like we did here
    public int WordId { get; set; }
    //required modifer used to make sure the property is not null.
    //It is necessary for Entity Framework to work properly.
    public required string Text { get; set; }
    public bool IsCommon { get; set; }
}
