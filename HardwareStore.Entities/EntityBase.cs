namespace HardwareStore.Entities;

public class EntityBase
{
    public int Id { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool Status { get; set; } 

    protected EntityBase()
    {
        CreationDate = DateTime.Now;
        Status = true;
    }
}