namespace FitControl.API.Entities;

public class InstrutorModalidade : BaseEntity
{
    public int InstrutorId { get; set; }
    public Instrutor Instrutor { get; set; }
    public int ModalidadeId { get; set; }
    public Modalidade Modalidade { get; set; }
}