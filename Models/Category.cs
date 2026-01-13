using FitnessSimulation.Models.Common;

namespace FitnessSimulation.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public ICollection<Trainer> Trainers { get; set; } = [];
    }
}
