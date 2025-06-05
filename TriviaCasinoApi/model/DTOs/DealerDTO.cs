namespace TriviaCasinoApi.Model;

public class DealerDTO
{
    public List<Card>? Hand { get; set; }
    public int Score { get; set; }
    
    public override string ToString()
    {
        return $"Hand: {Hand}, Score: {Score}";
    }
}
