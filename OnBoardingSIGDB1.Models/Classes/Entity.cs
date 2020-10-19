using OnBoardingSIGDB1.Models.Classes;

namespace OnboardingSIGDB1.Models.Classes
{
    public abstract class Entity : EntityValidator
    {
        public int Id { get; set; }
	}
}
