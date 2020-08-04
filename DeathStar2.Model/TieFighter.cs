namespace DeathStar2.Model
{
    public class TieFighter
    {
        public TieFighter(string code, bool isDamaged)
        {
            Code = code;
            IsDamaged = isDamaged;
        }

        public TieFighter()
        {

        }

        public string Code { get; set; } = "";
        public bool IsDamaged { get; set; }
    }
}
